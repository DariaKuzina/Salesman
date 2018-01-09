using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesmanUI
{
    public partial class Salesman : Form
    {
        private List<PointF> _sourcePoints = new List<PointF>();

        private List<PointF> _resultPoints = new List<PointF>();
        private List<int> _permutation = new List<int>();
        private double _length;
        private Regex _parseCities = new Regex(@"\((\d+);(\d+)\)");

        private float _squareSide = 6;
        private Bitmap _sourceBuffer;
        private Bitmap _resultBuffer;

        public Salesman()
        {
            InitializeComponent();

            _sourceBuffer = new Bitmap(sourcePanel.Width, sourcePanel.Height);
            _resultBuffer = new Bitmap(resultPanel.Width, resultPanel.Height);

            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, sourcePanel, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
           BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
           null, resultPanel, new object[] { true });
        }

        private void sourcePanel_Paint(object sender, PaintEventArgs e)
        {
            PaintPanel(e, _sourceBuffer);
        }

        private void sourcePanel_Resize(object sender, EventArgs e)
        {
            ResizePanel(sourcePanel, _sourceBuffer);
        }

        private void ResizePanel(Panel panel, Bitmap buffer)
        {
            Bitmap newBuffer = new Bitmap(Max(panel.Width, buffer.Width), Max(panel.Height, buffer.Height));
            if (buffer != null)
                using (Graphics bufferGrph = Graphics.FromImage(newBuffer))
                    bufferGrph.DrawImageUnscaled(buffer, Point.Empty);
            buffer = newBuffer;
        }

        private void PaintPanel(PaintEventArgs e, Bitmap buffer)
        {
            e.Graphics.DrawImageUnscaled(buffer, Point.Empty);
        }

        private void sourcePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _sourcePoints.Add(new PointF(e.X, e.Y));
                DrawPoints(sourcePanel, _sourceBuffer, _sourcePoints);
            }
        }

        private void DrawPoints(Panel panel, Bitmap buffer, List<PointF> points)
        {
            using (Graphics g = Graphics.FromImage(buffer))
            {
                if (points.Count == 0)
                    return;
                g.Clear(this.BackColor);
                int n = points.Count;
                for (int i = 0; i < n; i++)
                    g.DrawRectangle(Pens.DarkGreen, points[i].X - _squareSide / 2, points[i].Y - _squareSide / 2, _squareSide, _squareSide);
                panel.Invalidate();
            }
        }

        private int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _sourcePoints.Clear();
            using (Graphics g = Graphics.FromImage(_sourceBuffer))
            {
                g.Clear(this.BackColor);
                sourcePanel.Invalidate();
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            using (StreamWriter file = new StreamWriter(ConfigurationManager.AppSettings["exportPath"]))
            {
                file.WriteLine(_sourcePoints.Count());

                foreach (var city in _sourcePoints)
                {
                    file.WriteLine(string.Format("({0};{1})", city.X, city.Y));
                }
            }

            MessageBox.Show("Successfully");
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            _resultPoints.Clear();
            _permutation.Clear();
            ReadFromFile();
            DrawSolution();
            textBoxLength.Clear();
            textBoxLength.AppendText($"Length: {_length}");
        }

        private void ReadFromFile()
        {
            try
            {
                using (var reader = new StreamReader(ConfigurationManager.AppSettings["importPath"]))
                {
                    int pointsCount = int.Parse(reader.ReadLine());
                    for (int i = 0; i < pointsCount; i++)
                    {
                        var city = _parseCities.Match(reader.ReadLine());
                        _resultPoints.Add(new PointF(float.Parse(city.Groups[1].Value), float.Parse(city.Groups[2].Value)));
                    }

                    var lenght = reader.ReadLine();
                    _length = double.Parse(lenght, CultureInfo.InvariantCulture);

                    var permutation = reader.ReadLine();
                    _permutation.AddRange(permutation.Split(',').Select(i => int.Parse(i)));                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }

        }

        private void importPanel_Paint(object sender, PaintEventArgs e)
        {
            PaintPanel(e, _resultBuffer);
        }

        private void importPanel_Resize(object sender, EventArgs e)
        {
            ResizePanel(resultPanel, _resultBuffer);
        }

        private void DrawSolution()
        {
            DrawPoints(resultPanel, _resultBuffer, _resultPoints);
            DrawPath();
        }

        private double CalculateLength()
        {
            double res = 0;
            int n = _resultPoints.Count;

            for (int i = 0; i < n - 1; i++)
            {
                res += Math.Sqrt(Math.Pow(_resultPoints[i + 1].X - _resultPoints[i].X, 2) + Math.Pow(_resultPoints[i + 1].Y - _resultPoints[i].Y, 2));
            }

            return res;
        }

        private void DrawPath()
        {
            using (Graphics g = Graphics.FromImage(_resultBuffer))
            {
                for (int i = 0; i < _permutation.Count() - 1; i++)
                {
                    g.DrawLine(Pens.DarkRed, _resultPoints[_permutation[i]], _resultPoints[_permutation[i + 1]]);
                }
            }
            resultPanel.Invalidate();
        }
    }
}

namespace SalesmanUI
{
    partial class Salesman
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sourcePanel = new System.Windows.Forms.Panel();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelSource = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.buttonImport = new System.Windows.Forms.Button();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sourcePanel
            // 
            this.sourcePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourcePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourcePanel.Location = new System.Drawing.Point(12, 34);
            this.sourcePanel.Name = "sourcePanel";
            this.sourcePanel.Size = new System.Drawing.Size(875, 178);
            this.sourcePanel.TabIndex = 0;
            this.sourcePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sourcePanel_Paint);
            this.sourcePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sourcePanel_MouseDown);
            this.sourcePanel.Resize += new System.EventHandler(this.sourcePanel_Resize);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(12, 218);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(79, 40);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(108, 218);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(93, 40);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(12, 9);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(65, 13);
            this.labelSource.TabIndex = 3;
            this.labelSource.Text = "Source data";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(12, 261);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(37, 13);
            this.labelResult.TabIndex = 4;
            this.labelResult.Text = "Result";
            // 
            // resultPanel
            // 
            this.resultPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resultPanel.Location = new System.Drawing.Point(15, 294);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(874, 181);
            this.resultPanel.TabIndex = 5;
            this.resultPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.importPanel_Paint);
            this.resultPanel.Resize += new System.EventHandler(this.importPanel_Resize);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(15, 481);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(98, 38);
            this.buttonImport.TabIndex = 6;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // textBoxLength
            // 
            this.textBoxLength.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLength.Location = new System.Drawing.Point(897, 294);
            this.textBoxLength.Multiline = true;
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.ReadOnly = true;
            this.textBoxLength.Size = new System.Drawing.Size(84, 180);
            this.textBoxLength.TabIndex = 7;
            // 
            // Salesman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 536);
            this.Controls.Add(this.textBoxLength);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.labelSource);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.sourcePanel);
            this.Name = "Salesman";
            this.Text = "Salesman";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel sourcePanel;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.TextBox textBoxLength;
    }
}


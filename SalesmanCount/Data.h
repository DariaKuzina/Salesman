#pragma once
#include<math.h>
#include<string>
#include<vector>

int const citiesMax = 100;
struct Point {
	float X;
	float Y;

	Point(){}

	Point(float x, float y)
	{
		X = x;
		Y = y;
	}

	float distantion(Point point)
	{
		return sqrt((X - point.X)*(X - point.X) + (Y - point.Y)*(Y - point.Y));
	}
};

typedef struct mytype_s
{
	int permutation[citiesMax];
	float length;
} Result;



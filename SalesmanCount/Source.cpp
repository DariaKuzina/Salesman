#include<iostream>
#include<fstream>
#include<vector>
#include <time.h>
#include <random>
#include <algorithm> 
#include<string>
#include "mpi.h"
#include"Data.h"
using namespace std;

string toString(int* vec, int n)
{
	string res = "";
	for (int i = 0; i < n - 1; i++)
		res += (to_string(vec[i]) + ",");
	res += to_string(vec[n - 1]);
	return res;
}

float calculatePath(vector<Point> cities, vector<int> permutation)
{
	float res = 0;
	int n = cities.size();

	for (int i = 0; i < n - 1; i++)
	{
		res += cities[permutation[i + 1]].distantion(cities[permutation[i]]);
	}

	return res;
}

void copyFile(FILE*source, FILE*dest)
{
	char ch;
	rewind(source);

	while ((ch = fgetc(source)) != EOF)
		fputc(ch, dest);

	rewind(source);
}

MPI_Datatype registerResult(Result const&) {

	const int size = 2;
	MPI_Datatype MPI_DeltaType_proto, MPI_DeltaType;
	int blocklengths[size] = { citiesMax, 1 };
	MPI_Datatype types[size] = { MPI_INT,  MPI_FLOAT };
	MPI_Aint offsets[size], lb, extent;

	offsets[0] = (MPI_Aint)offsetof(Result, permutation);
	offsets[1] = (MPI_Aint)offsetof(Result, length);


	MPI_Type_create_struct(size, blocklengths, offsets, types, &MPI_DeltaType_proto);
	MPI_Type_get_extent(MPI_DeltaType_proto, &lb, &extent);

	extent = sizeof(Result);

	MPI_Type_create_resized(MPI_DeltaType_proto, lb, extent, &MPI_DeltaType);
	MPI_Type_commit(&MPI_DeltaType);	return MPI_DeltaType;}

MPI_Datatype registerPoint(Point const&) {

	const int    nItems = 2;
	int          blocklengths[nItems] = { 1, 1 };
	MPI_Datatype types[nItems] = { MPI_FLOAT, MPI_FLOAT };
	MPI_Datatype MPI_DeltaType_proto, MPI_DeltaType;
	MPI_Aint     offsets[nItems];

	offsets[0] = offsetof(Point, X);
	offsets[1] = offsetof(Point, Y);

	MPI_Type_create_struct(nItems, blocklengths, offsets, types, &MPI_DeltaType_proto);

	// Resize the type so that its length matches the actual structure length

	// Get the constructed type lower bound and extent
	MPI_Aint lb, extent;
	MPI_Type_get_extent(MPI_DeltaType_proto, &lb, &extent);

	extent = sizeof(Point);

	// Create a resized type whose extent matches the actual distance
	MPI_Type_create_resized(MPI_DeltaType_proto, lb, extent, &MPI_DeltaType);
	MPI_Type_commit(&MPI_DeltaType);	return MPI_DeltaType;}

void resultMin(Result* in, Result* inout, int* len, MPI_Datatype* dptr)
{
	int n = *len;
	int  k;
	for (k = 0; k < n; k++)
	{
		if (in[k].length < inout[k].length)
		{
			inout[k].length = in[k].length;
			for (int j = 0; j < citiesMax; j++)
			{
				(inout[k].permutation)[j] = (in[k].permutation)[j];
			}
		}
	}

}

int main(int argc, char *argv[])
{
	MPI_Init(&argc, &argv);
	MPI_Status status;

	string inputPath = "C:\\Users\\User\\Desktop\\cities.txt";
	string outputPath = "C:\\Users\\User\\Desktop\\citiesCalc.txt";

	int ProcRank, ProcNum, n;
	float x, y, length;
	FILE *infile, *outputfile;

	MPI_Op myOp;

	MPI_Comm_size(MPI_COMM_WORLD, &ProcNum);
	MPI_Comm_rank(MPI_COMM_WORLD, &ProcRank);

	vector<Point> cities;
	vector<int> permutation;

	Result result;
	Result localResult;
	localResult.length = INT_MAX;
	int attempts = 100000;

	MPI_Datatype res_type = registerResult(result);
	MPI_Op_create((MPI_User_function *)resultMin, 1, &myOp);

	if (ProcRank == 0)
	{
		result.length = INT_MAX;
		infile = fopen(inputPath.c_str(), "r");
		fscanf(infile, "%d\n", &n);
		for (int i = 0; i < n; i++)
			permutation.push_back(i);

		for (int i = 0; i < n; i++)
		{
			fscanf(infile, "(%f;%f)\n", &x, &y);
			cities.push_back(Point(x, y));
		}
	}

	MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
	permutation.resize(n);
	cities.resize(n);

	MPI_Datatype type = registerPoint(cities[0]);
	MPI_Bcast(&permutation[0], n, MPI_INT, 0, MPI_COMM_WORLD);

	MPI_Bcast(&cities[0], n, type, 0, MPI_COMM_WORLD);

	for (int i = 0; i < attempts; i++)
	{
		std::random_device seed;
		std::mt19937 rng(seed());

		shuffle(permutation.begin(), permutation.end(), rng);

		length = calculatePath(cities, permutation);

		if (length < localResult.length)
		{
			localResult.length = length;
			copy(permutation.begin(), permutation.end(), localResult.permutation);
		}

	}
	printf("%d %f\n", ProcRank, localResult.length);
	printf("%d %s\n", ProcRank, toString(localResult.permutation, n).c_str());

	//MPI_Reduce(&localResult, &result, 1, res_type, myOp, 0, MPI_COMM_WORLD);
	if (ProcRank != 0)
		MPI_Send(&localResult, 1, res_type, 0, 55, MPI_COMM_WORLD);

	if (ProcRank == 0)
	{
		result = localResult;
		for (int i = 0; i < ProcNum - 1; i++)
		{
			MPI_Recv(&localResult, 1, res_type, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status);
			if (localResult.length < result.length)
				result = localResult;
		}

		printf("Result %f\n", result.length);
		printf("Result %s\n", toString(result.permutation, n).c_str());
		outputfile = fopen(outputPath.c_str(), "w");
		copyFile(infile, outputfile);
		fputs((to_string(result.length) + '\n').c_str(), outputfile);
		fputs(toString(result.permutation, n).c_str(), outputfile);
		fclose(outputfile);
		fclose(infile);
	}

	MPI_Type_free(&type);
	MPI_Type_free(&res_type);
	MPI_Op_free(&myOp);
	MPI_Finalize();
}


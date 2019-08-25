// ImperiaMessengers.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <vector>

using namespace std;

void updateTimes(vector<unsigned int> &times, int i, vector<vector<int>> &matrix)
{
	for (int j = 0; j < matrix[i].size(); j++)
	{
		if (matrix[i][j] == 0) break;
		if (matrix[i][j] == -1) continue;
		if (j == 0)
		{
			times[i] = min(times[i], (unsigned int)matrix[i][j]);
			continue;
		}
		if (times[i] == UINT_MAX && times[j] != UINT_MAX)
		{
			times[i] = times[j] + (unsigned int)matrix[i][j];
		}
		if (times[j] > times[i] + (unsigned int)matrix[i][j])
		{
			times[j] = times[i] + (unsigned int)matrix[i][j];
			updateTimes(times, j, matrix);
		}
	}
}

unsigned int Messenger(vector<vector<int>> &matrix)
{
	int n = matrix.size();
	vector<unsigned int> times(n, UINT_MAX);
	for (int i = 0; i < matrix.size(); i++)
	{
		updateTimes(times, i, matrix);
	}
	for (auto i : times) cout << i << ",";
	cout << endl;

	unsigned int res = 0;
	for (int i = 1; i < times.size(); i++) res = max(res, times[i]);
	return res;
}

vector<vector<int>> handleInput(vector<vector<int>> &input)
{
	int n = input[0][0];
	if (n < 1 || n > 100) return {};

	vector<vector<int>>matrix(n, vector<int>{});
	for (auto &m : matrix) m.resize(n);
	for (int i = 1; i < input.size(); i++)
	{
		if (input[i].size() != i) return {};
		for (int j = 0; j < input[i].size(); j++)
		{
			matrix[i][j] = input[i][j];
			matrix[j][i] = matrix[i][j];
		}
	}

	for (auto &m : matrix)
	{
		for (auto &n : m) cout << n << "\t";
		cout << endl;
	}

	return matrix;
}

int main()
{
	vector<vector<int>> input = { {5}, {50}, {30,5}, {100,20,50}, {10,-1,-1,10} };
	vector<int> answer = { 35,30,20,10 };

	// 需要递归的用例
	//vector<vector<int>> input = { {5}, {40}, {30,5}, {20,10,5}, {10,20,10,5} };
	//vector<int> answer = { 25,20,15,10 };

	// 不能直接到达的用例
	//vector<vector<int>> input = { {5}, {10}, {-1,10}, {-1,-1,10}, {-1,-1,-1,10} };
	//vector<int> answer = { 10,20,30,40 };

	// 超过INT_MAX的用例
	//vector<vector<int>> input = { {5}, {2147483647}, {-1,10}, {-1,-1,10}, {-1,-1,-1,10} };
	//vector<unsigned int> answer = { 2147483647,2147483657,2147483667,2147483677 };

	vector<vector<int>> matrix = handleInput(input);
	unsigned int res = Messenger(matrix);
	for (auto a : answer) cout << a << ",";
	cout << endl << res;
}


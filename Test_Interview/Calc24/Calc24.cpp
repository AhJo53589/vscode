// Calc24.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

//////////////////////////////////////////////////////////////////////////
//vector<int> getAllCalcResult(int a, int b)
//{
//	vector<int> ret;
//	ret.push_back(a + b);
//	ret.push_back(a - b);
//	ret.push_back(b - a);
//	ret.push_back(a * b);
//	if (b != 0 && a % b == 0) ret.push_back(a / b);
//	if (a != 0 && b % a == 0) ret.push_back(b / a);
//	return ret;
//}
//
//bool Calc24_1_1(int x, int a)
//{
//	vector<int> res = getAllCalcResult(x, a);
//	for (auto n : res) if (n == 24)
//	{
//		cout << x << ", " << a << endl;
//		return true;
//	}
//	return false;
//}
//
//bool Calc24_1_2(int x, vector<int> nums)
//{
//	for (int i = 0; i < nums.size(); i++)
//	{
//		int a = -1;
//		for (int k = 0; k < nums.size(); k++)
//		{
//			if (k == i) continue;
//			a = nums[k];
//		}
//
//		vector<int> res = getAllCalcResult(x, nums[i]);
//		for (auto n : res) if (Calc24_1_1(n, a))
//		{
//			cout << x << ", " << nums[0] << ", " << nums[1] << endl;
//			return true;
//		}
//
//		res.clear();
//		res = getAllCalcResult(a, nums[i]);
//		for (auto n : res) if (Calc24_1_1(x, n)) return true;
//	}
//	return false;
//}
//
//bool Calc24(vector<int> nums)
//{
//	for (int i = 0; i < nums.size(); i++)
//	{
//		for (int j = i + 1; j < nums.size(); j++)
//		{
//			int a = -1;
//			int b = -1;
//			for (int k = 0; k < nums.size(); k++)
//			{
//				if (k == i || k == j) continue;
//				if (a == -1) a = nums[k];
//				else b = nums[k];
//			}
//
//			vector<int> res = getAllCalcResult(nums[i], nums[j]);
//			for (auto n : res) if (Calc24_1_2(n, { a, b })) return true;
//		}
//	}
//	return false;
//}


//////////////////////////////////////////////////////////////////////////
vector<int> getAllCalcResult(int a, int b)
{
	vector<int> ret;
	ret.push_back(a + b);
	ret.push_back(a - b);
	ret.push_back(a * b);
	if (b != 0 && a % b == 0) ret.push_back(a / b);
	return ret;
}

bool Calc24(vector<int> nums)
{
	if (nums.size() == 2)
	{
		vector<int> res = getAllCalcResult(nums[0], nums[1]);
		for (auto n : res) if (n == 24) return true;
	}
	else if (nums.size() > 2)
	{
		sort(nums.begin(), nums.end());
		do
		{
			vector<int> res = getAllCalcResult(nums[0], nums[1]);
			for (auto n : res)
			{
				if (nums.size() == 3) if (Calc24({ n, nums[2] })) return true;
				if (nums.size() == 4) if (Calc24({ n, nums[2], nums[3] })) return true;
			}
		} while (next_permutation(nums.begin(), nums.end()));
	}

	return false;
}

int main()
{


	vector<vector<int>> N;
	vector<int> A;

	N.push_back({ 7,2,1,10 });
	A.push_back(1);

	N.push_back({ 1,2,3,5 });
	A.push_back(1);

	N.push_back({ 1,2,4,4 });
	A.push_back(1);

	N.push_back({ 5,5,5,5 });
	A.push_back(1);

	N.push_back({ 5,5,5,3 });
	A.push_back(0);

	N.push_back({ 9,6,4,1 });
	A.push_back(1);

	N.push_back({ 7,9,10,9 });
	A.push_back(0);
	

	for (int i = 0; i < N.size(); i++)
	{
		cout << endl << "/////////////////////////////////////////////////" << endl;
		for (auto &n : N[i]) cout << n << ", ";
		cout << endl;
		cout << "Calc24 = " << A[i] << endl;
		cout << "My answer = " << Calc24(N[i]) << endl;
	}

}

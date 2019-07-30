// Quicksort.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <time.h>  

#include <vector>

using namespace std;

//void swap(int &a, int &b)
//{
// 	if (a == b) return;	// 防止a和b同一地址而出现问题
//	a ^= b;
//	b ^= a;
//	a ^= b;
//}
//
//int Partition(int A[], int p, int r)
//{
//	int k = p;
//	for (int i = p + 1; i < r; i++)
//	{
//		if (A[i] < A[r])
//		{
//			swap(A[k], A[i]);
//			k += 1;
//		}
//	}
//	swap(A[k], A[r]);
//	return k;
//}
//
//void QuickSort(int A[], int p, int r)
//{
//	if (p < r)
//	{
//		int q = Partition(A, p, r);
//		QuickSort(A, p, q - 1);
//		QuickSort(A, q + 1, r);
//	}
//}

int Partition(vector<int> &nums, int l, int r)
{
	int m = l;
	for (int i = l; i < r; i++)
	{
		if (nums[i] < nums[r]) swap(nums[m++], nums[i]);
	}
	swap(nums[m], nums[r]);
	return m;
}

void QuickSort(vector<int> &nums, int l, int r)
{
	if (l < r)
	{
		int m = Partition(nums, l, r);
		QuickSort(nums, l, m - 1);
		QuickSort(nums, m + 1, r);
	}
}

int main()
{
	srand((unsigned)time(NULL));
	const int cNumMax = 10;

	//int iTest[cNumMax];
	//cout << "iTest = ";
	//for (int i = 0; i < cNumMax; i++)
	//{
	//	iTest[i] = rand() % cNumMax;
	//	cout << iTest[i] << " ";
	//}
	//cout << endl;

	//QuickSort(iTest, 0, cNumMax - 1);
	//cout << "QuickSort = ";
	//for (int i = 0; i < cNumMax; i++)
	//{
	//	cout << iTest[i] << " ";
	//}
	//cout << endl;


	//vector<int> nums{ 2,4,0,3,7,8,6,9,1,5 };
	vector<int> nums{ 2,3,0,3,3,8,6,9,1,3 };
	cout << "nums = ";
	for (auto i : nums) cout << i << " ";
	cout << endl;

	QuickSort(nums, 0, nums.size() - 1);
	cout << "QuickSort = ";
	for (auto i : nums) cout << i << " "; 
	cout << endl;
}


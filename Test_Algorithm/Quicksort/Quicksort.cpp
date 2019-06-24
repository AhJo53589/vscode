// Quicksort.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <time.h>  

void swap(int &a, int &b)
{
 	if (a == b) return;	// 防止a和b同一地址而出现问题
	a ^= b;
	b ^= a;
	a ^= b;
}

int Partition(int A[], int p, int r)
{
	int k = p;
	for (int i = p + 1; i < r; i++)
	{
		if (A[i] < A[r])
		{
			swap(A[k], A[i]);
			k += 1;
		}
	}
	swap(A[k], A[r]);
	return k;
}

void QuickSort(int A[], int p, int r)
{
	if (p < r)
	{
		int q = Partition(A, p, r);
		QuickSort(A, p, q - 1);
		QuickSort(A, q + 1, r);
	}
}

int main()
{
	srand((unsigned)time(NULL));

	const int cNumMax = 10;
	int iTest[cNumMax];

	std::cout << "iTest = ";
	for (int i = 0; i < cNumMax; i++)
	{
		iTest[i] = rand() % cNumMax;
		std::cout << iTest[i] << " ";
	}
	std::cout << std::endl;

	QuickSort(iTest, 0, cNumMax - 1);
	std::cout << "QuickSort = ";
	for (int i = 0; i < cNumMax; i++)
	{
		std::cout << iTest[i] << " ";
	}
	std::cout << std::endl;
}


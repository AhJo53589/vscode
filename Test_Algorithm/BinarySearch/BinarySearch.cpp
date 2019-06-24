// BinarySearch.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
// https://www.zhihu.com/question/36132386

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <time.h>

int LowerBound(int _Array[], int _First, int _Last, int _Val)	// 返回[first, last)内第一个不小于value的值的位置
{
	while (_First < _Last)	// 搜索区间[first, last)不为空
	{
		int _Mid = _First + (_Last - _First) / 2;	// 防溢出
		if (_Array[_Mid] < _Val)
		{
			_First = _Mid + 1;
		}
		else
		{
			_Last = _Mid;
		}
	}
	return _First;	// last也行，因为[first, last)为空的时候它们重合
}

bool BinarySearch(int _Array[], int _First, int _Last, int _Val)
{
	int result = LowerBound(_Array, _First, _Last, _Val);

	return (result != _Last && _Array[result] == _Val);
}

void printArray(int _Array[], int _First, int _Last)
{
	for (int i = _First; i < _Last; i++)
	{
		std::cout << _Array[i] << " ";
	}
	std::cout << std::endl;
}

int main()
{
	srand(time(NULL));

	const int cNumMax = 5;
	int A[cNumMax];
	//int B[cNumMax] = { 1, 1, 1, 1, 1 };

	int cTestTime = 10;

	while (--cTestTime > 0)
	{
		// init
		for (int i = 0; i < cNumMax; i++)
		{
			A[i] = rand() % 100;
		}
		std::cout << "Init A[] = ";
		printArray(A, 0, cNumMax);

		// sort
		auto first = std::begin(A);
		auto last = std::end(A);
		std::sort(first, last);
		std::cout << "Sort: A[] = ";
		printArray(A, 0, cNumMax);
		//printArray(B, 0, cNumMax);

		// find
		int val = rand() % 100;
		std::cout << "val = " << val << std::endl;

		int result = LowerBound(A, 0, cNumMax, val);
		//int result = LowerBound(B, 0, cNumMax, val);
		if (result)
		{
			std::cout << "Find result: A[" << result << "] = " << A[result] << std::endl;
		}
		else
		{
			std::cout << "No results found" << std::endl;
		}


		std::cout << std::endl;
	}
}

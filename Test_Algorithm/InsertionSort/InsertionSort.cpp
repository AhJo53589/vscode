// InsertionSort.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

void InstertionSort(int _Array[], int _Len)
{
	for (int i = 1; i < _Len; i++)
	{
		int temp = _Array[i];
		int j = i - 1;
		while (j >= 0 && temp < _Array[j])
		{
			_Array[j + 1] = _Array[j];
			j -= 1;
		}
		_Array[j + 1] = temp;
	}
}

int main()
{
    std::cout << "Hello World!\n"; 
}

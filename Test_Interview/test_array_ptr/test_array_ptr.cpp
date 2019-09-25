// test_array_ptr.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

int main()
{
	int a[5] = { 1,2,3,4,5 };
	int *ptr = (int*)(&a + 1);
	//int *ptr = (int*)(&a[0] + 1);
	//int *ptr = (int*)(a + 1);
	printf("%d,%d", *(a + 1), *(ptr - 1));

	cout << endl;
	cout << endl;
	cout << "&a \t= " << &a << endl;
	cout << "&a + 1 \t= " << &a + 1 << endl;

	cout << endl;
	for (int i = 0; i < 5; i++)
	{
		cout << "&a[" << i << "] \t= " << &a[i] << endl;
	}
}


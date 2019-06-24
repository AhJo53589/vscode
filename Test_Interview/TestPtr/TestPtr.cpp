// TestPtr.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

int main()
{
	int a[5] = { 1,2,3,4,5 };
	int *ptr = (int*)(&a + 1);
	printf("%d,%d", *(a + 1), *(ptr - 1));
}


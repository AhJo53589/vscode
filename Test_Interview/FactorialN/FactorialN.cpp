// FactorialN.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

int recv(unsigned int n)
{
	if (0 == n) return 1;
	if (1 == n) return 1;

	unsigned int sum = n * recv(n - 1);
	return sum;
}

template<unsigned n>
struct factorial 
{
	enum { value = n * factorial<n - 1>::value };
};

template<>
struct factorial<0> 
{
	enum { value = 1 };
};

int main()
{
	const unsigned int num = 10;
	cout << recv(num) << endl;

	cout << factorial<num>::value << endl;

	return 0;
}

// test_template_fun.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;


template<int N>
struct fact
{
	enum { value = N * fact<N - 1>::value };
};

template<>
struct fact<1>
{
	enum { value = 1 };
};

long long f(int n)
{
	if (n == 1) return 1;
	return (long long)n * f(n - 1);
}

int main()
{
	cout << fact<1>::value << endl;
	cout << fact<2>::value << endl;
	cout << fact<3>::value << endl;
	cout << fact<4>::value << endl;
	cout << fact<5>::value << endl;
	cout << fact<6>::value << endl;
	cout << fact<7>::value << endl;
	cout << fact<8>::value << endl;
	cout << fact<9>::value << endl;
	cout << fact<10>::value << endl;

	for (int i = 11; i < 25; i++)
	{
		cout << i << " = " << f(i) << endl;
	}
}


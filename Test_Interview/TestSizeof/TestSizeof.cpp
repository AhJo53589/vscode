// TestSizeof.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>


class A
{
public:
	virtual void Foo() {}
	char a[5];
	int c;
	double *b;
	short d;
};

int main()
{
	std::cout << "A" << sizeof(A) << std::endl;

	//std::cout << "char" << sizeof(char) << std::endl;
	//std::cout << "int" << sizeof(int) << std::endl;
	//std::cout << "double *" << sizeof(double *) << std::endl;
	//std::cout << "short" << sizeof(short) << std::endl;
}

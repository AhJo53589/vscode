// TestSizeof.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

class A
{
public:
	virtual void Foo() {}
	char a[5];
	int c;
	double *b;
	short d;
};

struct Empty
{

};

struct Test
{
	int a;
	char b;
	double c[10];
	char d;
};

struct Test_bit
{
	int a : 8;
	int b : 8;
	int c : 8;
	char f;
	int d : 8;
	int e : 8;
	char g;
};

// 字节对齐
int main()
{
	cout << endl;
	cout << "sizeof(Empty) = " << sizeof(Empty) << endl;

	cout << endl;
	cout << "sizeof(Test) = " << sizeof(Test) << endl;

	cout << endl;
	cout << "sizeof(int) = " << sizeof(int) << endl;

	cout << endl;
	cout << "sizeof(double) = " << sizeof(double) << endl;

	Test Obj;
	cout << endl << "Test Obj;" << endl;
	cout << "sizeof(Obj) = " << sizeof(Obj) << endl;

	Test *pObj;
	cout << endl << "Test *pObj;" << endl;
	cout << "sizeof(pObj) = " << sizeof(pObj) << endl;

	std::shared_ptr<Test> ptr = std::make_shared<Test>();
	cout << endl << "std::shared_ptr<Test> ptr = std::make_shared<Test>();" << endl;
	cout << "sizeof(ptr) = " << sizeof(ptr) << endl;
}

//////////////////////////////////////////////////////////////////////////
// 位域字节对齐
//int main()
//{
//	cout << "sizeof(Test_bit) = " << sizeof(Test_bit) << endl;
//}

//////////////////////////////////////////////////////////////////////////
//int main()
//{
//	std::cout << "A " << sizeof(A) << std::endl;
//
//	//std::cout << "char" << sizeof(char) << std::endl;
//	//std::cout << "int" << sizeof(int) << std::endl;
//	//std::cout << "double *" << sizeof(double *) << std::endl;
//	//std::cout << "short" << sizeof(short) << std::endl;
//}

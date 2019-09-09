// test_static.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

class A 
{
public:
	static int x;    // 声明一个静态成员变量 
	// static int x = 10;    // error 声明一个静态成员变量 
	static void fun(); // 声明一个静态成员函数

	int y1 = 10;
	int y2;

};

// int A::x = 10; // 定义静态成员变量并初始化
int A::x; // 默认初始化为0，必须定义静态数据成员，不然会报错，在类内只能是声明

void A::fun() 
{
	cout << x << endl;
	// cout<<y1<<endl;  error 不能访问非静态成员函数
	// cout<<y2<<endl;
}


int main()
{
	A a;   // 创建一个 类 A 对象 
	A *b = &a;
	cout << A::x << '\t' << a.x << '\t' << b->x << endl;  // 访问静态数据成员的方式 A::x  / a.x  / b->x

	A::fun();    // 访问静态成员函数的方式 A::fun()  / a.fun()
	a.fun();
	b->fun();   // b->fun()

	return 0;
}


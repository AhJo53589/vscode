// test_class_memory.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
// #include <iostream>

class Base
{
	int a;
	int b;
public:
	void CommonFunction();
	void virtual VirtualFunction();
};

class DerivedClass : public Base
{
	int c;
public:
	void DerivedCommonFunction();
	//void virtual VirtualFunction();
	void virtual VirtualFunction2();
};



//////////////////////////////////////////////////////////////////////////
// 多重继承
//class Base
//{
//	int a;
//	int b;
//public:
//	void CommonFunction();
//	void virtual VirtualFunction();
//};
//
//
//class DerivedClass1 : public Base
//{
//	int c;
//public:
//	void DerivedCommonFunction();
//	void virtual VirtualFunction();
//};
//
//class DerivedClass2 : public Base
//{
//	int d;
//public:
//	void DerivedCommonFunction();
//	void virtual VirtualFunction();
//};
//
//class DerivedDerivedClass : public DerivedClass1, public DerivedClass2
//{
//	int e;
//public:
//	void DerivedDerivedCommonFunction();
//	void virtual VirtualFunction();
//};

//////////////////////////////////////////////////////////////////////////
// 结构体
//struct Base
//{
//	void virtual VirtualFunction();
//};
//
//struct DerivedClass : public Base
//{
//	void virtual VirtualFunction();
//};

int main()
{
}


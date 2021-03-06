﻿// Chapter_13_Copy_Control.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <numeric>
#include <functional>
#include <iterator>
#include <sstream>
#include <fstream>

#include <vector>
#include <string>
#include <list>

#include "Message.h"
#include "String.h"

using namespace std;

//////////////////////////////////////////////////////////////////////////
class HasPtr
{
public:
	HasPtr(const string &s = string()) : ps(new string(s)), i(0)
	{}

	// 13.5 
	HasPtr(const HasPtr &h) : ps(new string(*(h.ps))), i(h.i)
	{}

	// 13.8
	HasPtr& operator= (const HasPtr& h)
	{
		if (this == &h)
		{
			return *this;
		}
		delete ps;
		ps = new string(*(h.ps));
		i = h.i;
		return *this;
	}

	HasPtr(HasPtr &&p) noexcept : ps(p.ps), i(p.i) { p.ps = nullptr; }
	//HasPtr& operator= (HasPtr rhs)
	//{
	//	swap(*this, rhs);
	//	return *this;
	//}

	~HasPtr()
	{
		delete ps;
	}

	void show()
	{
		cout << *ps << endl;
	}

private:
	string *ps;
	int i;
};

//////////////////////////////////////////////////////////////////////////
struct NoDtor
{
	NoDtor() = default;
	~NoDtor() = delete;
};

//////////////////////////////////////////////////////////////////////////
class Employee
{
public:
	Employee();//默认构造函数
	Employee(string& s);//接受一个string的构造函数	
	Employee(const Employee&) = delete;//不需要拷贝构造函数，怎么可能有人一样。将其声明为 =delete
	Employee& operator= (const Employee&) = delete;
	int number() { return _number; }
private:
	string employee;
	int _number;
	static int O_number;//static静态成员数据在类内声明，但只可以在类外定义，在类外定义时可不加static
};

int Employee::O_number = 0;
Employee::Employee()//默认构造函数
{
	_number = O_number++;
}
Employee::Employee(string& s)//接受一个string的构造函数
{
	employee = s;
	_number = O_number++;
}


//////////////////////////////////////////////////////////////////////////
// 13.27
class Hasptr1
{
	friend void swap(Hasptr1&, Hasptr1&);

public:
	Hasptr1(const string& s = string()) : ps(new string(s)), i(0), use(new size_t(1)) {}
	Hasptr1(const Hasptr1& p) : ps(p.ps), i(p.i), use(p.use) { ++*use; }

	Hasptr1& operator= (const Hasptr1& rhs)
	{
		++*rhs.use;//首先递增右侧运算符对象的引用计数
		if (--*use == 0)//递减本对象的引用计数，若没有其他用户，则释放本对象的成员
		{
			delete ps;
			delete use;
		}
		ps = rhs.ps;//进行拷贝
		use = rhs.use;
		i = rhs.i;
		return *this;
	}

	//Hasptr1& operator= (Hasptr1 rhs)
	//{
	//	swap(*this, rhs);
	//	return *this;
	//}

	~Hasptr1()
	{
		if (--*use == 0)//引用计数变为0，说明已经没有对象再需要这块内存，进行释放内存操作
		{
			delete ps;
			delete use;
		}
	}
private:
	//定义为指针，是我们想将该string对象保存在动态内存中
	string *ps;
	size_t *use;//将计数器的引用保存
	int i;
};

void swap(Hasptr1 &lhs, Hasptr1 &rhs)
{
	using std::swap;
	swap(lhs.ps, rhs.ps);
	swap(lhs.use, rhs.use);
	swap(lhs.i, rhs.i);
}


//////////////////////////////////////////////////////////////////////////
int main()
{
	{
		HasPtr h("Hello");
		HasPtr h2("AhJo");
		h = h;
		h.show();
		h2.show();
	}

	{
		//NoDtor nd;	// 错误 
		//NoDtor *p = new NoDtor();
		//delete p;		// 错误
	}

	{
		Employee a, b, c;
		cout << a.number() << endl;
		cout << b.number() << endl;
		cout << c.number() << endl;
	}

	{
		cout << "------------------Message-------------------" << endl;

		Folder F0, F1;
		Message MA("AAA"), MB("BBB");

		MA.save(F0);
		MA.save(F1);
		MB.save(F0);
		//MB.save(F1);

		F0.show();
		F1.show();
		MA.show();
		MB.show();

		//{
		//	cout << "-------------swap(F0, F1)--------------" << endl;
		//	swap(F0, F1);
		//	F0.show();
		//	F1.show();
		//	MA.show();
		//	MB.show();
		//}

		{
			cout << "-------------swap(MA, MB)--------------" << endl;
			swap(MA, MB);
			F0.show();
			F1.show();
			MA.show();
			MB.show();
		}

		{
			cout << "--------------Folder F2(F0)---------------" << endl;
			Folder F2(F0);
			F2.show();
			MA.show();
			MB.show();
		}

		{
			cout << "-----------------F3 = F0-----------------" << endl;
			Folder F3;
			F3 = F0;
			F3.show();
			MA.show();
			MB.show();
		}

		{
			cout << "-------------Message MC(MA)--------------" << endl;
			Message MC(MA);
			F0.show();
			F1.show();
			MC.show();
		}

		{
			cout << "----------------MD = MA-----------------" << endl;
			Message MD("DDD");
			MD = MA;
			F0.show();
			F1.show();
			MD.show();
		}
	}

	{
		int i = 42;
		int &r = i;
		//int &&r = i;
		//int &r2 = i * 42;
		const int &r3 = i * 42;
		int &&rr1 = i * 42;
		//int &&rr2 = rr1;
		int &&rr2 = std::move(rr1);
	}

	{
		// 13.48
		vector<String> vec;
		String s1("hello");
		String s2 = s1;		// 拷贝构造函数 1
		vec.push_back(s1);	// 2
		vec.push_back(s2);	// 3,4
	}
}


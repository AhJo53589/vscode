// Chapter_13_ Copy_Control.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
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
}

// test_operate.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <string>

using namespace std;


class point
{
public:
	point() : m_x(0), m_y(0) {};
	point(int x, int y) : m_x(x), m_y(y) {};

	point & operator+ (const point &p)
	{
		point newP;
		newP.m_x = this->m_x + p.m_x;
		newP.m_y = this->m_y + p.m_y;
		return newP;
	}

	friend ostream & operator<<(ostream& os, const point &p)
	{
		string s = "(" + to_string(p.m_x) + "," + to_string(p.m_y) + ")";
		os << s.c_str();
		return os;
	}

private:
	int m_x;
	int m_y;
};


int main()
{
	point p1(1,1);
	point p2(2, 2);

	cout << p1 << endl;
	cout << p2 << endl;

	p1 = p1 + p2;
	cout << p1 << endl;

}


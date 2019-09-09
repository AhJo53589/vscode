// test_bitset.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <bitset>

using namespace std;

void binary_out(int x)
{
	stack<int> s;
	while (x != 0)
	{
		s.push(x % 2);
		x /= 2;
	}
	while (!s.empty())
	{
		cout << s.top();
		s.pop();
	}
}

int main()
{
	int a = 9999;
	cout << bitset<sizeof(a) * 4>(a) << endl;	//int占4字节，一个字节8位，最终输出的是32个0或1
	binary_out(a);
	cout << endl;
}
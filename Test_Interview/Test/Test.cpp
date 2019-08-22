// Test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <algorithm>
#include <map>
#include <unordered_map>
#include <unordered_set>
#include <vector>
#include <queue>
#include <set>
#include <stack>
#include <string>
#include <random>
#include <bitset>
#include <functional>

#include <thread>
#include <atomic>
#include <stdio.h>

#include <memory>


using namespace std;

//template<class T>
//void swap2(T& a, T& b)
//{
//	T tmp(std::move(a));
//	a = std::move(b);
//	b = std::move(tmp);
//}
//
//void LR(int &a)
//{
//	cout << "L" << endl;
//}
//
//void LR(int &&a)
//{
//	cout << "R" << endl;
//}
//
//int main()
//{
//	int a = 5;
//	int b = 6;
//	//swap2(a, b);
//	LR(a);
//	LR(std::move(a));
//	LR(std::move(std::move(6)));
//	//LR(static_cast<int&&>(b));
//}



//int main() {
//	std::vector<std::string> foo(3);
//	std::vector<std::string> bar{ "one","two","three" };
//
//	std::copy(make_move_iterator(bar.begin()),
//		make_move_iterator(bar.end()),
//		foo.begin());
//
//	// bar now contains unspecified values; clear it:
//	bar.clear();
//
//	std::cout << "foo:";
//	for (std::string& x : foo) std::cout << ' ' << x;
//	std::cout << '\n';
//
//	return 0;
//}

//////////////////////////////////////////////////////////////////////////
// 一段代码，说一下运行结果
//class A {
//public: 
//	void f1() { }
//	virtual void f2() {}
//};
//
//int main()
//{
//	A* a = nullptr;
//	a->f1();
//	a->f2();
//}



//////////////////////////////////////////////////////////////////////////
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
// test_template_fun.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <stack>

using namespace std;

//////////////////////////////////////////////////////////////////////////
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




//////////////////////////////////////////////////////////////////////////
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

//////////////////////////////////////////////////////////////////////////
//class cc
//{
//public:
//	cc(int v) : val(v)
//	{
//		cout << "构造" << endl;
//	}
//	~cc()
//	{
//		val = 0;
//		cout << "析构, val = 0" << endl;
//	}
//
//	cc(const cc& rhs)
//	{
//		val = rhs.val;
//		cout << "拷贝" << endl;
//	}
//
//	int val;
//};
//
//template<typename T> class CQueue
//{
//public:
//	CQueue();
//	~CQueue();
//
//	void append(const T& node);
//	void deleteHead();
//
//private:
//	stack<T> st1;
//	stack<T> st2;
//};
//
//template<typename T>
//CQueue<T>::CQueue()
//{
//}
//
//template<typename T>
//CQueue<T>::~CQueue()
//{
//}
//
//template<typename T>
//void CQueue<T>::append(const T& node)
//{
//	cout << "st1.push() start" << endl;
//	st1.push(node);
//	cout << "st1.push() end" << endl << endl;
//}
//
//template<typename T>
//void CQueue<T>::deleteHead()
//{
//	cout << "st1.top() start" << endl;
//	T& data = st1.top();
//	cout << "T& data = st1.top();" << endl;
//	cout << "st1.top() end" << endl << endl;
//	cout << "st1.pop() start" << endl;
//	st1.pop();
//	cout << "st1.pop() end" << endl << endl;
//
//	cout << "输出data的val " << data.val << endl << endl;
//
//	//st2.push(data);
//}
//
//void main()
//{
//	cout << "实例化元素，val = 3" << endl;
//	cc c(3);
//	CQueue<cc> cq;
//	cq.append(c);
//	cq.deleteHead();
//}
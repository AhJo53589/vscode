// Chapter_16_Templates_and_Generic_Programming.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <vector>
#include <list>

#include "Blob.h"
#include "debug_rep.h"
#include "16_4_variadic_template.h"

using namespace std;


template<typename T>
int compare(const T &v1, const T &v2)
{
	//if (v1 < v2) return -1;
	//if (v2 < v1) return 1;
	//return 0;

	if (less<T>()(v1, v2)) return -1;
	if (less<T>()(v2, v1)) return 1;
	return 0;
}

template<unsigned N, unsigned M>
int compare(const char(&p1)[N], const char(&p2)[M])
{
	return strcmp(p1, p2);
}

template<typename T>
using twin = pair<T, T>;

template<class T = int> class Numbers
{
public:
	Numbers(T v = 0) : val(v) {}
private:
	T val;
};

int main()
{
	{
		cout << compare(1, 2) << endl;
	}

	{
		cout << compare("hi", "mom") << endl;
	}

	{
		twin<int> win_loss;
	}

	{
		Numbers<long double> lots_of_precision;
		Numbers<> average_precision;
	}

	{
		int ia[] = { 0,1,2,3,4,5,6,7,8,9 };
		vector<long> vi = { 0,1,2,3,4,5,6,7,8,9 };
		list<const char*> w = { "now", "is", "the", "time" };

		Blob<int> a1(begin(ia), end(ia));
		Blob<int> a2(vi.begin(), vi.end());
		Blob<string> a3(w.begin(), w.end());
	}

	{
		//template<typename T> T fobj(T, T);
		//template<typename T> T fref(const T&, const T&);

		//string s1("a value");
		//const string s2("another value");
		//fobj(s1, s2);	// fobj(string, string)
		//fref(s1, s2);	// fref(const string&, const string&)

		//int a[10], b[42];
		//fobj(a, b);	// f(int *, int *)
		//fref(a, b);	// error
	}

	{
		string s("hi");
		cout << debug_rep(s) << endl;
		cout << debug_rep(&s) << endl;

		const string *sp = &s;
		cout << debug_rep(sp) << endl;

		cout << debug_rep("Hello") << endl;
	}

	{
		foo(0, 1, 2, 3, 4, 5);
	}

	{
		print(cout, 1, "Hello", 2, "AhJo");
	}
}


// test_Functor.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

//////////////////////////////////////////////////////////////////////////
template<typename T, typename Func>
void for_each(T* begin, T* end, const Func& f)
{
	while (begin != end) f(*begin++);
}

template<typename T>
struct Print
{
	mutable int count = 0;

	void operator()(const T& x) const
	{
		cout << count << " : " << x << endl;
		count++;
	}
};


//////////////////////////////////////////////////////////////////////////
template <typename T, size_t ROWS, size_t COLS>
struct Matrix
{
	T data[ROWS][COLS];

	T operator() (int x, int y) const
	{
		return data[x][y];
	}

	T& operator() (int x, int y)
	{
		return data[x][y];
	}

	template <typename... Args>
	auto get(Args&&... args) const
	{
		return this->operator()(forward<Args>(args)...);
	}
};


int main()
{
	int arr[10] = { 1,2,3,4,5,6,7,8,9,0 };
	for_each(arr, arr + 5, Print<int>{});
	for_each(arr, arr + 5, [](auto&& x) { cout << x << " "; });

	Matrix<int, 10, 20> m;
	m(5, 5) = 10;
	cout << m(5, 5) << endl;
	cout << m.get(5, 5) << endl;
}

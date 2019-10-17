// Chapter_17_Specialized_Library_Facilities.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <vector>
#include <list>
#include <tuple>

using namespace std;

int main()
{
	{
		//tuple<size_t, size_t, size_t> threeD = { 1,2,3 }; // error
		tuple<size_t, size_t, size_t> threeD{ 1,2,3 };

		tuple<string, vector<double>, int, list<int>>
			someVal("constants", { 3.14,2.718 }, 42, { 0,1,2,3,4,5 });

		auto item = make_tuple("0-999", 3, 20.0);
		auto book = get<0>(item);
		auto cnt = get<1>(item);
		auto price = get<2>(item) / cnt;
		get<2>(item) *= 0.8;

		typedef decltype(item) trans;
		size_t sz = tuple_size<trans>::value;
		tuple_element<1, trans>::type cnt = get<1>(item);
	}
}

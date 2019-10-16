// Chapter_12_DynamicMemory.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
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

#include "query.h"
#include "StrBlob.h"

using namespace std;



//////////////////////////////////////////////////////////////////////////
void runQueries(ifstream &infile)
{
	TextQuery tq(infile);
	while (true)
	{
		cout << "enter word to look for, or q to quit: ";
		string s;
		if (!(cin >> s) || s == "q") break;
		print(cout, tq.query(s)) << endl;
	}
}

int main()
{
	{
		unique_ptr<int> p1(new int(42));
		unique_ptr<int> p2(p1.release());
		unique_ptr<int> p3;
		p3.reset(p2.release());
		auto p = p3.release();
		delete p;
		unique_ptr<int[]>pp(new int[10]);
		for (size_t i = 0; i != 10; ++i)
		{
			pp[i] = i;
		}
	}

	{
		string s;
		allocator<string> alloc;
		auto p = alloc.allocate(10);//用的是()，只分配内存
		auto q = p;
		string str = "a b c d e f g h i j";
		istringstream ss(str);
		while (ss >> s && q != p + 10)
		{
			alloc.construct(q++, s);//创建对象并幅值
		}
		for (auto i = 0; i < 10; i++)
		{
			cout << *(p + i) << ",";
		}
		cout << endl;
		while (q != p)
		{
			alloc.destroy(--q);//逐个销毁对象，destory接受一个指针
		}
		alloc.deallocate(p, 10);//分配多少内存，释放多少
	}

	{
		// 文本查询程序
		ifstream f("text.txt");
		runQueries(f);
	}
}

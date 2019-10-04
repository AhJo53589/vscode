// test_stl_shared_ptr.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
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

#include "Query.h"

using namespace std;

class StrBlobPtr;
//////////////////////////////////////////////////////////////////////////
class StrBlob
{
	friend class StrBlobPtr;
public:
	typedef vector<string>::size_type size_type;
	StrBlob();
	StrBlob(initializer_list<string> il);
	size_type size() const { return data->size(); }
	bool empty() const { return data->empty(); }
	void push_back(const string &t) { data->push_back(t); }
	void pop_back();

	string& front();
	string& back();

	//StrBlobPtr begin() { return StrBlobPtr(*this); }
	//StrBlobPtr end()
	//{
	//	auto ret = StrBlobPtr(*this, data->size());
	//	return ret;
	//}
private:
	shared_ptr<vector<string>> data;
	void check(size_type i, const string &msg) const;
};

StrBlob::StrBlob() : data(make_shared<vector<string>>()) {}
StrBlob::StrBlob(initializer_list<string> il) : data(make_shared<vector<string>>(il)) {}

void StrBlob::pop_back()
{
	check(0, "pop_back on empty StrBlob");
	data->pop_back();
}

string & StrBlob::front()
{
	check(0, "front on empty StrBlob");
	return data->front();
}

string & StrBlob::back()
{
	check(0, "back on empty StrBlob");
	return data->back();
}

void StrBlob::check(size_type i, const string & msg) const
{
	if (i >= data->size())
	{
		throw out_of_range(msg);
	}
}

//////////////////////////////////////////////////////////////////////////
class StrBlobPtr
{
public:
	StrBlobPtr() : curr(0) {}
	StrBlobPtr(StrBlob &a, size_t sz = 0) : wptr(a.data), curr(sz) {}
	string &deref() const;
	StrBlobPtr& incr();
private:
	shared_ptr<vector<string>> check(size_t, const string&) const;
	weak_ptr<vector<string>> wptr;
	size_t curr;
};

string & StrBlobPtr::deref() const
{
	auto p = check(curr, "dereference past end");
	return (*p)[curr];
}

StrBlobPtr & StrBlobPtr::incr()
{
	check(curr, "increment past end of StrBlobPtr");
	++curr;
	return *this;
}

shared_ptr<vector<string>> StrBlobPtr::check(size_t i, const string &msg) const
{
	auto ret = wptr.lock();
	if (!ret)
	{
		throw runtime_error("unbound StrBlobPtr");
	}
	if (i >= ret->size())
	{
		throw out_of_range(msg);
	}
	return ret;
}

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


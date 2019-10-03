// test_stl_generic_algorithm.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <numeric>
#include <functional>
#include <iterator>

#include <vector>
#include <string>
#include <list>

using namespace std;

void elimDups(vector<string> &words)
{
	sort(words.begin(), words.end());
	auto end_unique = unique(words.begin(), words.end());
	words.erase(end_unique, words.end());
}

void biggies(vector<string> words, vector<string>::size_type sz)
{
	cout << "words = " << endl;
	for (auto i : words) cout << i << ","; cout << endl;

	elimDups(words);
	cout << "words (sort by dict) = " << endl;
	for (auto i : words) cout << i << ","; cout << endl;

	stable_sort(words.begin(), words.end(), [](const string &s1, const string &s2) 
	{ 
		return s1.size() < s2.size();
	});
	cout << "words (stable_sort by size) = " << endl;
	for (auto i : words) cout << i << ","; cout << endl;

	auto wc = find_if(words.begin(), words.end(), [sz](const string &a)
	{
		return a.size() >= sz;
	});
	auto count = words.end() - wc;
	auto make_plural = [](size_t ctr, const string &word, const string &ending)
	{
		return (ctr <= 1) ? word : word + ending;
	};
	cout << count << " " << make_plural(count, "word", "s") << " of length " << sz << " or longer" << endl;

	for_each(wc, words.end(), [](const string &s)
	{
		cout << s << ",";
	});
	cout << endl;

	cout << "words partition (size > 5) = " << endl;
	auto it = partition(words.begin(), words.end(), [](const string &s)
	{
		return s.size() > 5;
	});
	for (auto i : words) cout << i << ","; cout << endl;
	cout << "[" << *it << "]" << endl;
}

int main()
{
	{
		vector<int> vec = { 1,2,3,4 };
		int sum = accumulate(vec.cbegin(), vec.cend(), 0);	// #include <numeric>
		cout << "accumulate = " << sum << endl;
	}

	{
		vector<string> v = {"Hello", "AhJo"};
		string sum = accumulate(v.cbegin(), v.cend(), string("+"));
		cout << "accumulate = " << sum << endl;
	}

	{
		vector<int> a = { 1,2,3,4,5 };
		vector<int> b = { 1,2,3,4,5,6,7,8,9 };
		cout << "equal (a,b) = " << boolalpha << equal(a.cbegin(), a.cend(), b.cbegin()) << endl;
	}

	{
		vector<int> vec = { 1,2,3,4 };
		fill(vec.begin(), vec.end(), 0);
		for (auto i : vec) cout << i << ",";
		cout << endl;
		fill_n(vec.begin(), 2, 1);
		for (auto i : vec) cout << i << ","; cout << endl;
	}

	{
		vector<int> vec;
		auto it = back_inserter(vec);
		*it = 1;
		fill_n(back_inserter(vec), 10, 2);
		for (auto i : vec) cout << i << ","; cout << endl;
	}

	{
		int a1[] = { 0,1,2,3,4,5,6,7,8,9 };
		int a2[sizeof(a1) / sizeof(*a1)];
		// ret指向拷贝到a2的尾元素之后的位置
		auto ret = copy(begin(a1), end(a1), a2);
	}

	{
		vector<int> vec = { 1,2,3,4,5,1,2,3,4,5 };
		replace(vec.begin(), vec.end(), 1, 2);
		list<int> lst;
		replace_copy(vec.cbegin(), vec.cend(), back_inserter(lst), 2, 3);
		for (auto i : lst) cout << i << ","; cout << endl;
	}

	{
		vector<string> words = { "the", "quick", "red", "fox", "jumps", "over", "the", "slow", "red", "turtle" };
		biggies(words, 4);
	}

	{
		vector<string> words = { "a", "a", "b", "c", "d", "e", "e", "e", "f", "g" };
		vector<string> n;
		unique_copy(words.cbegin(), words.cend(), back_inserter(n));
		for (auto i : n) cout << i << ","; cout << endl;
	}

	{
		string str = "MyLove";
		vector<int> vec = { 5,5,6,8,5,4,9,5,2,4 };
		auto length = str.size();
		auto f = [](const int &a, const size_t t)
		{
			return a > t;
		};
		auto it = find_if(vec.begin(), vec.end(), bind(f, placeholders::_1, length)); // #include <functional>
		cout << "fist val > MyLove length = " << *it << endl;
	}

	{
		list<int> lst = { 1,2,3,4 };
		list<int> lst2;
		list<int> lst3;
		copy(lst.cbegin(), lst.cend(), front_inserter(lst2));	// #include <iterator>
		copy(lst.cbegin(), lst.cend(), inserter(lst3, lst3.begin()));
		for (auto l : lst) cout << l << ","; cout << endl;
		for (auto l : lst2) cout << l << ","; cout << endl;
		for (auto l : lst3) cout << l << ","; cout << endl;
	}

	//{
	//	istream_iterator<int> in(cin), eof;
	//	cout << accumulate(in, eof, 0) << endl;
	//}

	{
		string line = { "FIRST,MIDDLE,LAST" };
		auto rcomma = find(line.crbegin(), line.crend(), ',');
		cout << string(rcomma.base(), line.cend()) << endl;
	}

	{
		list<int> lst = { 1,2,3,4,5,6,7,8,9 };
		list<int> lst2 = { 11,12,13,14,15 };
		auto it_1 = lst.begin();
		auto it_2 = lst2.begin();
		it_1++;
		it_2++;
		//lst.merge(lst2);
		lst.remove(1);
		//lst.sort();
		//lst.unique();
		//lst.splice(it_1, lst2);
		//lst.splice(it_1, lst2, it_2);
		lst.splice(it_1, lst2, it_2, lst2.end());
		for (auto l : lst) cout << l << ","; cout << endl;
		for (auto l : lst2) cout << l << ","; cout << endl;
	}
}

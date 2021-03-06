﻿// test_20190926.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <numeric>

#include <bitset>
#include <vector>
#include <map>

using namespace std;

//////////////////////////////////////////////////////////////////////////
//int main()
//{
//	//unsigned long long a = 0x7FFFFFFFFFFFFFFFULL;
//	//cout << hex << a << endl;
//	//a *= 10ULL;
//	//cout << hex << a << endl;
//	unsigned long long a = 0x7FFFFFFFFFFFFFFF;
//	cout << hex << a << endl;
//	a *= 10;
//	cout << hex << a << endl;
//}

//////////////////////////////////////////////////////////////////////////
//int func(int x) 
//{ 
//	int a = 0; 
//	while (x) 
//	{
//		cout << "x = " << x << "\t<--" << bitset<sizeof(int) * 8>(x) << endl;
//		a++; 
//		x = x & (x - 1);
//	}
//	return a; 
//}
//
//int main()
//{
//	cout << func(9999) << endl;
//}


//////////////////////////////////////////////////////////////////////////
//#pragma pack(1)
//struct a
//{
//	int i{ 0 };
//	char c1;
//	char c2;
//	long l{ 0 };
//	long long ll{ 0 };
//};
//#pragma pack()
//
//int main()
//{
//	cout << sizeof(a) << endl;
//}


//////////////////////////////////////////////////////////////////////////
//template<typename VALUE, bool>
//struct GFMapValueType
//{
//	using value_type = VALUE * ;
//};
//
//template<typename VALUE>
//struct GFMapValueType<VALUE, true>
//{
//	using value_type = std::shared_ptr<VALUE>;
//};
//
//template<typename KEY, typename VALUE, bool is_share_ptr>
//class GFMapBase
//{
//public:
//	using k_type = KEY;
//	using v_type = typename GFMapValueType<VALUE, is_share_ptr>::value_type;
//	using map_type = std::map<k_type, v_type>;
//	using value_type = typename map_type::value_type;
//	using reference = value_type & ;
//	using const_reference = const value_type &;
//	using iterator = typename map_type::iterator;
//	using const_iterator = typename map_type::const_iterator;
//	using reverse_iterator = typename map_type::reverse_iterator;
//	using const_reverse_iterator = typename map_type::const_reverse_iterator;
//
//	std::size_t size() const { return nodes_->size(); }
//	std::pair<iterator, bool> insert(const k_type& key, const v_type& value)
//	{
//		return nodes_.insert(value_type(key, value));
//	}
//	iterator erase(iterator it) { return nodes_.erase(it); }
//	std::size_t erase(const k_type& key) { return nodes_.erase(key); };
//	iterator find(const k_type& key) { return nodes_.find(key); }
//	reverse_iterator rbegin() { return nodes_.rbegin(); }
//	reverse_iterator rend() { return nodes_.rend(); }
//	v_type find_value(const k_type& key) 
//	{
//		auto it = find(key);
//		if (it != nodes_.end())
//		{
//			return it->second;
//		}
//		return v_type(nullptr);
//	}
//	v_type first() 
//	{
//		if (!nodes_.empty())
//		{
//			return *nodes_.begin();
//		}
//		return v_type(nullptr);
//	}
//	v_type next()
//	{
//		if (_curIter != nodes_.end())
//		{
//			return _curIter++->second;
//		}
//		return v_type(nullptr);
//	}
//	void reset() { _curIter = nodes_.begin(); }
//private:
//	map_type nodes_;
//	iterator _curIter;
//};
//
//// test
//GFMapBase<int, int, false> a;


//////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////
//字符串替换
//replacedas$REPLACE_ME$fj32RE3$REPLACE_ME$ad34$REPLACE_ME$2R34$REPLACE_ME$
//将上述字符串中的 $REPLACE_ME$ 替换为 HELLO
//
//string ReplaceStr(string str, string strOld, string strNew)
//{
//	string strRet;
//	for (size_t i = 0; i < str.size(); i++)
//	{
//		if (str[i] == strOld[0] && i + strOld.size() - 1 < str.size()
//			&& str.substr(i, strOld.size()) == strOld)
//		{
//			strRet += strNew;
//			i += strOld.size() - 1;
//		}
//		else
//		{
//			strRet += str[i];
//		}
//	}
//	return strRet;
//}
//
//string replaceString(string str, string strOld, string strNew)
//{
//	string strRet;
//	//auto it = str.begin();
//	//while (it != str.end())
//	//{
//	//	auto it_next = find(it, str.end(), strOld);
//	//	string temp(it, it_next);
//	//	if (equal(it_next, it_next + strOld.size(), strOld.begin()))
//	//	{
//	//		strRet.append(strNew);
//	//	}
//	//	it = it_next;
//	//}
//	return strRet;
//}
//
//int main()
//{
//	string str = "replacedas$REPLACE_ME$fj32RE3$REPLACE_ME$ad34$REPLACE_ME$2R34$REPLACE_ME&";
//	string strA = "$REPLACE_ME$";
//	string strB = "HELLO";
//
//	cout << str << endl;
//	string ans = ReplaceStr(str, strA, strB);
//	cout << ans << endl;
//	//string ans2 = replaceString(str, strA, strB);
//	//cout << ans2 << endl;
//}


//////////////////////////////////////////////////////////////////////////
//编写程序输出一个数的二进制表示中，能重新组成的最大整数（十进制转换为二进制整数采用“除2取余，逆序排列”法）。
//如：输入：5（101）输出6（110）；输入1（1）输出1（1）；输入：10（1010）输出：12（1100）

//int changeNum(int n)
//{
//	int cnt = 0;
//	int cnt_1 = 0;
//	while (n > 0)
//	{
//		cnt++;
//		cnt_1 += (n % 2);
//		n /= 2;
//		//cnt_1 += (n & 1);
//		//n = n >> 1;
//	}
//
//	int x = 0;
//	while (cnt_1-- > 0)
//	{
//		x += pow(2, --cnt);
//	}
//	return x;
//}
//
//int main()
//{
//	vector<int> TESTS;
//	vector<int> ANSWERS;
//
//	TESTS.push_back(5);
//	ANSWERS.push_back(6);
//
//	TESTS.push_back(1);
//	ANSWERS.push_back(1);
//
//	TESTS.push_back(10);
//	ANSWERS.push_back(12);
//
//	TESTS.push_back(2048);
//	ANSWERS.push_back(2048);
//
//	TESTS.push_back(2047);
//	ANSWERS.push_back(2047);
//
//	TESTS.push_back(9999);
//	ANSWERS.push_back(16320);
//
//	for (size_t i = 0; i < TESTS.size(); i++)
//	{
//		auto ans = changeNum(TESTS[i]);
//		cout << "num = " << TESTS[i] << endl;
//		cout << "ans = " << ans << "\t<--" << ANSWERS[i] << endl << endl;
//	}
//}
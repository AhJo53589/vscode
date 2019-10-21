// test_stringstream.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <sstream>
#include <iomanip>	//setw setfill

#include <vector>
#include <string>

using namespace std;


template<typename out_type, typename in_value>
out_type convert(const in_value & t) 
{
	std::stringstream stream;
	stream << t;
	out_type result;
	stream >> result;
	return result;
}

string i2s(int i, int len = 0)
{
	stringstream ss;
	ss << setw(len) << setfill('0') << i;
	return ss.str();
}

int main()
{
	{
		vector<int> vec;
		string s = "[1,2,3]";
		vec.push_back(convert<int>(s));
		for (auto i : vec) cout << i << ","; cout << endl;
	}


	{
		string s = "1\n23 # 4";
		stringstream ss;
		ss << s;
		while (ss >> s) 
		{
			cout << s << ",";
			int val = convert<int>(s);
			cout << val << endl;
		}
	}

	{
		string s = "true false True False TRUE FALSE 1 0";
		stringstream ss;
		ss << s;
		while (ss >> s)
		{
			cout << s << ",";
			bool val = convert<bool>(s);
			cout << val << boolalpha <<endl;
		}
	}

	{
		cout << i2s(123, 8) << endl;
	}

}

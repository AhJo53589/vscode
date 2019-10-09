// Chapter_11_Associative_Containers.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <fstream>
#include <sstream>

#include <string>
#include <map>
#include <set>
#include <unordered_map>
#include <unordered_set>
#include <vector>

using namespace std;

const string & transform(const string &s, const map<string, string> &m)
{
	auto map_it = m.find(s);
	if (map_it != m.cend())
	{
		return map_it->second;
	}
	else
	{
		return s;
	}
}

map<string, string> buildMap(ifstream &map_file)
{
	map<string, string> trans_map;
	string key;
	string value;
	while (map_file >> key && getline(map_file, value))
	{
		if (value.size() > 1)
		{
			trans_map[key] = value.substr(1);
		}
		else
		{
			throw runtime_error("no rule for " + key);
		}
	}
	return trans_map;
}

void word_transform(ifstream &map_file, ifstream &input)
{
	auto trans_map = buildMap(map_file);
	string text;
	while (getline(input, text))
	{
		istringstream stream(text);
		string word;
		bool firstword = true;
		while (stream >> word)
		{
			if (firstword)
			{
				firstword = false;
			}
			else
			{
				cout << " ";
			}
			cout << transform(word, trans_map);
		}
		cout << endl;
	}
}

int main()
{
	{
		set<string>::value_type v1;
		set<string>::key_type v2;
		map<string, int>::value_type v3;
		map<string, int>::key_type v4;
		map<string, int>::mapped_type v5;
	}

	{
		map<int, int> m;
		auto b = m.insert({ 1,1 });
		cout << "return from map insert. [using _Pairib = pair<iterator, bool>;]" << endl;
		cout << "first = (" << (b.first)->first << "," << (b.first)->second << ")" << endl;
		cout << "second = " << boolalpha << b.second << endl;
	}

	{
		multimap<int, int> m = { {1,1},{1,2},{1,3},{2,2} };
		auto cnt = m.erase(1);
		cout << "erase count = " << cnt << endl;
	}

	{
		set<int> s = { 1,2,3,4,5,6,7,8,9 };
		auto it = s.find(5);
		cout << "set find 5 = " << *it << endl;
		it = s.lower_bound(5);
		cout << "set lower_bound 5 = " << *it << endl;
		it = s.upper_bound(5);
		cout << "set upper_bound 5 = " << *it << endl;
		auto it_pair = s.equal_range(5);
		cout << "set equal_range 5, first = " << *it_pair.first << ", second = " << *it_pair.second << endl;
	}

	{
		// 返回满足条件的合集
		multimap<int, int> m = { {1,1},{1,2},{2,1},{2,2},{2,3},{3,1} };
		cout << "print multimap elements by count and find: " << endl;
		auto entries = m.count(2);
		auto it = m.find(2);
		while (entries)
		{
			cout << it->second << ",";
			++it;
			--entries;
		}
		cout << endl;

		cout << "print multimap elements by lower_bound & upper_bound: " << endl;
		for (auto beg = m.lower_bound(2), end = m.upper_bound(2); beg != end; ++beg)
		{
			cout << beg->second << ",";
		}
		cout << endl;

		cout << "print multimap elements by equal_range: " << endl;
		for (auto pos = m.equal_range(2); pos.first != pos.second; ++pos.first)
		{
			cout << pos.first->second << ",";
		}
		cout << endl;
	}

	{
		// 单词转换程序
		ifstream map_file("trans.txt");
		ifstream input("text.txt");
		word_transform(map_file, input);
	}

	{
		unordered_map<string, size_t> word_count;
		vector<string> word = { "d", "e", "d", "a", "b", "c", "b", "c" };
		for (const auto &w : word)
		{
			word_count[w]++;
		}
		for (const auto &w : word_count)
		{
			cout << w.first << " occurs " << w.second << ((w.second > 1) ? " times" : " time") << endl;
		}

		cout << "bucket_count = " << word_count.bucket_count() << endl;
		cout << "max_bucket_count = " << word_count.max_bucket_count() << endl;
		cout << "bucket_size(0) = " << word_count.bucket_size(0) << endl;
		cout << "bucket(a) = " << word_count.bucket("a") << endl;
	}
}
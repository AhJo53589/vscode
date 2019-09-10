// Test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <algorithm>
#include <map>
#include <unordered_map>
#include <unordered_set>
#include <vector>
#include <queue>
#include <set>
#include <stack>
#include <string>
#include <random>
#include <bitset>
#include <functional>

#include <thread>
#include <atomic>
#include <stdio.h>

#include <memory>


using namespace std;

//template<class T>
//void swap2(T& a, T& b)
//{
//	T tmp(std::move(a));
//	a = std::move(b);
//	b = std::move(tmp);
//}
//
//void LR(int &a)
//{
//	cout << "L" << endl;
//}
//
//void LR(int &&a)
//{
//	cout << "R" << endl;
//}
//
//int main()
//{
//	int a = 5;
//	int b = 6;
//	//swap2(a, b);
//	LR(a);
//	LR(std::move(a));
//	LR(std::move(std::move(6)));
//	//LR(static_cast<int&&>(b));
//}



//int main() {
//	std::vector<std::string> foo(3);
//	std::vector<std::string> bar{ "one","two","three" };
//
//	std::copy(make_move_iterator(bar.begin()),
//		make_move_iterator(bar.end()),
//		foo.begin());
//
//	// bar now contains unspecified values; clear it:
//	bar.clear();
//
//	std::cout << "foo:";
//	for (std::string& x : foo) std::cout << ' ' << x;
//	std::cout << '\n';
//
//	return 0;
//}

//////////////////////////////////////////////////////////////////////////
// 一段代码，说一下运行结果
//class A {
//public: 
//	void f1() { }
//	virtual void f2() {}
//};
//
//int main()
//{
//	A* a = nullptr;
//	a->f1();
//	a->f2();
//}



//int main()
//{
//	map<int, string> myMap;
//	for (int i = 0; i < 10; i++)
//	{
//		myMap.insert({ i, to_string(i) });
//	}
//
//	for (auto it = myMap.begin(); it != myMap.end(); it++)
//	{
//		if (it->first % 2 == 0)
//		{
//			it = myMap.erase(it);
//		}
//	}
//
//	for (auto &m : myMap)
//	{
//		cout << m.first << "\t" << m.second << endl;
//	}
//}

//////////////////////////////////////////////////////////////////////////
//void scan_delete(vector<string>& vec, const char* del_item)
//{
//	vector<string>::iterator itr = vec.begin();
//	//while (itr |= vec.end())
//	while (itr != vec.end())
//		{
//		if (*itr == del_item)
//			//vec.erase(itr);
//			itr = vec.erase(itr);
//		++itr;
//	}
//}
//
//void main()
//{
//	//vector<string> vec("C++", "C#", "php", "lua", "java");
//	vector<string> vec{ "C++", "C#", "php", "lua", "java" };
//	scan_delete(vec, "php");
//}

//////////////////////////////////////////////////////////////////////////
//const char *getName()
//{
//	return "Php Is The Best Language";
//}
//
//void main()
//{
//	std::string _name = getName();
//}

//////////////////////////////////////////////////////////////////////////
class Car
{
public:
	Car(const char* name) { m_Name = name; }
	std::string m_Name;
};

void main()
{
	std::map<int, Car*> _map;
	_map[0] = new Car("Benz");
	_map[1] = new Car("Audi");
	_map[2] = new Car("Jeep");

	// delete
	delete(_map[0]);
	_map[0] = nullptr;
	delete(_map[1]);
	_map[1] = nullptr;
	delete(_map[2]);
	_map[2] = nullptr;
	_map.clear();
}
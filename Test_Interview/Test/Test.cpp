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

template<class T>
void swap2(T& a, T& b)
{
	T tmp(std::move(a));
	a = std::move(b);
	b = std::move(tmp);
}

int main()
{
	int a = 5;
	int b = 6;
	swap2(a, b);
}
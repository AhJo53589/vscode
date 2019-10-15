#pragma once
#ifndef STRVEC_H
#define STRVEC_H

#include <string>
#include <algorithm>
#include <memory>
using namespace std;

class String
{
public:
	String() = default;

	String(const char*s)//接受c风格字符串参数的构造函数，s为指向字符串的指针(首位置)
	{
		auto s1 = const_cast<char*>(s);//转化为非常量的指针
		while (*s1)
		{
			++s1;//使其指向最后一个位置的尾部
		}
		alloc_n_copy(s, s1);//进行拷贝
	}
	String(const String&);//拷贝构造函数
	String& operator=(const String&);//拷贝赋值运算符
	~String()//析构函数
	{
		free();
	}
	void free()//释放内存
	{
		if (elements)
		{
			for_each(elements, end, [this](char &rhs) {alloc.destroy(&rhs); });
			alloc.deallocate(elements, end - elements);
		}
	}

private:
	allocator<char> alloc;//分配内存的方法
	char *elements;//首尾指针
	char *end;

	std::pair<char*, char*> alloc_n_copy(const char*a, const char*b)//拷贝赋值函数
	{
		auto s1 = alloc.allocate(b - a);//allocate参数为分配内存的大小
		auto s2 = uninitialized_copy(a, b, s1);//拷贝赋值，将a到b之间的元素拷贝至s1,返回的是最后一个构造元素之后的位置
		return make_pair(s1, s2);//返回首尾指针
	}

	void range_initializer(const char*c, const char*d)//初始化
	{
		auto p = alloc_n_copy(d, c);//拷贝并初始化新的string
		elements = p.first;
		end = p.second;//将新的string的首尾指针赋值
	}
};
#endif STRVEC_H

#include "pch.h"

#include <iostream>

#include "String.h"

using namespace std;

String::String(const String &rhs)
{
	range_initializer(rhs.elements, rhs.end);
	cout << "�������캯��" << endl;
}

String & String::operator=(const String &rhs)
{
	auto newstr = alloc_n_copy(rhs.elements, rhs.end);
	free();
	elements = newstr.first;
	end = newstr.second;
	cout << "������ֵ�����" << endl;
	return *this;
}

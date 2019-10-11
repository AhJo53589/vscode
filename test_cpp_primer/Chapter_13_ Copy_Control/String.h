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

	String(const char*s)//����c����ַ��������Ĺ��캯����sΪָ���ַ�����ָ��(��λ��)
	{
		auto s1 = const_cast<char*>(s);//ת��Ϊ�ǳ�����ָ��
		while (*s1)
		{
			++s1;//ʹ��ָ�����һ��λ�õ�β��
		}
		alloc_n_copy(s, s1);//���п���
	}
	String(const String&);//�������캯��
	String& operator=(const String&);//������ֵ�����
	~String()//��������
	{
		free();
	}
	void free()//�ͷ��ڴ�
	{
		if (elements)
		{
			for_each(elements, end, [this](char &rhs) {alloc.destroy(&rhs); });
			alloc.deallocate(elements, end - elements);
		}
	}

private:
	allocator<char> alloc;//�����ڴ�ķ���
	char *elements;//��βָ��
	char *end;

	std::pair<char*, char*> alloc_n_copy(const char*a, const char*b)//������ֵ����
	{
		auto s1 = alloc.allocate(b - a);//allocate����Ϊ�����ڴ�Ĵ�С
		auto s2 = uninitialized_copy(a, b, s1);//������ֵ����a��b֮���Ԫ�ؿ�����s1,���ص������һ������Ԫ��֮���λ��
		return make_pair(s1, s2);//������βָ��
	}

	void range_initializer(const char*c, const char*d)//��ʼ��
	{
		auto p = alloc_n_copy(d, c);//��������ʼ���µ�string
		elements = p.first;
		end = p.second;//���µ�string����βָ�븳ֵ
	}
};
#endif STRVEC_H

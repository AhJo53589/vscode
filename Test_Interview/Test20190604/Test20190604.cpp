// Test20190604.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

// 请找出以下代码中的错误并修正。


#include "pch.h"
#include <iostream>
using namespace std;

class TestClass
{
	char *m_data;
	int m_size;

public:
	TestClass(void)
		// 指针初始化
		//: m_data(NULL)
	{
		m_size = 0;
	}

	~TestClass(void)
	{
		if (m_data)
		{
			delete[] m_data;
		}
	}

	// 拷贝构造函数
	//TestClass(const TestClass& _copy) 
	//	: m_data(NULL)
	//{
	//	copyData(_copy.m_data, _copy.m_size);
	//	m_size = _copy.m_size;
	//}

	void copyData(const char*data, int size)
	{
		// 删除m_data防止内存泄露
		//if (m_data != NULL)
		//{
		//	delete[] m_data;
		//	m_data = NULL;
		//}
		m_data = new char[size];
		memcpy(m_data, data, size);
		m_size = size;
	}

	int getDataSize(void) const 
	{
		return m_size;
	}
};

void showSize(TestClass a)
// 或者不写拷贝构造函数，参数改为引用可避免出现问题
//void showSize(const TestClass &a)
{
	cout << "size = " << a.getDataSize() << endl;
}

int main()
{
	const char * const szData = "ABC";

	TestClass a;
	a.copyData(szData, strlen(szData) + 1);

	showSize(a);

	return 0;
}


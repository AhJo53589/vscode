// test_0x11_shared_ptr.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

////////////////////////////////////////////////////////////////////////////
class Test
{
public:
	Test()
	{
		std::cout << "Test()" << std::endl;
	}
	~Test()
	{
		std::cout << "~Test()" << std::endl;
	}
};
int main()
{
	std::shared_ptr<Test> p1 = std::make_shared<Test>();
	std::cout << "1 ref:" << p1.use_count() << std::endl;
	{
		std::shared_ptr<Test> p2 = p1;
		std::cout << "2 ref:" << p1.use_count() << std::endl;
	}
	std::cout << "3 ref:" << p1.use_count() << std::endl;
	return 0;
}

////////////////////////////////////////////////////////////////////////////
//class A
//{
//public:
//	int i;
//	A(int n) :i(n) { };
//	~A() { cout << i << " " << "destructed" << endl; }
//};
//int main()
//{
//	shared_ptr<A> sp1(new A(2)); //A(2)由sp1托管，
//	shared_ptr<A> sp2(sp1);       //A(2)同时交由sp2托管
//	shared_ptr<A> sp3;
//	sp3 = sp2;   //A(2)同时交由sp3托管
//	cout << sp1->i << "," << sp2->i << "," << sp3->i << endl;
//	A * p = sp3.get();      // get返回托管的指针，p 指向 A(2)
//	cout << p->i << endl;  //输出 2
//	sp1.reset(new A(3));    // reset导致托管新的指针, 此时sp1托管A(3)
//	sp2.reset(new A(4));    // sp2托管A(4)
//	cout << sp1->i << endl; //输出 3
//	sp3.reset(new A(5));    // sp3托管A(5),A(2)无人托管，被delete
//	cout << "end" << endl;
//	return 0;
//}

////////////////////////////////////////////////////////////////////////////
//int main() {
//	std::shared_ptr<int> p1(new int(1)); //方式1
//	std::shared_ptr<int> p2 = p1; //方式2
//	std::shared_ptr<int> p3;
//	//方式3 reset，如果原有的shared_ptr不为空，会使原对象的引用计数减1
//	p3.reset(new int(1));
//	//方式4
//	std::shared_ptr<int> p4 = std::make_shared<int>(2);
//
//	//使用方法例子：可以当作一个指针使用
//	std::cout << *p4 << std::endl;
//	//std::shared_ptr<int> p4 = new int(1);
//	if (p1) 
//	{ //重载了bool操作符
//		std::cout << "p is not null" << std::endl;
//	}
//	int* p = p1.get();//获取原始指针 
//	std::cout << *p << std::endl;
//}

////////////////////////////////////////////////////////////////////////////
//template<typename T>
//std::shared_ptr<T> make_shared_array(size_t size) 
//{
//	return std::shared_ptr<T>(new T[size], std::default_delete<T[]>());
//}
//
//int main() 
//{
//	//lambda
//	std::shared_ptr<int> p(new int[10], [](int* p) {delete[] p; });
//	//指定默认删除器
//	std::shared_ptr<int> p1(new int[10], std::default_delete<int[]>());
//	//自定义泛型方法 
//	std::shared_ptr<char> p2 = make_shared_array<char>(10);
//}
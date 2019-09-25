// test_shared_ptr.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

class B;
class A
{
public:
	shared_ptr<B> pb_;

	~A()
	{
		cout << "A delete\n";
	}

	void funA()
	{
		cout << "funA()" << endl;
	}

};

class B
{
public:
	//shared_ptr<A> pa_;
	weak_ptr<A> pa_;

	~B()
	{
		cout << "B delete\n";
	}

	void funB()
	{
		cout << "funB()" << endl;
	}
};

void fun()
{
	shared_ptr<B> pb(new B());
	shared_ptr<A> pa(new A());
	pb->pa_ = pa;
	pa->pb_ = pb;

	pa->funA();
	pb->funB();

	pa->pb_->funB();
	pb->pa_.lock()->funA();

	cout << pb.use_count() << endl;
	cout << pa.use_count() << endl;
}
int main()
{
	fun();
	return 0;
}
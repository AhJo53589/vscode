// testvirtual.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

class Base
{
public:
	virtual ~Base() {}
	void A()
	{
		B();
		C();
	}

private:
	virtual void B()
	{
		std::cout << "Base::B() !\n" << std::endl;
	}

	void C()
	{
		std::cout << "Base::C() !\n" << std::endl;
	}
};

class Child : public Base
{
private:
	virtual void B() override
	{
		std::cout << "Child::B() !\n" << std::endl;
	}

	void C()
	{
		std::cout << "Child::C() !\n" << std::endl;
	}

};

int main()
{
	Base *m = new Base();
	Child *n = new Child();
	m->A();
	n->A();
}


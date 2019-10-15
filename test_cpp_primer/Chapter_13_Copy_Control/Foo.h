#pragma once

#include <vector>

class Foo
{
public:
	Foo &operator= (const Foo&) &;
	Foo sorted() && ;
	Foo sorted() const &;
private:
	std::vector<int> data;
};
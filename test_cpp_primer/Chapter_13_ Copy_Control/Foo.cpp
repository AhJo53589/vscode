#include "pch.h"

#include <algorithm>

#include "Foo.h"

using namespace std;

Foo & Foo::operator=(const Foo &rhs) &
{
	// todo
	return *this;
}

Foo Foo::sorted() &&
{
	sort(data.begin(), data.end());
	return *this;
}

Foo Foo::sorted() const &
{
	Foo ret(*this);
	sort(ret.data.begin(), ret.data.end());
	return ret;
}

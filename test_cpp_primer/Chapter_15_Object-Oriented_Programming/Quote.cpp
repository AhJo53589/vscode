#include "pch.h"
#include "Quote.h"

using namespace std;

Quote::Quote(const Quote & q)
	: bookNo(q.bookNo), price(q.price) 
{
	cout << "拷贝构造函数" << endl;
}

Quote & Quote::operator=(const Quote & rhs)
{
	if (this != &rhs)//防止自赋值的情况
	{
		bookNo = rhs.bookNo;
		price = rhs.price;
	}
	cout << "拷贝赋值运算符" << endl;
	return *this;
}

Quote::Quote(const Quote && q)
	: bookNo(move(q.bookNo)), price(move(q.price))
{
	cout << "移动构造函数" << endl;
}

Quote & Quote::operator=(const Quote && rhs) noexcept
{
	if (this != &rhs)
	{
		bookNo = move(rhs.bookNo);
		price = move(rhs.price);
	}
	cout << "拷贝赋值运算符" << endl;
	return *this;
}

void Quote::debug()
{
	cout << "This is Quote Class" << endl;
	cout << "ISBN: \t\t" << bookNo << endl;
	cout << "Price: \t\t" << price << endl;
}

double Bulk_quote::net_price(std::size_t cnt) const
{
	if (cnt >= quantity)
	{
		return cnt * (1 - discount) * price;
	}
	return cnt * price;
}

void Bulk_quote::debug()
{
	cout << "This is bulk_quote Class" << endl;
	cout << "DISCOUNT: \t" << discount << endl;
	cout << "Quantity: \t" << quantity << endl;
	cout << "Price: \t\t" << price << endl;
}

double Bulk_number_quote::net_price(std::size_t cnt) const
{
	if (cnt >= quantity)
	{
		return quantity * (1 - discount) * price
			+ (cnt - quantity) * price;
	}
	return cnt * (1 - discount) * price;
}

void Bulk_number_quote::debug()
{
	cout << "This is Bulk_number_quote Class" << endl;
	cout << "DISCOUNT: \t" << discount << endl;
	cout << "Quantity: \t" << quantity << endl;
	cout << "Price: \t\t" << price << endl;//protected成员也可以包含
}

double Basket::total_receipt(std::ostream &os) const
{
	double sum = 0.0;
	for (auto iter = items.cbegin(); iter != items.cend(); iter = items.upper_bound(*iter))
	{
		sum += print_total(os, **iter, items.count(*iter));
	}
	os << "Total Sale: " << sum << endl;
	return sum;
}

double print_total(std::ostream & os, const Quote & item, size_t n)
{
	double ret = item.net_price(n);
	os << "ISBN: " << item.isbn() 
		<< " # sold: " << n
		<< " total due: " << ret << std::endl;
	return ret;
}

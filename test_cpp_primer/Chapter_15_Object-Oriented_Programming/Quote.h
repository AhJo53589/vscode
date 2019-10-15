#pragma once

#include <string>
#include <iostream>
#include <set>

//////////////////////////////////////////////////////////////////////////
class Quote
{
public:
	Quote() = default;
	Quote(const std::string &book, double sales_price) : bookNo(book), price(sales_price) {}

	Quote(const Quote&);//拷贝构造函数
	Quote& operator=(const Quote&); //拷贝赋值运算符
	Quote(const Quote&&); //移动构造函数
	Quote& operator=(const Quote&& quote4) noexcept; //移动赋值运算符


	std::string isbn() const { return bookNo; }
	virtual double net_price(std::size_t n) const
	{
		return n * price;
	}

	virtual ~Quote() = default;

	virtual void debug();

	virtual Quote* clone() const & { return new Quote(*this); }
	virtual Quote* clone() const && { return new Quote(std::move(*this)); }

private: 
	std::string bookNo;

protected:
	double price = 0.0;

};

//////////////////////////////////////////////////////////////////////////
class Disc_quote : public Quote
{
public:
	Disc_quote() = default;
	Disc_quote(const std::string& book, double price, std::size_t qty, double disc)
		: Quote(book, price), quantity(qty), discount(disc) {}
	double net_price(std::size_t) const = 0;

protected:
	std::size_t quantity = 0;
	double discount = 0.0;
};

//////////////////////////////////////////////////////////////////////////
class Bulk_quote : public Disc_quote
{
public:
	//Bulk_quote() = default;
	using Disc_quote::Disc_quote;
	Bulk_quote(const std::string& book, double p, std::size_t qty, double disc)
		: Disc_quote(book, p, qty, disc) {}
	double net_price(std::size_t) const override;
	void debug() override;

	Bulk_quote *clone() const & { return new Bulk_quote(*this); }
	Bulk_quote *clone() && { return new Bulk_quote(std::move(*this)); }
};

//////////////////////////////////////////////////////////////////////////
class Bulk_number_quote : public Disc_quote
{
public:
	Bulk_number_quote() = default;
	Bulk_number_quote(const std::string &book, double p, std::size_t qty, double disc)
		: Disc_quote(book, p, qty, disc) {}
	double net_price(std::size_t) const override;
	void debug() override;

	Bulk_number_quote *clone() const & { return new Bulk_number_quote(*this); }
	Bulk_number_quote *clone() && { return new Bulk_number_quote(std::move(*this)); }
};

//////////////////////////////////////////////////////////////////////////
class Basket
{
public:
	void add_item(const Quote& sale)
	{
		items.insert(std::shared_ptr<Quote>(sale.clone()));
	}
	void add_item(Quote&& sale)
	{
		items.insert(std::shared_ptr<Quote>(std::move(sale).clone()));
	}
	void add_item(const std::shared_ptr<Quote> &sale)
	{
		items.insert(sale);
	}
	double total_receipt(std::ostream&) const;

private:
	static bool compare(const std::shared_ptr<Quote> &lhs, const std::shared_ptr<Quote> &rhs)
	{
		return lhs->isbn() < rhs->isbn();
	}
	std::multiset<std::shared_ptr<Quote>, decltype(compare)*> items{ compare };
};

//////////////////////////////////////////////////////////////////////////
double print_total(std::ostream &, const Quote &, size_t);

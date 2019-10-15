// Chapter_15_Object-Oriented_Programming.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <vector>
#include <fstream>

#include "Quote.h"
#include "Query.h"
using namespace std;


//////////////////////////////////////////////////////////////////////////
void runQueries(ifstream &infile)
{
	TextQuery tq(infile);
	while (true)
	{
		cout << "enter word to look for, or q to quit: ";
		string s;
		getline(cin, s);
		if (s == "q") break;
		Query q(s);
		cout << q.eval(tq) << endl;
	}
}

//////////////////////////////////////////////////////////////////////////
int main()
{
	{
		Quote q("Book_1", 10.0);
		Bulk_quote bq("Book_2", 10.0, 5, 0.2);
		Bulk_number_quote bnq("Book_3", 10.0, 5, 0.2);

		print_total(cout, q, 1);
		print_total(cout, q, 5);
		print_total(cout, bq, 1);
		print_total(cout, bq, 5);
		print_total(cout, bnq, 1);
		print_total(cout, bnq, 5);
		print_total(cout, bnq, 6);
	}

	{
		Quote q("Book_1", 10.0);
		Bulk_quote bq("Book_2", 10.0, 5, 0.2);
		Bulk_number_quote bnq("Book_3", 10.0, 5, 0.2);

		q.debug();
		bq.debug();
		bnq.debug();
	}

	{
		vector<shared_ptr<Quote>> basket;
		basket.push_back(make_shared<Quote>("Book_1", 10.0));
		basket.push_back(make_shared<Bulk_quote>("Book_2", 10.0, 5, 0.2));
		basket.push_back(make_shared<Bulk_number_quote>("Book_3", 10.0, 5, 0.2));

		cout << basket.back()->net_price(6) << endl;
	}

	{
		Basket b;
		Bulk_quote bq("Book_2", 10.0, 5, 0.2);
		Bulk_number_quote bnq("Book_3", 10.0, 5, 0.2);
		b.add_item(Quote("Book_1", 10.0));
		for (int i = 0; i < 5; i++)
		{
			b.add_item(bq);
			b.add_item(bnq);
		}
		b.add_item(make_shared<Bulk_number_quote>("Book_3", 10.0, 5, 0.2));

		b.total_receipt(cout);
	}

	{
		// 文本查询程序
		ifstream f("text.txt");
		runQueries(f);
		//		Query q = Query("fiery") & Query("bird") | Query("wind");

	}
}

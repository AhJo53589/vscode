// testBook.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

// 图书馆，借阅卡初始金额 300

// 借阅规则：

// 图书价格 >= 100, 借阅天数 <= 15天 时，5元/天；借阅天数 > 15天 时，超过部分 3元/天
// 否则，图书价格 >= 50, 借阅天数 <=15天 时，3元/天；借阅天数 > 15天 时，超过部分 2元/天
// 否则，1元每天

// 如果超期未还，超期部分额外 1元/天

// 如果余额 < 图书价格，不能借书
// 如果图书价格 < 借书时扣除的租金，不能借书
// 但是可以借其他书

// 数据格式：图书价格,预借天数,实际天数

int getCostDay(int price, int day)
{
	if (price >= 100) return (day <= 15) ? 5 : 3;
	else if (price >= 50) return (day <= 15) ? 3 : 2;
	return 1;
}

int getCost(int price, int day)
{
	if (day > 15) return (day - 15) * getCostDay(price, day) + 15 * getCostDay(price, 15);
	return day * getCostDay(price, day);
}

bool Valid(int account, int price, int preDay, int factDay)
{
	bool flag1 = (account >= price);
	bool flag2 = (price >= getCost(price, preDay));
	bool flag3 = true;//(price >= getCost(price, factDay));
	return (flag1 && flag2 && flag3);
}

int BorrowBook(int price, int preDay, int factDay)
{
	int costOver = (factDay > preDay) ? (factDay - preDay) * 1 : 0;
	return costOver + getCost(price, factDay);
}

int main()
{
	char ch;
	int bookPrice, preBorrowDay, factBorrowDay;
	int account = 300;

	while (cin >> bookPrice >> ch >> preBorrowDay >> ch >> factBorrowDay)
	{
		cout << endl << "////////////////////////////////////" << endl;
		if (Valid(account, bookPrice, preBorrowDay, factBorrowDay))
		{
			cout << "true" << endl;
			//account -= BorrowBook(bookPrice, preBorrowDay, factBorrowDay);
			int cost = BorrowBook(bookPrice, preBorrowDay, factBorrowDay);
			cout << "cost = " << cost << endl;
			account -= BorrowBook(bookPrice, preBorrowDay, factBorrowDay);
			cout << "account = " << account << endl;
		}
	}

	cout << account << endl;
}


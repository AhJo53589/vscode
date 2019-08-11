// testBook.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <vector>

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

int getCostWithoutOverdue(int price, int day)
{
	auto getDailyPrice = [](int price, int day)
	{
		if (price >= 100) return (day <= 15) ? 5 : 3;
		else if (price >= 50) return (day <= 15) ? 3 : 2;
		return 1;
	};

	if (day > 15)
	{
		return (day - 15) * getDailyPrice(price, day)
			+ 15 * getDailyPrice(price, 15);
	}
	return day * getDailyPrice(price, day);
}

bool Valid(int account, int price, int preDay, int factDay)
{
	if (price < 0) return false;
	if (preDay < 0) return false;
	if (factDay < 0) return false;
	if (account < price) return false;
	if (price < getCostWithoutOverdue(price, preDay)) return false;
	//if (price < getCostWithoutOverdue(price, factDay)) return false;
	return true;
}

int ReturnBook(int price, int preDay, int factDay)
{
	int costOver = (factDay > preDay) ? (factDay - preDay) * 1 : 0;
	return costOver + getCostWithoutOverdue(price, factDay);
}

void BorrowBook(int &account, int price, int preDay, int factDay)
{
	if (!Valid(account, price, preDay, factDay)) return;
	account -= ReturnBook(price, preDay, factDay);

	// for test
	//cout << endl << "////////////////////////////////////" << endl;
	//cout << "true" << endl;
	//int cost = ReturnBook(price, preDay, factDay);
	//cout << "cost = " << cost << endl;
	//cout << "account = " << account << endl;
}

//int main()
//{
//	char ch;
//	int price, preDay, factDay;
//	int account = 300;
//
//	while (cin >> price >> ch >> preDay >> ch >> factDay)
//	{
//		BorrowBook(account, price, preDay, factDay);
//	}
//
//	cout << account << endl;
//}

// for test
int main()
{
	vector<vector<vector<int>>> input;
	vector<vector<int>> answer;

	// case
	input.push_back({ { 180, 10, 10 }, { 80, 10, 3 }, {30, 10, 12} });
	answer.push_back({ 250, 241, 227 });

	// case
	input.push_back({ { 180, 10, 15 } });
	answer.push_back({ 220 });

	// case
	input.push_back({ { 110, 30, 10 } });
	answer.push_back({ 300 });

	// case
	input.push_back({ { -110, 10, 10 } });
	answer.push_back({ 200 });

	for (int i = 0; i < input.size(); i++)
	{
		cout << endl << "////////////////////////////////////" << endl;
		int account = 300;
		for (int j = 0; j < input[i].size(); j++)
		{
			cout << endl << "---------" << endl;
			cout << input[i][j][0] << ", " << input[i][j][1] << ", " << input[i][j][2] << endl;
			BorrowBook(account, input[i][j][0], input[i][j][1], input[i][j][2]);
			string check = (account == answer[i][j]) ? "" : "                   WRONG!";
			cout << account << " == " << answer[i][j] << check.c_str() << endl;
		}
	}
}

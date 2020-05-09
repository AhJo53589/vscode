// BorrowBook.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <vector>

using namespace std;

// 图书馆，提供图书借阅服务

// 借书需要使用借阅卡，初始金额 300
// 借书时
//     根据书的价格，和申请借阅的天数，计算出根据申请的天数需要支付的租金
//     需要先检查是否可以借书
// 还书时
//     根据实际借阅的天数，计算出实际需要支付的租金
//     扣除租金

// 检查是否可以借书的规则：
//     如果卡内余额 < 图书价格，不能借书
//     如果图书价格 < 借书时申请天数所计算的租金，不能借书
//     如果不能借这本书，还可以借其他书（跳过本条数据）

// 扣费规则：
//     图书价格 >= 100, 借阅天数 <= 15 天时，5 元/天；借阅天数 > 15 天时，超过部分 3 元/天
//     图书价格 >= 50 并且 < 100, 借阅天数 <= 15 天时，3 元/天；借阅天数 > 15 天时，超过部分 2 元/天
//     图书价格 < 50，1 元每天
//     还书时，如果实际天数超过申请天数，超期部分额外 1 元/天


// 数据格式：
//     输入：图书价格，预借天数，实际天数
//     输出：余额

class Solution
{
public:
	void borrowBook(int price, int preDay, int factDay)
	{
		if (!valid(account, price, preDay, factDay)) return;
		account -= returnBook(price, preDay, factDay);
	}

private:
	int calcPrice(int price, int day)
	{
		int idx = (price >= 100) ? 0 : (price >= 50) ? 1 : 2;
		int dayReduce = max(day - priceReduceDay, 0);
		day = min(day, priceReduceDay);
		return day * priceList[idx] + dayReduce * priceReduceList[idx];
	}

	bool valid(int account, int price, int preDay, int factDay)
	{
		if (price < 0) return false;
		if (preDay < 0) return false;
		if (factDay < 0) return false;
		if (account < price) return false;
		if (price < calcPrice(price, preDay)) return false;
		return true;
	}

	int returnBook(int price, int preDay, int factDay)
	{
		int costOver = max(factDay - preDay, 0) * priceExtra;
		return costOver + calcPrice(price, factDay);
	}

private:
	int account = 300;

	const int priceReduceDay = 15;
	const int[] priceList = { 5,3,1 };
	const int[] priceReduceList = { 3,2,1 };
	const int priceExtra = 1;
};


//int main()
//{
//	char ch;
//	int price, preDay, factDay;
//	int account = 300;
//
//	Solution sln;
//	while (cin >> price >> ch >> preDay >> ch >> factDay)
//	{
//		sln.BorrowBook(account, price, preDay, factDay);
//	}
//
//	cout << account << endl;
//}

// for test
int main()
{
	vector<vector<vector<int>>> input;
	vector<vector<int>> answer;

	//     输入：图书价格，预借天数，实际天数
	//     输出：余额

	// case
	input.push_back({ { 180, 10, 10 },{ 80, 10, 3 },{ 30, 10, 12 } });
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

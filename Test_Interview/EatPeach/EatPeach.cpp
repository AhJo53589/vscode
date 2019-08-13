// EatPeach.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <vector>

using namespace std;

/* 
孙悟空吃蟠桃

H 天兵天将离开的时间，可以吃蟠桃的时间
N 桃树的数量
K 吃蟠桃的速度，每个小时K个

孙悟空喜欢吃的慢，又要全部都吃完。
一个小时吃K个，如果这个树小于K个蟠桃，吃完之后不会去吃别的树。

输入为一行数字，前面N个是每个桃树上的桃子数量，最后一个数字是时间。
输出为K值，如果非法就输出-1。

例子

输入：
3 11 6 7 8
输出
4
*/

/*


H - N = 可以额外分次吃完的数量
N - (H - N) = 2N - H = 必须一次就吃完的数量
N[i] / K + 1 = 第i颗树，需要几次吃完

sum(N[i] / k) <= H - N



3 11 6 7 (8) = 4
27 / 8 = 3


2 2 2 2 11 (8) = 3
19 / 8 = 2

2 11 11 11 11 (8) = 11
43 / 8 = 5
*/


bool isValid(vector<int> &nums, int H)
{
	if (H < nums.size()) return false;
	for (auto n : nums) if (n <= 0) return false;
	return true;
}

int getSum(vector<int> &nums, int K)
{
	int t = 0;

	int low = 0;
	int high = nums.size();
	int i;
	while (low < high)
	{
		i = low + (high - low) / 2;
		int s = nums[i];
		if (s < K) low = i + 1;
		else high = i;
	}

	for (; i < nums.size(); i++) t += nums[i] / K;
	return t + nums.size();
}

int eat(vector<int> &nums, int H)
{
	if (!isValid(nums, H)) return -1;

	int K = 0;
	int N = nums.size();

	int low = nums[N * 2 - H];
	int high = nums[N - 1];
	while (low < high)
	{
		int i = low + (high - low) / 2;
		int s = getSum(nums, i);
		if (s > H) low = i + 1;
		else high = i;
	}
	return low;
}

//int main()
//{
//	int H;
//	int num;
//	vector<int> nums;
//	while (cin >> num)
//	{
//		nums.push_back(num);
//	}
//	H = nums.back();
//	nums.pop_back();
//	sort(nums.begin(), nums.end());
//	cout << eat(nums, H) << endl;
//}


int main()
{
	int H;
	int num;
	vector<vector<int>> TESTS;
	//while (cin >> num)
	//{
	//	nums.push_back(num);
	//}
	TESTS.push_back({ 3,11,6,7,8 });
	TESTS.push_back({ 2,2,2,2,11,8 });
	TESTS.push_back({ 2,11,11,11,11,8 });


	for (int i = 0; i < TESTS.size(); i++)
	{
		cout << endl << "///////////////////" << endl;
		H = TESTS[i].back();
		TESTS[i].pop_back();
		sort(TESTS[i].begin(), TESTS[i].end());
		cout << eat(TESTS[i], H) << endl;
	}
}

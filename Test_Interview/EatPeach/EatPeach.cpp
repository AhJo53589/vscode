// EatPeach.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <windows.h>

#include <algorithm>
#include <vector>
#include <queue>

using namespace std;

/* 
//////////////////////////////////////////////////////////////////////////
题目：
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
//////////////////////////////////////////////////////////////////////////
分析：

H - N						=> 可以额外分次吃完的数量
N - (H - N) = 2N - H		=> 必须一次就吃完的数量
res = (N[i] - 1) / K + 1		=> 第i颗树，需要几次吃完
sum(res) <= H					=> 与H的关系公式



3 11 6 7 (8) = 4
27 / 8 = 3


2 2 2 2 11 (8) = 3
19 / 8 = 2

2 11 11 11 11 (8) = 11
43 / 8 = 5
*/




int getEatTime_slow(vector<int> &nums, int K)
{
	int res = 0;
	for (int i = nums.size() - 1; i >= 0; i--)
	{
		int temp = (nums[i] - 1) / K;
		if (temp == 0) break;
		res += temp;
	}
	return res + nums.size();
}

int eat_slow(vector<int> &nums, int H)
{
	int N = nums.size();
	if (H < N) return false;

	for (int i = 1; i <= nums[N - 1]; i++)
	{
		int T = getEatTime_slow(nums, i);
		if (T <= H) return i;
	}
	return 1;
}

int getEatTime(vector<int> &nums, int K)
{
	int N = nums.size();
	int res = 0;

	auto f_lower_bound = [nums, N](int target)
	{
		int low = 0;
		int high = N;
		while (low <high)
		{
			int mid = low + (high - low) / 2;
			if (nums[mid] < target) low = mid + 1;
			else high = mid;
		}
		return low;
	};

	for (int i = 1; i <= nums[N - 1] / K; i++)
	{
		// 依次寻找 K的i倍+1 的数字的索引。这个索引后面的数字，每颗都需要多吃一次
		// 时间复杂度在 N 很大时，会降低。

		// 例：1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5,6,6,6,6 [2]
		// K = 2，i = 1，找到3，3后面每个都多吃一次
		// K = 2，i = 2，找到5，5后面每个又多吃一次
		// 结果  ==> 1,2 = 各0次，3,4 = 各1次，5,6 = 各2次

		// 最后再在返回值那里加上 N ，即为总共吃了多少次
		int target = K * i + 1;
		res += N - f_lower_bound(target);
	}

	return res + N;
}


int eat(vector<int> &nums, int H)
{
	int N = nums.size();
	if (H < N) return false;

	int min_low_index = N * 2 - H - 1;
	min_low_index = max(0, min_low_index);
	// 因为 H < N，所以不用下面的判断，也可以保证范围
	//min_low_index = min(min_low_index, N - 1);
	int low = nums[min_low_index];

	int high = nums[N - 1] + 1;

	while (low < high)
	{
		int mid = low + (high - low) / 2;
		//int t = getEatTime_slow(nums, mid);
		int t = getEatTime(nums, mid);

		// res == H 是错的
		// if (res == H) return mid;
		if (t > H) low = mid + 1;
		else high = mid;
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
	//////////////////////////////////////////////////////////////////////////
	double time = 0;
	LARGE_INTEGER nFreq;
	LARGE_INTEGER nBeginTime;
	LARGE_INTEGER nEndTime;
	QueryPerformanceFrequency(&nFreq);

	auto f_time_cout = [&]()
	{
		time = (double)(nEndTime.QuadPart - nBeginTime.QuadPart) / (double)nFreq.QuadPart;//计算程序执行时间单位为res  
		cout << "////////////////////////////////////////////////////////// time: " << time * 1000 << "ms" << endl;
	};
	//////////////////////////////////////////////////////////////////////////


	int H;
	int num;
	vector<vector<int>> TESTS;
	vector<int> ANSWER;

	//TESTS.push_back({ 3,11,6,7,8 });
	//ANSWER.push_back(4);

	//TESTS.push_back({ 2,2,2,2,11,8 });
	//ANSWER.push_back(3);

	//TESTS.push_back({ 2,11,11,11,11,8 });
	//ANSWER.push_back(11);

	//TESTS.push_back({ 1,2,3,4,5,6,7,8,8 });
	//ANSWER.push_back(8);
	//TESTS.push_back({ 1,2,3,4,5,6,7,8 });
	//ANSWER.push_back(6);
	//TESTS.push_back({ 1,2,3,4,5,6,8 });
	//ANSWER.push_back(4);
	//TESTS.push_back({ 1,2,3,4,5,8 });
	//ANSWER.push_back(3);
	//TESTS.push_back({ 1,2,3,4,8 });
	//ANSWER.push_back(2);
	//TESTS.push_back({ 1,2,3,8 });
	//ANSWER.push_back(1);
	//TESTS.push_back({ 1,2,8 });
	//ANSWER.push_back(1);
	//TESTS.push_back({ 1,8 });
	//ANSWER.push_back(1);

	vector<int> test_nums;
	vector<int> test_num_sorted(test_nums);
	int test_H = 1000;
	int test_H_Max = 10000;
	int test_step = 100;

//#define TEST_NUM_1000
#ifdef TEST_NUM_1000
	//////////////////////////////////////////////////////////////////////////
	// num = 1000
	test_nums.clear();
	test_num_sorted.clear();
	test_H = 1000;
	test_H_Max = 10000; 
	test_step = 100; 
	for (int i = 0; i < test_H; i++) test_nums.push_back(i + 1);
	test_num_sorted = test_nums;
	sort(test_num_sorted.begin(), test_num_sorted.end());
	
	for (; test_H < test_H_Max; test_H += test_step)
	{
		TESTS.push_back(test_nums);
		TESTS.back().push_back(test_H);
		ANSWER.push_back(eat_slow(test_num_sorted, test_H));
	}
#endif // TEST_NUM_1000

#define TEST_NUM_10000
#ifdef TEST_NUM_10000
	//////////////////////////////////////////////////////////////////////////
	// num = 10000
	test_nums.clear();
	test_num_sorted.clear();
	test_H = 10000;
	test_H_Max = 100000;
	test_step = 10000;
	for (int i = 0; i < test_H; i++) test_nums.push_back(i + 1);
	test_num_sorted = test_nums;
	sort(test_num_sorted.begin(), test_num_sorted.end());

	for (; test_H < test_H_Max; test_H += test_step)
	{
		TESTS.push_back(test_nums);
		TESTS.back().push_back(test_H);
		//ANSWER.push_back(eat_slow(test_num_sorted, test_H));
		ANSWER.push_back(eat(test_num_sorted, test_H));
	}
#endif



	for (int i = 0; i < TESTS.size(); i++)
	{
		cout << endl << "//////////////////////////////////////////////////////////////////////////" << endl;
		H = TESTS[i].back();
		TESTS[i].pop_back();
		
		cout << "nums" << "[" << TESTS[i].size() << "] = ";
		for (int j = 0; j < TESTS[i].size(); j++)
		{
			if (j > 15)
			{
				cout << "...";
				break;
			}
			cout << TESTS[i][j] << ",";
		}
		cout << "(" << H << ")" << endl;

		//////////////////////////////////////////////////////////////////////////
		QueryPerformanceCounter(&nBeginTime);						//开始计时  

		sort(TESTS[i].begin(), TESTS[i].end());

		int res = 0;
		if (TESTS[i][0] <= 0) res = -1;
		else res = eat(TESTS[i], H);
		//else res = eat_slow(TESTS[i], H);

		string check = (res == ANSWER[i]) ? "" : "\t\t\t WRONG!";
		cout << "Result = " << res << "\t <== " << ANSWER[i] << check.c_str() << endl;

		QueryPerformanceCounter(&nEndTime);							//停止计时  
		//////////////////////////////////////////////////////////////////////////
		f_time_cout();
	}
}

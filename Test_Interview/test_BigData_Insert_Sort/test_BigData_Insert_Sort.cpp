// test_BigData_Insert_Sort.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <windows.h>

#include <algorithm>
#include <vector>
#include <queue>
#include <set>

using namespace std;


int main()
{
	double time = 0;
	LARGE_INTEGER nFreq;
	LARGE_INTEGER nBeginTime;
	LARGE_INTEGER nEndTime;
	QueryPerformanceFrequency(&nFreq);

	vector<vector<int>> TESTS;

	//#define TEST_NUM_1000
#ifdef TEST_NUM_1000
	{
		//////////////////////////////////////////////////////////////////////////
		// num = 1000
		vector<int> test_nums;
		for (int i = 0; i < 1000; i++) test_nums.push_back(i + 1);
		vector<int> test_num_sorted(test_nums);
		sort(test_num_sorted.begin(), test_num_sorted.end());
		TESTS.push_back(test_nums);
	}
#endif // TEST_NUM_1000

#define TEST_NUM_10000
#ifdef TEST_NUM_10000
	{
		//////////////////////////////////////////////////////////////////////////
		// num = 10000
		vector<int> test_nums;
		for (int i = 0; i < 10000; i++) test_nums.push_back(i + 1);
		vector<int> test_num_sorted(test_nums);
		sort(test_num_sorted.begin(), test_num_sorted.end());
		TESTS.push_back(test_nums);
	}
#endif


	//////////////////////////////////////////////////////////////////////////
	// Test start
	//////////////////////////////////////////////////////////////////////////
	auto f_time_cout = [&]()
	{
		time = (double)(nEndTime.QuadPart - nBeginTime.QuadPart) / (double)nFreq.QuadPart;//计算程序执行时间单位为t  
		cout << "////////////////////////////////////////////////////////// time: " << time * 1000 << "ms" << endl;
	};

	for (int i = 0; i < TESTS.size(); i++)
	{
		cout << endl << "//////////////////////////////////////////////////////////////////////////" << endl;

		{
			cout << "vector<int> sort" << endl;
			QueryPerformanceCounter(&nBeginTime);//开始计时  

			vector<int> test;
			for (auto &n : TESTS[i]) test.push_back(n);
			sort(test.begin(), test.end());

			QueryPerformanceCounter(&nEndTime);//停止计时  
			f_time_cout();
		}

		{
			cout << "set<int> sort" << endl;
			QueryPerformanceCounter(&nBeginTime);//开始计时  

			set<int> s;
			for (auto &n : TESTS[i]) s.insert(n);
			//for (auto &n : s) test.push_back(n);

			QueryPerformanceCounter(&nEndTime);//停止计时  
			f_time_cout();
		}

		{
			cout << "priority_queue<int> sort" << endl;
			QueryPerformanceCounter(&nBeginTime);//开始计时  

			priority_queue<int, vector<int>> pq;
			for (auto &n : TESTS[i]) pq.push(n);
			//while (!pq.empty())
			//{
			//	test.push_back(pq.top());
			//	pq.pop();
			//}

			QueryPerformanceCounter(&nEndTime);//停止计时  
			f_time_cout();
		}
	}
}

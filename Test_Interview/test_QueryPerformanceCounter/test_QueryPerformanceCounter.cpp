// test_QueryPerformanceCounter.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>


#include <windows.h>
using namespace std;


char a[1024][1024];
char b[1024][1024];

int main()
{
	double time = 0;
	LARGE_INTEGER nFreq;
	LARGE_INTEGER nBeginTime;
	LARGE_INTEGER nEndTime;
	QueryPerformanceFrequency(&nFreq);

	auto f_time_cout = [&]()
	{
		time = (double)(nEndTime.QuadPart - nBeginTime.QuadPart) / (double)nFreq.QuadPart;//计算程序执行时间单位为t  
		cout << "////////////////////////////////////////////////////////// time: " << time * 1000 << "ms" << endl;
	};

	//////////////////////////////////////////////////////////////////////////
	QueryPerformanceCounter(&nBeginTime);//开始计时  



	//////////////////////////////////////////////////////////////////////////
	QueryPerformanceCounter(&nEndTime);//停止计时  

	//计算程序执行时间单位为t  
	f_time_cout();

}

// Test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <map>

using namespace std;

class C_1
{
	void fun() {}
};

class C_2
{
	virtual void fun() = 0;
};

#pragma pack(2)
class C_3
{
	char name[5];
	int score;
	short grade;
};
#pragma pack()

#pragma pack(4)
class C_4
{
	char name[5];
	int score;
	short grade;
};
#pragma pack()

void fun(char szParam[64][100])
{
	std::cout << sizeof(szParam) << std::endl;
}

const char *getName()
{
// 	static char p[] = "php is the best language";
// 	return p;
	return "php is the best language";
}

const char *getName2()
{
// 	static char p[] = "AAAAAAAAAAAAAA";
// 	return p;
	return "AAAAAAAAAAAAAA";
}

#define VALUE(x) x+x

int caishuzigame(int iVal)
{
	int iTarget = 76;
	
	cout << iVal << " ";
	if (iVal < iTarget)
	{
		cout << "小了" << endl;
		return -1;
	}
	else if (iVal > iTarget)
	{
		cout << "大了" << endl;
		return 1;
	}
	return 0;
}

void guessNum()
{
	int iLow = 0;
	int iHigh = 100;

	while (iLow <= iHigh)
	{
		int iMid = iLow + (iHigh - iLow) / 2;
		int iGuessResult = caishuzigame(iMid);

		if (iGuessResult == 0) return;
		if (iGuessResult == -1) iLow = iMid + 1;
		if (iGuessResult == 1) iHigh = iMid;
	}
}

int main()
{
//	const char* a = getName();
//	const char* b = getName2();
//	std::string _name = a;
//// 	std::string _name = getName();
//
//	std::cout << _name.c_str();

	std::cout << sizeof(C_1) << " ";
	std::cout << sizeof(C_2) << " ";
	std::cout << sizeof(C_3) << " ";
	std::cout << sizeof(C_4) << " " << std::endl;
	char szParam[64][100] = { 'A' };
	fun(szParam);
	int res = 5 * VALUE(5);
	std::cout << res << std::endl;


	std::map<int, std::string> mymap;
	mymap[1] = "A";
	mymap[2] = "B";
	mymap[3] = "A";
	mymap[4] = "B";
	mymap[5] = "A";
	mymap[6] = "B";

	auto it = mymap.begin();
	while (it != mymap.end())
	{
		if (it->first % 2 == 0)
		{
			it = mymap.erase(it);
		}
		else
		{
			it++;
		}
	}
	for (auto m : mymap)
	{
		cout << m.first << " " << m.second.c_str() << endl;
	}

}

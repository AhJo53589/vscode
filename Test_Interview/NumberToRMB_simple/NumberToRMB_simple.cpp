// NumberToRMB_simple.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <windows.h>
#include <iostream>
#include <string>
#include <vector>

using namespace std;

const TCHAR* cChWord_Num = TEXT("零壹贰叁肆伍陆柒捌玖");
const TCHAR* cChWord_Digit = TEXT("拾佰仟");
const TCHAR* cChWord_BigDigit = TEXT("圆万亿");

wstring FourNumberToRMB(string sNum, bool bHighZeroFlag)
{
	wstring ret;
	bool bNumFlag = false;
	bool bZeroFlag = false;
	for (size_t i = 0; i < sNum.size(); i++)
	{
		if (sNum[i] == '0')
		{
			if (bNumFlag)
			{
				bNumFlag = false;
				bZeroFlag = true;
			}
		}
		else
		{
			if (bHighZeroFlag)
			{
				if (i != 0)
				{
					ret += cChWord_Num[0];
				}
				bHighZeroFlag = false;
			}
			if (bZeroFlag)
			{
				ret += cChWord_Num[0];
				bZeroFlag = false;
			}

			bNumFlag = true;
			int _index = sNum[i] - '0';
			ret += cChWord_Num[_index];
			int _iDigit = sNum.size() - i - 2;
			if (_iDigit >= 0 && _iDigit < 3)
			{
				ret += cChWord_Digit[_iDigit];
			}
		}
	}
	return ret;
}

// (int)12345000 ==> (string)壹仟贰佰叁拾肆万伍仟圆
wstring NumberToRMB(int iNumber)
{
	wstring ret;
	if (iNumber == 0)
	{
		ret = cChWord_Num[0] + cChWord_Digit[0];
		return ret;
	}
	int i = 0;
	int j = 0;
	string str;
	while (iNumber > 0)
	{
		int t = iNumber % 10;
		str = to_string(t) + str;
		iNumber /= 10;
		j++;
		if (j == 4)
		{
			wstring temp = FourNumberToRMB(str, iNumber != 0);
			temp += cChWord_BigDigit[i];
			ret = temp + ret;

			str.clear();
			j = j % 4;
			i++;
		}
	}
	wstring temp = FourNumberToRMB(str, iNumber != 0);
	temp += cChWord_BigDigit[i];
	ret = temp + ret;

	return ret;
}


// (int)123450006000 ==> (string)1234-5000-6000
wstring FormatNumber(unsigned __int64 iNumber)
{
	wstring strNumber;
	if (iNumber == 0) return strNumber;

	int iDigit = 0;
	bool bHighZeroFlag = false;

	do
	{
		if (iDigit % 4 == 0 && iDigit != 0)
		{
			strNumber.insert(strNumber.begin(), TEXT('-'));
		}

		int n = iNumber % 10;
		strNumber.insert(0, to_wstring(n));

		iDigit += 1;
		iNumber /= 10;
	} while (iNumber > 0);

	return strNumber;
}


int main()
{
	setlocale(LC_ALL, "chs");

	int iNum = INT_MAX;
	wprintf(TEXT("%s\n"), FormatNumber(iNum).c_str());
	wprintf(TEXT("%s\n"), NumberToRMB(iNum).c_str());

	iNum = 10010;
	wprintf(TEXT("%s\n"), FormatNumber(iNum).c_str());
	wprintf(TEXT("%s\n"), NumberToRMB(iNum).c_str());

	iNum = 101010;
	wprintf(TEXT("%s\n"), FormatNumber(iNum).c_str());
	wprintf(TEXT("%s\n"), NumberToRMB(iNum).c_str());
}


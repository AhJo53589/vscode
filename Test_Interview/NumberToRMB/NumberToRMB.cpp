// FourNumberToRMB.cpp : 此文件包含 'main' 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <windows.h>
#include <iostream>
#include <string>
#include <vector>

using namespace std;

//////////////////////////////////////////////////////////////////////////
// code by zichen.yang
//const TCHAR* cChineseWord = TEXT("零壹贰叁肆伍陆柒捌玖");
//const TCHAR* cChineseDigit = TEXT("圆拾佰仟万亿");
//
//wstring ConvertNumber(int lNumber)
//{
//	wstring strNumber;
//	int i = 0;
//	int digitS = 4;
//	bool flag = false;
//	do
//	{
//		int n = lNumber % 10;
//		if (i > 3)
//		{
//			if (strNumber[0] == cChineseDigit[4] && digitS == 5)
//			{
//				strNumber[0] = cChineseDigit[digitS];
//			}
//			else
//			{
//				strNumber.insert(strNumber.begin(), cChineseDigit[digitS]);
//			}
//			flag = false;
//			digitS += 1; 
//			i = 0;
//		}
//		else
//		{
//			if (n != 0 || i == 0)
//			{
//				strNumber.insert(strNumber.begin(), cChineseDigit[i]);
//				if (i != 0)
//				{
//					flag = true;
//				}
//			}
//		}
//
//		if (n!= 0 || (n == 0 && strNumber[0] != cChineseWord[0] && flag))
//		{
//			strNumber.insert(strNumber.begin(), cChineseWord[n]);
//		}
//
//		i += 1;
//		lNumber /= 10;
//	} while (lNumber > 0);
//
//	return strNumber;
//}
//
//int main()
//{
//	setlocale(LC_ALL, "chs");
//
//	wprintf(TEXT("%s\n"), ConvertNumber(1205008000).c_str());
//	wprintf(TEXT("%s\n"), ConvertNumber(1200080300).c_str());
//
//	system("pause");
//}


// https://wenku.baidu.com/view/7318dd17a21614791711282e.html
// https://www.iamwawa.cn/renminbi.html

// 阿拉伯数字与中文大写数字对应表
// 1 	2 	3 	4 	5 	6 	7 	8 	9 	0 	十 	百 	千
// 壹 	贰 	叁 	肆 	伍 	陆 	柒 	捌 	玖 	零 	拾 	佰 	仟
//
// 阿拉伯数字小写金额数字中有"0"时，中文大写应按照汉语语言规律、金额数字构成和防止涂改的要求进行书写。举例如下：
//
// 1·阿拉伯数字中间有"0"时，中文大写要写"零"字，如￥1409.50，应写成人民币陆壹仟肆佰零玖元伍角。
//
// 2·阿拉伯数字中间连续有几个"0"时，中文大写金额中间可以只写一个"零"字，如￥6007.14，应写成人民币陆仟零柒元壹角肆分。
//
// 3·阿拉伯金额数字万位和元位是"0"，或者数字中间连续有几个"0"，万位、元位也是"0"，
//    但千位、角位不是"0"时，中文大写金额中可以只写一个零字，也可以不写"零"字。如￥1680.32，应写成人民币壹仟陆佰捌拾元零叁角贰分，
//    或者写成人民币壹仟陆佰捌拾元叁角贰分，又如￥107000.53，应写成人民币壹拾万柒仟元零伍角叁分，或者写成人民币壹拾万零柒仟元伍角叁分。
//
// 4·阿拉伯金额数字角位是"0"，而分位不是"0"时，中文大写金额"元"后面应写"零"字。
//    如￥16409.02，应写成人民币壹万陆仟肆佰零玖元零贰分；又如￥325.04，应写成人民币叁佰贰拾伍元零肆分。


const TCHAR* cChineseWord = TEXT("零壹贰叁肆伍陆柒捌玖");
const TCHAR* cChineseDigitWord = TEXT("圆拾佰仟万亿");

wstring GetDigitWord(int iDigit)
{
	wstring strDititWord;
	
	if (iDigit == 0)
	{
		strDititWord.push_back(cChineseDigitWord[0]);	// 圆
		return strDititWord;
	}

	if (iDigit % 4 == 0)
	{
		int i = iDigit / 4;
		if (i % 2 == 1)
		{
			strDititWord.push_back(cChineseDigitWord[4]);	// 万
			i -= 1;
		}
		while (i > 0)
		{
			strDititWord.push_back(cChineseDigitWord[5]);	// 亿
			i -= 2;
		}
	}
	else
	{
		int i = iDigit % 4;
		strDititWord.push_back(cChineseDigitWord[i]);	// 拾佰仟
	}

	return strDititWord;
}

wstring FourNumberToRMB(unsigned int iNumber, int iDigit, bool bHighZeroFlag)
{
	wstring strNumber;
	bool bLowZeroFlag = false;

	if (iNumber > 9999 || iNumber == 0) return strNumber;

	do 
	{
		int n = iNumber % 10;
		if (n == 0 && bLowZeroFlag)	// 如果这一位是0，低位有不是0数字，循环未结束所以高位肯定还有数字，补零
		{
			strNumber.insert(strNumber.begin(), cChineseWord[0]);	// 补零
			bLowZeroFlag = false;
		}

		if (n == 0 && iDigit %4 == 0)
		{
			strNumber.insert(0, GetDigitWord(iDigit));	// 数位，个位为0时也要写圆/万/亿
		}

		if (n != 0)
		{
			strNumber.insert(0, GetDigitWord(iDigit));	// 数位，圆/万/亿
			strNumber.insert(strNumber.begin(), cChineseWord[n]);	// 数值
			bLowZeroFlag = true;
		}

		iDigit += 1;
		iNumber /= 10;
	} while (iNumber > 0);

	if (iDigit % 4 != 0 && bLowZeroFlag && bHighZeroFlag)	// 如果没有到第4位就结束循环，相当于数字是0，低位有不是0数字，高位还有数字，补零
	{
		strNumber.insert(strNumber.begin(), cChineseWord[0]);	// 补零
	}

	return strNumber;
}

// (int)12345000 ==> (string)壹仟贰佰叁拾肆万伍仟圆
wstring NumberToRMB(unsigned __int64 iNumber)
{
	wstring strNumber;
	if (iNumber == 0) return strNumber;

	int iDigit = 0;
	bool bHighZeroFlag = false;

	do 
	{
		int n = iNumber % 10000;

		bool bHighZeroFlag = ((iNumber / 10000) > 0);
		strNumber.insert(0, FourNumberToRMB(n, iDigit, bHighZeroFlag));

		iDigit += 4;
		iNumber /= 10000;
	} while (iNumber > 0);

	return strNumber;
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

	unsigned __int64 lNumber = 10000000110010101001;
	wprintf(TEXT("%s\n"), FormatNumber(lNumber).c_str());
	wprintf(TEXT("%s\n"), NumberToRMB(lNumber).c_str());

	system("pause");
}


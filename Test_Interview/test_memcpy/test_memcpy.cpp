// test_memcpy.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
using namespace std;

void * memcpy(void *pDst, void *pSrc, int nLength)
{
	if (pDst == nullptr || pSrc == nullptr) return pDst;
	if (pDst == pSrc) return pDst;
	if ((char *)pSrc < (char *)pDst && (char *)pSrc + nLength >= (char *)pDst)
	{
		for (int i = nLength - 1; i >= 0; i--)
		{
			*((char *)pDst + i) = *((char *)pSrc + i);
		}
	}
	else
	{
		for (int i = 0; i < nLength; i++)
		{
			*((char *)pDst + i) = *((char *)pSrc + i);
		}
	}
	return pDst;
}

//void * memcpy(void *pDst, void *pSrc, int nLength)
//{
//	if (pDst == nullptr || pSrc == nullptr) return pDst;
//	if (pDst == pSrc) return pDst;
//
//	if ((char *)pSrc < (char *)pDst && (char *)pSrc + nLength >= (char *)pDst)
//	{
//		int i = nLength;
//		while (i-- > 0)
//		{
//			*((char *)pDst + i) = *((char *)pSrc + i);
//		}
//	}
//	else
//	{
//		int i = 0;
//		while (i < nLength)
//		{
//			*((char *)pDst + i) = *((char *)pSrc + i);
//			i++;
//		}
//	}
//	return pDst;
//}

int main()
{
	const int LEN = 10;
	char test[LEN];
	for (int i = 0; i < LEN; i++)
	{
		test[i] = (i % 10) + '0';
	}
	for (int i = 0; i < LEN; i++) cout << test[i];
	cout << "\t <-- Origin" << endl;

	memcpy(&test[1], &test[3], 5);
	cout << "0345676789" << "\t <-- Answer" << endl;

	//memcpy(&test[3], &test[1], 9);
	//cout << "0121234567" << "\t <-- Answer (and crash)" << endl;
	
	for (int i = 0; i < LEN; i++) cout << test[i];
	cout << endl;
}

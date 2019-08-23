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
		int i = nLength;
		while (i-- > 0)
		{
			*((char *)pDst + i) = *((char *)pSrc + i);
		}
	}
	else
	{
		int i = 0;
		while (i < nLength)
		{
			*((char *)pDst + i) = *((char *)pSrc + i);
			i++;
		}
	}
	return pDst;
}

int main()
{
	const int LEN = 10;
	char test[LEN];
	for (int i = 0; i < LEN; i++)
	{
		test[i] = (i % 10) + '0';
	}
	for (int i = 0; i < LEN; i++) cout << test[i];
	cout << endl;

	//memcpy(&test[1], &test[3], 5);
	memcpy(&test[3], &test[1], 9);

	
	for (int i = 0; i < LEN; i++) cout << test[i];
	cout << endl;
}

// test_ListNode.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <vector>
#include <queue>
#include <string>

#include "ListNode.h"
using namespace std;


ListNode *reverseList(ListNode *pHead)
{
	ListNode *pNode = pHead;
	ListNode *pPrev = nullptr;

	while (pNode != nullptr)
	{
		ListNode *pNext = pNode->next;
		pNode->next = pPrev;
		pPrev = pNode;
		pNode = pNext;
	}
	return pPrev;
}

int main()
{
	vector<ListNode *> TESTS;
	vector<bool> ANSWER;

	TESTS.push_back(StringToListNode("[1,2,3,4,5,6,7,8,9]"));
	ANSWER.push_back(true);


	for (int i = 0; i < TESTS.size(); i++)
	{
		cout << endl << "///////////////////////////////////////" << endl;
		cout << TESTS[i] << endl;

		reverseList(TESTS[i]);
	}
}
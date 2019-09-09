// test_ListNode.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <vector>
#include <queue>
#include <string>

#include "ListNode.h"
using namespace std;


int main()
{
	vector<ListNode *> N;
	vector<bool> A;

	N.push_back(StringToListNode("3,9,20,null,null,15,7"));
	A.push_back(true);


	for (int i = 0; i < N.size(); i++)
	{
		cout << endl << "///////////////////////////////////////" << endl;
		cout << N[i] << endl;

		preorder(N[i]);
	}
}
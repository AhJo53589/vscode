// MaxSum_NoBothParentAndChild.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//


// 从一个组织里面选择任意人数的人员参加一个活动，
// 要求参加的人员当中不能有直接的上下级关系，
// 选择每个人都会得到一个非负分数，要求选取的人员分数总和最大。
// 
// 例如A是BCD的直接上司，B是EF的直接上司，ABCDEF 对应的分数分别为150, 30, 20, 40, 100, 10
// 则最优解为AEF

#include "pch.h"
#include <iostream>

#include "..\Common\MultiTreeNode.h"

int CalcAllChildValidSum(MultiTreeNode *pNode);
int CalcValidNum(MultiTreeNode *pNode);

int FindLargeNode(MultiTreeNode *pNode)
{
	if (pNode == NULL) return 0;

	int sum = 0;
	if (pNode->val > CalcAllChildValidSum(pNode))
	{
		sum += pNode->val;
		cout << pNode->val << " ";
		for (MultiTreeNode *p : pNode->child)
		{
			if (p == NULL) continue;
			for (MultiTreeNode *q : p->child)
			{
				sum += FindLargeNode(q);
			}
		}
	}
	else
	{
		for (MultiTreeNode *p : pNode->child)
		{
			sum += FindLargeNode(p);
		}
	}
	return sum;
}

int CalcAllChildValidSum(MultiTreeNode *pNode)
{
	if (pNode == NULL) return 0;

	int sum = 0;
	for (MultiTreeNode *p : pNode->child)
	{
		sum += CalcValidNum(p);
	}
	return sum;
}

int CalcValidNum(MultiTreeNode *pNode)
{
	if (pNode == NULL) return 0;

	return (pNode->val > CalcAllChildValidSum(pNode)) ? pNode->val : 0;
}

int main()
{
	vector<string> strInput;
	strInput.push_back("1,null,2,3,4,null,5,6,7,null,8,9,null,10,null");
	strInput.push_back("150,null,30,20,40,null,100,10");
	strInput.push_back("50,null,30,20,40,null,100,10,null,50,10");
	strInput.push_back("50,null,30,20,40,null,100,10,null,50,10,null,null,null,null,50,10");
	strInput.push_back("50,null,30,20,40,null,100,10,null,50,10,null,50,null,null,null,50,10");

	for (string s : strInput)
	{
		cout << "////////////////////////////////////" << endl;
		cout << "Input: " << endl;
		MultiTreeNode *root = NULL;
		initMultiTree(&root, s);
		printMultiTreeNode(root, 0);

		cout << "Find best: ";
		int sum = FindLargeNode(root);
		cout << endl << "Sum : " << sum << endl << endl;
	}
}


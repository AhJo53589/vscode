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
#include <unordered_map>

#include "..\Common\MultiTreeNode.h"

using namespace std;

void PostOrder(MultiTreeNode *root, vector<MultiTreeNode *>&pNodeList)
{
	if (root == NULL) return;
	for (auto p : root->child) PostOrder(p, pNodeList);
	pNodeList.push_back(root);
}

unordered_map<MultiTreeNode *, int> robMemo[2];
int rob(MultiTreeNode *pNode)
{
	if (pNode == NULL) return 0;
	vector<MultiTreeNode *> pNodeList;
	PostOrder(pNode, pNodeList);

	for (auto p : pNodeList)
	{
		int s[2] = { 0,0 };	// 0 == include node val, 1 == not include node val

		s[0] = p->val;
		for (auto c : p->child) s[0] += robMemo[1][c];
		for (auto c : p->child) s[1] += robMemo[0][c];
		s[0] = max(s[0], s[1]);	// copy best val

		robMemo[0][p] = s[0];	// record
		robMemo[1][p] = s[1];
	}
	return robMemo[0][pNode];
}

int main()
{
	vector<string> strInput;
	vector<int> iAnswer;
	strInput.push_back("1,null,2,3,4,null,5,6,7,null,8,9,null,10,null");
	iAnswer.push_back(46);

	strInput.push_back("150,null,30,20,40,null,100,10");
	iAnswer.push_back(260);

	strInput.push_back("50,null,30,20,40,null,100,10,null,50,10");
	iAnswer.push_back(220);

	strInput.push_back("50,null,30,20,40,null,100,10,null,50,10,null,null,null,null,50,10");
	iAnswer.push_back(230);

	strInput.push_back("50,null,30,20,40,null,100,10,null,50,10,null,50,null,null,null,50,10");
	iAnswer.push_back(280);


	for (int i = 0; i < strInput.size(); i++)
	{
		cout << "////////////////////////////////////" << endl;
		cout << "Input: " << endl;
		MultiTreeNode *root = NULL;
		initMultiTree(&root, strInput[i]);
		printMultiTreeNode(root, 0);

		cout << endl << "Answer = " << iAnswer[i];
		cout << endl << "Result = " << rob(root) << endl  << endl;
	}
}


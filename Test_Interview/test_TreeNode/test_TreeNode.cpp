// test_TreeNode.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <vector>
#include <queue>
#include <string>

#include "TreeNode.h"
using namespace std;


void preorder(TreeNode *pNode)
{
	if (pNode == nullptr) return;
	cout << pNode->val << ",";
	preorder(pNode->left);
	preorder(pNode->right);
}

int main()
{
	vector<TreeNode *> N;
	vector<bool> A;

	N.push_back(StringToTreeNode("3,9,20,null,null,15,7"));
	A.push_back(true);


	for (int i = 0; i < N.size(); i++)
	{
		cout << endl << "///////////////////////////////////////" << endl;
		cout << N[i] << endl;

		preorder(N[i]);
	}
}


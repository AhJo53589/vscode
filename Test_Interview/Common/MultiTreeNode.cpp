// MultiTreeNode.cpp 
//

//#include "pch.h"

#include <iostream>

#include <algorithm>
#include <map>
#include <vector>
#include <queue>
#include <string>

#include "MultiTreeNode.h"
using namespace std;


std::vector<std::string> split(std::string str, std::string pattern)
{
	std::string::size_type pos;
	std::vector<std::string> result;
	str += pattern;	// 扩展字符串以方便操作
	size_t size = str.size();
	for (size_t i = 0; i < size; i++)
	{
		pos = str.find(pattern, i);
		if (pos < size)
		{
			std::string s = str.substr(i, pos - i);
			result.push_back(s);
			i = pos + pattern.size() - 1;
		}
	}
	return result;
}

vector<TreeNode_Val> initTreeNode_Val(string strInitData)
{
	vector<string> strArray = split(strInitData, ",");

	vector<TreeNode_Val> vecTreeNode_Val;
	for (auto str : strArray)
	{
		if (str == "null")
		{
			vecTreeNode_Val.push_back(TreeNode_Val(true));
		}
		else
		{
			vecTreeNode_Val.push_back(TreeNode_Val(stoi(str.c_str())));
		}
	}
	return vecTreeNode_Val;
}

void initMultiTree(MultiTreeNode **root, string strInitData)
{
	vector<TreeNode_Val> initData = initTreeNode_Val(strInitData);
	if (initData[0].isNull) return;

	queue<MultiTreeNode *> qTree;
	*root = new MultiTreeNode(initData[0].val);
	qTree.push(*root);

	int i = 2;
	while (!qTree.empty())
	{
		MultiTreeNode *qHead = qTree.front();
		qTree.pop();
		if (i == initData.size()) return;

		while (i < initData.size() && !initData[i].isNull)
		{
			MultiTreeNode *pNode = new MultiTreeNode(initData[i].val);
			(*qHead).child.push_back(pNode);
			qTree.push(pNode);
			i++;
		}
		i++;
	}
}

void printMultiTreeNode(MultiTreeNode *root, int iTabMask)
{
	if (root == NULL) return;

	cout << root->val << "\t";
	iTabMask += 1;

	for (int i = 0; i < root->child.size(); i++)
	{
		printMultiTreeNode(root->child[i], iTabMask);
		if (i != root->child.size() - 1)
		{
			cout << endl;
			for (int j = 0; j < iTabMask; j++)
			{
				cout << "\t";
			}
		}
	}

	if (iTabMask == 1)
	{
		cout << endl;
	}
}

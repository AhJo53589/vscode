#ifndef TREE_NODE_H
#define TREE_NODE_H

#include <iostream>

#include <algorithm>
#include <map>
#include <vector>
#include <queue>
#include <string>

using namespace std;


struct MultiTreeNode
{
	int val;
	MultiTreeNode *parnet;
	vector<MultiTreeNode *> child;
	MultiTreeNode(int x) : val(x) {}
};

struct TreeNode_Val
{
	int val;
	bool isNull;
	TreeNode_Val(int x) : val(x), isNull(false) {}
	TreeNode_Val(bool isN) : val(0), isNull(isN) {}
};

void initMultiTree(MultiTreeNode **root, string strInitData);
void printMultiTreeNode(MultiTreeNode *root, int iTabMask);



#endif //TREE_NODE_H

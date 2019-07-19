## 题目
从一个组织里面选择任意人数的人员参加一个活动，  
要求参加的人员当中不能有直接的上下级关系，  
选择每个人都会得到一个非负分数，要求选取的人员分数总和最大。  

例如A是BCD的直接上司，B是EF的直接上司，
ABCDEF 对应的分数分别为150, 30, 20, 40, 100, 10。  
则最优解为AEF  

> Description.jpg  
![](https://raw.githubusercontent.com/AhJo53589/vscode/master/Test_Interview/MaxSum_NoBothParentAndChild/Description.jpg)

## 思路
思路解法可以参考 [337.house-robber-iii 打家劫舍 III](https://github.com/AhJo53589/leetcode-cn/blob/master/problems/337.house-robber-iii/README.md)  

简单来说，就是先通过后序遍历，使结点可以从下向上处理。  
运用动态规划的思想，逐步将最优解积累和传递到最上方。  


## 答题
```C++
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
```
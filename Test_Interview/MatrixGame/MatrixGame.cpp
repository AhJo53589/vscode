// MatrixGame.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

/*
Description

小Q是一个非常聪明的孩子，除了国际象棋，他还很喜欢玩一个电脑益智游戏——矩阵游戏。
矩阵游戏在一个N*N黑白方阵进行（如同国际象棋一般，只是颜色是随意的）。

每次可以对该矩阵进行两种操作：
行交换操作：选择矩阵的任意两行，交换这两行（即交换对应格子的颜色）
列交换操作：选择矩阵的任意行列，交换这两列（即交换对应格子的颜色）

游戏的目标，即通过若干次操作，使得方阵的主对角线(左上角到右下角的连线)上的格子均为黑色。
对于某些关卡，小Q百思不得其解，以致他开始怀疑这些关卡是不是根本就是无解的！！
于是小Q决定写一个程序来判断这些关卡是否有解。


Input

第一行包含一个整数T，表示数据的组数。接下来包含T组数据，每组数据第一行为一个整数N，表示方阵的大小；接下来N行为一个N*N的01矩阵（0表示白色，1表示黑色）。


Output

输出文件应包含T行。对于每一组数据，如果该关卡有解，输出一行Yes；否则输出一行No。
Sample Input
2
2
0 0
0 1
3
0 0 1
0 1 0
1 0 0
Sample Output
No
Yes

「数据规模」
对于20%的数据，N ≤ 7
对于50%的数据，N ≤ 50
对于100%的数据，N ≤ 200

*/

#include "pch.h"
#include <iostream>
#include <vector>
using namespace std;

int mp[201][201], y[201];
int lk[201];
int n;

bool find(int x)
{
	for (int i = 0; i < n; i++)
	{
		if (!y[i] && mp[x][i])
		{
			y[i] = 1;
			if (!lk[i] || find(lk[i]))
			{
				lk[i] = x;
				return 1;
			}
		}
	}
	return 0;
}

bool work()
{
	for (int i = 0; i < n; i++)
	{
		memset(y, 0, sizeof(y));
		if (!find(i))return 0;
	}
	return 1;
}

int main()
{
	vector<vector<vector<int>>> mapdata;
	mapdata.push_back({ { 0,0 }, { 0,1 } });
	mapdata.push_back({ { 0,0,1 }, { 0,1,0 }, { 1,0,0 } });

	int id = 0;
	for (auto & m : mapdata)
	{
		id++;
		memset(lk, 0, sizeof(lk));
		memset(mp, 0, sizeof(mp));

		n = m.size();
		for (size_t i = 0; i < n; i++)
		{
			for (size_t j = 0; j < n; j++)
			{
				mp[i][j] = m[i][j];
			}
		}

		cout << "id:" << id << "\t";
		if (work())
		{
			cout << "YES" << endl;
		}
		else
		{
			cout << "NO" << endl;
		}
	}
}



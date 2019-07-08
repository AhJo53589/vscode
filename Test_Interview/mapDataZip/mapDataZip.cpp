// mapDataZip.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

// 在某个二维游戏中，将地图划分为二维的方格，人物的坐标是(x, y)的形式(x, y为整数)。
// 人物每次移动只能移动到其周围八个格子。移动路径的一种表示方式是记录其每次移动后的坐标。
// 为了压缩这个移动路径，我们需要将移动路径表示成 初始坐标 + 一系列移动方向的形式。
// __由于每次移动只有8个可能的方向，可以用3个bit表示，两次移动就需要6bit，
// 这样可以将这6个bit放进一个字节8bit内（浪费的2个bit不用管）。__
// 
// 示例：
// 
// 假设x轴正方向为→，y轴正方向为↓
// 原始表示方法：(395, 2098) = > (395, 2099) = > (396, 2099) = > (395, 2098)
// 初始坐标 + 偏移表示方法：(395, 2098) ↓ → ↖
// 
// 题目要求：
// 你需要利用上述思路，实现压缩算法与对应的解压缩算法。
// __注意一个字节内要压缩进去两步（即第一段标注部分）
// 初始坐标允许不压缩直接放进去（初始坐标是两个int）__
// 
// void zip(const vector<pair<int, int>>& inBuffer, vector<uint8_t>& outBuffer);
// 压缩算法，输入为一系列的坐标点，已经确认是符合规范的移动路径（每次都是移动到相邻8个位置，且没有原地不动的情况），函数内不需要再验证。
// 输出为压缩后的数据，里面存放什么，如何存放，都由你自己定义。
// __提示：可以在数据的前面保存有几次偏移，或者在数据的结尾以特殊标记表示结束。__
// 
// void unzip(const vector<uint8_t>& inBuffer, vector<pair<int, int>>& outBuffer);
// 解压缩，输入为压缩算法产生的数据，输出为原始移动路径。



#include "pch.h"
#include <iostream>

#include <vector>
#include <bitset>

using namespace std;

const int g_iEndMark = 15;

void initDirectionKey(vector<pair<int, int>> &key)
{
	for (int i = 0; i < 3; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			if (i == 1 && j == 1) continue;
			key.push_back(make_pair(i - 1, j - 1));
		}
	}
}

int getDirection(const pair<int, int> &p1, const pair<int, int> &p2, const vector<pair<int, int>> &key)
{
	// 0 3 5
	// 1   6
	// 2 4 7
	int x = p2.first - p1.first;
	int y = p2.second - p1.second;
	for (int i = 0; i < 8; i++)
	{
		if (key[i].first == x && key[i].second == y)
		{
			return i;
		}
	}
	return -1;
}

pair<int, int> getNextPos(const pair<int, int> &p1, const int iDirection, const vector<pair<int, int>> &key)
{
	return make_pair(p1.first + key[iDirection].first, p1.second + key[iDirection].second);
}

void zip(const vector<pair<int, int>>& inBuffer, vector<uint8_t>& outBuffer)
{
	//uint8_t c;
	//c = (0b00001111 & a1) << 4;
	//c |= 0b00001111 & a2;

	if (inBuffer.empty()) return;

	// init key
	vector<pair<int, int>> key;
	initDirectionKey(key);

	// posStart to uint8_t
	pair<int, int> posStart = inBuffer[0];
	uint8_t temp[4];
	memcpy(temp, &posStart.first, sizeof(int));
	for (int i = 0; i < 4; i++)
	{
		outBuffer.push_back(temp[i]);
	}
	memcpy(temp, &posStart.second, sizeof(int));
	for (int i = 0; i < 4; i++)
	{
		outBuffer.push_back(temp[i]);
	}

	// pos to uint8_t
	uint8_t c;
	bool highFlag = true;
	for (int i = 0; i < inBuffer.size() - 1; i++)
	{
		pair<int, int> pos1 = inBuffer[i];
		pair<int, int> pos2 = inBuffer[i + 1];

		int iDirect = getDirection(pos1, pos2, key);
		if (highFlag)
		{
			c = (0b00001111 & iDirect) << 4;
		}
		else
		{
			c |= (0b00001111 & iDirect);
			outBuffer.push_back(c);
		}
		highFlag = !highFlag;
	}

	// End Mark
	if (highFlag)
	{
		c = (0b00001111 & g_iEndMark) << 4;
	}
	else
	{
		c |= (0b00001111 & g_iEndMark);
	}
	outBuffer.push_back(c);
}

void unzip(const vector<uint8_t>& inBuffer, vector<pair<int, int>>& outBuffer)
{
	//int a1 = (c & 0b11110000) >> 4;
	//int a2 = c & 0b00001111;

	if (inBuffer.size() < 8) return;

	// uint8_t to posStart
	uint8_t temp[4];
	for (int i = 0; i < 4; i++)
	{
		temp[i] = inBuffer[i];
	}
	int x = 0;
	memcpy(&x, temp, sizeof(int));
	for (int i = 0; i < 4; i++)
	{
		temp[i] = inBuffer[i + 4];
	}
	int y = 0;
	memcpy(&y, temp, sizeof(int));

	pair<int, int> pos = make_pair(x, y);
	outBuffer.push_back(pos);

	// init key
	vector<pair<int, int>> key;
	initDirectionKey(key);

	// uint8_t to pos
	for (int i = 8; i < inBuffer.size(); i++)
	{
		uint8_t c = inBuffer[i];

		const int iDirectNum = 2;
		for (int j = 0; j < iDirectNum; j++)
		{
			int iDirect;
			if (j == 0)
			{
				iDirect = (c & 0b11110000) >> 4;
			}
			else if (j == 1)
			{
				iDirect = c & 0b00001111;
			}

			if (iDirect == g_iEndMark) return;
			pair<int, int> posNext = getNextPos(pos, iDirect, key);
			outBuffer.push_back(posNext);
			pos = posNext;
		}
	}
}

void printPathData(const vector <pair<int, int>>& pathData)
{
	for (pair<int, int> i : pathData)
	{
		cout << "(" << i.first << ", " << i.second << ") => ";
	}
	cout << endl;
}

void printZipData(const vector<uint8_t> &zipData)
{
	if (zipData.size() < 8) return;

	// uint8_t to posStart
	uint8_t temp[4];
	for (int i = 0; i < 4; i++)
	{
		temp[i] = zipData[i];
	}
	int x = 0;
	memcpy(&x, temp, sizeof(int));
	for (int i = 0; i < 4; i++)
	{
		temp[i] = zipData[i + 4];
	}
	int y = 0;
	memcpy(&y, temp, sizeof(int));

	pair<int, int> pos = make_pair(x, y);

	cout << "(" << pos.first << ", " << pos.second << ") => ";

	for (int i = 8; i < zipData.size(); i++)
	{
		bitset<8> b(zipData[i]);
		cout << b << " => ";
	}
	cout << endl;
}

int main()
{
	vector<pair<int, int>> pathData;
	vector<uint8_t> zipData;

	// Test: zipData from 0 to 8
	pathData.push_back(make_pair(10000, 100));
	pathData.push_back(make_pair(9999, 99));
	pathData.push_back(make_pair(9998, 99));
	pathData.push_back(make_pair(9997, 100));
	pathData.push_back(make_pair(9997, 99));
	pathData.push_back(make_pair(9997, 100));
	pathData.push_back(make_pair(9998, 99));
	pathData.push_back(make_pair(9999, 99));
	pathData.push_back(make_pair(10000, 100));
	// Test End

	// Test
	pathData.push_back(make_pair(9999, 100));
	pathData.push_back(make_pair(9999, 101));
	pathData.push_back(make_pair(9998, 101));
	pathData.push_back(make_pair(9997, 100));
	pathData.push_back(make_pair(9998, 99));
	// Test End

	cout << endl << "origin data:" << endl;
	printPathData(pathData);


	// Zip
	zip(pathData, zipData);
	cout << endl << "zip data:" << endl;
	printZipData(zipData);

	// Clear
	pathData.clear();

	// Unzip
	unzip(zipData, pathData);
	cout << endl << "unzip data:" << endl;
	printPathData(pathData);
}


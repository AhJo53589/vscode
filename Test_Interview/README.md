# Test_Interview




---
## 20190719
今天做力扣发现了之前做的题（上下级不能同时参加活动）的原题，不过不是多叉树，而是二叉树。  
还很高兴把之前的答案匹配一下贴过去，结果发现是错的。  
花了一天时间，终于做对了。  
这里也顺便改一下。  

[查看详细](./MaxSum_NoBothParentAndChild/rob3.md)


---
## 20190708
使用bitset替换二进制输出函数。  
[C++ bitset 用法](https://www.cnblogs.com/magisk/p/8809922.html)


---
## 20190531

* 完成的例子：  
**这个解法是错误的！**

从一个组织里面选择任意人数的人员参加一个活动，  
要求参加的人员当中不能有直接的上下级关系，  
选择每个人都会得到一个非负分数，要求选取的人员分数总和最大。  

例如A是BCD的直接上司，B是EF的直接上司，
ABCDEF 对应的分数分别为150, 30, 20, 40, 100, 10。  
则最优解为AEF  

> Description.jpg  
![](https://raw.githubusercontent.com/AhJo53589/vscode/master/Test_Interview/MaxSum_NoBothParentAndChild/Description.jpg)


前几天学了动态规划，想起这道题应该也是。  
翻出来按照之前写的伪代码实现了一遍。  
答案是对的。  
但是似乎每次都要重新计算子问题，似乎并不那么“动态规划”。  
这道题和前两天做的 __198.Rob 打家劫舍__ 很像，  
但是加和更复杂一点，不知道能不能像那道题一样用简单的加法解决。  


思路：  
从上向下判断每个结点，结点的值大还是它所有子结点的有效值大。  
如果结点的值大，就确定这个结点是最优解中的一个点，输出。 
然后跳过它的所有子结点，对所有子结点的子结点依次做这个判断。   
否则，对它所有的子结点依次做这个判断。  

有效值的意思是，如果这个结点比它所有子结点的和要小，  
那它就不会被选中参加活动，它的有效值就是0。  
当然子结点也是要递归下去确定自己的有效值是自身还是0。  



``` C++
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
``` 





---
## 20190529

* 完成的例子：  

地图数据压缩和解压。  
在某个二维游戏中，将地图划分为二维的方格，人物的坐标是(x,y)的形式(x,y为整数)。
人物每次移动只能移动到其周围八个格子。移动路径的一种表示方式是记录其每次移动后的坐标。
为了压缩这个移动路径，我们需要将移动路径表示成 初始坐标 + 一系列移动方向的形式。
 __由于每次移动只有8个可能的方向，可以用3个bit表示，两次移动就需要6bit，
这样可以将这6个bit放进一个字节8bit内（浪费的2个bit不用管）。__ 

示例：

假设x轴正方向为→，y轴正方向为↓
原始表示方法：(395,2098) => (395,2099) => (396,2099) => (395,2098)
初始坐标+偏移表示方法：(395,2098) ↓ → ↖

题目要求：
你需要利用上述思路，实现压缩算法与对应的解压缩算法。
 __注意一个字节内要压缩进去两步（即第一段标注部分）  
初始坐标允许不压缩直接放进去（初始坐标是两个int）__ 

void zip(const vector<pair<int, int>>& inBuffer, vector<uint8_t>& outBuffer);  
压缩算法，输入为一系列的坐标点，已经确认是符合规范的移动路径（每次都是移动到相邻8个位置，且没有原地不动的情况），函数内不需要再验证。  
输出为压缩后的数据，里面存放什么，如何存放，都由你自己定义。
__提示：可以在数据的前面保存有几次偏移，或者在数据的结尾以特殊标记表示结束。__

void unzip(const vector<uint8_t>& inBuffer, vector<pair<int, int>>& outBuffer);  
解压缩，输入为压缩算法产生的数据，输出为原始移动路径。


更新一下，发现忘记int是4字节，直接存1字节的uint8_t是错的。  
因为用例是100，所以没发现。  

吃了不会二进制的亏，现场因为这个没有做出来。  
实际上就这么简单。 

解答：

二进制位操作。  
<< 移位操作符，& | 位运算操作符。  
压缩  
```C++
uint8_t c;
c = (0b00001111 & a1) << 4;
c |= 0b00001111 & a2;
```
解压缩  
```C++
uint8_t c;
int a1 = (c & 0b11110000) >> 4;
int a2 = c & 0b00001111;
```


坐标和方向互相转换，也很简单。  
将8种方向转换成0-8，使用一个pair数组，将8种方向的x/y差值记录下来。  
转换时只需要+-标准值即可。  
```C++
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
```


还需要注意，起始坐标int（4字节），存入压缩数据（1字节）头部。  
```C++
uint8_t temp[4];
memcpy(temp, &posStart.first, sizeof(int));
```




附上zip和unzip完整代码  
```C++
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
```


---
## 20190416

* 完成的例子：  

阿拉伯数字转大写人民币NumberToRMB  
这道题原来的要求没那么严格，我后来网上越查越强迫症给弄的要求更多。
最后单位也支持到了“万亿亿”、“亿亿亿亿”这样子。

```C++
wstring GetDigitWord(int iDigit)
{
	wstring strDititWord;
	
	if (iDigit == 0)
	{
		strDititWord.push_back(cChineseDigitWord[0]);	// 圆
		return strDititWord;
	}

	if (iDigit % 4 == 0)
	{
		int i = iDigit / 4;
		if (i % 2 == 1)
		{
			strDititWord.push_back(cChineseDigitWord[4]);	// 万
			i -= 1;
		}
		while (i > 0)
		{
			strDititWord.push_back(cChineseDigitWord[5]);	// 亿
			i -= 2;
		}
	}
	else
	{
		int i = iDigit % 4;
		strDititWord.push_back(cChineseDigitWord[i]);	// 拾佰仟
	}

	return strDititWord;
}
```
整体思路就是要转换的字符串，从后往前填写。先写圆，然后写个位，然后十位如果有数字，先写单位拾，再写数字，再往前。  
每一位应该填什么单位，写了个方法来解决。根据iDigit来获得对应的“圆拾佰仟万亿”。  
然后考虑零的问题。使用两个标志位bLowZeroFlag，bHighZeroFlag应对。  
有一条需求是105000可以写成10w5k，也可以写成10w零5k。我做的是前者。想做后者更简单。

```C++
wstring FourNumberToRMB(unsigned int iNumber, int iDigit, bool bHighZeroFlag)
{
	wstring strNumber;
	bool bLowZeroFlag = false;

	if (iNumber > 9999 || iNumber == 0) return strNumber;

	do 
	{
		int n = iNumber % 10;
		if (n == 0 && bLowZeroFlag)	// 如果这一位是0，低位有不是0数字，循环未结束所以高位肯定还有数字，补零
		{
			strNumber.insert(strNumber.begin(), cChineseWord[0]);	// 补零
			bLowZeroFlag = false;
		}

		if (n == 0 && iDigit %4 == 0)
		{
			strNumber.insert(0, GetDigitWord(iDigit));	// 数位，个位为0时也要写圆/万/亿
		}

		if (n != 0)
		{
			strNumber.insert(0, GetDigitWord(iDigit));	// 数位，圆/万/亿
			strNumber.insert(strNumber.begin(), cChineseWord[n]);	// 数值
			bLowZeroFlag = true;
		}

		iDigit += 1;
		iNumber /= 10;
	} while (iNumber > 0);

	if (iDigit % 4 != 0 && bLowZeroFlag && bHighZeroFlag)	// 如果没有到第4位就结束循环，相当于数字是0，低位有不是0数字，高位还有数字，补零
	{
		strNumber.insert(strNumber.begin(), cChineseWord[0]);	// 补零
	}

	return strNumber;
}
```
我把核心算法写成了只接受4位数的转换，是考虑到后面更大的数值，超过unsigned __int64时，可以在用户输入的时候保存成字符串，通过字符串操作每次裁剪成4位数计算。不过最后也没写。

```C++
// (int)12345000 ==> (string)壹仟贰佰叁拾肆万伍仟圆
wstring NumberToRMB(unsigned __int64 iNumber)
{
	wstring strNumber;
	if (iNumber == 0) return strNumber;

	int iDigit = 0;
	bool bHighZeroFlag = false;

	do 
	{
		int n = iNumber % 10000;

		bool bHighZeroFlag = ((iNumber / 10000) > 0);
		strNumber.insert(0, FourNumberToRMB(n, iDigit, bHighZeroFlag));

		iDigit += 4;
		iNumber /= 10000;
	} while (iNumber > 0);

	return strNumber;
}
```


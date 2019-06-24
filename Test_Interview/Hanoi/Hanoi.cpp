// Hanoi.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
using namespace std;

// 用程序实现汉诺塔搬运n个盘子的过程。
// 举例：如果柱子标为ABC，要由A搬至C，则记为A->C，在只有一个盘子时，就将它直接搬至C，输出为A->C。
// 当有两个盘子，就将B当做辅助柱，也就是：A->B、A->C、B->C

void move(int n, char a, char c)
{
	cout << n << " : " << a << "-->" << c << endl;
}

void hanoi(int n, char a, char b, char c)
{
	if (n == 1)
	{
		// 如果剩下一个盘子，直接从a-->c 
		move(n, a, c);
	}
	else
	{
		// 把n-1个盘子从a移动到b借助于c
		hanoi(n - 1, a, c, b);
		// 把第n和盘子从a移动c
		move(n, a, c);
		// 把n-1个盘子从b移动到c借助于a
		hanoi(n - 1, b, a, c);
	}
}

int main()
{
	int num = 0;
	cin >> num;
	hanoi(num, 'A', 'B', 'C');
	return 0;
}


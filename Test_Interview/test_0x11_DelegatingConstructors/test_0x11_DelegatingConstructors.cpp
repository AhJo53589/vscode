// test_0x11_DelegatingConstructors.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
using namespace std;

struct Point
{
	int x;
	int y;
};

struct Rect
{
	Rect() : Rect(0, 0, 0, 0, 0, 0) {}

	Rect(int l, int t, int r, int b) : Rect(l, t, r, b, 0, 0) {}

	Rect(Point topleft, Point bottomright)
		: Rect(topleft.x, topleft.y, bottomright.x, bottomright.y, 0, 0) {}

	Rect(int l, int t, int r, int b, int lc, int fc)
		: left(l), top(t), right(r), bottom(b), line_color(lc), fill_color(fc)
	{
		//do something else...
	}

	int left;
	int top;
	int right;
	int bottom;
	int line_color;
	int fill_color;
};


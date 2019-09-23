// test_stack_overflow.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <stdio.h>
#include <stdlib.h>

using namespace std;

//Have we invoked this function?
void why_here(void)
{
	printf("why u r here?!\n");
	exit(0);
}

int main(int argc, char * argv[])
{
	int buff[1];
	buff[2] = (int)why_here;

	cout << "&main() \t= " << &main << endl;
	cout << "&buff   \t= " << &buff << endl;
	cout << "&buff[0]\t= " << &buff[0] << endl;
	cout << "&buff[1]\t= " << &buff[1] << endl;
	cout << "&buff[2]\t= " << &buff[2] << endl;
	cout << "&why_here\t= " << &why_here << endl;
	cout << "(int)why_here\t= " << (int)why_here << endl;
	return 0;
}
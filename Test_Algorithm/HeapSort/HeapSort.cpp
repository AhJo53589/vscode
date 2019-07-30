// HeapSort.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include <vector>

using namespace std;

void printVectorInt(vector<int> &nums)
{
	for (auto i : nums) cout << i << " ";
	cout << endl;
}

void Heapify(vector<int> &nums, int i, int heap_size)
{
	int l = i * 2 + 1;
	int r = i * 2 + 2;
	if (l < heap_size && nums[i] < nums[l])
	{
		swap(nums[i], nums[l]);
		printVectorInt(nums);
		Heapify(nums, l, heap_size);
	}
	if (r < heap_size && nums[i] < nums[r])
	{
		swap(nums[i], nums[r]);
		printVectorInt(nums);
		Heapify(nums, r, heap_size);
	}
}

void HeapSort(vector<int> &nums)
{
	int heap_size = nums.size();
	for (int i = heap_size / 2 - 1; i >= 0; i--)
	{
		Heapify(nums, i, heap_size);
	}

	while (heap_size > 0)
	{
		swap(nums[0], nums[heap_size - 1]);
		printVectorInt(nums);
		heap_size--;
		for (int i = heap_size / 2 - 1; i >= 0; i--)
		{
			Heapify(nums, i, heap_size);
		}
	}
}

int main()
{
	vector<int> nums = { 5, 2, 9, 4, 7, 6, 1, 3, 8 };
	printVectorInt(nums);
	HeapSort(nums);
	cout << "堆排序结果：" << endl;
	printVectorInt(nums);
	return 0;
}
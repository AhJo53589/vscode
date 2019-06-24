# Test_Algorithm


---
## 20190416

实在没办法了，还是写个备忘吧。  
前面几个例子搞的时候，也都用心搞懂了，包括细节。  
然而隔两天就记混了。

* 今天学习的问题：
1. 使用异或交换两个数字，要注意a和b是同一地址会产生错误
```C++
void swap(int &a, int &b)
{
 	if (a == b) return;	// 防止a和b同一地址而出现问题
	a ^= b;
	b ^= a;
	a ^= b;
}
```

* 之前学习的问题：
1. 二分查找BinarySearch  
可以对一个有序数列进行查找，  
查找[first, last)内第一个不小于value的值的位置。  
```C++
int LowerBound(int _Array[], int _First, int _Last, int _Val)	// 返回[first, last)内第一个不小于value的值的位置
{
	while (_First < _Last)	// 搜索区间[first, last)不为空
	{
		int _Mid = _First + (_Last - _First) / 2;	// 防溢出
		if (_Array[_Mid] < _Val)
		{
			_First = _Mid + 1;
		}
		else
		{
			_Last = _Mid;
		}
	}
	return _First;	// last也行，因为[first, last)为空的时候它们重合
}
```
算法是取数组first（第一位下标）和last（最后一位下标）的平均值mid，对比mid的值和目标。  
根据结果让first或者last等于mid，循环重新计算对比。

2. 快速排序Quicksort
快速排序一开始看网上的算法和算法导论上的不一样，纠结了好久。
两个都搞懂了，决定只用算法导论的方法，比较好记好描述。  

```C++
int Partition(int A[], int p, int r)
{
	int i = p;
	for (int j = p + 1; j < r; j++)
	{
		if (A[j] < A[r])
		{
			swap(A[i], A[j]);
			i += 1;
		}
	}
	swap(A[i], A[r]);
	return i;
}
```
算法是Partition需要传入数组以及数组的p（第一位）和r（最后一位），以其中一个值（选了r）为目标，所有比目标小的数值都换到前面，最后再把目标换过去，实现目标前面都比他小，后面都比他大的情况。把目标的下标返回（q）。  
```C++
void QuickSort(int A[], int p, int r)
{
	if (p < r)
	{
		int q = Partition(A, p, r);
		QuickSort(A, p, q - 1);
		QuickSort(A, q + 1, r);
	}
}
```
把数组从q分开，前面和后面分别递归下去。

3. 插入排序InsertionSort
```C++
void InstertionSort(int _Array[], int _Len)
{
	for (int i = 1; i < _Len; i++)
	{
		int temp = _Array[i];
		int j = i - 1;
		while (j >= 0 && temp < _Array[j])
		{
			_Array[j + 1] = _Array[j];
			j -= 1;
		}
		_Array[j + 1] = temp;
	}
}
```

原理就像扑克抓牌时，新抓到的牌，往前面找合适的地方。
外层循环i就相当于新抓到的牌，每循环一次抓一张。
j=i-1就相当于之前手牌里最后一张，j-=1每次往前查看一张。
如果这张（_Array[j]）比新抓到的（temp=_Array[i]）大，就把这张牌往后挪一张，相当于给前面留个空位。
一直找到合适的位置，把新抓到的放在那里就可以了。
外层循环继续抓下一张新的。

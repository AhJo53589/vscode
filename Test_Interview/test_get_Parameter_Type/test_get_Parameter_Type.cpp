// test_get_Parameter_Type.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <vector>
#include <functional>
#include <tuple>
#include <typeinfo>

using namespace std;

template<typename T>
struct function_traits;
// R为返回类型
// ...Args 为输入参数类型，个数不限
template<typename R, typename ...Args>
struct function_traits<std::function<R(Args...)>>
{
	static const size_t nargs = sizeof...(Args);
	// 返回类型
	typedef R result_type;

	// 输入参数类型,i为从0开始的参数类型索引
	template <size_t i>
	struct arg
	{
		typedef typename std::tuple_element<i, std::tuple<Args...>>::type type;
	};
};


int main()
{
	typedef std::function<void(int, unsigned int, vector<vector<int>>)> feacomp_fun;

	//if (typeid(function_traits<feacomp_fun>::result_type) != typeid(void))
	//{
	//}
	cout << typeid(function_traits<feacomp_fun>::result_type).name() << endl;
	cout << typeid(function_traits<feacomp_fun>::arg<0>::type).name() << endl;
	cout << typeid(function_traits<feacomp_fun>::arg<1>::type).name() << endl;
	cout << typeid(function_traits<feacomp_fun>::arg<2>::type).name() << endl;
}


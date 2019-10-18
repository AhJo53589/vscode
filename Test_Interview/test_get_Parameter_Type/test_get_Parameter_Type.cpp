// test_get_Parameter_Type.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <vector>
#include <functional>
#include <tuple>
#include <typeinfo>

using namespace std;


//////////////////////////////////////////////////////////////////////////
// 使用模板获取函数的返回值和参数列表
// 用std::function将 返回值R 和 参数列表...Args 切开
// 将参数列表声明成tuple，实现获取类型
template<typename T>
struct function_traits;
template<typename R, typename ...Args>
struct function_traits<std::function<R(Args...)>>
{
	static const size_t nargs = sizeof...(Args);
	typedef R result_type;

	template <size_t i>
	struct arg
	{
		typedef typename std::tuple_element<i, std::tuple<Args...>>::type type;
	};
};

int test(int a, size_t b, vector<int> c)
{
	return 0;
}

int main()
{
	//////////////////////////////////////////////////////////////////////////
	// 识别和绑定测试函数
	typedef std::function<decltype(test)> feacomp_fun;
	feacomp_fun fun = test;

	// 识别函数各个参数类型
	typedef function_traits<feacomp_fun> fun_traits;
	cout << fun_traits::nargs << endl;
	cout << typeid(fun_traits::result_type).name() << endl;
	cout << typeid(fun_traits::arg<0>::type).name() << endl;
	cout << typeid(fun_traits::arg<1>::type).name() << endl;
	cout << typeid(fun_traits::arg<2>::type).name() << endl;

	// 测试用例转换成参数
	//fun_traits::tuple_type _tuple;


	fun_traits::arg<0>::type arg_0 = 0;
	fun_traits::arg<1>::type arg_1 = 1;
	fun_traits::arg<2>::type arg_2 = { 2 };
	fun_traits::result_type answer = 0;


	// 运行测试函数
	auto ans = fun(arg_0, arg_1, arg_2);
	cout << "ans = " << ans << " check " << answer << endl;
}


// test_get_Parameter_Type_2.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <sstream>
#include <fstream>

#include <vector>
#include <string>
#include <functional>
#include <tuple>
#include <typeinfo>

#include "parameter_type.h"

using namespace std;



template<class R,class ...Params>
class type_warp;

template<class R,class Head,class... Tail>
class type_warp<R,Head,Tail...> : public type_warp<R,Tail...>
{
public:
	using Base = type_warp<R,Tail...>;
	template<class F, class... Args>
	static R call(F&& f, TestCases& caster, Args... args)
	{
		Head head = caster.get<Head>();
		return Base::call(f, caster,args...,head);
	}
};

template<class R>
class type_warp<R>
{
public:
	template<class F,class... Args>
	static R call(F&& f, TestCases& caster, Args... args)
	{
		return f(args...);
	}
};

template<typename T>
struct function_type;
template<typename R, typename ...Args>
struct function_type<std::function<R(Args...)>> : public type_warp<R,Args...>
{
	using res_t = R;
};




int test(int a, vector<int> b)
{
	if (a < b.size()) return b[a];
	return 0;
}

int test1(int a, int b)
{
	return a + b;
}

int test2(int a)
{
	return a;
}

int test3()
{
	return 1;
}

int test4(int a,int b,int c)
{
	return a+b+c;
}

#define TEST_FUNC test

int main()
{

	// 读取测试用例
	ifstream f("tests.txt");

	using func_t = function_type<function<decltype(TEST_FUNC)>>;
	TestCases test_cases(f);
	while (!test_cases.empty())
	{
		func_t::res_t ans = func_t::call(TEST_FUNC, test_cases);
		func_t::res_t answer = test_cases.get<func_t::res_t>();
		cout << "ans = " << ans << " check " << answer << endl;
	}


	//////////////////////////////////////////////////////////////////////////
	//// 识别和绑定测试函数
	//typedef function<decltype(test)> feacomp_fun;
	//feacomp_fun fun = test;

	//// 识别函数各个参数类型
	//typedef function_traits<feacomp_fun> fun_traits;
	//cout << fun_traits::nargs << endl;
	//cout << typeid(fun_traits::result_type).name() << endl;
	//cout << typeid(fun_traits::arg<0>::type).name() << endl;
	//cout << typeid(fun_traits::arg<1>::type).name() << endl;

	//// 测试用例转换成参数
	////fun_traits::tuple_type _tuple;


	//fun_traits::arg<0>::type arg_0 = test_cases.getValue(fun_traits::arg<0>::type());
	//fun_traits::arg<1>::type arg_1 = test_cases.getValue(fun_traits::arg<1>::type());
	//fun_traits::result_type answer = test_cases.getValue(fun_traits::result_type());


	//// 运行测试函数
	//auto ans = fun(arg_0, arg_1);
	//cout << "ans = " << ans << " check " << answer << endl;

	//RunCase(test_cases, fun, 0);



	//////////////////////////////////////////////////////////////////////////
	//typedef typename tuple<int, vector<int>> t_type;
	//t_type tp(1, { 1,2,3,4,5 });
	////CreateTuple<0, int, int, int, int, int>(_tp);
	////CreateTuple<0>(_tp);
	////cout << get<0>(_tp) << get<1>(_tp) << get<2>(_tp) << get<3>(_tp) << get<4>(_tp) << endl;


	//auto a = RunCasesWithTuple<feacomp_fun, t_type>(fun, tp, );
	//cout << a << endl;
}


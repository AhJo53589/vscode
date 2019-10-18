// test_get_Parameter_Type_2.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <sstream>
#include <fstream>

#include <vector>
#include <string>
#include <functional>
#include <typeinfo>

#include "parameter_type.h"

using namespace std;


#define TEST_FUNC test
int test(int a, vector<int> b)
{
	if (a < b.size()) return b[a];
	return 0;
}


int main()
{
	ifstream f("tests.txt");
	TestCases test_cases(f);

	using func_t = function_type<function<decltype(TEST_FUNC)>>;
	while (!test_cases.empty())
	{
		func_t::return_type ans = func_t::call(TEST_FUNC, test_cases);
		func_t::return_type answer = test_cases.get<func_t::return_type>();
		cout << "ans = " << ans << " check " << answer << endl;
	}
}


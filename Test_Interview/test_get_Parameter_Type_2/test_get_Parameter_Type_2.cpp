// test_get_Parameter_Type_2.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <sstream>
#include <vector>
#include <string>
#include <functional>
#include <tuple>
#include <typeinfo>

#include "parameter_type.h"

using namespace std;


int stringToInt(string s)
{
	return stoi(s);
}

vector<int> stringToVectorInt(string input)
{
	vector<int> output;
	input = input.substr(1, input.length() - 2);
	stringstream ss;
	ss.str(input);
	string item;
	char delim = ',';
	while (getline(ss, item, delim)) {
		output.push_back(stoi(item));
	}
	return output;
}

int test(int a, vector<int> b)
{
	cout << "test a = " << a << endl;
	cout << "test b[0] = " << b[0] << endl;
	return a + b.back();
}

int main()
{
	typedef function<decltype(test)> feacomp_fun;
	typedef function_traits<feacomp_fun> fun_traits;
	cout << fun_traits::nargs << endl;
	cout << typeid(fun_traits::result_type).name() << endl;
	cout << typeid(fun_traits::arg<0>::type).name() << endl;
	cout << typeid(fun_traits::arg<1>::type).name() << endl;



	vector<string> strInput = { "1", "[1,2,3,4,5]", "6" };

	fun_traits::result_type answer = stringToInt(strInput.back());
	fun_traits::arg<0>::type arg_0 = stringToInt(strInput[0]);
	fun_traits::arg<1>::type arg_1 = stringToVectorInt(strInput[1]);

	auto ans = test(arg_0, arg_1);
	cout << "ans = " << ans << " check " << answer << endl;
}


#pragma once

#include <functional>
#include <tuple>
#include <typeinfo>



//////////////////////////////////////////////////////////////////////////
template<typename T>
struct function_traits;
template<typename R, typename ...Args>
struct function_traits<std::function<R(Args...)>>
{
	static const size_t nargs = sizeof...(Args);
	typedef R result_type;

	typedef std::tuple<Args...> tuple_type;

	template <size_t i>
	struct arg
	{
		typedef typename std::tuple_element<i, std::tuple<Args...>>::type type;
	};
};

//////////////////////////////////////////////////////////////////////////
class TestCases
{
public:
	TestCases(std::ifstream& is) : curr(0)
	{
		std::string text;
		while (getline(is, text))
		{
			file.push_back(text);
		}
	}
	std::string popString()
	{
		if (curr == file.size()) return {};
		return file[curr++];
	}


	template<class T>
	T get();

	template<>
	int get<int>()
	{
		if (curr == file.size()) return {};
		return stringToInt(popString());
	}

	template<>
	std::vector<int> get<std::vector<int>>()
	{
		if (curr == file.size()) return {};
		return stringToVectorInt(popString());
	}

	bool empty() { return curr == file.size(); }

private:
	int stringToInt(std::string s)
	{
		return std::stoi(s);
	}

	std::vector<int> stringToVectorInt(std::string input)
	{
		std::vector<int> output;
		input = input.substr(1, input.length() - 2);
		std::stringstream ss;
		ss.str(input);
		std::string item;
		char delim = ',';
		while (getline(ss, item, delim)) {
			output.push_back(stoi(item));
		}
		return output;
	}


private:
	std::vector<std::string> file;
	std::size_t curr;
};

//////////////////////////////////////////////////////////////////////////



#pragma once

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <numeric>
#include <functional>
#include <iterator>
#include <sstream>
#include <fstream>

#include <vector>
#include <string>
#include <list>
#include <map>
#include <set>

class QueryResult;
class TextQuery
{
public:
	using line_no = std::vector<std::string>::size_type;
	TextQuery(std::ifstream&);
	QueryResult query(const std::string&) const;
private:
	std::shared_ptr<std::vector<std::string>> file;
	std::map<std::string, std::shared_ptr<std::set<line_no>>> wm;
};

class QueryResult
{
	using line_no = std::vector<std::string>::size_type;
	friend std::ostream& print(std::ostream&, const QueryResult&);
public:
	QueryResult(std::string s,
		std::shared_ptr<std::set<line_no>> p,
		std::shared_ptr<std::vector<std::string>> f) : 
		sought(s), lines(p), file(f) {}
private:
	std::string sought;
	std::shared_ptr<std::set<line_no>> lines;
	std::shared_ptr<std::vector<std::string>> file;
};
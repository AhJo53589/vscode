#pragma once

#include <string>
#include <vector>
#include <set>
#include <map>

class Query;
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
	friend std::ostream& operator<< (std::ostream&, const QueryResult&);

public:
	QueryResult(std::string s,
		std::shared_ptr<std::set<line_no>> p,
		std::shared_ptr<std::vector<std::string>> f) :
		sought(s), lines(p), file(f) {}

	std::shared_ptr<std::vector<std::string>> get_file() { return file; }
	std::set<line_no>::iterator begin() { return lines->begin(); }
	std::set<line_no>::iterator end() { return lines->end(); }

private:
	std::string sought;
	std::shared_ptr<std::set<line_no>> lines;
	std::shared_ptr<std::vector<std::string>> file;
};


class Query_base
{
	friend class Query;
protected:
	using line_no = TextQuery::line_no;
	virtual ~Query_base() = default;
private:
	virtual QueryResult eval(const TextQuery&) const = 0;
	virtual std::string rep() const = 0;
};

//////////////////////////////////////////////////////////////////////////
class Query
{
	friend Query operator~ (const Query&);
	friend Query operator| (const Query&, const Query&);
	friend Query operator& (const Query&, const Query&);

public:
	Query(const std::string&);
	QueryResult eval(const TextQuery &t) const { return q->eval(t); }
	std::string rep() const { return q->rep(); }

private:
	Query(std::shared_ptr<Query_base> query) : q(query) {}
	std::shared_ptr<Query_base> q;
};

//////////////////////////////////////////////////////////////////////////
class WordQuery : public Query_base
{
	friend class Query;
	WordQuery(const std::string &s) : query_word(s) {}
	QueryResult eval(const TextQuery &t) const { return t.query(query_word); }
	std::string rep() const { return query_word; }
	std::string query_word;
};

//////////////////////////////////////////////////////////////////////////
class NotQuery : public Query_base
{
	friend Query operator~ (const Query &);
	NotQuery(const Query &q) : query(q) {}
	std::string rep() const { return "~(" + query.rep() + ")"; }
	QueryResult eval(const TextQuery&) const;
	Query query;
};

//////////////////////////////////////////////////////////////////////////
class BinaryQuery : public Query_base
{
protected:
	BinaryQuery(const Query &l, const Query &r, std::string s) : lhs(l), rhs(r), opSym(s) {}
	std::string rep() const { return "(" + lhs.rep() + " " + opSym + " " + rhs.rep() + ")"; }
	Query lhs, rhs;
	std::string opSym;
};

//////////////////////////////////////////////////////////////////////////
class AndQuery : public BinaryQuery
{
	friend Query operator& (const Query&, const Query&);
	AndQuery(const Query &left, const Query &right) : BinaryQuery(left, right, "&") {}
	QueryResult eval(const TextQuery&) const;
};

//////////////////////////////////////////////////////////////////////////
class OrQuery : public BinaryQuery
{
	friend Query operator| (const Query&, const Query&);
	OrQuery(const Query &left, const Query &right) : BinaryQuery(left, right, "|") {}
	QueryResult eval(const TextQuery&) const;
};


//////////////////////////////////////////////////////////////////////////
inline
Query operator~(const Query &operand)
{
	return std::shared_ptr<Query_base>(new NotQuery(operand));
}

inline
Query operator|(const Query &lhs, const Query &rhs)
{
	return std::shared_ptr<Query_base>(new OrQuery(lhs, rhs));
}

inline
Query operator&(const Query &lhs, const Query &rhs)
{
	return std::shared_ptr<Query_base>(new AndQuery(lhs, rhs));
}

inline
Query::Query(const std::string & s) : q(new WordQuery(s))
{
}

//////////////////////////////////////////////////////////////////////////
//std::ostream & operator<< (std::ostream &, const Query &);
inline
std::ostream & operator<<(std::ostream & os, const Query & q)
{
	return os << q.rep();
}
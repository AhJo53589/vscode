#pragma once

#include <string>

class StrVec
{
public:
	StrVec() : elements(nullptr), first_free(nullptr), cap(nullptr) {}
	// 13.40
	StrVec(std::initializer_list<std::string>lst)
	{
		auto newdata = alloc_n_copy(lst.begin(), lst.end());
		elements = newdata.first;
		first_free = cap = newdata.second;
	}
	StrVec(const StrVec&);
	StrVec &operator= (const StrVec&);
	StrVec(StrVec&& s) noexcept
		: elements(s.elements), first_free(s.first_free), cap(s.cap)
	{
		s.elements = s.first_free = s.cap = nullptr;
	}
	StrVec &operator= (StrVec &&rhs) noexcept
	{
		if (this != &rhs)
		{
			free();
			elements = rhs.elements;
			first_free = rhs.first_free;
			cap = rhs.cap;
			rhs.elements = rhs.first_free = rhs.cap = nullptr;
		}
		return *this;
	}
	~StrVec();

	void push_back(const std::string&);
	size_t size() const { return first_free - elements; }
	// 13.39
	size_t capacity() const { return cap - elements; }
	void reserve(size_t);
	void resize(size_t);
	void resize(size_t, const std::string&);
	std::string *begin() const { return elements; }
	std::string *end() const { return first_free; }

private:
	std::allocator<std::string> alloc;
	void chk_n_alloc()
	{
		if (size() == capacity()) reallocate();
	}
	std::pair<std::string*, std::string*> alloc_n_copy(const std::string*, const std::string*);
	void alloc_n_move(size_t new_cap);
	void free();
	void reallocate();
	std::string *elements;
	std::string *first_free;
	std::string *cap;
};
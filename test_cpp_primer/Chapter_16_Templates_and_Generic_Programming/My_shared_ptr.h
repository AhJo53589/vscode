#pragma once

template<typename T>class My_shared_ptr
{
public:
	My_shared_ptr(T* t) : ptr(t) {}

private:
	T* ptr;
};
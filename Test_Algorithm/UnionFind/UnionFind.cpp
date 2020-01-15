// UnionFind.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include <vector>
#include <numeric>
#include <algorithm>

using namespace std;

class UnionFind
{
public:
    UnionFind(int cnt) : parnet(cnt, 0), sz(cnt, 1), comp_cnt(cnt)
    {
        iota(parnet.begin(), parnet.end(), 0);
    }

    void unite(int x, int y)
    {
        int x0 = find(x);
        int y0 = find(y);
        if (sz[x0] < sz[y0])
        {
            swap(x0, y0);
        }
        parnet[y0] = x0;
        sz[x0] += sz[y0];
        comp_cnt--;
    }

    int find(int x)
    {
        return (x == parnet[x]) ? x : (parnet[x] = find(parnet[x]));
    }

    int getComponentCnt()
    {
        return comp_cnt;
    }

private:
    vector<int> parnet;
    vector<int> sz;
    int comp_cnt;
};

int main()
{
    cout << "Hello World!\n";
}


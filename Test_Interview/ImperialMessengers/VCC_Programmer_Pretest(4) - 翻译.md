C++ Engineer Candidate Pre-Test  
Visual Concepts China Confidential  


# Visual Concepts China Programming Challenge “Imperial Messengers”
# 仟游软件科技（上海）有限公司 编程挑战 “帝国信使”

## Requirements 要求

* Code must be ANSI Standard C.
* Please submit the source code for your solution as a .c file.
* Read input from standard in and write output to standard out.
* Input should be passed to the program via the command line, not an input file.
* Please write the code entirely from scratch, without referencing any sources for code or algorithms (referencing a C manual for syntax is fine).
* Include total time you spend on the solution with your solution.
* Choose your coding style carefully.
* Briefly describe the algorithm in your documentation.
* Your code should demonstrate your best standards of readability, maintainability, scalability, robustness, speed and efficient resource use.
* Design and code the solution to show your best skills. The code should be nice and elegant, and in plain ANSI C, not C++.

---

* 代码必须是ANSI标准C.
* 请将您的解决方案的源代码作为.c文件提交。
* 从标准输入读取输入并将输出写入标准输出。
* 输入应通过命令行传递给程序，而不是输入文件。
* 请完全从头开始编写代码，而不引用代码或算法的任何来源（参考C手册的语法很好）。
* 包括您使用解决方案在解决方案上花费的总时间。
* 仔细选择您的编码风格。
* 在文档中简要描述算法。
* 您的代码应该展示您的可读性，可维护性，可伸缩性，健壮性，速度和有效资源使用的最佳标准。
* 设计和编写解决方案以显示您的最佳技能。 代码应该漂亮而优雅，并且采用普通的ANSI C，而不是C ++。

## Problem 问题
The empire has a capitol and a number of cities. Some cities are connected to other cities. A route
connecting two cities can transport a message in both directions. All cities are reachable using some path
from the capitol city. The connections among all cities are described in an adjacency matrix with the format
shown below. At the start, a message is sent from the capitol city to all other cities, and we want to know
what's the minimal time for a message to spread out from the capitol to the whole empire.  

---

帝国有一个国会大厦和一些城市。  
有些城市与其他城市相连。  
一条路线连接两个城市可以双向传输信息。  
所有城市都可以通过一些路径到达从国会大厦。  
所有城市之间的连接在具有格式的邻接矩阵中描述如下所示。  
一开始，从国会大厦城市向所有其他城市发送消息，我们想知道什么是消息从国会大厦传播到整个帝国的最短时间。

## Input (Adjacency Matrix) 输入（邻接矩阵）
The first line of the input file will be n, the number of cities, 1 <= n <= 100. The rest of the input defines
an adjacency matrix, A. The adjacency matrix is square and of size n×n. The value of A(i, j) indicates the
time required to travel from city i to city j. A value of character 'x' for A(i, j) indicates that there is no route
between city i to city j. Since a message sent to itself doesn't require time, and the route between cities is
two way, we always have A(i, i) = 0 and A(i, j) = A(j, i). Thus only the entries on the (strictly) lower
triangular portion of A will be supplied as input, and the second line of input will contain one entry, A(2, 1).
The next line will contain two entries, A(3, 1) and A(3, 2), and so on.

---

输入文件的第一行将是n，即城市数，1 <= n <= 100。  
其余输入定义邻接矩阵，A。  
邻接矩阵是正方形，大小为n×n。  
A（i，j）的值表示从城市i到城市j旅行所需的时间。  
A（i，j）的字符'x'的值表示没有路由在城市之间我到城市j。  
由于发送给自己的消息不需要时间，城市之间的路由是两种方式，我们总是有A（i，i）= 0和A（i，j）= A（j，i）。  
因此只提供矩阵的（严格）下半个三角形部分将作为输入，第二行输入将包含一个条目A（2,1）。  
再下一行将包含两个条目，A（3,1）和A（3,2），依此类推。

## Output 输出
Your program should output the minimum time required before a message sent from the capitol (city #1) is
known throughout the empire, i.e. the time it is received in the last city to get the message.

---

您的程序应输出从国会大厦（城市＃1）发送的消息开始到全帝国都知道的最短时间，即在最后一个城市收到消息的时间。  


## Sample Input  输入样本
5  
50  
30 5  
100 20 50  
10 x x 10  

## Output for the Sample Input  样本的输出
35

---
**PS: Please do not share the pretest to anyone, thanks for your cooperation.**  
**Here is the submission mail. hrcn@vcentertainment.com**
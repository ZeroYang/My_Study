
## Shell script: 获取第10+个参数
在Shell脚本中，可以用$n的方式获取第n个参数，例如，一个名为paramtest的脚本：

　　　　#!/bin/bash
　　　　echo $1 $2

执行./paramtest a b 的结果是打印出第1个和第2个参数：

　　　　a b

但是，若脚本需要10个以上的参数，直接写数字会有问题。例如，脚本为:

　　　　#!/bin/bash
　　　　echo $1 $2 $3 $4 $6 $7 $8 $9 $10

执行./paramtest a b c d e f g h i j，结果如下，第10个参数是不对的：

　　　　a b c d e f g h i a0

显然$10被解释成了$1+0。

解决方法很简单，第10个参数加花括号即可：

　　　　#!/bin/bash
　　　　echo $1 $2 $3 $4 $6 $7 $8 $9 ${10}

再次执行./paramtest a b c d e f g h i j，结果正确：

　　　　a b c d e f g h i j
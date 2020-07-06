#Middle of a Text File

##Problem
[Middle of a Text File](https://www.hackerrank.com/challenges/text-processing-in-linux---the-middle-of-a-text-file/problem)

##Solution

    head -n22 | tail +12


## 知识
##head 命令详解

-n: 输出的行数

例如，如果想输出前25行，下面三个命令是等价的：

    head -n25 input.txt
    head -n     25 input.txt
    head -25 input.txt

可以看出，-n后面有没有空格都可以，有几个空格都可以，甚至-n本身都可以省略。

-n 后面也可以跟负数，例如：

    head -n -5 input.txt

此命令表示输出input.txt除了后5行以外的全部内容。

-c: 输出的字节数

    head -c 5 input.txt    # 5 bytes 
    head -c -5b input.txt    # 5*512
    head -c5k input.txt    # 5*1024
    head -c 5m input.txt    #5*1048576

-c 没有默认值，所以后面必须跟数值。可以是负数，表示输出除了后N个字节以外的内容。

#tail 命令详解

将一段文本的结尾一部分输出到标准输出，也就是从某个节点位置开始输出。

基本用法

tail的参数基本用法和head完全一样，在此不作赘述。

只有一点需要注意，如果想表达从第N(比如25)行开始输出，要使用加号：

tail -n +25 input.txt

tail +5c input.txt    #从第5个字节开始打印

常见用法

tail 有一个常见的用法：用来实时查看一个不断更新的log file。例如后台有一个logfile.txt不断更新，我们可以用下面的命令将更新内容打印在屏幕上：

tai -f logfile.txt

-f: 循环读取

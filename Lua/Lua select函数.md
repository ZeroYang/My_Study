# Lua select函数
Lua中用 ... 表示函数的可变参数，比如

    function fun(...)                 --此处的...表示可变参数
        for i in pairs({...})  do     --此处的｛...｝表示可变参数构成的数组
            xxx
        end
    end
select(n, ...)  --数字n表示起点，select(n, ...)返回从起点n到结束的可变参数，比如：

n=3，... 是 0，1，2，3，4，5

则 select(n, ...) 就表示...中从第3个到最后一个的多个数：2，3，4，5。并且2，3，4，5是4个数，不是列表或其他的数据结构

所以， 下面的代码中，a = select(3,...) 就表示的是  a = 2,3,4,5。所以，a=2;

    function f(...)
        a = select(3,...)
        print (a)
        print (select(3,...))
    end

    f(0,1,2,3,4,5)
    >>
    2
    2,3,4,5

 

    select('#', ...)  --返回可变参数的数量

    function f(...)
        print (select('#', ...))
    end

    f(1,2,3)
    >>3
返回可变参数数量的方法还有：


    方法1：

    function f(...)
        print (#{...}))
    end
    f(1,2,3)
    >>3

    方法2：

    function f(...)
        local x = {...}
        print (#x))
    end
    f(1,2,3)
    >>3

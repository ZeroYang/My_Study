# Lua 知识点总结

## 闭包closure
闭包的典型出现形式： 一个 closure 就是一个函数加上该函数所需访问的所有"非局部的变量"

```
function test(x)

    return function (value)

        return value * x

    end

end
func = test(10)
print( func(11) ) 

```
## 循环
    
* for循环
```
    for var=exp1,exp2,exp3 do  
        <执行体>  
    end  
```

    var 从 exp1 变化到 exp2，每次变化以 exp3 为步长递增 var，并执行一次 "执行体"。exp3 是可选的，如果不指定，默认为1。

    *for的三个表达式在循环开始前一次性求值，以后不再进行求值*

```
    for i=1,5 do
	    print(i)
	    i=i+1
    end

    输出
    1
    2
    3
    4
    5
```

## 运算符

* 三目运算符

    无 a ? b : c
```
    a and b or c
```
```
    local a = nil

    a and a or 0
```

* 自加、自减

    无 ++, --操作
```
    a = a+1
    a = a-1
```

## 内存泄漏

Lua的垃圾回收是自动进行的，但是我们可以collectgarbage方法进行手动回收。collectgarbage方法的第一个参数是字符串，代表操作类型，第二个参数只有某些操作类型有，是该操作所需要的参数。常用的操作类型有：

collect：执行一次完整的垃圾回收

count：返回当前使用的内存，单位是kb

* 示例： 
```
function PrintCount()
    print("内存为：", collectgarbage("count"))--输出当前内存占用
end

function A()
    collectgarbage("collect")--进行垃圾回收，减少干扰
    PrintCount()
    local a = {}
    for i=1,5000 do
        table.insert(a, {})
    end
    PrintCount()
    collectgarbage("collect")
    PrintCount()
end

A()
PrintCount()
collectgarbage("collect")
PrintCount()
```

* 减少内存泄漏的方法
    1. 尽量用局部变量，这样当其生命周期结束时，就能被回收；对于全局变量，可以根据使用情况置空，及时回收内存。
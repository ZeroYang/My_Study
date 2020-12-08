# Lua 知识点总结

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
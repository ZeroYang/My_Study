<!--
 * @Author: ooo
 * @Description: 
 * @Date: 2021-04-27 16:22:30
 * @LastEditors: YTB
 * @LastEditTime: 2021-04-29 16:45:34
-->
# Luau loadstring

每次调用loadstring时都被编译，由于loadstring每次编译时不涉及词法域

    a = 100
    local a = 1
    local content = [[
        return a
    ]]

    local func = loadstring(content)
    print(func())

    local g = function()
        a = a+1
        print(a)
    end

    g()

    --[[
        打印：
        100
        2
    --]]


因为loadstring总是在全局环境中编译它的字符串
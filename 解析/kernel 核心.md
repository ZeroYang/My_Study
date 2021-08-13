--[[

]]

#kernel 核心

* __index & metatable
lua __index
setmetatable

* _G
lua _G,_G是一张表，保存了lua所用的所有全局函数和全局变量

```
    for a,b in pairs(_G) do
        print(a,"\t",b)
        if type(b) == "table" then
            for x,y in pairs(b) do
                print("\t","|--",x,y)
            end
        end
    end
```

* string 方法扩展

```
    local version = "1.11.2"
    string.split = function(s,delim)
        print(s)
        if type(delim) ~= "string" or string.len(delim) <= 0 then
            return
        end
        local start = 1
        local t = {}
        while true do
            local pos = string.find(s, delim,start, true)
            if not pos then
                break
            end
            table.insert(t,string.sub(s,start,pos-1))
            start = pos + string.len(delim)
        end
        table.insert(t, string.sub(s, start))
        return t
    end

    for _,v in pairs(string.split(version, ".")) do
        print("split part=", v)
    end
```

* lua中使用Macro
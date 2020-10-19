#Lua 字符串分割 奇葩bug

Mac & iOS系统上执行有乱码，PC & Android执行正常。

string.gmatch(s, "%S+") 执行结果乱码

string.gsub()函数根据给定的配对表达式对源字符串str进行配对, 同时返回源字符串的一个副本, 该副本中成功配对的所有子字符串都将被替换. 

代码：

	function CommonUtil.StringSplit(str, sub)
	    local s = string.gsub(str, sub, " ")
	
	    local result = {}
	    for part in string.gmatch(s, "%S+") do
	        table.insert(result, part)
	    end
	    return result
	end

	local str = "消耗%s可增加星级怪挑战数量#超出今日初始数量的额外部分累计到第二天#当前剩余星级怪数量：%d"
    local result = CommonUtil.StringSplit(str,"#")
    for i,v in ipairs(result) do
        print("+++"..v) -- 打印分给串显示有乱码
    end


正确方法

	function HelperUI.Split(input, delimiter)
	    input = tostring(input)
	    delimiter = tostring(delimiter)
	    if (delimiter == "") then
	        return false
	    end
	    local pos, arr = 0, {}
	    for st, sp in function()
	        return string.find(input, delimiter, pos, true)
	    end do
	        table.insert(arr, string.sub(input, pos, st - 1))
	        pos = sp + 1
	    end
	    table.insert(arr, string.sub(input, pos))
	    return arr
	end


参考链接：
https://www.cnblogs.com/yyxt/p/3869821.html
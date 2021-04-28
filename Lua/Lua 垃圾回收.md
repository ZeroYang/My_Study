
#Lua 垃圾回收

* collectgarbage("setpause", 200) ： 内存增大 2 倍（200/100）时自动释放一次内存 （200 是默认值）。

* collectgarbage("setstepmul", 200) ：收集器单步收集的速度相对于内存分配速度的倍率，设置 200 的倍率等于 2 倍（200/100）。（200 是默认值）

    使用百分数为单位 （例如：值 100 在内部表示 1 ）。

## 垃圾回收器函数
Lua 提供了以下函数collectgarbage ([opt [, arg]])用来控制自动内存管理:

* collectgarbage("collect"): 做一次完整的垃圾收集循环。通过参数 opt 它提供了一组不同的功能：

* collectgarbage("count"): 以 K 字节数为单位返回 Lua 使用的总内存数。 这个值有小数部分，所以只需要乘上 1024 就能得到 Lua 使用的准确字节数（除非溢出）。

* collectgarbage("restart"): 重启垃圾收集器的自动运行。

* collectgarbage("setpause"): 将 arg 设为收集器的 间歇率。 返回 间歇率 的前一个值。

* collectgarbage("setstepmul"): 返回 步进倍率 的前一个值。

* collectgarbage("step"): 单步运行垃圾收集器。 步长"大小"由 arg 控制。 传入 0 时，收集器步进（不可分割的）一步。 传入非 0 值， 收集器收集相当于 Lua 分配这些多（K 字节）内存的工作。 如果收集器结束一个循环将返回 true 。

* collectgarbage("stop"): 停止垃圾收集器的运行。 在调用重启前，收集器只会因显式的调用运行。


我们只需要将不适用的变量设置为nil，它之前所引用的类型就会在一定时间内被自动回收。

当设置了setstepmul和setpause，Lua便会开启自动垃圾回收。
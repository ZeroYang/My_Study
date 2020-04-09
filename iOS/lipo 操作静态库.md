# lipo 操作静态库

* 查看静态库的架构

	lipo -info xxx.a

* 静态库合并

  静态库分为32位和64位，并且，还分为真机版和模拟器版

    lipo -create  xxxx32/libKalCalendar.a   xxxxx64/libKalCalendar.a  -output  libKalCalendar.a

* 静态库拆分或者“瘦身”（提取单个平台）

  lipo 静态库源文件路径 -thin CPU架构名称 -output 拆分后文件存放路径  
  架构名为armv7/armv7s/arm64等，与lipo -info 输出的架构名一致

	lipo  libname.a  -thin  armv7  -output  libname-armv7.a

* 提取、替换和去除指定CPU架构

  提取出armv7架构并新建一个通用文件，类似于-thin选项

	lipo -extract armv7 libname.a -output libname_armv7.a  

  去除armv7架构  

	lipo -remove armv7 libname.a -output libname_exceptArmv7.a

  对输入文件libname.a中的armv7架构文件采用 librepace.a进行替换，并输出到liboutput.a中

	lipo libname.a -replace armv7 libreplace.a -output liboutput.a


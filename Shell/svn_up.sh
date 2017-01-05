#!/bin/bash
#coding=utf-8
echo "Hello SVN_UP !"

work_dir="E:\work_jsws\jsws-trunk"
res_source_dir="Res_SourceFile4.7"
scenes_proj_dir="unity4.7_Android"
res_ios_dir="res_ios"


#获取当前dir
Cur_Dir=$(pwd)
echo "Cur_Dir = $Cur_Dir"


#定时更新
#while [[ true ]]; do
#	svn up --username=yangtianbo  --password=Qj6K9zZZSFgbzk9ENpOWktRitbjR9o > update.txt
#	sleep 60*60*24
#done

#函数,默认带参数 $n 取出对应参数
fun_up_jsws_trunk(){
	cd $1

	echo "svn update $1"
	svn up --username=yangtianbo  --password=Qj6K9zZZSFgbzk9ENpOWktRitbjR9o > update.txt
}

echo $(fun_up_jsws_trunk $work_dir)

#fun_up_jsws_trunk $res_ios_dir

fun_with_return(){
	echo "函数返回值"
	echo "参数个数 $#"
	echo "打印所有参数 $*"
	return $(($1+$2))
}

echo "输入两个数求和值："
fun_with_return 1 2
# $? 获得函数返回值
echo "和值：$?"

# -eq否相等 -ne 不相等返回 -gt左边的数是否大于右边 -lt左边的数是否小于右边 -ge左边的数是否大等于右边 -le左边的数是否小于等于右边

# !非运算 -o或运算 -a与运算
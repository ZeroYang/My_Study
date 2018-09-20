@echo off
:: 注释
rem 表示此命令后的字符为解释行（注释）
echo test

goto a_block

::参数表示符“%”
rem type命令 显示文件内容
type %1

if %1 == test.txt (
echo tets.txt)

::goto
: a_block

echo this is a_block

::goto end


echo a.Key pack debug apk file                         [Key pack debug]
echo b.Key pack release apk file                       [Key pack release]


rem set /p selIdx=Select item: 

if %selIdx% == a echo Key pack debug apk file
if %selIdx% == b echo Key pack release apk file

rem for
@ECHO OFF  
rem FOR %%C IN (*.BAT *.TXT *.SYS) DO TYPE %%C

::set 目标字符串=%源字符串:~起始值,截取长度%
rem 输出时间
set d=%date:~0,10%
set d=%d:-=%
set t=%time:~0,8%
::set t=%t::=%
echo "%p% %d% %t%"

@echo off
set str1=This is string1
set str2=This is string2
echo str1=%str1%
echo str2=%str2%
::先输出一次原有的字符串
set str1=%str1%%str2%

:: exist 命令  del命令
@echo off 
if exist c:\windows\temp\*.* del c:\windows\temp\*.* 

::call 命令
call test.bat 

pause
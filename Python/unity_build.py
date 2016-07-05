#! /usr/bin/env python
#coding=utf-8
#这里需要引入三个模块
import time,os,sched

# /Applications/Unity/Unity.app/Contents/MacOS/Unity

#-quit 其他命令执行完毕后将退出Unity编辑器。
#-buildWindowsPlayer <pathname>
#Build a standalone Windows player (eg, -buildWindowsPlayer path/to/your/build.exe). 
#-buildOSXPlayer <pathname>
#Build a standalone Mac OSX player (eg, -buildOSXPlayer path/to/your/build.app). 

#$UNITY_PATH -projectPath $PROJECT_PATH -executeMethod ProjectBuild.BuildForIPhone project-$1 -quit
#ProjectBuild.BuildForIPhone  自定义方法

#-buildTarget <name>	Allows the selection of an active build target before a project is loaded. 
#Possible options are: win32, win64, osx, linux, linux64, ios, android, web, webstreamed, webgl, xbox360, xboxone, ps3, ps4, psp2, wsa, wp8, tizen, samsungtv
unity_command = "D:\\Unity4.7.2\\Editor\\Unity.exe -projectPath D:\Unity-Pro\unity-autobuild -buildWindowsPlayer D:\Unity-Pro\unity-autobuild\build_out\\projectname.exe -quit"
os.system(cmd)

wait = input("wait:")
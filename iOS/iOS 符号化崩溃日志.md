#iOS Xcode 疑难杂症

Xcode 连上设备真机run Debug、Release环境下都没有问题，Archvie后导出IPA文件，真机安装启动闪退，抓狂。。。。。

##解决办法
iOS Crash log `symbolicatecrash`工具，将iOS的crash log 符号化，找出具体的崩溃栈。

###简述操作流程

symbolicatecrash 工具位于xcode.app下 具体路径

	/Applications/Xcode.app/Contents/Developer/Platforms/iPhoneOS.platform/Developer/Library/PrivateFrameworks/DTDeviceKitBase.framework/Versions/A/Resources/symbolicatecrash

Device Crash Log
将刚刚闪退的设备连上xcode，选择菜单window-》device and simulator；选中设备，然后菜单选择 View Device Logs，选择刚刚的crash Log，然后导出crash文件。

dSYM

dsym文件在你archive项目的文件中，XCODE window-》organizer； 选中你项目的Archive，show in finder,找到dsym文件。

新建文件夹，将上面的3个文件拷贝到一个目录 然后执行如下命令：

	./symbolicatecrash ./XXXX.crash /XXXX.app.dSYM > app.log

示例：

    zwwxdeiMac:IP5C Crash zwwx$ ./symbolicatecrash /Users/zwwx/Documents/work_my/IP5C\ Crash/ProductName\ \ 2018-11-6\ 下午3-32.crash /Users/zwwx/Documents/work_my/IP5C\ Crash/ProductName.app.dSYM >app.log
    2018-11-06 16:53:46.566 xcodebuild[67741:1196677] [MT] PluginLoading: Required plug-in compatibility UUID B395D63E-9166-4CD6-9287-6889D507AD6A for plug-in at path '~/Library/Application Support/Developer/Shared/Xcode/Plug-ins/Unity4XC.xcplugin' not present in DVTPlugInCompatibilityUUIDs
    No symbolic information found


执行上面命令可能遇到的错误

这时候终端有可能会出现：`Error: "DEVELOPER_DIR" is not defined at ./symbolicatecrash line 60.`

输入命令：`export DEVELOPER_DIR="/Applications/XCode.app/Contents/Developer" `

## 参考

[http://www.cocoachina.com/ios/20171026/20921.html](http://www.cocoachina.com/ios/20171026/20921.html)
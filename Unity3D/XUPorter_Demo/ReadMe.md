#XUPorter Unity iOS 打包自动编辑.xcodeproj

##功能
* 添加framework
* 添加.h .m .mm
* 编辑info.plist
* code sign
* 编辑 .h .m .mm file

##原理

*利用Unity Build iOS平台导出Xcode工程时，Unity Editor 会自动调用

    [PostProcessBuild (100)]
    public static void OnPostProcessBuild (BuildTarget target, string pathToBuiltProject)

XUPorter在此方法中进行编辑xcode工程的操作

##share.projmods


    {
    "group": "AddThing",
    "libs": [
    "libsqlite3.0.tbd"
    ],
    "frameworks": [
    "CoreTelephony.framework",
    "AdSupport.framework:optional",
    "StoreKit.framework"
       ],
    "headerpaths": [],
    "files":   [
    "../../iOS/XGSDK/libxgCommon.a",
    "../../iOS/XGSDK/libxgsdk_unity3d.a",
    "../../iOS/XGSDK/libXgsdkData.a",
    ],
    "folders": ["../../iOS/Common/"],
    "excludes": ["^.*.meta$", "^.*.mdown$", "^.*.pdf$","^.*.DS_Store$"],
    "linker_flags": []
    }
  

配置文件，XUporter根据该文件的配置设置xcodeproj  



##other

Assets\Plugins\iOS  Unity for iOS 扩展插件：

* sqlitehelper  sqlite3助手
* ZipArchive zip插件
* IPV6Adapter
* 其他OC接口
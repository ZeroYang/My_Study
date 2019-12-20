# iOS 启动图

##奇葩问题

1. 最近unity导出iOS打包，发现不放闪图，游戏启动不能全屏。。。。
2. ios launchimage 替换后，在已安装过app的设备上 启动图替换不成功(ios系统有缓存启动图，改名后就正常了)

## LaunchImage

这种方式需要提供各个设备的尺寸的image

*优点* 不会被拉伸

*缺点* 繁琐需要多张图片

## LaunchScreen

采用xcode提供的 xib或storyboard, 利用界面自动布局和约束条件，可以只用一张图片作为闪图， 闪图要支持裁减或平铺。

*优点* 资源少
*缺点* 显示图片会有被裁减和拉伸等情况

参考 [https://www.cnblogs.com/zk1947/p/11800917.html](https://www.cnblogs.com/zk1947/p/11800917.html)

##  共存

通过unity设置splash 导出的包中 是同时包含LaunchImage & LaunchScreen的

LaunchScreen~iPhone
LaunchScreen~iPad
针对不同设备提供不同的xib
# Mac 硬盘空间清理

Clean Mac 工具清理， 缺点：需要付费激活，但是可以通过他的扫描知道，那些文件目录下存在大量的垃圾文件

### 清理Xcode缓存文件

build和archive产生的垃圾文件

    /Users/username/Library/Developer/Xcode/DerivedData

    /Users/zwwx/Library/Developer/Xcode/Archives

### com.apple.DeveloperTools

Clean Mac 扫描发现 这个目录下居然有80多个G的缓存，我去。。。， 老子256G的硬盘一下少了3分之1。

    /private/var/folders/fh/rgpw0kkn36b2j1rpkr_ypbcc0000gn/C/com.apple.DeveloperTools/All/Xcode/EmbeddedAppDeltas

好家伙，这路径藏得够深的，这下面都是xcode打包APP，通过xcode联调安装到ios设备上的缓存。

不同Mac可能路径大同小异。

直接删除就行
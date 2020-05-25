# iOS UIWebView剔除



> ITMS-90809: Deprecated API Usage- New apps that use UIWebView are no longer accepted. Instead, use WKWebView for improved security and reliability. Learn more (https://developer.apple.com/documentation/uikit/uiwebview).

UIWebView已经废弃，有使用相关API的APP上架Appstore都不能上传成功，要我去掉，要么用WKWebView替换

## 检查项目中是否使用UIWebView

cd 项目根目录
    find . -type f | grep -e ".a" -e ".framework" | xargs grep -s UIWebView

## Unity项目中剔除UIWebView

Unity的新版本和持续维护的版本已经升级解决了UIWebView的问题；Unity的老版本不在维护，只能自己动手解决，下面是网上总结的方法。

用上面检查命令，检查导出Unity导出的iOS工程，发现libiPhone-lib.a中有使用UIWebView。下面是具体剔除操作：

* 查看lib的架构：  

    lipo -info libiPhone-lib.a 
Architectures in the fat file: libiPhone-lib.a are: armv7 arm64 armv7s 

* 提取arm64部分：  

    lipo libiPhone-lib.a -thin arm64 -output libiPhone-lib-arm64.a

将一下代码保存为URLUtility.mm，并引入到工程中

    #include <iostream>
    #import <UIKit/UIKit.h>

    using namespace std;

    namespace core {
        template <class type>
        class StringStorageDefault {};
        template <class type,class type2>
        class basic_string {
        public:
            char *c_str(void);
        };
    }

    void OpenURLInGame(core::basic_string< char,core::StringStorageDefault<char> > const&arg){}

    void OpenURL(core::basic_string<char,core::StringStorageDefault<char> >const&arg){
        const void *arg2= &arg;
        UIApplication *app = [UIApplication sharedApplication];
        NSString *urlStr = [NSString stringWithUTF8String:(char *)arg2];
        NSURL *url = [NSURL URLWithString:urlStr];
        [app openURL:url];
    }


    void OpenURL(std::string const&arg){
        UIApplication *app = [UIApplication sharedApplication];
        NSString *urlStr = [NSString stringWithUTF8String:arg.c_str()];
        NSURL *url = [NSURL URLWithString:urlStr];
        [app openURL:url];
        
    }

* 从lib库中剔除URLUtility.o   

    ar -d libiPhone-lib-arm64.a URLUtility.o

由于iOS系统基本上都是64位的了，我们只使用arm64的就够了。 删除原有的libiPhone-lib.a，然后引用libiPhone-lib-arm64.a。在打包，成功上传到ITunes connect。
//
//  IosInterface.m
//  Unity-iPhone
//
//  Created by lee han on 2/13/14.
//
//

#import "IosInterface.h"
#import "Reachability.h"
#import <CoreTelephony/CTTelephonyNetworkInfo.h>
#import <CoreTelephony/CTCarrier.h>
#import <AdSupport/ASIdentifierManager.h>


/*****************************************/
#if defined(__cplusplus)
extern "C" {
#endif

    unsigned short DogFlag( unsigned int count, unsigned int mask ) {
        if ( count > 0 ) {
            unsigned int flag = ((count * 30) * 0x00015CD7);
            flag = (flag) % (mask << 4);
            flag = flag ^ 7099;
            return (unsigned short)(flag & 0x0000FFFF);
        } else 
            return 0xFB;
    }

    //获取设备ios版本号
    float IOSGetVersion()
    {
        return [[[UIDevice currentDevice] systemVersion] floatValue];
    }
    
    //获取IDFA
    const char* IOSGetIMEI()
    {
        return [[[[ASIdentifierManager sharedManager] advertisingIdentifier] UUIDString] UTF8String];
    }
    
    ///获取运营商
    /*
     1.中国移动
     2.中国联通
     3.中国电信
     4.中国铁通
     5.未知运营商
     */
    const char* IOSGetCarriar()
    {
        NSString* networkstr = [NSString stringWithFormat:@"未知运营商"];
        //获取运营商信息
        CTTelephonyNetworkInfo* info = [[CTTelephonyNetworkInfo alloc] init];
        CTCarrier* carrier = info.subscriberCellularProvider;
        if (!carrier)
        {
            return networkstr.UTF8String;
        }
        if ([[carrier mobileCountryCode] length] < 3) {
            return networkstr.UTF8String;
        }
        NSString* mcc = [[carrier mobileCountryCode] substringWithRange:NSMakeRange(0, 3)];
        if ([[carrier mobileNetworkCode] length] < 2) {
            return networkstr.UTF8String;
        }
        int mnc = [[[carrier mobileNetworkCode] substringWithRange:NSMakeRange(0, 2)] intValue];
        if ([mcc isEqualToString:@"460"])
        {
            switch (mnc)
            {
                case 00:
                case 02:
                case 07:
                    networkstr = [NSString stringWithFormat:@"中国移动"];
                    break;
                case 01:
                case 06:
                    networkstr = [NSString stringWithFormat:@"中国联通"];
                    break;
                case 03:
                case 05:
                    networkstr = [NSString stringWithFormat:@"中国电信"];
                    break;
                case 20:
                    networkstr = [NSString stringWithFormat:@"中国铁通"];
                    break;
                default:
                    break;
            }
        }
        
        NSLog(@"IOSGetCarriar : %@", networkstr);
        return networkstr.UTF8String;
    }
    
    //获取imsi前五位
    const char* IOSGetIMSI()
    {
        CTTelephonyNetworkInfo* info = [[CTTelephonyNetworkInfo alloc] init];
        CTCarrier* carrier = info.subscriberCellularProvider;
        if (!carrier)
        {
            return "";
        }
        if ([[carrier mobileCountryCode] length] < 3) {
            return "";
        }
        NSString* mcc = [[carrier mobileCountryCode] substringWithRange:NSMakeRange(0, 3)];
        if ([[carrier mobileNetworkCode] length] < 2) {
            return "";
        }
        NSString* mnc = [[carrier mobileNetworkCode] substringWithRange:NSMakeRange(0, 2)];
        NSLog(@"IOSGetIMSI : %@", [mcc stringByAppendingString:mnc]);
        return [mcc stringByAppendingString:mnc].UTF8String;
    }

    //获取网络类型
    const char* IOSGetNetwork()
    {
        NSString* networkstr = [NSString stringWithFormat:@"未连接"];
        MyReachability* status = [MyReachability reachabilityWithHostName:@"www.baidu.com"];
        if ([status currentReachabilityStatus] == ReachableViaWiFi)
        {
            networkstr = [NSString stringWithFormat:@"wifi"];
        }
        else if ([status currentReachabilityStatus] == ReachableViaWWAN)
        {
            networkstr = [NSString stringWithFormat:@"3G"];
        }
        NSLog(@"IOSGetNetwork : %@", networkstr);
        return networkstr.UTF8String;
    }
    
//const char* readFile(const char* fileName,const char* zipFile) {
//    NSString *fileNameStr = [NSString stringWithUTF8String:fileName];
//    NSString *filePath = [NSString stringWithUTF8String:zipFile];
//    
//        if (0 == [fileNameStr length] || 0 == [filePath length])
//        {
//            return nil;
//        }
//        
//        BOOL isDir = NO;
//        NSFileManager *fileMgr = [NSFileManager defaultManager];
//        if (![fileMgr fileExistsAtPath:filePath isDirectory:&isDir]) {
//            NSLog(@"zipFile Not Exists!");
//        }
//        
////        NSAssert((0 != [fileNameStr length]), nil);
//    
//        ZipArchive *za = [[ZipArchive alloc] init];
//        
//        BOOL ret = [za UnzipOpenFile:filePath];
//        if (!ret)
//        {
//            return nil;
//        }
//        
//        NSData *data = nil;
//        ret = [za locateFileInZip:fileNameStr];
//        if (ret)
//        {
//            data = [za readCurrentFileInZip];
//        }
//        
//        [za UnzipCloseFile];
//        
//        NSString* text = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
////        NSLog(@"%@",text);
//        return text.UTF8String ;
//    }

    
#if defined(__cplusplus)
}
#endif
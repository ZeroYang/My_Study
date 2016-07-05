#import "CZipArchive+XZipArchive.h"

@interface ZipInterface : NSObject {

}

+(ZipInterface*) shareInstance;

//获取zip文件中的文件数
- (int) GetTotalCount;

//获取解压文件个数
- (int) GetExtractedCount;

- (BOOL) HasErr;

//解压zip到指定文件夹
- (BOOL) UnZipFile:(NSString*) zipFile To:(NSString*)dstDir;

- (BOOL) UnZipFileDoInThread:(NSString*) zipFile To:(NSString*)dstDir;

@end
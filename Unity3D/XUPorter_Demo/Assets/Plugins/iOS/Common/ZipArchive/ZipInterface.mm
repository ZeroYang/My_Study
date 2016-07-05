#import "ZipInterface.h"
#import "CZipArchive+XZipArchive.h"

#if defined(__cplusplus)
extern "C" {
#endif

extern int GetTotalCount()
{
    return [[ZipInterface shareInstance] GetTotalCount];
}

extern int GetExtractedCount()
{
    return [[ZipInterface shareInstance] GetExtractedCount];
}
    
extern bool HasError()
{
    return [[ZipInterface shareInstance] HasErr];
}

extern bool ExtractZipFile(const char* zipfile, const char* output_dir)
{
    return [[ZipInterface shareInstance] UnZipFileDoInThread:[NSString stringWithUTF8String:zipfile] To:[NSString stringWithUTF8String:output_dir]];
}

#if defined(__cplusplus)
}
#endif  

@interface ZipInterface()
{
    CZipArchive* _zip;
    BOOL _hasErr;
}

@end

static ZipInterface * instance = nil;

@implementation ZipInterface

+ (ZipInterface*) shareInstance {
    if (!instance) {
        instance = [[ZipInterface alloc] init];
    }
    return instance;
}

//获取zip文件中的文件数
- (int) GetTotalCount
{
    return [_zip GetTotalCount];
}

//获取解压文件个数
- (int) GetExtractedCount
{
    return [_zip GetExtractedCount];
}

- (BOOL) HasErr
{
    return _hasErr;
}

-(CZipArchive*)OpenZipFile:(NSString*) zipFile
{
    CZipArchive *zip = [[CZipArchive alloc] init];
    if([zip UnzipOpenFile:zipFile Password:@""])
    {
        _hasErr = false;
        return zip;
    }
    _hasErr = true;
    return nil;
}

- (BOOL) UnZipFileDoInThread:(NSString*) zipFile To:(NSString*)dstDir
{
    _zip = [self OpenZipFile:zipFile];
    if(_zip)
    {
        [NSThread detachNewThreadSelector:@selector(DoUnZipFile:) toTarget:self withObject:dstDir];
        return true;
    }
    return false;
}

- (BOOL) DoUnZipFile:(NSString *)dstDir
{
    BOOL success = [_zip UnzipFileTo:dstDir overWrite:YES];
    _hasErr = !success && [_zip UnzipCloseFile];
    return success && [_zip UnzipCloseFile];
    
}

//解压zip到指定文件夹
- (BOOL) UnZipFile:(NSString*) zipFile To:(NSString*)dstDir
{
    NSString *zipFilePath = zipFile;
    NSString *dstDirPath = dstDir;
    
    NSFileManager* fileMrg = [NSFileManager defaultManager];
    BOOL isExisted = NO;
    isExisted = [fileMrg fileExistsAtPath:zipFilePath];
    //zip文件不存在
    if (!isExisted)
    {
        NSLog(@"%@ file not exist!!", zipFile);
        _hasErr = true;
        return false;
    }
    
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentpath = ([paths count] > 0) ? [paths objectAtIndex:0] : nil;
    dstDirPath = [documentpath stringByAppendingPathComponent:dstDir];

    _zip = [[CZipArchive alloc] init];
    if([_zip UnzipOpenFile:zipFilePath Password:@""])
    {
        BOOL success = [_zip UnzipFileTo:dstDir overWrite:YES];
        _hasErr = !success && [_zip UnzipCloseFile];
        return success && [_zip UnzipCloseFile];
    }
    _hasErr = true;
    return false;

}

@end
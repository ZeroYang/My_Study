//
//  XZipArchive.m
//

#import "CZipArchive+XZipArchive.h"

#define FILE_IN_ZIP_MAX_NAME_LENGTH (256)

@implementation CZipArchive (XZipArchive)

- (BOOL) locateFileInZip:(NSString *)fileNameInZip
{
    BOOL ret = NO;

    int err = unzLocateFile(_unzFile, [fileNameInZip cStringUsingEncoding:NSUTF8StringEncoding], 1);
    if (UNZ_END_OF_LIST_OF_FILE == err)
    {
        return ret;
    }

    ret = (UNZ_OK == err);
    if (!ret)
    {
        NSLog(@"Error in locating the file in zip");
    }
    return ret;
}

- (NSData *) readCurrentFileInZip
{
    char filename_inzip[FILE_IN_ZIP_MAX_NAME_LENGTH];
    unz_file_info file_info;

    int err = unzGetCurrentFileInfo(_unzFile, &file_info, filename_inzip, sizeof(filename_inzip), NULL, 0, NULL, 0);
    if (UNZ_OK != err)
    {
        NSLog(@"Error in getting current file info");
        return nil;
    }

    // 打开当前文件
    err = unzOpenCurrentFilePassword(_unzFile, NULL);
    if (UNZ_OK != err)
    {
        NSLog(@"Error in opening current file");
        return nil;
    }

    NSString *fileNameInZip = [NSString stringWithCString:filename_inzip encoding:NSUTF8StringEncoding];
    NSUInteger fileUncompressedSize = file_info.uncompressed_size;
    NSMutableData *data = [NSMutableData dataWithLength:fileUncompressedSize];

    // 读取当前文件数据
    int bytes = unzReadCurrentFile(_unzFile, [data mutableBytes], (unsigned)[data length]);
    if (bytes < 0)
    {
        NSLog(@"Error in reading '%@' in zip", fileNameInZip);
        return nil;
    }
    [data setLength:bytes];

    // 关闭当前文件
    err = unzCloseCurrentFile(_unzFile);
    if (UNZ_OK != err)
    {
        NSLog(@"Error in  closing '%@' in zip", fileNameInZip);
    }

    return data;
}

//获取zip文件中的文件数
-(int) GetTotalCount
{
    return _totalCount;
}

- (int) GetExtractedCount
{
    return _extractCount;
}

@end

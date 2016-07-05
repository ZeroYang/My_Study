//
//  CZipArchive.h
//
//

#import "CZipArchive.h"

/**
	定义CZipArchive category.
	为CZipArchive添加新方法，以获取压缩包内指定的文件内容
 */
@interface CZipArchive (XZipArchive)

/**
	定位压缩包内的指定文件.
	该方法执行成功时，表明在压缩包内找到指定文件，且已经将指定文件变为当前文件.
	@param fileNameInZip 待定位的文件名称
	@returns 如果在压缩包内找到指定文件，返回YES,否则返回NO
 */
- (BOOL) locateFileInZip:(NSString *)fileNameInZip;

/**
	读取压缩包中当前文件的全部数据.
	@returns 成功时返回读取到的文件数据,失败时返回nil
 */
- (NSData *) readCurrentFileInZip;

//获取zip文件中的文件数
- (int) GetTotalCount;

- (int) GetExtractedCount;
@end

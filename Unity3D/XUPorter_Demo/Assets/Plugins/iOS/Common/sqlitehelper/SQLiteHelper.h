//
//  SQLiteHelper.h
//  Unity-iPhone
//
//  Created by zwwx on 16/3/17.
//
//

#import <Foundation/Foundation.h>

@interface SQLiteHelper : NSObject

+(NSString*)filePath:(NSString*)fileName;

-(BOOL)openDB:(NSString*)fileName;

@end

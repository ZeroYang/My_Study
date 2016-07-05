//
//  SQLiteHelper.m
//  Unity-iPhone
//
//  Created by zwwx on 16/3/17.
//
//

#import "SQLiteHelper.h"
#import "sqlite3.h"

#if defined(__cplusplus)
extern "C" {
#endif
    
    void F_LoadTextAsset(const char* f)
    {
    }
    
    bool ReadDll(const char* path, void *f)
    {
        return false;
    }
    
    sqlite3* openDataBase(const char* name, int flags)
    {
        sqlite3_config(SQLITE_CONFIG_MULTITHREAD);
        sqlite3 *database = NULL;
        NSString *path = [SQLiteHelper filePath:[NSString stringWithUTF8String:name]];
        int r = sqlite3_open_v2([path UTF8String], &database, flags, NULL);
        if ( r ==  SQLITE_OK )
            return database;
        else
            NSLog(@"open database:%@ errorcode:%d",[NSString stringWithUTF8String:name],r);
        return NULL;
    }
    
    bool closeDataBase(sqlite3 *database)
    {
        int r = sqlite3_close(database);
        if ( r ==  SQLITE_OK )
            return true;
        else
            NSLog(@"close database errorcode:%@",[NSString stringWithUTF8String:sqlite3_errmsg(database)]);
            return false;
    }
    
    int ExecuteNoQuery( sqlite3 *database, const char* query )
    {
        NSLog(@"ExecuteNoQuery here ==========");
        sqlite3_stmt *stmt = NULL;
        if(sqlite3_prepare_v2(database, query, -1, &stmt, NULL) != SQLITE_OK)
        {
            NSLog(@"ExecuteNoQuery error msg: %@",[NSString stringWithUTF8String:sqlite3_errmsg(database)]);
            NSCAssert(0, @"");
        }

        int r = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        if(r == SQLITE_DONE)
        {
            int rows_affected = sqlite3_changes(database);
            return rows_affected;
        }
        if(r == SQLITE_ERROR)
            NSLog(@"ExecuteNoQuery error msg: %@",[NSString stringWithUTF8String:sqlite3_errmsg(database)]);
        return -1;
    }
    
    sqlite3_stmt* Prepare( sqlite3 *database, const char* query)
    {
        sqlite3_stmt *stmt = NULL;
        int r = sqlite3_prepare_v2(database, query, -1, &stmt, NULL);
        if ( r ==  SQLITE_OK )
            return stmt;
        else
            NSLog(@"sqlite3 Prepare  error msg: %@",[NSString stringWithUTF8String:sqlite3_errmsg(database)]);
            //NSCAssert(0, @"sqlite3 Prepare error");
            return NULL;
    }
    
    bool Step(sqlite3_stmt *stmt)
    {
        int r = sqlite3_step(stmt);
        if ( r == SQLITE_DONE )
            return false;
        else if ( r == SQLITE_ROW ) {
            return true;
        } else {
            NSLog(@"sqlite3 step error");
            //NSCAssert(0, @"sqlite3 step error");
            return false;
        }
    }
    
#pragma mark -
#pragma mark sqlite3 func
    
    int sqlite3openv2(const char *filename,   /* Database filename (UTF-8) */
                      sqlite3 **ppDb,         /* OUT: SQLite db handle */
                      int flags,
                      const char *zVfs)
    {
        int mode = sqlite3_threadsafe();
        int ret = sqlite3_config(SQLITE_CONFIG_SINGLETHREAD);
        //NSLog(@"==========open database fileName:%@",[NSString stringWithUTF8String:filename]);
        return sqlite3_open_v2(filename,ppDb,flags,NULL);
    }
    
    int sqlite3enableloadextension(sqlite3 *db, int onoff)
    {
        return sqlite3_enable_load_extension(db, onoff);
    }
    
    int sqlite3_enable_load_extension(sqlite3 *db, int onoff)
    {
        return sqlite3_enable_load_extension(db, onoff);
    }
    
    int sqlite3close(sqlite3* db)
    {
        return sqlite3_close(db);
    }
    
    int sqlite3closev2(sqlite3* db)
    {

        return sqlite3_close_v2(db);
    }
    
    int sqlite3config(int option)
    {
        return sqlite3_config(option);
    }
    
    int sqlite3busytimeout(sqlite3 *db, int ms)
    {
        return sqlite3_busy_timeout(db, ms);
    }
    
    int sqlite3changes(sqlite3 *db)
    {
        return sqlite3_changes(db);
    }
    
    int sqlite3preparev2(  sqlite3 *db,            /* Database handle */
                         const char *zSql,       /* SQL statement, UTF-8 encoded */
                         int nByte,              /* Maximum length of zSql in bytes. */
                         sqlite3_stmt **ppStmt,  /* OUT: Statement handle */
                         const char **pzTail)
    {
        //NSLog(@"==========sqlite3preparev2 sql:%@",[NSString stringWithUTF8String:zSql]);
        return sqlite3_prepare_v2( db, zSql, nByte, ppStmt, pzTail);
    }
    
    
    int sqlite3step(sqlite3_stmt *stmt)
    {
        return sqlite3_step(stmt);
    }
    
    int sqlite3bindparameterindex( sqlite3_stmt *stmt, const char *zName )
    {
        return sqlite3_bind_parameter_index(stmt, zName);
    }
    
    int sqlite3bindnull( sqlite3_stmt *stmt, int index, int value )
    {
        return sqlite3_bind_null(stmt, index);
    }
    
    int sqlite3bindint( sqlite3_stmt *stmt, int index, int value )
    {
        return sqlite3_bind_int(stmt, index, value);
    }
    
    int sqlite3bindint64( sqlite3_stmt *stmt, int index, long value )
    {
        return sqlite3_bind_int64(stmt, index, value);
    }
    
    int sqlite3binddouble( sqlite3_stmt *stmt, int index, double value )
    {
        return sqlite3_bind_double(stmt, index, value);
    }

    int sqlite3bindtext( sqlite3_stmt* stmt, int index, const char* value, int size )
    {
        return sqlite3_bind_text(stmt, index, value, size, SQLITE_TRANSIENT);
    }
    
    int sqlite3bindtext16( sqlite3_stmt* stmt, int index, const void* value, int size )
    {
        return sqlite3_bind_text16(stmt, index, value, size, SQLITE_TRANSIENT);
    }
    
    int sqlite3bindblob( sqlite3_stmt* stmt, int index, const void* value, int size )
    {
        return sqlite3_bind_blob(stmt, index, value, size, SQLITE_TRANSIENT);
    }
    
    int sqlite3reset(sqlite3_stmt *stmt)
    {
        return sqlite3_reset(stmt);
    }
    
    int sqlite3finalize(sqlite3_stmt *stmt)
    {
        return sqlite3_finalize( stmt );
    }
    
    const char * sqlite3errmsg(sqlite3* db)
    {
        return sqlite3_errmsg(db);
    }
    
    const void * sqlite3errmsg16(sqlite3* db)
    {
        return sqlite3_errmsg16(db);
    }
    
    long sqlite3lastinsertrowid(sqlite3* db)
    {
        return sqlite3_last_insert_rowid(db);
    }

    int sqlite3columncount( sqlite3_stmt *stmt )
    {
        return sqlite3_column_count( stmt );
    }
    
    const char* sqlite3columnname( sqlite3_stmt *stmt, int index )
    {
        return sqlite3_column_name(stmt, index);
    }
    
    const void* sqlite3columnname16( sqlite3_stmt *stmt, int index )
    {
        return sqlite3_column_name16(stmt, index);
    }
    
    const char* ColumnValue( sqlite3_stmt *stmt, int index )
    {
        int ctype = sqlite3_column_type(stmt, index);
        NSString *str= @"";
        switch (ctype) {
            case SQLITE_INTEGER:
                str = [NSString stringWithFormat:@"%d",sqlite3_column_int(stmt, index)];
                break;
            case SQLITE_FLOAT:
                str = [NSString stringWithFormat:@"%f",sqlite3_column_double(stmt, index)];
                break;
            case SQLITE_TEXT:
                str = [NSString stringWithUTF8String:(char*)sqlite3_column_text(stmt, index)];
                break;
            case SQLITE_BLOB:
            case SQLITE_NULL:
                break;
            default:
                break;
        }
        
        return [str UTF8String];
    }
    
    int sqlite3columntype(sqlite3_stmt *stmt, int index)
    {
        return sqlite3_column_type(stmt, index);
    }
    
    int sqlite3columnint(sqlite3_stmt *stmt, int index)
    {
        return sqlite3_column_int(stmt, index);
    }
    
    long sqlite3columnint64(sqlite3_stmt *stmt, int index)
    {
        return sqlite3_column_int64(stmt, index);
    }
    
    double sqlite3columndouble(sqlite3_stmt *stmt, int index)
    {
        return sqlite3_column_double(stmt, index);
    }
    
    const unsigned char* sqlite3columntext(sqlite3_stmt *stmt, int index)
    {
        return sqlite3_column_text(stmt, index);
    }
    
    const void* sqlite3columntext16(sqlite3_stmt *stmt, int index)
    {
        return sqlite3_column_text16(stmt, index);
    }
    
    const void* sqlite3columnblob(sqlite3_stmt *stmt, int index)
    {
        return sqlite3_column_blob(stmt, index);
    }
    
    int sqlite3columnbytes(sqlite3_stmt *stmt, int index)
    {
        return sqlite3_column_bytes(stmt, index);
    }
    
#if defined(__cplusplus)
}
#endif

@interface SQLiteHelper ()
{
    sqlite3 *_db;
}
@end

@implementation SQLiteHelper

+(NSString*)filePath:(NSString*)fileName
{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory,NSUserDomainMask, YES);
    NSString *documentsDir = [paths objectAtIndex:0];
    return [documentsDir stringByAppendingPathComponent:fileName];
}


-(BOOL)openDB:(NSString*)fileName
{
    if (sqlite3_open([fileName  UTF8String], &_db) != SQLITE_OK) {
        sqlite3_close(_db);
        return FALSE;
    }
    return TRUE;
}
/*
-(void)closeDB:(sqlite3*)db
{
    sqlite3_close(db);
}

-(BOOL)execSQL:(NSString*)sql
{
    char *err;
    if(sqlite3_exec(_db, [sql UTF8String], NULL, NULL, &err) != SQLITE_OK) {
        NSLog(@"execSQL %@,error:%@",sql,[NSString stringWithUTF8String:err]);
        return  FALSE;
    }
    return TRUE;
    
//    //方法1：经典方法
//     NSString *sql = [NSString stringWithFormat:@"INSERT INTO '%@' ('%@', '%@', '%@') VALUES('%@', '%@', '%@')", tableName, field1, field2, field3, field1Value, field2Value, field3Value];
//     char *err;
//     if (sqlite3_exec(db, [sql UTF8String], NULL, NULL, &err) != SQLITE_OK) {
//     sqlite3_close(db);
//     NSAssert(0, @"插入数据错误！");
//     }
//    //方法2：变量的绑定方法
//    NSString *sql = [NSString stringWithFormat:@"INSERT INTO '%@' ('%@', '%@', '%@') VALUES (?, ?, ?)",tableName, field1, field2, field3];
//    
//    sqlite3_stmt *statement;
//    
//    if (sqlite3_prepare_v2(db, [sql UTF8String], -1, &statement, nil) == SQLITE_OK) {
//        sqlite3_bind_text(statement, 1, [field1Value UTF8String], -1,NULL);
//        sqlite3_bind_text(statement, 2, [field2Value UTF8String], -1,NULL);
//        sqlite3_bind_text(statement, 3, [field3Value UTF8String], -1,NULL);
//    }
//    if (sqlite3_step(statement) != SQLITE_DONE) {
//        NSAssert(0, @"插入数据失败！");
//        sqlite3_finalize(statement);
//    }
}

-(int)getColumnCount
{
    sqlite3_stmt *stmt;
    return sqlite3_column_count(stmt);;
}

-(const char*)getColumnName
{
    sqlite3_stmt *stmt;
    int index;
    return sqlite3_column_name(stmt, index);
}
*/
@end

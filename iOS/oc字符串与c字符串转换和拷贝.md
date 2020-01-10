#oc字符串与c字符串转换和拷贝

	// Helper method to create C string copy
	NSString* MakeNSString (const char* string) {
	    if (string) {
	        return [NSString stringWithUTF8String: string];
	    } else {
	        return [NSString stringWithUTF8String: ""];
	    }
	}

	char* MakeCString(NSString *str) {
	    const char* string = [str UTF8String];
	    if (string == NULL) {
	        return NULL;
	    }
	
	    char* res = (char*)malloc(strlen(string) + 1);
	    strcpy(res, string);
	    return res;
	}
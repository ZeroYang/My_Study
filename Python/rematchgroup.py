 #!/usr/bin/python
import re

line = "Cats are smarter than dogs";

searchObj = re.search( r'(.*) are (.*?) .*', line, re.M|re.I)

print searchObj.span()
if searchObj:
   print "searchObj.group() : ", searchObj.group()
   print "searchObj.group(1) : ", searchObj.group(1)
   print "searchObj.group(2) : ", searchObj.group(2)
   print "searchObj.groups() : ", searchObj.groups()
   #print "searchObj.group(4) : ", searchObj.group(4)
else:
   print "Nothing found!!"

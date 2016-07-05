# -*- coding: UTF-8 -*-
from optparse import OptionParser


word = "你好"
print "你好"
print word

#str = raw_input("请输入：")
#print "你输入的内容是：",str

def copyipa(srcfile,dstdir):
    import shutil
    import os.path
    filename = os.path.basename(srcfile)
    print filename
    dstfile = os.path.join(dstdir,filename)
    print dstfile
    shutil.copy(srcfile,dstfile)

from ftplib import FTP
def ftp_up(filename="E:\sqlite3.exe"):
    ftp = FTP()
    ftp.set_debuglevel(2)
    ftp.connect('192.168.1.10','21')
    #ftp.login('admin','admin')
    ftp.cwd('SGclient\Y杨田波\未命名文件夹')
    bufsize = 1024
    file_handler = open(filename,'rb')
    ftp.storbinary('STOR %s' % os.path.basename(filename),file_handler,bufsize)
    ftp.set_debuglevel(0)
    ftp_handler.close()
    ftp.quit()
    print "ftp up ok"

def WinCommand():
    import os
    cmd = 'cmd.exe /k ping www.baidu.com'
    os.system(cmd)

def xcbuild(options):
    workspace = options.workspace

    if workspace is None:
        pass
    else:
        print "workspace: %s" % (workspace)

def main():
    parser = OptionParser()
    parser.add_option("-w", "--workspace", help="Build the workspace name.xcworkspace.", metavar="name.xcworkspace")
    (options, args) = parser.parse_args()
    print "options: %s,args: %s" % (options, args)
    xcbuild(options)
    #WinCommand()

if __name__ == '__main__':
    main()
    #ftp_up()
    copyipa('E:\sqlite3.exe','F:/test')

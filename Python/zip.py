#coding=utf-8
#使用zipfile压缩指定目录文件夹

import os,os.path
import zipfile

def zip_dir(dirname,zipfilename):
    filelist = []
    if os.path.isfile(dirname):
        filelist.append(dirname)
    else:
        for root, dirs,files in os.walk(dirname):
            for name in files:
                filelist.append(os.path.join(root,name))

    zf = zipfile.ZipFile(zipfilename, "w", zipfile.zlib.DEFLATED)
    for tar in filelist:
        arcname = tar[len(dirname):]
        #print arcname
        zf.write(tar,arcname)
    zf.close()


from zipfile import ZipFile
from os import listdir
from os.path import isfile, isdir, join

def addFileIntoZipfile(srcDir, fp):
    print "srcDir:" srcDir
    if os.path.isfile(srcDir):
        fp.write(srcDir)
    for subpath in listdir(srcDir):
        subpath = join(srcDir, subpath)
        if isfile(subpath):
            fp.write(subpath)
        elif isdir(subpath):
            fp.write(subpath)
            addFileIntoZipfile(subpath, fp)

def zipCompress(srcDir, desZipfile):
    fp = ZipFile(desZipfile, mode='a')
    addFileIntoZipfile(srcDir, fp)
    fp.close()

if __name__ == '__main__':
    paths = [r'1',r'2',r'3.png']
    #zip_dir(r'D:/Screenshots',r'D:/Screenshots/0.zip')
    homedir = os.getcwd()
    print homedir
    zipfilename = '0.zip'
    zipfilepath = os.path.join(homedir,zipfilename)
    print zipfilepath
    for path in paths:
        fullpath = os.path.join(homedir,path)
        #print fullpath
        #zip_dir(os.path.join(homedir,path),os.path.join(homedir,zipfilename))
        zipCompress(fullpath,zipfilepath)

import os
homedir = os.getcwd();

os.system('cat /proc/cpuinfo')

output = os.poen('cat /proc/cpuinfo')
print output.read()

(status,output) = commands.getstatusoutput('cat /proc/cpuinfo')
print status,output

def UpdateSvn():  
        strExec = "svn up ";  
        print("cmd:%s" %strExec);  
  	process = subprocess.Popen(cleanCmd, shell = True)
	process.wait()
        #nRet = os.system(strExec);  
        #print("nRet = %d" %nRet);  
  
        return (0 == nRet);  
  
if "__main__" == __name__:  
        bUp = UpdateSvn();  
  
        if bUp:  
                print("svn up succ!");  
        else:  
                print("svn up failed!");  

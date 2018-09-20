@echo off  
choice /C dme /M "defrag,mem,end" 
if errorlevel 3 goto end 
if errorlevel 2 goto mem 
if errorlevel 1 goto defrag 

:defrag  
c:\dos\defrag  
goto end  
:mem  
mem  
goto end  
:end  
echo good bye 
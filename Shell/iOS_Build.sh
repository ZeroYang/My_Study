#!/bin/bash
echo "Clientbuild-----------------------"
#update Clientbuild
svn update
#获取当前dir
Cur_Dir=$(pwd)
echo $Cur_Dir
res_path="${Cur_Dir}/Assets/StreamingAssets/res/"
echo $res_path
#update res
svn update $res_path

rm -r "${Cur_Dir}/Assets/Plugins/Android"
rm -r "${Cur_Dir}/Assets/Plugins/x86"
rm -r "${Cur_Dir}/Assets/Plugins/x86_64"

#拷贝NGUI.dll
cp "${Cur_Dir}/NGUI/IOS/NGUI.dll" "${Cur_Dir}/Assets/NGUI/NGUI.dll"

echo "复制成功"
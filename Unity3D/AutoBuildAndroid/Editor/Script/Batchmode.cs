using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
 
/// <summary>
/// 命令行批处理工具类
/// </summary>
public class Batchmode {
 
    static List<string> levels = new List<string>();
 
    public static void BuildAndroid() {

        string buildJsonFile = Directory.GetCurrentDirectory() + @"\build.json";
        Debug.Log(buildJsonFile);
        if (!File.Exists(buildJsonFile))
            throw new Exception("Not find build.json file");

        StreamReader sr = File.OpenText(buildJsonFile);
        string text = sr.ReadToEnd().Trim();
        JsonData cfg = JsonMapper.ToObject(text);
       
        PlayerSettings.companyName = (string)cfg["companyName"];
        PlayerSettings.productName = (string)cfg["productName"];

        PlayerSettings.bundleIdentifier = (string)cfg["bundleIdentifier"];
        PlayerSettings.bundleVersion = (string)cfg["bundleVersion"];
        PlayerSettings.Android.bundleVersionCode = (int)cfg["bundleVersionCode"];

        PlayerSettings.Android.keystoreName = (string)cfg["keystoreName"];
        PlayerSettings.Android.keystorePass = (string)cfg["keystorePass"];
        PlayerSettings.Android.keyaliasName = (string)cfg["keyaliasName"];
        PlayerSettings.Android.keyaliasPass = (string)cfg["keyaliasPass"];

        #region 默认设置
        PlayerSettings.Android.showActivityIndicatorOnLoading = AndroidShowActivityIndicatorOnLoading.DontShow;
        PlayerSettings.Android.splashScreenScale = AndroidSplashScreenScale.Center;
        PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.PreferExternal;
        PlayerSettings.Android.targetDevice = AndroidTargetDevice.ARMv7;
        PlayerSettings.Android.forceInternetPermission = true;
        PlayerSettings.Android.forceSDCardPermission = true;
        #endregion

        string apkName = (string)cfg["apkName"];

        foreach ( EditorBuildSettingsScene scene in EditorBuildSettings.scenes ) {
            if ( !scene.enabled ) continue;
            levels.Add( scene.path );
        }
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        string res = BuildPipeline.BuildPlayer(levels.ToArray(), apkName, BuildTarget.Android, BuildOptions.None);
        if (res.Length > 0)
            throw new Exception("BuildPlayer failure: " + res);
    }
}

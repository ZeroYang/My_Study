using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

public class AndroidBuildConfig {

    public class Platform {
        public string name;
        public string sdk;
        public string splash;
        public string keystoreName;
        public string keystorePass;
        public string keyaliasName;
        public string keyaliasPass;
        public List<string> macros;
    }

    public string sdkFolder;
    public string keysotoreFolder;
    public string andriodTempFile;
    public string projectSplash;
    public List<Platform> platforms;
}

public class AndroidBuilder {

    public static AndroidBuildConfig configs;

    public static AndroidBuildConfig LoadConfig() {
        TextAsset txt = Resources.Load("android_build") as TextAsset;
        if(txt == null) { MyDebug.Log("not found android_build file"); return null; }
        configs = null;
        try {
            configs = LitJson.JsonMapper.ToObject<AndroidBuildConfig>(txt.text);
        } catch(Exception e) {
            MyDebug.Log("android_build file error -> " + e);
        }
        return configs;
    }

    public static void Build(AndroidBuildConfig.Platform p, string outFile = null) {
        if(configs == null)
            throw new Exception("请先加载配置");
        //CheckConfigError();
        //PreProcessBuildFile(p);
        SetBuildParam(p);

        if(string.IsNullOrEmpty(outFile))
            CompileAPK(p, GetSavePath(p));
        else
            CompileAPK(p, outFile);
    }

    public static void CopyDirectory(string srcDir, string tgtDir) {
        DirectoryInfo source = new DirectoryInfo(srcDir);
        DirectoryInfo target = new DirectoryInfo(tgtDir);
        if(target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("父目录不能拷贝到子目录！");
        if(!source.Exists)
            throw new Exception("src file is not exist");
        if(!target.Exists)
            target.Create();

        FileInfo[] files = source.GetFiles();
        for(int i = 0; i < files.Length; i++)
            File.Copy(files[i].FullName, target.FullName + @"\" + files[i].Name, true);
        DirectoryInfo[] dirs = source.GetDirectories();
        for(int j = 0; j < dirs.Length; j++)
            CopyDirectory(dirs[j].FullName, target.FullName + @"\" + dirs[j].Name);
    }

    public static string GetPackageName() {
        XmlDocument doc = new XmlDocument();
        doc.Load(Application.dataPath + "/Plugins/Android/AndroidManifest.xml");
        XmlNode node = doc.SelectSingleNode("/manifest");
        return node.Attributes["package"].Value;
    }

    public static string GetDefaultAPKName(AndroidBuildConfig.Platform p) {
        string apkName = "sgzs";
        string time = DateTime.Now.Year.ToString()
            + DateTime.Now.Month.ToString("00")
            + DateTime.Now.Day.ToString("00")
            + DateTime.Now.Hour.ToString("00")
            + DateTime.Now.Minute.ToString("00");
        string version = string.Format("v{0}", PlayerSettings.Android.bundleVersionCode);
        apkName = "sgzs_" + time + "_" + version + "_" + p.name;
        return apkName;
    }

    public static string GetSavePath(AndroidBuildConfig.Platform p) {
        string apkName = AndroidBuilder.GetDefaultAPKName(p);
        string targetDir = EditorUtility.SaveFilePanel( "请选择发布安卓包目录", Directory.GetCurrentDirectory(), GetDefaultAPKName(p), "" );
        targetDir = targetDir.Replace(".apk", "");
        return targetDir;
    }

    #region private 
    private static void CheckConfigError() {
        DirectoryInfo sdkRootFolder = new DirectoryInfo(configs.sdkFolder);
        DirectoryInfo androidTempFile = new DirectoryInfo(configs.andriodTempFile);
        if(!sdkRootFolder.Exists)
            throw new Exception("需要先设置SDK总目录");
        if(!androidTempFile.Exists)
            throw new Exception("需要设置Android标准文件目录");
    }

    private static void PreProcessBuildFile(AndroidBuildConfig.Platform p) {
        AssetDatabase.Refresh();
        AssetDatabase.DeleteAsset("Assets/Plugins/Android");
        AssetDatabase.Refresh();
        try {
            CopyDirectory(configs.andriodTempFile, Application.dataPath + "/Plugins/Android");
            CopyDirectory(configs.sdkFolder + "/" + p.sdk, Directory.GetParent(Application.dataPath).FullName);
        } catch(Exception e) {
            MyDebug.Log("copy error -> " + e);
        }
        FileInfo android_support_v4 = new FileInfo(Application.dataPath + "/Plugins/Android/libs/android-support-v4.jar");
        FileInfo android_support_v13 = new FileInfo(Application.dataPath + "/Plugins/Android/libs/android-support-v13.jar");
        if(android_support_v4.Exists && android_support_v13.Exists) {
            AssetDatabase.DeleteAsset("Assets/Plugins/Android/libs/android-support-v4.jar");
        }
        AssetDatabase.Refresh();
    }

    public static void SetBuildParam(AndroidBuildConfig.Platform p) {
        PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Enabled;
        string pName = PlayerSettings.bundleIdentifier;
        AssetDatabase.Refresh();
        if(!string.IsNullOrEmpty(p.splash))
            File.Copy(Directory.GetParent(Application.dataPath).FullName + "/" + p.splash, Directory.GetParent(Application.dataPath).FullName + "/" + configs.projectSplash, true);
        PlayerSettings.bundleIdentifier = GetPackageName();
        PlayerSettings.Android.keystoreName = configs.keysotoreFolder + "/" + p.keystoreName;
        PlayerSettings.Android.keystorePass = p.keystorePass;
        PlayerSettings.Android.keyaliasName = p.keyaliasName;
        PlayerSettings.Android.keyaliasPass = p.keyaliasPass;

        //string[] defines = new string[p.macros.Count];
        //for(int i = 0; i < p.macros.Count; i++)
        //    defines[i] = "-define:" + p.macros[i];
        //File.WriteAllLines(Application.dataPath + "/smcs.rsp", defines, Encoding.UTF8);

        AssetDatabase.Refresh();
    }

    private static void CompileAPK(AndroidBuildConfig.Platform p, string savePath) {
        if(string.IsNullOrEmpty(savePath))
            return;
        List<string> EditorScenes = new List<string>();
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
            if(!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        string res = BuildPipeline.BuildPlayer(
                        EditorScenes.ToArray(),
                        string.Format("{0}.apk", savePath),
                        BuildTarget.Android,
                        BuildOptions.None);
        if(res.Length > 0)       // 这里最后加入一下打包日志，得到哪些包成功哪些失败
            MyDebug.Log(string.Format("build {0} error -> {1}", p.name, res));
    }
    #endregion
}

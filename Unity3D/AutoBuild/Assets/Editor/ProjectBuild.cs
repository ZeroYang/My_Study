using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ProjectBuild : MonoBehaviour {

    [MenuItem("Assets/UI相关/BuildTex")]
    static void BuildResources()
    {
        if (UnityEditor.EditorUserBuildSettings.activeBuildTarget != BuildTarget.iPhone)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iPhone);
        }
        BuildForIPhone();
    }

    [MenuItem("Assets/GetAssetFromBundle")]
    static void GetAssetFromBundle()
    {
        //string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        string path = "file://" + Application.dataPath + "/StreamingAssets/res/resources.assets"; 
        Debug.Log("Selected path: " + path);
        WWW bundle = new WWW(path);
        Object[] objs = bundle.assetBundle.LoadAll();
        foreach(Object obj in objs)
        {
            Debug.Log("obj name: " + obj.name);
        }
    }

    [MenuItem("Asset/Build AssetBundles From Directory of Files")]
    static void ExportAssetBundles()
    {
        // Get the selected directory
        //获取选择的目录
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        Debug.Log("Selected Folder: " + path);
        if (path.Length != 0)
        {
            path = path.Replace("Assets/", "");
            string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);
            List<Object> assets = new List<Object>();
            string bundlePath = "Assets/" + path + "/" + "resources" + ".unity3d";
            foreach (string fileName in fileEntries)
            {
                //string filePath = fileName;
                string filePath = fileName.Replace("\\", "/");
                int index = filePath.LastIndexOf("/");
                filePath = filePath.Substring(index);
                Debug.Log(filePath);
                string localPath = "Assets/" + path;
                if (index > 0)
                    localPath += filePath;
                Object t = AssetDatabase.LoadMainAssetAtPath(localPath);
                if (t != null)
                {
                    Debug.Log(t.name);
                    assets.Add(t);
                }
            }
            BuildPipeline.BuildAssetBundle
(null, assets.ToArray(), bundlePath, BuildAssetBundleOptions.CompleteAssets);
        }
    }

    static void BuildRes() { }

    static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();
 
		foreach(EditorBuildSettingsScene e in EditorBuildSettings.scenes)
		{
			if(e==null)
				continue;
			if(e.enabled)
				names.Add(e.path);
		}
		return names.ToArray();
    }

    //得到项目的名称
    public static string projectName
    {
        get
        {
            //在这里分析shell传入的参数， 还记得上面我们说的哪个 project-$1 这个参数吗？
            //这里遍历所有参数，找到 project开头的参数， 然后把-符号 后面的字符串返回，
            //这个字符串就是 91 了。。
            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("project"))
                {
                    return arg.Split("-"[0])[1];
                }
            }
            return "test";
        }
    }

    static void BuildForIPhone() {
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子， 这里取到的就是 shell传进来的字符串 91
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), projectName, BuildTarget.iPhone, BuildOptions.None);
    }
}

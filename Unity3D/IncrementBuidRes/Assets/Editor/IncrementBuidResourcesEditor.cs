/// <summary>
/// 增量打包资源工具
/// </summary>
/// <returns></returns>
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class IncrementBuidResourcesEditor : Editor {
    [MenuItem ("Assets/UI相关/增量打包游戏AB资源")]
    public static void Execute () {
        string ui_path = Application.dataPath + "/Resources/UIPrefab";
        string rawimage_path = Application.dataPath + "/Resources/UI/RawImage";
        string texture_path = Application.dataPath + "/Resources/UI/Textures";
        string fonts_path = Application.dataPath + "/Resources/UI/BMfont";
        string icons_path = Application.dataPath + "/Resources/UI/Icon/icons";
        string icons2_path = Application.dataPath + "/Resources/UI/Icon/icons2";

        List<string> build_path = new List<string> () {
            ui_path,
            rawimage_path,
            texture_path,
            fonts_path,
            icons_path,
            icons2_path
        };

#if UNITY_IPHONE
        build_path = new List<string> () {
            ui_path,
            rawimage_path,
            texture_path,
            fonts_path,
            icons_path,
            icons2_path
        };
#endif

        List<string> files = new List<string> ();
        List<string> rebuildFiles = new List<string> ();
        List<ResourceSetting.ResourceInfo> resinfos = new List<ResourceSetting.ResourceInfo>();

        //更新资源信息
        for (int i = 0; i < build_path.Count; i++) {
            files.AddRange (GetFiles (build_path[i]));
        }

        string resourceSetting = "Assets/ResourceSetting.asset";
        ResourceSetting sd = null;
        if (File.Exists (Application.dataPath + resourceSetting.Replace ("Assets", ""))) {
            sd = AssetDatabase.LoadMainAssetAtPath (resourceSetting) as ResourceSetting;
        } else {
            sd = ScriptableObject.CreateInstance<ResourceSetting> ();
            for (int i = 0; i < files.Count; i++) {
                string p = files[i].Replace (Application.dataPath, "Assets");
                sd.Add (files[i]);
            }
            AssetDatabase.CreateAsset (sd, resourceSetting);
            return;
        }

        //增量打Ab资源
        int rebuildcount = 0;
        string relativePath = fonts_path.Replace (Application.dataPath, "Assets");
        string[] depances = AssetDatabase.GetDependencies (new string[] { relativePath });

        //build uis 
        List<string> uis = GetFiles (ui_path);
        for (int i = 0; i < uis.Count; i++) {
            FileInfo fi = new FileInfo (uis[i]);
            if (sd.isModified (uis[i])) {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (uis[i].Replace (Application.dataPath, "Assets"), typeof (UnityEngine.Object));
                if (obj == null) {
                    //Debug.LogError ("resource is null.--->" + uis[i]);
                    continue;
                }
                Debug.Log ("rebuild ui.--->" + uis[i]);
                rebuildcount++;
                MOYU_UIToolsEditor.BuildUI(obj as GameObject, false);
                rebuildFiles.Add (uis[i]);
            }
        }

        //build rawimage
        List<string> rawimages = GetFiles (rawimage_path);
        for (int i = 0; i < rawimages.Count; i++) {
            FileInfo fi = new FileInfo (rawimages[i]);
            if (sd.isModified (rawimages[i])) {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (rawimages[i].Replace (Application.dataPath, "Assets"), typeof (Texture));
                if (obj == null) {
                    //Debug.LogError ("resource is null.--->" + rawimages[i]);
                    continue;
                }
                Debug.Log ("rebuild rawimage tex.--->" + rawimages[i]);
                rebuildcount++;
                MOYU_UIToolsEditor.BuildTex(obj as Texture);
                rebuildFiles.Add (rawimages[i]);
            }
        }

        //build tex
        List<string> textures = GetFiles (texture_path);
        for (int i = 0; i < textures.Count; i++) {
            FileInfo fi = new FileInfo (textures[i]);
            if (sd.isModified (textures[i])) {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (textures[i].Replace (Application.dataPath, "Assets"), typeof (Texture));
                if (obj == null) {
                    //Debug.LogError ("resource is null.--->" + textures[i]);
                    continue;
                }
                Debug.Log ("rebuild tex.--->" + textures[i]);
                rebuildcount++;
                MOYU_UIToolsEditor.BuildTex(obj as Texture);
                rebuildFiles.Add (textures[i]);
            }
        }

        //build font
        List<string> fonts = GetFiles (fonts_path);
        for (int i = 0; i < fonts.Count; i++) {
            FileInfo fi = new FileInfo (fonts[i]);
            if (sd.isModified (fonts[i])) {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (fonts[i].Replace (Application.dataPath, "Assets"), typeof (Font));
                if (obj == null) {
                    //Debug.LogError ("resource is null.--->" + fonts[i]);
                    continue;
                }
                Debug.Log ("rebuild font.--->" + fonts[i]);
                rebuildcount++;
                MOYU_UIToolsEditor.BuildFont(obj as Font);
                rebuildFiles.Add (fonts[i]);
            }
        }

        //build icons
        List<string> icons = GetFiles (icons_path);
        for (int i = 0; i < icons.Count; i++) {
            FileInfo fi = new FileInfo (icons[i]);
            if (sd.isModified (icons[i])) {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (icons[i].Replace (Application.dataPath, "Assets"), typeof (Texture));
                if (obj == null) {
                    //Debug.LogError ("resource is null.--->" + icons[i]);
                    continue;
                }
                Debug.Log ("rebuild icons.--->" + icons[i]);
                rebuildcount++;
                MOYU_UIToolsEditor.BuildAsset(obj, "tex", "Assets/StreamingAssets/res/ui/tex/icons");
                rebuildFiles.Add (icons[i]);
            }
        }

        //build icons2
        List<string> icons2 = GetFiles (icons2_path);
        for (int i = 0; i < icons2.Count; i++) {
            FileInfo fi = new FileInfo (icons2[i]);
            if (sd.isModified (icons2[i])) {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (icons2[i].Replace (Application.dataPath, "Assets"), typeof (Texture));
                if (obj == null) {
                    //Debug.LogError ("resource is null.--->" + icons2[i]);
                    continue;
                }
                Debug.Log ("rebuild icons2.--->" + icons2[i]);
                rebuildcount++;
                MOYU_UIToolsEditor.BuildAsset(obj, "tex", "Assets/StreamingAssets/res/ui/tex/icons2");
                rebuildFiles.Add (icons2[i]);
            }
        }

        //更新资源信息
        //删除设置中无用的信息
        for (int i = sd.infos.Count - 1; i >= 0; i--) {
            bool hasdata = false;
            for (int j = 0; j < files.Count; j++) {
                if (Application.dataPath + sd.infos[i].RelativePath != files[j]) {
                    hasdata = true;
                    break;
                }
            }
            if (!hasdata) {
                sd.Remove (Application.dataPath + sd.infos[i].RelativePath);
            }
        }

        //更新信息（添加或修改）
        for (int i = 0; i < files.Count; i++) {
            string p = files[i].Replace (Application.dataPath, "Assets");
            sd.Add (files[i]);
        }

        ResourceSetting sdt = ScriptableObject.CreateInstance<ResourceSetting> ();
        sdt.infos.AddRange (sd.infos);
        AssetDatabase.DeleteAsset (resourceSetting);
        AssetDatabase.CreateAsset (sdt, resourceSetting);

        //log
        StringBuilder rebuildBuilder = new StringBuilder ();
        for(int i = 0;i < rebuildFiles.Count;i++){
            rebuildBuilder.AppendLine(string.Format("{0}、rebuild assetbundle:{1}",i,rebuildFiles[i]));
        }
        Debug.Log (string.Format ("rebuild success.count={0} ----->\n{1}", rebuildFiles.Count, rebuildBuilder.ToString ()));
        AssetDatabase.Refresh ();
    }

    /// <summary>
    /// full path
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    private static List<string> GetFiles (string dic) {
        string[] fileNames = Directory.GetFiles (dic);
        string[] directories = Directory.GetDirectories (dic);

        List<string> files = new List<string> ();
        files.AddRange (fileNames);
        for (int i = 0; i < directories.Length; i++) {
            //files.AddRange(Directory.GetFiles(directories[i]));
            if (directories[i].Contains (".svn"))
                continue;
            files.AddRange (GetFiles (directories[i]));
        }

        for (int i = files.Count - 1; i >= 0; i--) {
            if (files[i].EndsWith (".meta")) {
                files.Remove (files[i]);
                continue;
            }

            files[i] = files[i].Replace ("\\", "/");
        }

        return files;
    }
}
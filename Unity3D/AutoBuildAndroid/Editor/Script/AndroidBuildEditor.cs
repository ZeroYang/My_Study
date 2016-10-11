using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

public class AndroidBuildEditor : EditorWindow {

    AndroidBuildConfig configs;
    string apkOutPath = "";
    public static List<bool> selections = new List<bool>();
    public Vector2 scrollPosition;

    [MenuItem("ZWWX/AndroidBuilder")]
    public static void OpenUI() {
        AndroidBuildEditor wnd = EditorWindow.GetWindow<AndroidBuildEditor>();
        wnd.configs = AndroidBuilder.LoadConfig();
        if(wnd.configs == null)
            return;
        for(int i = 0; i < wnd.configs.platforms.Count; ++i)
            selections.Add(true);
        wnd.Show();
    }

    void OnGUI() {

        if(configs == null)
            return;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        #region File Option
        GUILayout.BeginHorizontal("box");
        GUILayout.Label( string.Format( "输出目录 ->  {0}", apkOutPath ), GUILayout.MaxWidth( 270 ) );
        if(GUILayout.Button("...", GUILayout.MaxWidth(30))) {
            string tempPath;
            tempPath = EditorUtility.SaveFolderPanel("请选择发布安卓包目录", Directory.GetCurrentDirectory(), "");
            if(string.IsNullOrEmpty(tempPath))
                return;
            apkOutPath = tempPath;
        }
        GUILayout.EndHorizontal();
        #endregion

        #region Batch Build Option
        GUILayout.BeginHorizontal("box");
        if(GUILayout.Button("全选", GUILayout.MaxWidth(40), GUILayout.MaxHeight(30), GUILayout.MinHeight(30))) {
            for(int i=0; i<selections.Count; ++i) selections[i] = true;
        }
        if(GUILayout.Button("取消", GUILayout.MaxWidth(40), GUILayout.MaxHeight(30), GUILayout.MinHeight(30))) {
            for(int i = 0; i < selections.Count; ++i) selections[i] = false;
        }
        if ( GUILayout.Button( "开始打包(禁用)", GUILayout.MaxWidth( 100 ), GUILayout.MinHeight( 30 ), GUILayout.MaxHeight( 30 ) ) ) {
            for(int i = 0; i < selections.Count; ++i) {
                AndroidBuildConfig.Platform p = configs.platforms[i];
                if(selections[i]) {
                    if(!string.IsNullOrEmpty(apkOutPath))
                        AndroidBuilder.Build(p, apkOutPath + "/" +AndroidBuilder.GetDefaultAPKName(p));
                    else
                        throw new Exception("先选择输出目录");
                }
            }
        }
        GUILayout.EndHorizontal();
        #endregion

        #region Single Build Option
        for(int i = 0; i < configs.platforms.Count; i++) {
            AndroidBuildConfig.Platform p = configs.platforms[i];
            GUILayout.BeginHorizontal("box");
            selections[i] = GUILayout.Toggle(selections[i], "", GUILayout.MaxWidth(20));
            if ( GUILayout.Button( p.sdk, GUILayout.MaxWidth( 200 ), GUILayout.MaxHeight( 30 ), GUILayout.MinHeight( 30 ) ) )
                AndroidBuilder.SetBuildParam( p );
            if ( GUILayout.Button( "开始打包", GUILayout.MaxWidth( 80 ), GUILayout.MaxHeight( 30 ), GUILayout.MinHeight( 30 ) ) )
                AndroidBuilder.Build( p );
            GUILayout.EndHorizontal();
        }
        #endregion

        GUILayout.EndScrollView();
    }
}


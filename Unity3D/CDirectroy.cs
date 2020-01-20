using System;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

//public static class CString
//{
//    private static StringBuilder cachedStringBuilder;

//    private static StringBuilder AcquireBuilder()
//    {
//        StringBuilder result = cachedStringBuilder;
//        if (result == null)
//            return new StringBuilder();
//        result.Remove(0, result.Length);
//        cachedStringBuilder = null;
//        return result;
//    }

//    private static string GetStringAndReleaseBuilder(StringBuilder sb)
//    {
//        string result = sb.ToString();
//        cachedStringBuilder = sb;
//        return result;
//    }

//    public static string Concat(params string[] args)
//    {
//        if (args == null || args.Length == 0)
//            return string.Empty;
//        StringBuilder sb = AcquireBuilder();
//        for (int i = 0; i < args.Length; i++)
//            sb.Append(args[i]);
//        return GetStringAndReleaseBuilder(sb);
//    }

//    public static string Format(string format, params object[] args)
//    {
//        //return string.Format(format, args);
//        if (string.IsNullOrEmpty(format) || args == null || args.Length == 0)
//            return string.Empty;
//        StringBuilder sb = AcquireBuilder();
//        sb.AppendFormat(format, args);
//        return sb.ToString();
//    }
//}

public static class CDirectory
{
    
    private static string streaming_path;
    private static string cache_path;

    static CDirectory() {
        Init();
    }

    public static void Init() {
            streaming_path = string.Concat(Application.streamingAssetsPath, "/");
        switch (Application.platform) {
        case RuntimePlatform.Android:
            cache_path = string.Concat(Application.persistentDataPath , "/");
            break;
        case RuntimePlatform.IPhonePlayer:
            cache_path = string.Concat(Application.persistentDataPath , "/");
            break;
        case RuntimePlatform.OSXEditor:
        case RuntimePlatform.WindowsEditor:
            cache_path = string.Format("{0}/../main.dir", Application.dataPath);
            if (!Directory.Exists(cache_path))
                Directory.CreateDirectory(cache_path);
            cache_path = string.Concat(cache_path , "/");
            break;
        case RuntimePlatform.WindowsPlayer:
            cache_path = string.Format("{0}/main.dir", Application.dataPath);
            if (!Directory.Exists(cache_path))
                Directory.CreateDirectory(cache_path);
            cache_path = string.Concat(cache_path , "/");
            break;
        }
    }

    public static string MakePath(string filename) {
        if (File.Exists(MakeCachePath(filename)))
            return MakeCachePath(filename);
        else
            return MakeStreamingPath(filename);
    }

    public static string MakeFullPath(string filename) {
        if (File.Exists(MakeCachePath(filename))) {
            return string.Format("file://{0}{1}", cache_path, filename);
        } else {
            switch (Application.platform) {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.IPhonePlayer:
                return string.Format("{0}{1}", "file://", MakeStreamingPath(filename));
            case RuntimePlatform.Android:
                return MakeStreamingPath(filename);
            default:
                return string.Format("{0}{1}", "file:///", MakeStreamingPath(filename));
            }
        }
    }

    public static List<string> GetFiles(string directory, string extension) {
        List<string> names = new List<string>();
        string path = string.Concat(cache_path , directory);
        if (!Directory.Exists(path)) {
            if (Application.isMobilePlatform) {
                return names;
            } else {
                path = string.Concat(streaming_path, directory);
            }
        }

        DirectoryInfo TheFolder = new DirectoryInfo(path);
        foreach (FileInfo NextFile in TheFolder.GetFiles(string.Concat("*" , extension))) {
            string name = NextFile.Name.Replace(extension, "");
            names.Add(name);
        }
        return names;
    }

    public static string MakeCachePath(string filename) {
        return string.Format("{0}{1}", cache_path, filename);
    }

    public static string MakeStreamingPath(string filename) {
        return string.Format("{0}{1}", streaming_path, filename);
    }

    public static string MakeOtherStreamingPath(string filename) {
        if (Application.platform == RuntimePlatform.Android)
            //这个路径只能使用AssetBundle.LoadFromFile来同步读取
            return string.Format("{0}{1}{2}", Application.dataPath, "!assets/", filename);
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            return string.Format("{0}{1}{2}", Application.dataPath, "/Raw/", filename);
        else
            return string.Format("{0}{1}", streaming_path, filename);
    }

    public static string AppendDirectoryChar(string dir) {
        if (dir == null || dir.Length == 0) {
            return string.Empty;
        } else if (dir[dir.Length - 1] != '\\') {
            return string.Concat(dir , '\\');
        } else {
            return dir;
        }
    }

    public static string Standardize(string path) {
        if (string.IsNullOrEmpty(path)) return null;
        return path.Replace('\\', '/');
    }

    internal static void ClearCachePath(string dir) {
        dir = string.Concat(cache_path , dir);
        if (!Directory.Exists(dir))
            return;
        ClearFolder(dir);
    }

    internal static void ClearFolder(string dir) {
        foreach (string d in Directory.GetFileSystemEntries(dir)) {
            if (File.Exists(d)) {
                FileInfo fi = new FileInfo(d);
                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;
                try {
                    File.Delete(d);
                } catch { }
            } else {
                DirectoryInfo dl = new DirectoryInfo(d);
                if (dl.GetFiles().Length != 0)
                    ClearFolder(dl.FullName);
                //Directory.Delete( d );
            }
        }
    }
}

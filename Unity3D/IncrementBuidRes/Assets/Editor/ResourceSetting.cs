using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class ResourceSetting : ScriptableObject
{
	[Serializable]
	public class ResourceInfo
	{
		public string RelativePath;

		public string CreationTime;

		public string LastAccessTime;

		public string LastWriteTime;

		public string Extension;

		public string Name;

		public string MD5;

		public ResourceInfo(FileInfo fileInfo)
		{
			this.Name = fileInfo.Name;
			this.CreationTime = ResourceSetting.ResourceInfo.ConvertDataTimeToLong(fileInfo.CreationTime).ToString();
			this.LastAccessTime = ResourceSetting.ResourceInfo.ConvertDataTimeToLong(fileInfo.LastAccessTime).ToString();
			this.LastWriteTime = ResourceSetting.ResourceInfo.ConvertDataTimeToLong(fileInfo.LastWriteTime).ToString();
			this.Extension = fileInfo.Extension.ToString();
			this.MD5 = ResourceSetting.ResourceInfo.GetMD5HashFromFile(fileInfo.Open(FileMode.Open));
			string text = fileInfo.FullName.Replace("\\", "/");
			this.RelativePath = text.Replace(Application.dataPath, "");
		}

		public static long ConvertDataTimeToLong(DateTime dt)
		{
			DateTime value = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = dt.Subtract(value).Ticks;
			return long.Parse(ticks.ToString().Substring(0, ticks.ToString().Length - 4));
		}

		public static DateTime ConvertLongToDateTime(long d)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = long.Parse(d + "0000");
			TimeSpan value = new TimeSpan(ticks);
			return dateTime.Add(value);
		}

		public static string GetMD5HashFromFile(string fileName)
		{
			string result;
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Open);
				MD5 mD = new MD5CryptoServiceProvider();
				byte[] array = mD.ComputeHash(fileStream);
				fileStream.Close();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
			}
			return result;
		}

		public static string GetMD5HashFromFile(FileStream file)
		{
			string result;
			try
			{
				MD5 mD = new MD5CryptoServiceProvider();
				byte[] array = mD.ComputeHash(file);
				file.Close();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
			}
			return result;
		}
	}

	[SerializeField]
	public List<ResourceSetting.ResourceInfo> infos = new List<ResourceSetting.ResourceInfo>();

	public void Add(string path)
	{
		FileInfo fileInfo = new FileInfo(path);
		for (int i = 0; i < this.infos.Count; i++)
		{
			bool flag = Application.dataPath + this.infos[i].RelativePath != path;
			if (!flag)
			{
				string mD5HashFromFile = ResourceSetting.ResourceInfo.GetMD5HashFromFile(fileInfo.Open(FileMode.Open));
				bool flag2 = mD5HashFromFile != this.infos[i].MD5;
				if (flag2)
				{
					this.infos[i] = new ResourceSetting.ResourceInfo(fileInfo);
					Debug.Log("[modify]" + path);
				}
				return;
			}
		}
		this.infos.Add(new ResourceSetting.ResourceInfo(fileInfo));
		Debug.Log("[Add]" + path);
	}

	public void Remove(string path)
	{
		for (int i = this.infos.Count - 1; i >= 0; i--)
		{
			bool flag = Application.dataPath + this.infos[i].RelativePath == path;
			if (flag)
			{
				this.infos.Remove(this.infos[i]);
				Debug.Log("[remove]" + path);
				break;
			}
		}
	}

	public bool isModified(string path)
	{
		FileInfo fileInfo = new FileInfo(path);
		bool result;
		for (int i = 0; i < this.infos.Count; i++)
		{
			bool flag = Application.dataPath + this.infos[i].RelativePath != path;
			if (!flag)
			{
				string mD5HashFromFile = ResourceSetting.ResourceInfo.GetMD5HashFromFile(fileInfo.Open(FileMode.Open));
				bool flag2 = mD5HashFromFile != this.infos[i].MD5;
				result = flag2;
				return result;
			}
		}
		result = true;
		return result;
	}

	public ResourceSetting.ResourceInfo Get(string path)
	{
		ResourceSetting.ResourceInfo result;
		for (int i = 0; i < this.infos.Count; i++)
		{
			bool flag = Application.dataPath + this.infos[i].RelativePath == path;
			if (flag)
			{
				result = this.infos[i];
				return result;
			}
		}
		result = null;
		return result;
	}
}

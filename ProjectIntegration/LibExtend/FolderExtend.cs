using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Folder
{
    #region 静态变量
    const string ERROR_FOLDER_PATH = "参数：{0}，{2} 路劲：{1}不存在！";
    #endregion
    /// <summary>
    /// 检查文件夹是否存在
    /// </summary>
    /// <param name="paranName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string CheckDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new Exception(string.Format(ERROR_FOLDER_PATH, "path", path,"文件夹"));
        }
        return path;
    }
    public static string CheckFile(string path) {
        if (!File.Exists(path))
        {
            throw new Exception(string.Format(ERROR_FOLDER_PATH, "path", path,"文件"));
        }
        return path;
    }
    /// <summary>
    /// 根据路径获取盘符
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetDrive(string path) {
        return path.Substring(0, path.IndexOf(":")).ToLower();
    }
    /// <summary>
    /// 获得当前应用程序集盘符
    /// </summary>
    public static string GetTheDrive {
        get {
            return GetDrive(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}

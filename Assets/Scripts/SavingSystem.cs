using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{
    public static void SaveData(string fileName, object data, bool hideFile = false)
    {
        string path = GetFullPath(fileName);

        if (FileExists(fileName))
        {
            File.SetAttributes(path, FileAttributes.Normal);
        }

        FileStream stream = new FileStream(path, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, data);

        stream.Close();

        if (hideFile)
        {
            File.SetAttributes(path, FileAttributes.Hidden);
        }

        Debug.Log($"Saved data to \"{fileName}\"");
    }

    public static T LoadData<T>(string fileName)
    {
        string path = GetFullPath(fileName);

        if (FileExists(fileName))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            T loadedData = (T)formatter.Deserialize(stream);

            stream.Close();

            Debug.Log($"Loaded data \"{fileName}\"");

            return loadedData;
        }
        else
        {
            Debug.LogWarning($"File \"{fileName}\" not found");

            return default;
        }
    }

    public static bool FileExists(string fileName)
    {
        return File.Exists(GetFullPath(fileName));
    }

    public static bool DirExists(string dirName)
    {
        return Directory.Exists(GetFullPath(dirName));
    }

    public static string[] GetDirectory(string dirName)
    {
        if (!DirExists(dirName))
        {
            return null;
        }

        string[] files = Directory.GetFiles(GetFullPath(dirName) + "/");

        if (files.Length > 0)
        {
            return files;
        }
        else
        {
            return null;
        }
    }

    public static string CreateDir(string dirName)
    {
        if (!DirExists(dirName))
        {
            Directory.CreateDirectory(GetFullPath(dirName));
        }

        return dirName;
    }

    public static void DeleteFile(string fileName)
    {
        if (FileExists(fileName))
        {
            File.Delete(GetFullPath(fileName));
        }
    }

    public static string GetFullPath(string fileName)
    {
        return Application.persistentDataPath + $"/{fileName}";
    }

    public static string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }
}
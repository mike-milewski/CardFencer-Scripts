﻿using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    private const string SAVE_EXTENSION = "txt";

    public static void Init() 
    {
        // Test if Save Folder exists
        if (!Directory.Exists(SAVE_FOLDER)) 
        {
            // Create Save Folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static bool CheckSaveFile()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);

        FileInfo[] saveFiles = directoryInfo.GetFiles("*." + SAVE_EXTENSION);

        return saveFiles.Length > 0;
    }

    public static void Save(string saveString) 
    {
        int saveNumber = 1;

        File.WriteAllText(SAVE_FOLDER + "save_" + saveNumber + "." + SAVE_EXTENSION, saveString);
    }

    public static string Load() 
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        // Get all save files
        FileInfo[] saveFiles = directoryInfo.GetFiles("*." + SAVE_EXTENSION);
        // Cycle through all save files and identify the most recent one
        FileInfo mostRecentFile = null;
        foreach (FileInfo fileInfo in saveFiles) 
        {
            if (mostRecentFile == null) 
            {
                mostRecentFile = fileInfo;
            } 
            else 
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime) 
                {
                    mostRecentFile = fileInfo;
                }
            }
        }

        // If theres a save file, load it, if not return null
        if (mostRecentFile != null)
        {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;
        }
        else return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static GraphicsSettings GraphicsSettings { get; private set; }

    private static string SavedSettingsFilePath => Application.persistentDataPath + "/SavedSettings.dat";

    public static void SaveSettings()
    {
        var settingsFile = new FileStream(SavedSettingsFilePath, FileMode.OpenOrCreate);
        var serializedSettings = JsonUtility.ToJson(GraphicsSettings);
        File.WriteAllText(SavedSettingsFilePath, serializedSettings);
        settingsFile.Close();
    }

    public static void LoadSettings() 
    {
        if (File.Exists(SavedSettingsFilePath))
        {
            var settingsFile = new FileStream(SavedSettingsFilePath, FileMode.Open);
            var savedSettings = File.ReadAllText(SavedSettingsFilePath);
            GraphicsSettings = JsonUtility.FromJson<GraphicsSettings>(savedSettings);
            settingsFile.Close();
        }       
        else
        {
            GraphicsSettings = new GraphicsSettings()
            {
                ScreenResolution = Screen.currentResolution,
                ScreenMode = FullScreenMode.ExclusiveFullScreen,
                QualityLevel = QualitySettings.GetQualityLevel(),
                VSyncState = 0,
                Brightness = Screen.brightness
            };
        }
    }

    private void Awake()
    {
        LoadSettings();
    }
}

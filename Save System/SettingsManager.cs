using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static GraphicsSettings GraphicsSettings { get; private set; }

    private static string SavedSettingsFilePath => Application.persistentDataPath + "/SavedSettings.dat";

    public static void SaveSettings()
    {
        using (StreamWriter writer = new StreamWriter(SavedSettingsFilePath))
        {
            var serializedSettings = JsonUtility.ToJson(GraphicsSettings);
            writer.Write(serializedSettings);
            writer.Close();
            Debug.Log("Настройки сохранены в файл: " + SavedSettingsFilePath);
            ApplySettings();
        }        
    }

    private void LoadSettings() 
    {
        if (File.Exists(SavedSettingsFilePath))
        {
            using (StreamReader reader = new StreamReader(SavedSettingsFilePath))
            {
                var savedSettings = reader.ReadToEnd();
                GraphicsSettings = JsonUtility.FromJson<GraphicsSettings>(savedSettings);
                reader.Close();
                Debug.Log("Настройки загружены из файла: " + SavedSettingsFilePath);
                ApplySettings();
            }          
        }       
        else
        {
            GraphicsSettings = new GraphicsSettings()
            {
                ScreenResolutionWidth = Screen.currentResolution.width,
                ScreenResolutionHeight = Screen.currentResolution.height,
                ScreenMode = FullScreenMode.ExclusiveFullScreen,
                QualityLevel = QualitySettings.GetQualityLevel(),
                VSyncState = 0,
                Brightness = RenderSettings.ambientLight.r
            };
            SaveSettings();
        }       
    }

    private static void ApplySettings()
    {
        Screen.SetResolution(GraphicsSettings.ScreenResolutionWidth, GraphicsSettings.ScreenResolutionHeight, GraphicsSettings.ScreenMode);
        QualitySettings.SetQualityLevel(GraphicsSettings.QualityLevel);
        QualitySettings.vSyncCount = GraphicsSettings.VSyncState;
        RenderSettings.ambientLight = new Color(GraphicsSettings.Brightness, GraphicsSettings.Brightness, GraphicsSettings.Brightness, 1);
    }

    private void Awake()
    {
        LoadSettings();
    }
}

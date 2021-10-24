using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace Utils
{
    public class SaveSystem
    {
        // public static void SaveSettings(GameSettings.SettingData settingData)
        // {
        //     BinaryFormatter formatter = new BinaryFormatter();
        //     string path = Application.persistentDataPath + "/setting.fun";
        //     FileStream stream = new FileStream(path, FileMode.Create);

        //     formatter.Serialize(stream, settingData);
        //     stream.Close();
        // }

        // public static GameSettings.SettingData LoadSettings()
        // {
            
        //     string path = Application.persistentDataPath + "/setting.fun";

        //     if (!File.Exists(path))
        //     {
        //         Debug.Log("Settings save not found in " + path);
        //         return null;
        //     }
        //     BinaryFormatter formatter = new BinaryFormatter();
        //     FileStream stream = new FileStream(path, FileMode.Open);
        //     GameSettings.SettingData setting = null;
        //     try {
        //         setting = formatter.Deserialize(stream) as GameSettings.SettingData;
        //     } catch {
        //         Debug.Log("Load failed");
        //     }
        //     stream.Close();
        //     return setting;
        // }

        public static void Save<T> (T data, string name) {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + name + ".fun";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T Load<T> (string name) where T : class {
            string path = Application.persistentDataPath + "/" + name + ".fun";

            if (!File.Exists(path))
            {
                Debug.Log("Save not found in " + path);
                return null;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            T data = null;
            try {
                data = formatter.Deserialize(stream) as T;
            } catch {
                Debug.Log("Load failed");
            }
            stream.Close();
            return data;
        }
    }
}
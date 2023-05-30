using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Data
{
    public class DataLoader : MonoBehaviour
    {
        
        private string[] _jsonFiles = new string[] {
            "Buildings.json",
            "Scores.json",
            "Success.json",
            "Choices/Academique.json",
            "Choices/Culture.json",
            "Choices/Economie.json",
            "Choices/Energie.json",
            "Choices/Environnement.json",
            "Choices/Mobilite.json",
            "Choices/Population.json",
        };
        
        public static DataLoader Instance { get; private set; }

        private void Awake() 
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public IEnumerator LoadJSON<T>(string fileName, Action<List<T>> onLoaded)
        {
            string filePath;
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "HardData", fileName);
                using (UnityWebRequest www = UnityWebRequest.Get(filePath))
                {
                    yield return www.SendWebRequest();
                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        string jsonContent = www.downloadHandler.text;
                        List<T> result = JsonConvert.DeserializeObject<List<T>>(jsonContent);
                        onLoaded?.Invoke(result);
                    }
                    else
                    {
                        Debug.LogError("Failed to load file: " + www.error);
                    }
                }
            }
            else
            {
                filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "HardData", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    string jsonContent = System.IO.File.ReadAllText(filePath);
                    List<T> result = JsonConvert.DeserializeObject<List<T>>(jsonContent);
                    onLoaded?.Invoke(result);
                }
                else
                {
                    Debug.LogError("Failed to load file: File does not exist");
                }
            }
        }

        public string[] GetJsonStrings() => _jsonFiles;
    }

}

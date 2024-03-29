using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using Data;

namespace Managers
{
    public class FileDataManager
    {
        private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
        private readonly string encryptionCodeWord = "word";
        
        public FileDataManager(){}
        
        public SaveData LoadData()
        {
            string fullPath = Path.Combine(SAVE_FOLDER, "save.txt");
            SaveData loadedData = null;
            if (File.Exists(fullPath))
            {
                try 
                {
                    // load the serialized data from the file
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    dataToLoad = EncryptDecrypt(dataToLoad);
                    loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);
                }
                catch (Exception e) 
                {
                    Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
                }
            }
            return loadedData;
        }

        public void SaveData(SaveData data) {
            string fullPath = Path.Combine(SAVE_FOLDER, "save.txt");
            try 
            {
                // create the directory the file will be written to if it doesn't already exist
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                // serialize the C# game data object into Json
                string dataToStore = JsonUtility.ToJson(data, true);
                dataToStore = EncryptDecrypt(dataToStore);
                // write the serialized data to the file
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream)) 
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e) 
            {
                Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
            }
        }
        
         private string EncryptDecrypt(string data) 
            {
                string modifiedData = "";
                for (int i = 0; i < data.Length; i++) 
                {
                    modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
                }
                return modifiedData;
            }
    }
}
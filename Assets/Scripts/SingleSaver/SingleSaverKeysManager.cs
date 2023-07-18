using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace SingleSaver
{

    public class SingleSaverKeysManager : MonoBehaviour
    {
        private static SingleSaverKeysManager instance;
        private string keysFilePath;

        public static SingleSaverKeysManager Instance { get { return instance; } }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            keysFilePath = Application.dataPath + "/Scripts/SingleSaver/SingleSaverKeys.txt";
        }

        #region GetData

        public int GetIntData(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.Log($"GetIntData 404: data with {key} key is empty, returning data is 0");
                return 0;
            }
            else
            {
                return PlayerPrefs.GetInt(key);
            }
        }

        public float GetFloatData(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.Log($"GetFloatData 404: data with {key} key is empty, returning data is 0");
                return 0;
            }
            else
            {
                return PlayerPrefs.GetFloat(key);
            }
        }

        public string GetStringData(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.Log($"GetStringData 404: data with {key} key is empty, returning data is \"empty\"");
                return "empty";
            }
            else
            {
                return PlayerPrefs.GetString(key);
            }
        }

        public Vector2 GetVector2Data(string key)
        {
            if (!PlayerPrefs.HasKey(key + "X") || !PlayerPrefs.HasKey(key + "Y"))
            {
                Debug.Log($"GetVector2Data 404: data with {key} key is empty or one of coordinates is missing, returning data is Vector2(0, 0)");
                return new Vector2(0, 0);
            }
            else
            {
                float dataX = PlayerPrefs.GetFloat(key + "X");
                float dataY = PlayerPrefs.GetFloat(key + "Y");
                return new Vector2(dataX, dataY);
            }
        }

        public Vector3 GetVector3Data(string key)
        {
            if (!PlayerPrefs.HasKey(key + "X") || !PlayerPrefs.HasKey(key + "Y") || !PlayerPrefs.HasKey(key + "Z"))
            {
                Debug.Log($"GetVector3Data 404: data with {key} key is empty or one of coordinates is missing, returning data is Vector3(0, 0, 0)");
                return new Vector3(0, 0, 0);
            }
            else
            {
                float dataX = PlayerPrefs.GetFloat(key + "X");
                float dataY = PlayerPrefs.GetFloat(key + "Y");
                float dataZ = PlayerPrefs.GetFloat(key + "Z");
                return new Vector3(dataX, dataY, dataZ);
            }
        }

        public List<int> GetIntListData(string key)
        {
            if (!PlayerPrefs.HasKey(key + "0"))
            {
                Debug.Log($"GetIntListData 404: data with {key} key is empty, returning data is new List<int>");
                return new List<int>();
            }
            else
            {
                var data = new List<int>();

                int i = 0;
                while (PlayerPrefs.HasKey(key + i.ToString()))
                {
                    int intData = PlayerPrefs.GetInt(key + i.ToString());
                    data.Add(intData);
                    i++;
                }

                return data;
            }
        }

        public List<float> GetFloatListData(string key)
        {
            if (!PlayerPrefs.HasKey(key + "0"))
            {
                Debug.Log($"GetFloatListData 404: data with {key} key is empty, returning data is new List<int>");
                return new List<float>();
            }
            else
            {
                var data = new List<float>();

                int i = 0;
                while (PlayerPrefs.HasKey(key + i.ToString()))
                {
                    float floatData = PlayerPrefs.GetFloat(key + i.ToString());
                    data.Add(floatData);
                    i++;
                }

                return data;
            }
        }

        public List<string> GetStringListData(string key)
        {
            if (!PlayerPrefs.HasKey(key + "0"))
            {
                Debug.Log($"GetStringListData 404: data with {key} key is empty, returning data is new List<string>");
                return new List<string>();
            }
            else
            {
                var data = new List<string>();

                int i = 0;
                while (PlayerPrefs.HasKey(key + i.ToString()))
                {
                    string stringData = PlayerPrefs.GetString(key + i.ToString());
                    data.Add(stringData);
                    i++;
                }

                return data;
            }
        }

        #endregion

        #region SetData

        public bool IsKeyAwailable(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                Debug.Log($"{key} key already exists, if you know this - ignore this message");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SetData(string key, int data)
        {
            AddKeyToFile(key, "int");
            PlayerPrefs.SetInt(key, data);
        }

        public void SetData(string key, float data)
        {
            AddKeyToFile(key, "float");
            PlayerPrefs.SetFloat(key, data);
        }

        public void SetData(string key, string data)
        {
            AddKeyToFile(key, "string");
            PlayerPrefs.SetString(key, data);
        }

        public void SetData(string key, Vector2 data)
        {
            AddKeyToFile(key, "Vector2");
            PlayerPrefs.SetFloat(key + "X", data.x);
            PlayerPrefs.SetFloat(key + "Y", data.y);
        }

        public void SetData(string key, Vector3 data)
        {
            AddKeyToFile(key, "Vector3");
            PlayerPrefs.SetFloat(key + "X", data.x);
            PlayerPrefs.SetFloat(key + "Y", data.y);
            PlayerPrefs.SetFloat(key + "Z", data.z);
        }

        public void SetData(string key, List<int> data)
        {
            AddKeyToFile(key, "List<int>");
            for (int i = 0; i < data.Count; i++)
            {
                PlayerPrefs.SetInt(key + i.ToString(), data[i]);
            }
        }

        public void SetData(string key, List<float> data)
        {
            AddKeyToFile(key, "List<float>");
            for (int i = 0; i < data.Count; i++)
            {
                PlayerPrefs.SetFloat(key + i.ToString(), data[i]);
            }
        }

        public void SetData(string key, List<string> data)
        {
            AddKeyToFile(key, "List<string>");
            for (int i = 0; i < data.Count; i++)
            {
                PlayerPrefs.SetString(key + i.ToString(), data[i]);
            }
        }

        #endregion

        #region DeleteData

        public void DeleteIntData(string key)
        {
            DeleteSpecificData(key, "int");
        }

        public void DeleteFloatData(string key)
        {
            DeleteSpecificData(key, "float");
        }

        public void DeleteStringData(string key)
        {
            DeleteSpecificData(key, "string");
        }

        public void DeleteVector2Data(string key) //not completed
        {
            DeleteSpecificData(key, "Vector2");
        }

        public void DeleteVector3Data(string key) //not completed
        {
            DeleteSpecificData(key, "Vector3");
        }

        public void DeleteIntListData(string key) //not completed
        {
            DeleteSpecificData(key, "List<int>");
        }

        public void DeleteFloatListData(string key) //not completed
        {
            DeleteSpecificData(key, "List<float>");
        }

        public void DeleteStringListData(string key) //not completed
        {
            DeleteSpecificData(key, "List<string>");
        }

        public void DeleteAllData()
        {
            PlayerPrefs.DeleteAll();
            DeleteKeysFile();
        }

        private void DeleteSpecificData(string key, string dataType)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                DeleteKeyFromFile(key, dataType);
            }
            else
            {
                Debug.Log($"DeleteSpecificData 404: {key} key not found");
            }
        }

        #endregion

        #region KeysFileControll

        public void SetKeysFilePath(string filePath)
        {
            keysFilePath = Application.dataPath + filePath;
        }

        public void GetKeysFilePath()
        {
            Debug.Log($"Keys path: {keysFilePath}");
        }

        private void AddKeyToFile(string key, string dataType)
        {
            if (File.Exists(keysFilePath))
            {
                AddKey(key, dataType);
            }
            else
            {
                CreateKeysFile();
                AddKey(key, dataType);
            }
        }

        private void CreateKeysFile()
        {
            FileStream fileStream = File.Create(keysFilePath);
            fileStream.Close();
            File.AppendAllText(keysFilePath, "Don`t change anything inside of this file, use methods instead!" + Environment.NewLine + Environment.NewLine);
        }

        private void AddKey(string key, string dataType)
        {
            File.AppendAllText(keysFilePath, "Key name: " + key + ", data type: " + dataType + Environment.NewLine);
        }

        private void DeleteKeyFromFile(string key, string dataType)
        {
            string searchText = "Key name: " + key + ", data type: " + dataType;
            string fileContents = File.ReadAllText(keysFilePath);
            fileContents = fileContents.Replace(searchText, "Key deleted: " + searchText);
            File.WriteAllText(keysFilePath, fileContents);
        }

        private void DeleteKeysFile()
        {
            if (File.Exists(keysFilePath))
            {
                File.Delete(keysFilePath);
            }
        }

        #endregion
    }
}
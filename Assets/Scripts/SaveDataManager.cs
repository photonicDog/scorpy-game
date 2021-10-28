using Assets.Scripts.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Flags;
using UnityEngine;

    public class SaveDataManager : MonoBehaviour
    {
        private FileStream _saveFileStream;
        private string _savePath;
        private BinaryFormatter _bf;

        public SaveData saveData;

        private static SaveDataManager _instance;
        public static SaveDataManager Instance { get { return _instance; } }

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }

            _savePath = Application.persistentDataPath + "\\save.dat";
            _bf = new BinaryFormatter();
        }

        public bool SaveData(Vector3 position)
        {
            try
            {
                SaveData save = LoadData();

                save.Flags = FlagManager.Instance.GetCurrentFlags();
                save.Inventory = InventoryManager.Instance.inventory;
                save.Level = SceneManager.Instance.level;
                save.Position = position;

                OverwriteSave(save);
                
                saveData = save;
                return true;
            } catch (Exception ex)
            {
                Debug.Log($"Error saving data! {ex.Message}");
                return false;
            }
        }

        public SaveData LoadData()
        {
            SaveData save;

            if (!File.Exists(_savePath))
            {
                save = CreateNewSave();
            } else
            {
                _saveFileStream = File.Open(_savePath, FileMode.Open);
                save = (SaveData)_bf.Deserialize(_saveFileStream);
                _saveFileStream.Close();
            }

            saveData = save;
            return save;
        }

        public SaveData CreateNewSave()
        {
            SaveData save = new SaveData();

            OverwriteSave(save);

            saveData = save;
            return save;
        }

        private void OverwriteSave(SaveData save)
        {
            _saveFileStream = File.Create(_savePath);
            _bf.Serialize(_saveFileStream, save);
            _saveFileStream.Close();
        }

        public void SetPlayerPositionToSave(SaveData sd) {
            StartCoroutine(PlayerPositionSet(sd.Position));
        }

        private IEnumerator PlayerPositionSet(Vector3 position) {
            GameObject g;
            while (true) {
                g = GameObject.FindWithTag("Player");
                if (g) break;
                yield return null;
            }

            g.transform.position = position;
        }
    }

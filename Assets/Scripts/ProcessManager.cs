using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProcessManager : MonoBehaviour
{
    public static ProcessManager Instance;
    public int bestPoints;
    public string playerName, bestPlayerName;


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


        Load();
    }

    public void SaveName()
    {
        playerName = GameObject.Find("InputName").GetComponent<TMP_InputField>().text;
    }

    public void NewStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

        Save();
    }

    [System.Serializable]
    class SaveData
    {
        public int bestPoints;
        public string bestPlayerName;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.bestPoints = bestPoints;
        data.bestPlayerName = bestPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
       
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPoints = data.bestPoints;
            bestPlayerName = data.bestPlayerName;
        }
    }


}

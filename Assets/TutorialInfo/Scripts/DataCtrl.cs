using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class DataCtrl : MonoBehaviour
{
    
    public static DataCtrl instance = null;
    public GameData data;

    string filePathName;

    BinaryFormatter bf;





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
        bf = new BinaryFormatter();

        filePathName = Application.persistentDataPath + "/game.dat";



    }

    public void LoadData()
    {
        if (File.Exists(filePathName))
        {
            FileStream fs = new FileStream(filePathName, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);
            fs.Close();
        }
    }

    private void OnEnable()
    {
        LoadData();
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public bool isUnlocked(int levelNumber)
    {
        return data.levelData[levelNumber].unlocked;
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class GameController : MonoBehaviour
{

    public static GameController instance;
    
    public GameData data;
    public UI ui;
    public int coinValue;
    public float delay;
    public GameObject gameoverPanel;
    public GameObject pauseUI;
    public GameObject mobileUI;
    public GameObject wall;
    public GameObject enemySpawner;
    public GameObject levelCompleteUi;
    public GameObject levelendbtndlt;

    private BinaryFormatter binaryFormatter;
    private string filePath;

    private bool paused;
    private Animator anim;



    private void Awake()
    {
        anim = GetComponent<Animator>();

        if (instance == null)
        {
            instance = this;
        }
        binaryFormatter = new BinaryFormatter();
        

        filePath = Application.persistentDataPath + "/game.dat";
        
    }


    void Start()
    {
        LoadGame();
        UptadeHearts();
        paused = false;
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            DeleteData();
        }

        if (paused)
        {
            Time.timeScale = 0;
        }
        if (!paused)
        {
            Time.timeScale = 1;
        }
        
    }


    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);
        CheckLives();
        
        

    }
    public void PlayerHit(GameObject player)
    {

        Animator anim = player.GetComponent<Animator>();

       
        if (data.lives > 0)
        {
            data.lives -= 1;
            UptadeHearts();
            

        }
        else
        {
            anim.SetInteger("Status", 5);
            AudioController.instance.playerDieSound(gameObject.transform.position);
            player.GetComponent<PlayerController>().enabled = false;

            StartCoroutine("GamePause", player);

        }


    }
    IEnumerator GamePause(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        PlayerDied(player);
    }



    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MushroomCount()
    {
        data.mushroom += 1;
        ui.mytext.text = " " + data.mushroom;
        ScoreCount(coinValue);
    }

    public void ScoreCount(int value)
    {
        data.score += value;
        ui.scoretxt.text = "Score :" + data.score;

    }



    public void KeyCount(int key)
    {
        Debug.Log(data);
        data.keyValue[key] = true;
        
       /*
        if (key == 0)
        {
            ui.key3.sprite = ui.key_3;

        }
        else if (key == 1)
        {
            ui.key2.sprite = ui.key_2;
        }
        else if (key == 2)
        {
            ui.key1.sprite = ui.key_1;
        }*/
    }





    public void SaveData()
    {
        FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        binaryFormatter.Serialize(fileStream,data);
        fileStream.Close();




    }
    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            data = (GameData) binaryFormatter.Deserialize(fileStream);
            ui.mytext.text = " " + data.mushroom;
            ui.scoretxt.text = "Score :" + data.score;
            fileStream.Close();
        }

    }
    public void DeleteData()
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        data.mushroom = 0;
        data.score = 0;
        ui.mytext.text = "0";
        ui.scoretxt.text = "Score :" + data.score;
        data.lives = 3;
        for (int i = 0; i < 3; i++)
        {
            data.keyValue[i] = false;
        }

        foreach (LevelData level in data.levelData)
        {
            if (level.levelNum != 1)
            {
                level.unlocked = false;
            }
        }


        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }


    private void OnEnable()
    {
        LoadData();
    }

    private void OnDisable()
    {
        SaveData();
    }

    private void LoadGame()
    {
        if (data.firstLoading)
        {
            data.lives = 3;
            data.mushroom = 0;
            data.score = 0;
            data.firstLoading = false;

            for (int i = 0; i < 3; i++)
            {
                data.keyValue[i] = false;
            }
        }
        
    }

    private void UptadeHearts()
    {
        if (data.lives == 3)
        {
            ui.heart1.sprite = ui.fullheart;
            ui.heart2.sprite = ui.fullheart;
            ui.heart3.sprite = ui.fullheart;

        }
        if (data.lives == 2)
        {
            ui.heart1.sprite = ui.emptyHeart;
        }
        if (data.lives == 1)
        {
            ui.heart1.sprite = ui.emptyHeart;
            ui.heart2.sprite = ui.emptyHeart;
        }
       

    }

    private void CheckLives()
    {
        
        int currentLives = data.lives;
        currentLives -= 1;
        data.lives = currentLives;

        if (data.lives == 0)
        {

            data.lives = 3;
            SaveData();
            Invoke("GameOver", delay);
        }
        else
        {
            SaveData();
            Invoke("RestartLevel",delay);
        }
    }
    

    private void GameOver()
    {
        gameoverPanel.SetActive(true);
    }

    public void StopCamera()
    {
        Camera.main.GetComponent<CameraController>().enabled = false;
    }

    public void DisableWall()
    {
        wall.SetActive(false);
        DisableEnemySpawner();
        

    }
    public void EnableSpawner()
    {
        enemySpawner.SetActive(true);
    }

    public void DisableEnemySpawner()
    {
        enemySpawner.SetActive(false);
    }

    public int GetScore()
    {
        return data.score;
    }
    public void UnlockedLevel(int levelNum)
    {
        data.levelData[levelNum].unlocked = true;
    } 
    public void LevelComplete()
    {
        levelCompleteUi.SetActive(true);
        levelendbtndlt.SetActive(false);
    }
    public void ShowPauseMenu()
    {
        if (mobileUI.activeInHierarchy)
        {
            mobileUI.SetActive(false);
        }
        pauseUI.SetActive(true);
        paused = true;
    }
    public void HidePauseMenu()
    {
        if (!mobileUI.activeInHierarchy)
        {
            mobileUI.SetActive(true);
        }
        pauseUI.SetActive(false);
        paused = false;
    }
}

[System.Serializable]
public class GameData
{
    public int mushroom;
    public int score;
    public int lives;
    public bool firstLoading;
    public bool[] keyValue;
    public LevelData[] levelData;


    

}
[System.Serializable]
public class UI
{
    [Header("Text Özellikller")]
    public Text mytext; //coin
    public Text scoretxt;

    [Header("Image Özellikleri")]
    public Image key1;
    public Image key2;
    public Image key3;

    public Sprite key_1;
    public Sprite key_2;
    public Sprite key_3;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Sprite emptyHeart;
    public Sprite fullheart;
}
[System.Serializable]
public class LevelData
{
    public bool unlocked;
    public int levelNum;
}
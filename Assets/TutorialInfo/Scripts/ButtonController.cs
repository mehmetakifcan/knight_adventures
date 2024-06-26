using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    int levelNumber;
    Button btn;
    Image buttonImg;
    Text buttonTxt;

    public Sprite lockedButton;
    public Sprite unlockedButton;
    public string levelname;

    
    // Start is called before the first frame update
    void Start()
    {
        levelNumber = int.Parse(transform.gameObject.name);
        btn = transform.gameObject.GetComponent<Button>();
        buttonImg = btn.GetComponent<Image>();
        buttonTxt = btn.gameObject.transform.GetChild(0).GetComponent<Text>();
        ButtonStatus();
    }

    void ButtonStatus()
    {
        /*bool unlocked = DataCtrl.instance.isUnlocked(levelNumber);

        if (!unlocked)
        {
            buttonImg.overrideSprite = lockedButton;
            buttonTxt.text = "";
        }
        else
        {
           
        }
        */
        btn.onClick.AddListener(LoadScene);
    } 

    void LoadScene()
    {
        SceneManager.LoadScene(levelname);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

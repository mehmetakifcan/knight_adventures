using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    public Button nextButton;
    public Text txtScore;
    public GameObject star1,star2,star3;
    

    public int levelnum;
    public int score;
    public int score3Star;
    public int score2Star;
    public int score1Star;
    public int nextLevelScore;

    
    


    void Start()
    {
        
        
        score = GameController.instance.GetScore();
        txtScore.text = "" + score;
        if (score >= score3Star)
        {
            star3.SetActive(true);
            star2.SetActive(true);
            star1.SetActive(true);
            nextButton.interactable = true;
            GameController.instance.UnlockedLevel(levelnum);

        }
        if (score >= score2Star && score < score3Star)
        {
            star2.SetActive(true);
            star1.SetActive(true);
            if (score >= nextLevelScore)
            {
                nextButton.interactable = true;
                GameController.instance.UnlockedLevel(levelnum);

            }
            
        }
        if (score != 0 && score >= score1Star)
        {
            star1.SetActive(true);
            /*if (score >= nextLevelScore)
            {
                nextButton.interactable = true;
                //GameController.instance.UnlockedLevel(levelnum);

            } */
            

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
}

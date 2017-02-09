using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScorePanelScript : MonoBehaviour
{

    //Button which should send us to the SelectScene.
    GameObject nextLevelButton;
    //contaits 3 stars
    GameObject[] star;
    //
    private float unfoggingSpeed = 1f;


    public void StopPanelAnimation()
    {
        GameObject.Find("Canvas/ScorePanel").GetComponent<Animator>().Stop();
    }


    private void Start()
    {
        //size of array
        star = new GameObject[4];

        //1,2,3 coz 0 is returenable value from method GetLevelScore().
        star[1] = GameObject.Find("Canvas/ScorePanel/Star (1)");
        star[2] = GameObject.Find("Canvas/ScorePanel/Star (2)");
        star[3] = GameObject.Find("Canvas/ScorePanel/Star (3)");

        //find the button
        nextLevelButton = GameObject.Find("Canvas/ScorePanel/NextLevel");
        //
        nextLevelButton.GetComponent<Button>().onClick.AddListener(GoToSelectMenu);
    }

    private void GoToSelectMenu()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void ShowStar()
    {
        ShowStarOneByOne();
    }

    private void ShowStarOneByOne()
    {
        int score = GameStatus.GameScore.GetScoreForCurrentLevel();
        StartCoroutine(ShowStarProcessing(score));
    }


    private int currentStarNumber = 1;

    IEnumerator ShowStarProcessing(int score)
    {
        //if the user has 0 score points  ¯\_(ツ)_/¯ 
        if (score == 0)
        {
            yield break;
        }
        
        //change the color of star
        star[currentStarNumber].GetComponent<Image>().color = Color.Lerp(Color.white, Color.grey, unfoggingSpeed);
        //using for smooth color change
        unfoggingSpeed -= 0.01f;
        //speed[2]
        yield return new WaitForSeconds(0.01f);
        //if the star changed color already
        if (unfoggingSpeed <= 0)
        {
            //Gets reverse star number to our score.
            //It needs for the order-showing stars.
            if (currentStarNumber >= score + 1)
            {
                yield break;
                
            }

            currentStarNumber++;
         
            //reset for next use
            unfoggingSpeed = 1;
            //if all stars are done
            if (score != 0)
            {
                //recursion
                //for the next star
                StartCoroutine(ShowStarProcessing(--score));
            }
            yield break;
        }
        //recursion
        //for the current star
        StartCoroutine(ShowStarProcessing(score));
    }


   public class A
    {
        void ch()
        {
            
        }
    }

    class B : A
    {
      
    }
}

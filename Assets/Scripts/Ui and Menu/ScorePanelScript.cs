using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScorePanelScript : MonoBehaviour {

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
        star[1]  = GameObject.Find("Canvas/ScorePanel/Star (1)");
        star[2] = GameObject.Find("Canvas/ScorePanel/Star (2)");
        star[3] = GameObject.Find("Canvas/ScorePanel/Star (3)");
    }

    public void ShowStar()
    {
        ShowStarOneByOne();
    }

    private void ShowStarOneByOne()
    {
        int score = GameStatus.GameScore.FirstLevel.GetLevelScore();
        StartCoroutine(ShowStarProcessing(score));
    }
    

    IEnumerator ShowStarProcessing(int number)
    {
        //change the color of star
        star[number].GetComponent<Image>().color = Color.Lerp(Color.white, Color.grey, unfoggingSpeed);
        //using for smoothy color change
        unfoggingSpeed -= 0.01f;
        Debug.Log("Showing the star..." + number);
        //speed[2]
        yield return new WaitForSeconds(0.01f);
        //if the star changed color already
        if (unfoggingSpeed <= 0)
        {
            //reset for next use
            unfoggingSpeed = 1;
            //if all stars are done
            if (number != 0)
            {
                //recursion
                //for the next star
                StartCoroutine(ShowStarProcessing(--number));
                Debug.Log("Star Number: " + number);
            }
            yield break;
        }
        //recursion
        //for the current star
        StartCoroutine(ShowStarProcessing(number));
    }
}

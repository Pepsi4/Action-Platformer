using UnityEngine;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
    //The easiest way is using constructor.
    //Coz the method is not static : TODO
    ScorePanelScript scorePanel;
    //constructor
    private void Start()
    {
        scorePanel = GameObject.Find("Canvas/ScorePanel").GetComponent<ScorePanelScript>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainHero")
        {
            MainHeroEntered();
        }
    }

    //the actions if the main hero finished the level
    void MainHeroEntered()
    {
        //UI part
        //starts the animation
        GameObject.Find("Canvas/ScorePanel").GetComponent<Animator>().Play("ScorePanel");
        //updating text
        GameObject.Find("Canvas/ScorePanel/ScoreText").GetComponent<Text>().text = "Your time: " + GameStatus.TimeActive.ToString("0.##");

        //pause the game
        Pause();
        //show the stars
        scorePanel.ShowStar();
    }
    


    //We have to pause the game sometimes.
    //For ex: the level finished by player.
    public static void Pause()
    {
        //time counter frozen
        //Stops spawning the balloons
        GameStatus.IsActive = false;

        //The hero should be invulnerable.
        PlayerScript.IsCanTakeDamage = false;
        //freez the main hero
        GameObject.Find("MainHero").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        //balloons frozen
        GameObject[] movingObjects = GameObject.FindGameObjectsWithTag("Moving");
        foreach (GameObject obj in movingObjects)
        {
            obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

}

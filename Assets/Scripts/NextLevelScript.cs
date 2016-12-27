using UnityEngine;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
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
    }

    public static void Pause()
    {
        //time counter frozen
        //Stops spawning the balloons
        GameStatus.IsActive = false;

        //balloons frozen
        GameObject[] movingObjects = GameObject.FindGameObjectsWithTag("Moving");
        foreach (GameObject obj in movingObjects)
        {
            obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
    //Trigger can work 2 times. It's coz of using type of collision: sphere.
    private bool isTriggerEnter2D = false;

    public GameStatus GameStatusPrefab;
    public GameScore GameScorePrefab;
    //constructor
    private void Start()
    {
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainHero" && !isTriggerEnter2D)
        {
            MainHeroEntered();
            isTriggerEnter2D = true;
        }
    }

    //the actions if the main hero finished the level
    void MainHeroEntered()
    {
        //stops the game and doing some animations
        GameStatusPrefab.StopTheGame();

        //If we're playing tutorial, we shouldn't make available next level
        if (!GameStatusPrefab.IsTutorialNow)
        {
            //checked the level as passed
            GameStatusPrefab.IsLevelPassed[GameStatusPrefab.CurrentLevel] = true;
        }
        else //exit from the tutorial
        {
            GameStatusPrefab.IsTutorialNow = false;
        }
        //sets the best result for the current level
        GameScorePrefab.SetTheBestResult(GameScorePrefab.GetScoreForCurrentLevel());
    }
}

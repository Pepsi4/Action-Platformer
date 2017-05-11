using UnityEngine;
using UnityEngine.UI;
using System;

public class GameStatus : MonoBehaviour
{
    #region fields and properties
    /// <summary>
    /// If we are playing multiplayer.
    /// </summary>
    public static bool IsMultiplayerNow;

    /// <summary>
    /// If we scee the score panel (After passed or failed level).
    /// </summary>
    public static bool IsScorePanelShowing { get; set; }

    /// <summary>
    /// If the tutorial scene now.
    /// </summary>
    public static bool IsTutorialNow
    {
        get { return isTutorialNow; }
        set { isTutorialNow = value; }
    }
    
    /// <summary>
    /// The current value of level's count.
    /// </summary>
    public static int LevelCount
    {
        get { return levelsCount; }
    }

    /// <summary>
    /// Boolian shows us: if the game is active or not.
    /// </summary>
    public static bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    /// <summary>
    /// how long the level is playing
    /// </summary>
    public float TimeActive { get; set; }
    
    /// <summary>
    /// The current level.
    /// </summary>
    public int CurrentLevel
    { 
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    private static int levelsCount = 3;
    /// <summary>
    /// Array is contains bool values - if we passed the level already. Lengh = static int levelsCount.
    /// </summary>
    public static bool[] IsLevelPassed = new bool[levelsCount];
    
    private static bool isActive = true;
    private int currentLevel = 0;
    private static bool isTutorialNow = false;
    #endregion

    #region public methods
    
    //We have to pause the game sometimes.
    //For ex: the level finished by player.
    /// <summary>
    /// Stops the game. Shows the after game UI.
    /// </summary>
    public void StopTheGame()
    {
        //UI part
        //starts the animation
        GameObject.Find("Canvas/ScorePanel").GetComponent<Animator>().Play("ScorePanel");
        //updating text
        GameObject.Find("Canvas/ScorePanel/ScoreText").GetComponent<Text>().text = "Your time: " + TimeActive.ToString("0.##");

        //pause the game
        GameStatus.Pause();
        //show the stars
        GameObject.Find("Canvas/ScorePanel").GetComponent<ScorePanelScript>().ShowStar();
    }
    
    /// <summary>
    /// Pauses the game but the score panel is not showing.
    /// </summary>
    /// <param name="isForced"></param>
    public void StopTheGame(bool isForced)
    {
        if (isForced)
        {
            //UI part
            //starts the animation
            GameObject.Find("Canvas/ScorePanel").GetComponent<Animator>().Play("ScorePanel");
            //updating text
            GameObject.Find("Canvas/ScorePanel/ScoreText").GetComponent<Text>().text = "Your time: " +  TimeActive.ToString("0.##");

            //pause the game
            GameStatus.Pause();
        }
    }

    /// <summary>
    /// Freezing gameobjects. Stops hero and ballooon spawning. Makes game`s IsActive disabled. Invuled mainhero.
    /// </summary>
    public static void Pause()
    {
        Debug.Log("Paused");
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

    /// <summary>
    /// Makes game active; hero can take dmg; unfreezing balloons and main hero
    /// </summary>
    public static void UnPause()
    {

        //sets game as active again
        IsActive = true;

        //player can take dmg again 
        PlayerScript.IsCanTakeDamage = true;

        try //Main hero can be not exist right now on scene.
        {
            //unfreezing main hero
            GameObject.Find("MainHero").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        catch (NullReferenceException nullRef)
        {
            Debug.Log("Main hero is not exist right now. We can't unfreez it.");
        }

        //unfreezing balloons
        GameObject[] movingObjects = GameObject.FindGameObjectsWithTag("Moving");

        foreach (GameObject obj in movingObjects)
        {
            obj.GetComponent<Rigidbody2D>().constraints = 0;
        }
    }

    /// <summary>
    /// Unpausing the game.
    /// </summary>
    /// <param name="isTakeDamage"></param>
    public static void UnPause(bool isTakeDamage)
    {
        //sets game as active again
        IsActive = true;

        if (isTakeDamage)
        {
            //player can take dmg again 
            PlayerScript.IsCanTakeDamage = true;
        }

        //unfreezing main hero
        GameObject.Find("MainHero").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        //unfreezing balloons
        GameObject[] movingObjects = GameObject.FindGameObjectsWithTag("Moving");

        foreach (GameObject obj in movingObjects)
        {
            obj.GetComponent<Rigidbody2D>().constraints = 0;
        }
    }

    public static int GetPassedLevelsCount()
    {
        int count = 0;
        foreach (bool level in IsLevelPassed)
        {
            if (level == true) count++;
        }
        return count;
    }

    #endregion

    private void Update()
    {
        if (isActive)
        {
            TimeActive += (Time.deltaTime);
        }
    }
}

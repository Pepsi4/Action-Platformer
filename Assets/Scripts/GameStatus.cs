using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    /// <summary>
    /// Stops the game. Shows the after game UI.
    /// </summary>
    public static void StopTheGame()
    {
        //UI part
        //starts the animation
        GameObject.Find("Canvas/ScorePanel").GetComponent<Animator>().Play("ScorePanel");
        //updating text
        GameObject.Find("Canvas/ScorePanel/ScoreText").GetComponent<Text>().text = "Your time: " + GameStatus.TimeActive.ToString("0.##");

        //pause the game
        GameStatus.Pause();
        //show the stars
        GameObject.Find("Canvas/ScorePanel").GetComponent<ScorePanelScript>().ShowStar();
    }

    public static void StopTheGame(bool isForced)
    {
        if (isForced)
        {
            //UI part
            //starts the animation
            GameObject.Find("Canvas/ScorePanel").GetComponent<Animator>().Play("ScorePanel");
            //updating text
            GameObject.Find("Canvas/ScorePanel/ScoreText").GetComponent<Text>().text = "Your time: " + GameStatus.TimeActive.ToString("0.##");

            //pause the game
            GameStatus.Pause();
        }

    }

    //todo
    //public static int GetLevelsCount()
    //{

    //}

    //We have to pause the game sometimes.
    //For ex: the level finished by player.
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

        //unfreezing main hero
        GameObject.Find("MainHero").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        //unfreezing balloons
        GameObject[] movingObjects = GameObject.FindGameObjectsWithTag("Moving");

        foreach (GameObject obj in movingObjects)
        {
            obj.GetComponent<Rigidbody2D>().constraints = 0;
        }
    }

    private static int levelsCount = 3;
    /// <summary>
    /// The current value of level's count.
    /// </summary>
    public static int LevelCount
    {
        get { return levelsCount; }
    }

    //is game active
    private static bool isActive = true;
    /// <summary>
    /// Boolian shows us: if the game is active or not.
    /// </summary>
    public static bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    private void Update()
    {
        if (isActive)
        {
            TimeActive += (Time.deltaTime);
        }
    }

    /// <summary>
    /// how long the level is playing
    /// </summary>
    public static float TimeActive { get; set; }


    private static int currentLevel = 0;
    //the current level
    public static int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    /// <summary>
    /// Array is contains bool values - if we passed the level already. Lengh = static int levelsCount.
    /// </summary>
    public static bool[] IsLevelPassed = new bool[levelsCount];

    public static int GetPassedLevelsCount()
    {
        int count = 0;
        foreach (bool level in IsLevelPassed)
        {
            if (level == true) count++;
        }
        return count;
    }

    public class GameScore
    {
        //first level info
        public class FirstLevel
        {
            internal const float HightScore = 7f;
            internal const float MiddleScore = 11f;
            internal const float LowScore = 15f;

            internal static int bestResult = 0;
        }

        //second level info
        public class SecondLevel
        {
            internal const float HightScore = 5f;
            internal const float MiddleScore = 7f;
            internal const float LowScore = 9f;

            internal static int bestResult = 0;
        }

        public class ThirdLevel
        {
            internal const float HightScore = 5f;
            internal const float MiddleScore = 7f;
            internal const float LowScore = 9f;

            internal static int bestResult = 0;
        }

        internal enum Score : int
        {
            HightScore = 3,
            MiddleScore = 2,
            LowScore = 1,
            Nothing = 0
        }

        //todo: use generic

        /// <summary>
        /// Sets the best result for current level.
        /// </summary>
        /// <param name="score">the score from 0 - 3</param>
        public static void SetTheBestResult(int score)
        {
            switch (currentLevel)
            {
                case 0:
                    if (FirstLevel.bestResult < score) FirstLevel.bestResult = score;
                    break;

                case 1:
                    if (SecondLevel.bestResult < score) SecondLevel.bestResult = score;
                    break;

                case 2:
                    if (ThirdLevel.bestResult < score) ThirdLevel.bestResult = score;
                    break;
            }
        }

        /// <summary>
        /// Returns score for the variable current level. 
        /// Workout with data in classes as FirstLevel, SecondLevel etc. 
        /// </summary>
        /// <returns>
        /// 3-the best,2-middle,1-low,0-nothing
        /// </returns>
        internal static int GetScoreForCurrentLevel()
        {
            switch (CurrentLevel)
            {
                case 0:
                    return GetScore(FirstLevel.HightScore, FirstLevel.MiddleScore, FirstLevel.LowScore);
                case 1:
                    return GetScore(SecondLevel.HightScore, SecondLevel.MiddleScore, SecondLevel.LowScore);
                case 2:
                    return GetScore(ThirdLevel.HightScore, ThirdLevel.MiddleScore, ThirdLevel.LowScore);
            }
            return 0;
        }


        /// <returns>3-the best,2-middle,1-low,0-nothing</returns>
        private static int GetScore(float hight, float middle, float low)
        {
            if (TimeActive < hight)
            {
                return (int)Score.HightScore;
            }
            else if (TimeActive < middle)
            {
                return (int)Score.MiddleScore;
            }
            else if (TimeActive < low)
            {
                return (int)Score.LowScore;
            }
            else //if the score is lower, than the low score.
            {
                return (int)Score.Nothing;
            }
        }
    }
}

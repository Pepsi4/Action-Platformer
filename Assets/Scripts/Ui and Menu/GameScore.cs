using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameScore : MonoBehaviour
{
    public GameStatus GameStatusPrefab;

    public FirstLevel firstLevel;
    public SecondLevel secondLevel;
    public ThirdLevel thirdLevel;

    //first level info
    public class FirstLevel
    {
        internal const float HightScore = 7f;
        internal const float MiddleScore = 11f;
        internal const float LowScore = 15f;
        
        public int bestResult = 0;
    }

    //second level info
    public  class SecondLevel
    {
        internal const float HightScore = 5f;
        internal const float MiddleScore = 7f;
        internal const float LowScore = 9f;

        public int bestResult = 0;
    }

    public  class ThirdLevel
    {
        internal const float HightScore = 5f;
        internal const float MiddleScore = 7f;
        internal const float LowScore = 9f;

        public int bestResult = 0;
    }

    public class Tutorial
    {
        internal const float HightScore = 300f;
        internal const float MiddleScore = 600f;
        internal const float LowScore = 900f;

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
    public void SetTheBestResult(int score)
    {
        switch (GameStatusPrefab.CurrentLevel)
        {
            case -1:
                if (Tutorial.bestResult < score) Tutorial.bestResult = score;
                break;

            case 0:
                if (firstLevel.bestResult < score) firstLevel.bestResult = score;
                break;

            case 1:
                if (secondLevel.bestResult < score) secondLevel.bestResult = score;
                break;

            case 2:
                if (thirdLevel.bestResult < score) thirdLevel.bestResult = score;
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
    public int GetScoreForCurrentLevel()
    {
        switch (GameStatusPrefab.CurrentLevel)
        {
            case -1:
                return GetScore(Tutorial.HightScore, Tutorial.MiddleScore, Tutorial.LowScore);
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
    private int GetScore(float hight, float middle, float low)
    {
        if (GameStatusPrefab.TimeActive < hight)
        {
            return (int)Score.HightScore;
        }
        else if (GameStatusPrefab.TimeActive < middle)
        {
            return (int)Score.MiddleScore;
        }
        else if (GameStatusPrefab.TimeActive < low)
        {
            return (int)Score.LowScore;
        }
        else //if the score is lower, than the low score.
        {
            return (int)Score.Nothing;
        }
    }
}


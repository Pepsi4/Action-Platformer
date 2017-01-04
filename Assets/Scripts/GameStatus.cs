using UnityEngine;

public class GameStatus : MonoBehaviour
{
    //is game active
    private static bool isActive = true;
    public static bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public static float TimeActive { get; set; }

    private static int level = 1;
    public static int Level
    {
        get { return level; }
        set { level = value; }
    }

    public class GameScore
    {
        //Every level class should to have static public method 'GetLevelScore()'.

        //first level
        public class FirstLevel
        {
            private const float HightScore = 5f;
            private const float MiddleScore = 7f;
            private const float LowScore = 9f;

            public static int GetLevelScore()
            {
               return GetScore(HightScore, MiddleScore, LowScore);
            }
        }
        
        //second level
        public class SecondLevel
        {
            private const float HightScore = 5f;
            private const float MiddleScore = 7f;
            private const float LowScore = 9f;

            public static int GetLevelScore()
            {
               return GetScore(HightScore, MiddleScore, LowScore);
            }
        }

        /// <returns>3-the best,2-middle,1-low,0-nothing</returns>
        public static int GetLevelScoreByCurrnentLevel()
        {
            switch (level)
            {
                case 1:
                   return FirstLevel.GetLevelScore();
                case 2:
                    return SecondLevel.GetLevelScore();
            }
            return 0;
        }

        /// <returns>3-the best,2-middle,1-low,0-nothing</returns>
        private static int GetScore(float hight, float middle, float low)
        {
            int score = 0;

            if (TimeActive < hight)
            {
                score = 3;
                return score;
            }
            else if (TimeActive < middle)
            {
                score = 2;
                return score;
            }
            else if (TimeActive < low)
            {
                score = 1;
                return score;
            }
            else //if the score is lower, than low score.
            {
                score = 0;
                return score;
            }
        }
    }

}

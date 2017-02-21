using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    //blue color
    Color blue = new Color32(163, 240, 255, 255);

    //buttons 
    GameObject[] button;
    GameObject exitButton;
    GameObject howToPlayButton;

    //the size of button's array
    private const int ButtonCount = 3;

    private void Start()
    {
        button = new GameObject[ButtonCount];

        //here will be diference in 1 point.
        //ex: button[0] = first level button.

        //Initialization the each button in array.
        for (int x = 0; x < button.Length; x++)
        {
            button[x] = GameObject.Find("Canvas/Levels/Level (" + x + ")");
        }
        //adding to the levels onClick events
        button[0].GetComponent<Button>().onClick.AddListener(delegate
        {
            LoadTheLevel(0);
            ResetPastData();
        });
        button[1].GetComponent<Button>().onClick.AddListener(delegate
        {
            LoadTheLevel(1);
            ResetPastData();
        });
        button[2].GetComponent<Button>().onClick.AddListener(delegate
        {
            LoadTheLevel(2);
            ResetPastData();
        });

        //how to play button
        howToPlayButton = GameObject.Find("Canvas/HowToPlay");
        howToPlayButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            LoadTutorial();
            ResetPastData();
        });

        //exit button
        exitButton = GameObject.Find("Canvas/Exit");
        exitButton.GetComponent<Button>().onClick.AddListener(Exit);



        //open level if the player unlocked it before.
        OpenLevels();
        //opens small stars if it's avaible
        CheckStars();
    }

    #region Small stars

    //aray of small stars
    GameObject[] star;
    //the size of the array of small stars.
    private const int StarArraySize = 9;

    private void DeclareStars()
    {
        //creating an array with const size
        star = new GameObject[StarArraySize];

        int starNumber = 0;
        //declare each star. 3 stars per button.
        for (int i = 0; i < StarArraySize / 3; i++)
        {
            for (int x = 0; x < StarArraySize / 3; x++)
            {
                star[starNumber] = GameObject.Find("Canvas/Levels/Level (" + i + ")/Level Stars/Star (" + x + ")");
                starNumber++;
            }
        }
    }

    private void CheckStars()
    {
        DeclareStars();

        int bestResult = GameStatus.GameScore.FirstLevel.bestResult;
        ChangeStarColor(bestResult , 0);

        bestResult = GameStatus.GameScore.SecondLevel.bestResult;
        ChangeStarColor(bestResult, 1);

        bestResult = GameStatus.GameScore.ThirdLevel.bestResult;
        ChangeStarColor(bestResult, 2);
    }
    //ERROR
    private void ChangeStarColor(int starCounter, int level)
    {
        for (int x = level * 3; x < starCounter + level * 3; x++)
        {
            star[x].GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }

    #endregion
    private void LoadTheLevel(int level)
    {
        //loads level
        SceneManager.LoadScene("Level (" + level + ")");
        //level++
        GameStatus.CurrentLevel = level;
    }

    private void LoadTutorial()
    {
        GameStatus.CurrentLevel = -1;
        GameStatus.IsTutorialNow = true;
        SceneManager.LoadScene("HowToPlay");
    }

    private void Exit()
    {
        Application.Quit();
    }

    class LastButtonException : System.Exception
    {
    }

    private void OpenLevels()
    {
        //checks the passed levels and makes enable related buttons
        for (int i = 0; i < GameStatus.LevelCount; i++)
        {
            //if the previous level is passed
            if (GameStatus.IsLevelPassed[i])
            {
                if (i == GameStatus.LevelCount)
                {
                    //If the next button is not exist and we have nothing to open.
                    throw new System.IndexOutOfRangeException();
                }

                try
                {
                    //makes next button enabled
                    button[i + 1].GetComponent<Button>().enabled = true;
                    //changes the color of enabled button
                    button[i + 1].GetComponent<Image>().color = blue;
                }
                catch (System.IndexOutOfRangeException e)
                {
                    Debug.Log("All levels are passed.");
                }
            }
        }
    }

    /// <summary>
    /// Resets past static data as: static-float time, etc.
    /// </summary>
    private void ResetPastData()
    {
        //reset active time
        GameStatus.TimeActive = 0;
        //reset data
        GameStatus.UnPause();
        //Reset HP
        PlayerScript.Lifes = PlayerScript.LifesMax;
    }
}



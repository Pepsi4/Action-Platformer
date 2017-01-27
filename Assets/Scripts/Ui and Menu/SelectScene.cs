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
            button[x] = GameObject.Find("Canvas/Level (" + (x + 1) + ")");
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

        //exit button
        exitButton = GameObject.Find("Canvas/Exit");
        exitButton.GetComponent<Button>().onClick.AddListener(Exit);

        //how to play button
        howToPlayButton = GameObject.Find("Canvas/HowToPlay");
        howToPlayButton.GetComponent<Button>().onClick.AddListener(LoadTutorial);

        //open level if the player unlocked it before.
        OpenLevels();
        //opens small stars if it's avaible
        CheckStars();
    }

    #region Small stars

    //aray of small stars
    GameObject[] star;
    //the size of the array of small stars
    private const int StarArraySize = 10;

    private void DeclareStars()
    {
        //creating an array with const size
        star = new GameObject[StarArraySize];
        //declare each star. 3 stars per button.
        star[1] = GameObject.Find("Canvas/Level (1)/Level Stars (1)/Star (1)");
        star[2] = GameObject.Find("Canvas/Level (1)/Level Stars (1)/Star (2)");
        star[3] = GameObject.Find("Canvas/Level (1)/Level Stars (1)/Star (3)");

        //second button
        star[4] = GameObject.Find("Canvas/Level (2)/Level Stars (2)/Star (1)");
        star[5] = GameObject.Find("Canvas/Level (2)/Level Stars (2)/Star (2)");
        star[6] = GameObject.Find("Canvas/Level (2)/Level Stars (2)/Star (3)");

        //third button
        star[7] = GameObject.Find("Canvas/Level (3)/Level Stars (3)/Star (1)");
        star[8] = GameObject.Find("Canvas/Level (3)/Level Stars (3)/Star (2)");
        star[9] = GameObject.Find("Canvas/Level (3)/Level Stars (3)/Star (3)");
    }
    
    private void CheckStars()
    {
        DeclareStars();

        int bestResult = GameStatus.GameScore.FirstLevel.bestResult;
        ChangeStarColor(1, bestResult);

        bestResult = GameStatus.GameScore.SecondLevel.bestResult;
        ChangeStarColor(2, bestResult);
    }

    private void ChangeStarColor(int level, int starCounter)
    {
        if (level == 1)
        {
            for (int x = 1; x <= starCounter; x++)
            {
                star[x].GetComponent<Image>().color = new Color(255, 255, 255);
            }  
        }

        if (level == 2)
        {
            for (int x = 4; x <= starCounter + 3; x++)
            {
                star[x].GetComponent<Image>().color = new Color(255, 255, 255);
            }
        }
    }

    #endregion
    private void LoadTheLevel(int level)
    {
        //loads level
        SceneManager.LoadScene("Level (" + (level + 1) + ")");
        //level++
        GameStatus.CurrentLevel = level + 1;
    }

    private void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void OpenLevels()
    {
        //checks the passed levels and makes enable related buttons
        for (int i = 0; i < GameStatus.IsLevelPassed.Length; i++)
        {
            if (GameStatus.IsLevelPassed[i])
            {
                //makes button enabled
                button[i].GetComponent<Button>().enabled = true;
                //changes the color of enabled button
                button[i].GetComponent<Image>().color = blue;
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

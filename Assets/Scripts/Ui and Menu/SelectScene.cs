﻿using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    //blue color
    Color blue = new Color32(163, 240, 255, 255);

    //buttons 
    GameObject[] button;
    GameObject exitButton;
    GameObject howToPlayButton;

    public GameScore GameScorePrefab;
    public GameStatus GameStatusPrefab;
    //the size of button's array
    private const int ButtonCount = 3;

    private void Start()
    {
        button = new GameObject[ButtonCount];

        //here will be diference in 1 point.
        //ex: button[0] = first level button.

        GameStatusPrefab = GameObject.Find("GameStatus").GetComponent<GameStatus>();
        GameScorePrefab = GameObject.Find("GameScore").GetComponent<GameScore>();

        InitStars();

        //exit button
        exitButton = GameObject.Find("Canvas/Exit");
        exitButton.GetComponent<Button>().onClick.AddListener(Exit);

        //open level if the player unlocked it before.
        OpenLevels();
        //opens small stars if it's avaible
        CheckStars();
    }

    private void InitStars()
    {
        //Initialization the each button in array.
        for (int x = 0; x < button.Length; x++)
        {
            button[x] = GameObject.Find("Canvas/Levels/Level (" + x + ")");
        }
        //adding to the levels onClick events
        button[0].GetComponent<Button>().onClick.AddListener(delegate
        {
            LoadTheLevel(0);
            GameStatusPrefab.ResetPastData();
        });
        button[1].GetComponent<Button>().onClick.AddListener(delegate
        {
            LoadTheLevel(1);
            GameStatusPrefab.ResetPastData();
        });
        button[2].GetComponent<Button>().onClick.AddListener(delegate
        {
            LoadTheLevel(2);
            GameStatusPrefab.ResetPastData();
        });

        //how to play button
        howToPlayButton = GameObject.Find("Canvas/HowToPlay");
        howToPlayButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            LoadTutorial();
            GameStatusPrefab.ResetPastData();
        });
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
        int bestResult = GameScorePrefab.firstLevel.bestResult;
        ChangeStarColor(bestResult, 0);

        bestResult = GameScorePrefab.secondLevel.bestResult;
        ChangeStarColor(bestResult, 1);

        bestResult = GameScorePrefab.thirdLevel.bestResult;
        ChangeStarColor(bestResult, 2);
    }

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
        GameStatusPrefab.CurrentLevel = level;
        
        //Network.
        //NetworkManager.singleton.networkPort = 888;
        NetworkManager.singleton.StartHost();
    }

    private void LoadTutorial()
    {
        GameStatusPrefab.CurrentLevel = -1;
        GameStatusPrefab.IsTutorialNow = true;
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
        for (int i = 0; i < GameStatusPrefab.LevelCount; i++)
        {
            try
            {
                //if the previous level is passed
                if (GameStatusPrefab.IsLevelPassed[i])
                {
                    if (i == GameStatusPrefab.LevelCount)
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
            catch (System.IndexOutOfRangeException)
            {
                Debug.Log("Menu opens first time.");
            }
        }
    }
}



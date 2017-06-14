using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //public GameStatus GameStatusPrefab;

	void Start ()
	{
	    GameObject buttonStartTheGame =  GameObject.Find("Canvas/StartTheGame");
	    GameObject buttonStartTheMultiplayer = GameObject.Find("Canvas/StartTheMultiplayer");
	    GameObject buttonExit = GameObject.Find("Canvas/Exit");

	    buttonStartTheGame.GetComponent<Button>().onClick.AddListener(StartTheGame);
	    buttonStartTheMultiplayer.GetComponent<Button>().onClick.AddListener(StartTheMultiplayer);
	    buttonExit.GetComponent<Button>().onClick.AddListener(Application.Quit);
	}

    void StartTheMultiplayer()
    {
        //GameStatusPrefab.IsActive = true;
        SceneManager.LoadScene("MultiplayerMenu");
    }

    void StartTheGame()
    {
        SceneManager.LoadScene("SelectScene");
    }
	
	
}

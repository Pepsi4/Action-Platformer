using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    
	void Start ()
	{
	    GameObject buttonStartTheGame =  GameObject.Find("Canvas/StartTheGame");
	    GameObject buttonStartTheMultiplayer = GameObject.Find("Canvas/StartTheMultiplayer");
	    GameObject buttonExit = GameObject.Find("Canvas/Exit");

	    buttonStartTheGame.GetComponent<Button>().onClick.AddListener(StartTheGame);
	}

    void StartTheGame()
    {
        SceneManager.LoadScene("SinglePlayer");
    }
	
	
}

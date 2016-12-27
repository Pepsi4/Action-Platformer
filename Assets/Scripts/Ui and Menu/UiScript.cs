using UnityEngine.UI;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    //using for constructor
    GameObject timePanelText;

    void Start () //constructor
    {
	    timePanelText = GameObject.Find("Canvas/TimePanel/Text");
	}
	
	void Update ()
	{
	    UpdateTimer();
	}

    void UpdateTimer()
    {
        if (GameStatus.IsActive)
        {
            GameStatus.TimeActive = (float)(Time.time);
            timePanelText.GetComponent<Text>().text = GameStatus.TimeActive.ToString("0.##");
        }
    }
}

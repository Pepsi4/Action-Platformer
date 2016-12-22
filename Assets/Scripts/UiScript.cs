using System.Globalization;
using UnityEngine.UI;
using UnityEngine;

public class UiScript : MonoBehaviour
{


    GameObject timePanelText;

    void Start ()
	{
	    timePanelText = GameObject.Find("Canvas/TimePanel/Text");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float time = (float) (Time.time);
	    timePanelText.GetComponent<Text>().text = time.ToString("0.##");
	}
}

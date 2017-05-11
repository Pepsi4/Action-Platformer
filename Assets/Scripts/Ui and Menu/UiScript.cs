using UnityEngine.UI;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    public GameStatus GameStatusPrefab;
    //using for constructor
    GameObject timePanelText;

    void Start() //constructor
    {
        timePanelText = GameObject.Find("Canvas/TimePanel/Text");
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timePanelText.GetComponent<Text>().text = GameStatusPrefab.TimeActive.ToString("0.##");
    }
}

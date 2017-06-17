using UnityEngine.UI;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    public GameObject GameStatusPrefab;
    public GameObject TimePanel;

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        try
        {
            TimePanel.GetComponentInChildren<Text>().text = GameStatusPrefab.GetComponent<GameStatus>().TimeActive.ToString("0.##");
        }
        catch (UnassignedReferenceException)
        {
            Debug.Log("Time panel is not exists yet.");
        }
    }
}

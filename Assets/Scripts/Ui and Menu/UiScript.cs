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
        TimePanel.GetComponentInChildren<Text>().text = GameStatusPrefab.GetComponent<GameStatus>().TimeActive.ToString("0.##");
    }
}

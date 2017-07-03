using UnityEngine.UI;
using UnityEngine;

public class UiScript : MonoBehaviour //todo: rename
{
    public GameObject GameStatusPrefab;
    public GameObject TimePanel;
    public GameObject MainHero;

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (MainHero.GetComponent<PlayerScript>().IsTimerRecordable)
        {
            try
            {
                // Updates time on UI.
                TimePanel.GetComponentInChildren<Text>().text = GameStatusPrefab.GetComponent<GameStatus>().TimeActive.ToString("0.##");
            }
            catch (UnassignedReferenceException)
            {
                Debug.Log("Time panel is not exists yet.");
            }
        }
    }
}

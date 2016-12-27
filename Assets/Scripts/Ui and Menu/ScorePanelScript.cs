using UnityEngine;

public class ScorePanelScript : MonoBehaviour {

    public void StopPanelAnimation()
    {
        GameObject.Find("Canvas/ScorePanel").GetComponent<Animator>().Stop();
    }
}

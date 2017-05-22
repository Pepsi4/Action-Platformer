using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{

    public TutorialScene TutorialScenePrefab;
    public GameStatus GameStatusPrefab;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainHero" && GameStatusPrefab.IsActive)
        {
            Debug.Log("Next Tutorial tip");
            //makes the trigger zone unenabled
            Destroy(this.gameObject);
            //
            TutorialScene.ChangeTheText();
            //tip number is getting bigger
            TutorialScenePrefab.TipNumber++;
            //
            GameStatusPrefab.Pause();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainHero" && GameStatus.IsActive)
        {
            Debug.Log("Next Tutorial tip");
            //makes the trigger zone unenabled
            Destroy(this.gameObject);
            //
            TutorialScene.ChangeTheText();
            //tip number is getting bigger
            TutorialScene.TipNumber++;
            //
            GameStatus.Pause();
        }
    }
}

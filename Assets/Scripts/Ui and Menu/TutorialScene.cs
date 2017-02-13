using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScene : MonoBehaviour
{
    private Button _exitButton;
    private Text _text;

    //how many times the button 'F' pressed
    private static int _counter = 0;

    void Start()
    {
        Initialization();
        GameStatus.Pause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeTheText();
            _counter++;
        }
    }

    private void Initialization()
    {
        _text = GameObject.Find("Canvas/Panel/Text").GetComponent<Text>();
        _exitButton = GameObject.Find("Canvas/Exit").GetComponent<Button>();

        //Loading SelectScene when we click it 
        _exitButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("SelectScene");
        });
    }

    private void ChangeTheText()
    {
        //full text
        TextAsset textFull = Resources.Load<TextAsset>("Texts/Tutorial");
        //now our text is array and splited by new line.
        string[] textArray = textFull.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        //get and set the current string
        _text.text = textArray[_counter];
    }

    //private void HideTheDialog()
    //{
    //    _text.enabled = false;
    //    _
    //}
}

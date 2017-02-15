using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScene : MonoBehaviour
{
    private Button _exitButton;
    private Text _text;

    private static int _tipNumber = 0;

    public static int TipNumber
    {
        get { return _tipNumber; }
        set { _tipNumber = value; }
    }


    void Start()
    {
        Initialization();
        GameStatus.Pause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameStatus.UnPause();
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

    /// <summary>
    /// Set to the text panel string from the txt file with the currnet number. (the txt is splited by enter)
    /// </summary>
    public static void ChangeTheText()
    {
        //full text
        TextAsset textFull = Resources.Load<TextAsset>("Texts/Tutorial");
        //now our text is array which is splited by new line.
        string[] textArray = textFull.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        //get and set the current string
        GameObject.Find("Canvas/Panel/Text").GetComponent<Text>().text = textArray[_tipNumber];
    }

    //private void HideTheDialog()
    //{
    //    _text.enabled = false;
    //    _
    //}
}

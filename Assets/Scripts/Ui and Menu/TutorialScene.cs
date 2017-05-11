using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScene : MonoBehaviour
{
    private Button _exitButton;
    private Text _text;

    private static int _tipCoount = 0;
    private static int _tipNumber = 0;

    private  bool _isLevelFinished = false;

    public static int TipNumber
    {
        get { return _tipNumber; }
        set { _tipNumber = value; }
    }

    public bool IsLastTip(int number)
    {
        if (number  ==  _tipCoount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
        TutorialTrigger[] triggers = GameObject.Find("TutorialTriggers").GetComponentsInChildren<TutorialTrigger>();
        foreach (TutorialTrigger trigger in triggers)
        {
            _tipCoount++;
        }

        Debug.Log(_tipCoount + " : Tip`s count");
        Debug.Log(GameStatus.IsTutorialNow + " : is tutor now");

        Initialization();
        GameStatus.Pause();
        //in tutorial our hero can't take any dmg
        PlayerScript.IsCanTakeDamage = false;
        Debug.Log(PlayerScript.IsCanTakeDamage + "   is can take dmg");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isLevelFinished == false)
        {
            if (IsLastTip(_tipNumber))
            {
                _isLevelFinished = true;
            }
            GameStatus.UnPause(false);
        }
    }

    private void Initialization()
    {
        _text = GameObject.Find("Canvas/Panel/Text").GetComponent<Text>();
        _exitButton = GameObject.Find("Canvas/Exit").GetComponent<Button>();

        //exit from the tutorial
        _exitButton.onClick.AddListener(delegate
        {
            GameStatus.IsTutorialNow = false;
            //Loading SelectScene when we click it
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
}

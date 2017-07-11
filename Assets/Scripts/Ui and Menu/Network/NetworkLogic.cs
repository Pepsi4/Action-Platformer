using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkLogic : NetworkBehaviour {

    [Command]
    public void CmdShowResult(GameObject obj, Color col, int connectionId)
    {
        NetworkIdentity objNetId = obj.GetComponent<NetworkIdentity>();
        objNetId.AssignClientAuthority(connectionToClient);
    }

    [Command]
    public void CmdChangeColor(GameObject obj, Color col)
    {
        RpcChangeColor(obj, col);
    }

    [ClientRpc]
    public void RpcChangeColor(GameObject obj, Color col)
    {
        //Changes the color.
        obj.GetComponent<SpriteRenderer>().color = col;
    }

    [ClientRpc]
    public void RpcShowResult(GameObject obj, int connectionId)
    {
        //Makes the UI visible.
        GameObject.Find("Canvas/MultiplayerEndUi/Panel").GetComponent<Image>().enabled = true;
        GameObject.Find("Canvas/MultiplayerEndUi/Panel/Text").GetComponent<Text>().enabled = true;
        GameObject.Find("Canvas/MultiplayerEndUi/Panel/Exit").GetComponent<Image>().enabled = true;
        GameObject.Find("Canvas/MultiplayerEndUi/Panel/Exit/Text").GetComponent<Text>().enabled = true;

        //Adds EventSystem.
        if (GameObject.Find("EventSystem") == false)
        {
            GameObject eventSytem = Instantiate((GameObject)Resources.Load("GameObjects/EventSystem"));
            eventSytem.name = "EventSystem";
        }

        //Adds to the button delegate.
        GameObject.Find("Canvas/MultiplayerEndUi/Panel/Exit").GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Exit Button Pressed");
            SceneManager.LoadScene("WaitingRoom");
        });

        //Changes the text of the finall UI.
        if (connectionToServer.connectionId == connectionId)
        {
            GameObject.Find("Canvas/MultiplayerEndUi/Panel/Text").GetComponent<Text>().text = "You lose!";
        }
        else
        {
            GameObject.Find("Canvas/MultiplayerEndUi/Panel/Text").GetComponent<Text>().text = "You won!";
        }
    }

    [Command]
    public void CmdDebugLog(string message)
    {
        Debug.Log(message);
    }

    [Command]
    public void CmdLoadScene(string levelName)
    {
        if (NetworkManager.networkSceneName != levelName)
        {
            NetworkManager.singleton.ServerChangeScene(levelName);
        }
    }
}

using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class NetworkLogic : NetworkBehaviour {

    [Command]
    public void CmdShowResult(GameObject obj, Color col, int connectionId)
    {
        NetworkIdentity objNetId = obj.GetComponent<NetworkIdentity>();
        objNetId.AssignClientAuthority(connectionToClient);
        RpcShowResult(obj, col, connectionId);
    }

    [ClientRpc]
    public void RpcShowResult(GameObject obj, Color col, int connectionId)
    {
        //Changes the color too.
        obj.GetComponent<SpriteRenderer>().color = col;

        //Makes the UI visible.
        GameObject.Find("Canvas/MultiplayerEndUi/Panel").GetComponent<Image>().enabled = true;
        GameObject.Find("Canvas/MultiplayerEndUi/Panel/Text").GetComponent<Text>().enabled = true;

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
    public void CmdLoadScene()
    {
        if (NetworkManager.networkSceneName != "Level (0)")
        {
            NetworkManager.singleton.ServerChangeScene("Level (0)");
        }
    }
}

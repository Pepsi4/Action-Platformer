using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Msg
{
    public const short LoginClient = 100;
    public const short StartGame = 101;
}

public class OnReady : MessageBase
{
    public string Name;

    public OnReady()
    {
    }
}

public class MyNetworkManager : MonoBehaviour
{
    public WaitingRoomScript WaitingRoomScript;

    public string IpAddress;
    public string Port;

    private void OnReady(NetworkMessage netMsg)
    {
        OnOnReady(netMsg);
    }

    private void OnOnReady(NetworkMessage netMsg)
    {
        var readyCount = netMsg.ReadMessage<IntegerMessage>();
        WaitingRoomScript.ReadyCount = readyCount.value;
        //WaitingRoomScript.IsReady = true;
    }

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Network"));
    }

    private void OnLevelWasLoaded(int level)
    {
        try
        {
            GameObject.Find("Canvas/Join").GetComponent<Button>().onClick.AddListener(Join);
            GameObject.Find("Canvas/Host").GetComponent<Button>().onClick.AddListener(Host);
        }
        catch { }
    }

    private void Join()
    {
        // Trying to connect.
        IpAddress = GameObject.Find("Canvas/Join/IpField/Text").GetComponent<Text>().text;
        Port = GameObject.Find("Canvas/Join/PortField/Text").GetComponent<Text>().text;

        NetworkManager.singleton.networkAddress = IpAddress;
        NetworkManager.singleton.networkPort = int.Parse(Port);
        NetworkClient client = NetworkManager.singleton.StartClient();

        client.RegisterHandler(Msg.StartGame, OnReady);
    }

    private void Host()
    {
        // Trying to host.
        Port = GameObject.Find("Canvas/Host/PortField/Text").GetComponent<Text>().text;

        NetworkManager.singleton.networkAddress = IpAddress;
        NetworkManager.singleton.networkPort = int.Parse(Port);
        NetworkClient client = NetworkManager.singleton.StartHost();

        NetworkServer.RegisterHandler(Msg.StartGame, OnReady);
    }
}

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour
{
    public string IpAddress;
    public string Port;
    //public float GuiOffset;

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Network"));

        GameObject.Find("Canvas/Join").GetComponent<Button>().onClick.AddListener(Join);
        GameObject.Find("Canvas/Host").GetComponent<Button>().onClick.AddListener(Host);
    }

    private void Join()
    {
        // Trying to connect.
        IpAddress = GameObject.Find("Canvas/Join/IpField/Text").GetComponent<Text>().text;
        Port = GameObject.Find("Canvas/Join/PortField/Text").GetComponent<Text>().text;

        NetworkManager.singleton.networkAddress = IpAddress;
        NetworkManager.singleton.networkPort = int.Parse(Port);
        NetworkClient client = NetworkManager.singleton.StartClient();
        //NetworkManager.singleton.OnServerAddPlayer(client.connection, 0);
        //ClientScene.Ready(client.connection);
        //ClientScene.AddPlayer(0);
    }

    private void OnConnectedToServer()
    {
        Debug.Log("CONNETED");
    }

    private void Host()
    {
        // Trying to host.
        Port = GameObject.Find("Canvas/Host/PortField/Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = IpAddress;
        NetworkManager.singleton.networkPort = int.Parse(Port);
        NetworkClient client = NetworkManager.singleton.StartHost();
        //NetworkManager.singleton.OnServerAddPlayer(client.connection, 0);
        //NetworkServer.RegisterHandler(MsgType.AddPlayer, OnClientAddPlayer);
    }

    private void OnClientAddPlayer(NetworkMessage netMsg)
    {
            Debug.Log("Spawning player...");
            SpawnPlayer(netMsg.conn); // the above function
    }

    private void SpawnPlayer(NetworkConnection conn) // spawn a new player for the desired connection
    {
        GameObject playerObj = GameObject.Instantiate(NetworkManager.singleton.playerPrefab); // instantiate on server side
        NetworkServer.AddPlayerForConnection(conn, playerObj, 0); // spawn on the clients and set owner
    }
}

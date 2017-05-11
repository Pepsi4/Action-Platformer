using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class MyNetworkManager : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Network"));

        GameObject.Find("Canvas/Join").GetComponent<Button>().onClick.AddListener(Join);
        GameObject.Find("Canvas/Host").GetComponent<Button>().onClick.AddListener(Host);
        GameObject.Find("Canvas/Ready").GetComponent<Button>().onClick.AddListener(SetPlayerReady);
    }

    private void Join()
    {
        SceneManager.LoadScene("Level (0)");
    }

    private void Host()
    {
        SceneManager.LoadScene("Level (0)");
    }


    private void OnConnectedToServer()
    {
        Debug.Log("OnConnectedToServer");
    }

    private void SetPlayerReady()
    {
        Debug.Log("SetPlayerReady");

        StartMatch();
    }

    NetworkClient _clientServer;

    private void StartMatch()
    {
        // Trying to connect.
        try
        {
            NetworkManager.singleton.networkAddress = "localhost";
            NetworkManager.singleton.networkPort = 7777;
            NetworkManager.singleton.StartClient();
        }
        catch (Exception ex) { }

        // Trying to host.
        try
        {
            if (!NetworkServer.active)
            {
                NetworkManager.singleton.networkPort = 7777;
                _clientServer = NetworkManager.singleton.StartHost();
                Debug.Log("NetworkServer active: " + NetworkServer.active + " NetworkClient active: " + NetworkServer.localClientActive);
                return;
            }
        }
        catch (Exception ex)
        {
        }

        NetworkManager.singleton.OnServerAddPlayer(_clientServer.connection, 1);


    }
}

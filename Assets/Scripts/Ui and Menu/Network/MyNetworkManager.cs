using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour
{
    public string IpAddress;
    public string Port;

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
    }

    private void Host()
    {
        // Trying to host.
        Port = GameObject.Find("Canvas/Host/PortField/Text").GetComponent<Text>().text;

        NetworkManager.singleton.networkAddress = IpAddress;
        NetworkManager.singleton.networkPort = int.Parse(Port);
        NetworkClient client = NetworkManager.singleton.StartHost();
    }
}

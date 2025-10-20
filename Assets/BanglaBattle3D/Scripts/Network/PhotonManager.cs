using UnityEngine;
#if PHOTON_INSTALLED
using Photon.Pun;
#endif
public class PhotonManager : MonoBehaviour
{
    public string gameVersion = "0.4";
    public void Connect()
    {
        #if PHOTON_INSTALLED
        PhotonNetwork.ConnectUsingSettings();
        #else
        Debug.Log("Photon not installed - fallback: offline mode enabled."); 
        #endif
    }
    public void CreateRoom(string roomName){
        #if PHOTON_INSTALLED
        PhotonNetwork.CreateRoom(roomName);
        #else
        Debug.Log("CreateRoom scaffold: " + roomName);
        #endif
    }
    public void JoinRoom(string roomName){
        #if PHOTON_INSTALLED
        PhotonNetwork.JoinRoom(roomName);
        #else
        Debug.Log("JoinRoom scaffold: " + roomName);
        #endif
    }
}

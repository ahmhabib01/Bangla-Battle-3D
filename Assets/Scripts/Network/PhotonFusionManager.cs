#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
using UnityEngine;
#if FUSION_PRESENT
using Photon.Fusion;
using Photon.Fusion.Sockets;
#endif
using System.Threading.Tasks;

public class PhotonFusionManager : MonoBehaviour
{
#if FUSION_PRESENT
    private NetworkRunner runner;

    public async Task StartHost()
    {
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = "BanglaBattleSession",
            Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        });
        Debug.Log("Fusion host started.");
    }

    public async Task StartClient()
    {
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = "BanglaBattleSession",
            Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        });
        Debug.Log("Fusion client started.");
    }
#else
    // If Fusion SDK is not present, this script will not attempt to call Fusion APIs.
    public Task StartHost() { Debug.LogWarning("Photon Fusion SDK not present. Import Fusion SDK to enable networking."); return Task.CompletedTask; }
    public Task StartClient() { Debug.LogWarning("Photon Fusion SDK not present. Import Fusion SDK to enable networking."); return Task.CompletedTask; }
#endif
}

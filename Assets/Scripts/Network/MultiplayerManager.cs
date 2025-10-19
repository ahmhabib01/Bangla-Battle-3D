using UnityEngine;
using System.Threading.Tasks;

// NOTE: This is a placeholder. Integrate your chosen networking SDK (Photon/Netcode) here.
public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public async Task CreateMatchAsync(string matchName)
    {
        Debug.Log("[Multiplayer] CreateMatch: " + matchName);
        await Task.Yield();
        // TODO: call Photon/Netcode APIs to create a room
    }

    public async Task JoinMatchAsync(string matchId)
    {
        Debug.Log("[Multiplayer] JoinMatch: " + matchId);
        await Task.Yield();
        // TODO: call Photon/Netcode APIs to join a room
    }
}

using UnityEngine;
using System.Collections;

public enum GameState { Menu, Lobby, Match, Results }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; } = GameState.Menu;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        // TODO: add state transition handling (UI, scene loading)
        Debug.Log("Game state changed to: " + newState);
    }

    public IEnumerator StartMatch()
    {
        // Example flow: load scene, initialize players, start timer
        Debug.Log("Starting match..."); 
        yield return null;
    }
}

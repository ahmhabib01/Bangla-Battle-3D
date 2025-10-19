using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game Settings")]
    public string playerName;
    public string selectedCharacter;
    public string selectedDivision;
    public int playerCoins = 1200;
    public int playerLevel = 1;
    
    [Header("Game State")]
    public GameState currentGameState;
    public int playersOnline = 0;
    
    public enum GameState
    {
        MainMenu,
        CharacterSelection,
        MapSelection,
        InGame,
        Paused,
        GameOver
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadPlayerData();
        SetGameState(GameState.MainMenu);
    }

    public void SetGameState(GameState newState)
    {
        currentGameState = newState;
        
        switch (currentGameState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.InGame:
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public void StartGame(string division)
    {
        selectedDivision = division;
        SceneManager.LoadScene(division);
        SetGameState(GameState.InGame);
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        SavePlayerData();
    }

    void LoadPlayerData()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Bangla Player");
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 1200);
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
    }

    void SavePlayerData()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("PlayerCoins", playerCoins);
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.Save();
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject characterSelectPanel;
    public GameObject mapSelectPanel;
    public GameObject settingsPanel;
    public GameObject aboutPanel;
    
    [Header("Character Selection")]
    public GameObject[] characterPreviews;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterDescriptionText;
    
    [Header("Audio")]
    public AudioClip buttonClickSound;
    public AudioClip menuMusic;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMenuMusic();
        ShowMainMenu();
    }
    
    public void PlayMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    public void ShowMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }
    
    public void ShowCharacterSelection()
    {
        HideAllPanels();
        characterSelectPanel.SetActive(true);
    }
    
    public void ShowMapSelection()
    {
        HideAllPanels();
        mapSelectPanel.SetActive(true);
    }
    
    public void ShowAboutPanel()
    {
        HideAllPanels();
        aboutPanel.SetActive(true);
    }
    
    public void SelectCharacter(string characterId)
    {
        GameManager.Instance.selectedCharacter = characterId;
        PlayButtonSound();
        
        // Update character preview
        foreach (GameObject preview in characterPreviews)
        {
            preview.SetActive(false);
        }
        
        switch (characterId)
        {
            case "major":
                characterPreviews[0].SetActive(true);
                characterNameText.text = "Major Naim";
                characterDescriptionText.text = "Army commando with drone strike ability";
                break;
            case "polash":
                characterPreviews[1].SetActive(true);
                characterNameText.text = "Polash";
                characterDescriptionText.text = "Boatman turned warrior - Fast swimming ability";
                break;
            case "rokeya":
                characterPreviews[2].SetActive(true);
                characterNameText.text = "Rokeya";
                characterDescriptionText.text = "Rural teacher with book blast attack";
                break;
        }
    }
    
    public void StartGame(string divisionMap)
    {
        PlayButtonSound();
        GameManager.Instance.StartGame(divisionMap);
    }
    
    public void QuitGame()
    {
        PlayButtonSound();
        Application.Quit();
    }
    
    void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(false);
        mapSelectPanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(false);
    }
    
    void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}

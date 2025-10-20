using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public UnityEngine.UI.Text footerText;
    void Start() { if (footerText != null) footerText.text = "Developed by Ahsan Habib"; }
    public void PlayButton() { SceneManager.LoadScene("Lobby"); }
    public void AboutUsButton() { SceneManager.LoadScene("AboutUs"); }
    public void ProfileButton() { SceneManager.LoadScene("Profile"); }
    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

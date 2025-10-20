using UnityEngine;
using UnityEngine.UI;
public class ProfileController : MonoBehaviour
{
    public InputField nameInput;
    public RawImage profileImage;
    private string saveKey = "BB_Profile_";
    void Start(){ LoadProfile(); }
    public void SaveProfile(){ if(nameInput!=null) PlayerPrefs.SetString(saveKey+"name", nameInput.text); PlayerPrefs.Save(); }
    public void LoadProfile(){ if(nameInput!=null) nameInput.text = PlayerPrefs.GetString(saveKey+"name","Player"); }
}

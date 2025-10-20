using UnityEngine;
public class AboutUsController : MonoBehaviour
{
    public string fbUrl = "https://www.facebook.com/ahm.habib.39";
    public void OpenFacebook(){ Application.OpenURL(fbUrl); }
}

using UnityEngine;
using UnityEngine.UI;
public class LobbyController : MonoBehaviour
{
    public Text modeText;
    void Start(){ if(modeText!=null) modeText.text = "Mode: Offline (Bots)"; }
    public void SetModeOffline(){ if(modeText!=null) modeText.text = "Mode: Offline (Bots)"; }
    public void SetModeOnline(){ if(modeText!=null) modeText.text = "Mode: Multiplayer (Online)"; }
}

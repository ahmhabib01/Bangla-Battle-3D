using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public AudioClip bgmClip;
    private AudioSource source;
    void Awake(){
        source = GetComponent<AudioSource>();
        if(source==null) source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.playOnAwake = false;
        if(bgmClip!=null) source.clip = bgmClip;
    }
    void Start(){ if(source!=null && source.clip!=null) source.Play(); }
}

using UnityEngine;
public class BotSpawner : MonoBehaviour
{
    public GameObject botPrefab;
    public int botCount = 3;
    public Transform[] spawnPoints;
    void Start(){
        if(botPrefab==null){
            Debug.LogWarning("BotSpawner: botPrefab not assigned."); return;
        }
        if(spawnPoints==null || spawnPoints.Length==0){
            Debug.LogWarning("BotSpawner: no spawn points assigned."); return;
        }
        for(int i=0;i<botCount;i++){
            var sp = spawnPoints[i % spawnPoints.Length];
            if(sp!=null) Instantiate(botPrefab, sp.position, Quaternion.identity);
        }
    }
}

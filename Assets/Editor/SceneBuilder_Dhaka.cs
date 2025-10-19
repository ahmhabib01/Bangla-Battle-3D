#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using UnityEngine.AI;

public class SceneBuilderDhaka
{
    [MenuItem("Tools/BanglaBattle/Generate Dhaka Playable Scene")]
    public static void GenerateDhakaScene()
    {
        string scenePath = "Assets/Scenes/10_Map_Dhaka.unity";
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Managers
        var gm = new GameObject("GameManager"); gm.AddComponent<GameManager>();
        var score = new GameObject("ScoreManager"); score.AddComponent<ScoreManager>();
        var mission = new GameObject("MissionManager"); mission.AddComponent<MissionManager>();
        var spawner = new GameObject("SpawnManager"); spawner.AddComponent<SpawnManager>();

        // Ground
        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(10,1,10);

        // Simple Dhaka blockout: rows of low buildings (cubes) and market stalls
        for (int x = -3; x <= 3; x++)
        {
            for (int z = -1; z <= 3; z++)
            {
                var b = GameObject.CreatePrimitive(PrimitiveType.Cube);
                b.transform.position = new Vector3(x*3.5f, 0.75f, z*4f+2);
                b.transform.localScale = new Vector3(3, Random.Range(1.2f, 2.5f), 2.8f);
                b.name = "Building_" + x + "_" + z;
            }
        }

        // Market stalls (small cubes)
        for (int i=0;i<6;i++)
        {
            var s = GameObject.CreatePrimitive(PrimitiveType.Cube);
            s.transform.position = new Vector3(-9 + i*3.5f, 0.35f, -6);
            s.transform.localScale = new Vector3(2,0.7f,1.5f);
            s.name = "Stall_" + i;
        }

        // Player spawn
        var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player"; player.tag = "Player"; player.transform.position = new Vector3(0,1,-8);
        player.AddComponent<CharacterController>(); player.AddComponent<PlayerController>(); player.AddComponent<Health>();
        var weapon = new GameObject("Weapon"); weapon.transform.parent = player.transform; weapon.transform.localPosition = new Vector3(0,0.5f,0.7f);
        var we = weapon.AddComponent<Weapon>(); player.GetComponent<PlayerController>().weapon = we;

        // Camera
        var camobj = new GameObject("Main Camera"); camobj.tag = "MainCamera";
        camobj.transform.position = new Vector3(0,10,-14);
        camobj.AddComponent<ThirdPersonCamera>().target = player.transform;

        // Enemy spawns around city
        var spawnParent = new GameObject("EnemySpawns");
        for (int i=0;i<6;i++)
        {
            var sp = new GameObject("SpawnPoint" + i);
            sp.transform.parent = spawnParent.transform;
            float ang = i * Mathf.PI * 2f / 6f;
            sp.transform.position = new Vector3(Mathf.Cos(ang)*9f, 0, Mathf.Sin(ang)*6f+2);
        }
        var smComp = spawner.GetComponent<SpawnManager>();
        // create enemy prefab
        var enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        enemy.name = "EnemyPrefab";
        enemy.AddComponent<NavMeshAgent>();
        var simple = enemy.AddComponent<SimpleEnemy>();
        simple.target = player.transform;
        enemy.AddComponent<Health>();
        enemy.SetActive(false);
        Directory.CreateDirectory("Assets/Prefabs/Enemies");
        PrefabUtility.SaveAsPrefabAsset(enemy, "Assets/Prefabs/Enemies/EnemyPrefab.prefab");
        DestroyImmediate(enemy);
        var loadedEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemies/EnemyPrefab.prefab");
        smComp.enemyPrefab = loadedEnemy;
        smComp.enemySpawnPoints = spawnParent.GetComponentsInChildren<Transform>();
        smComp.enemyCount = 6;

        // Rickshaw objective (cylinder)
        var rick = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        rick.name = "RickshawObjective"; rick.transform.position = new Vector3(0,0.5f,6);
        rick.AddComponent<RickshawObjective>();

        // Respawn point and Result Screen (simple UI placeholder)
        var resp = new GameObject("RespawnPoint"); resp.transform.position = new Vector3(0,1,-8);
        var result = new GameObject("ResultScreen"); var canvas = result.AddComponent<UnityEngine.Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var txtGO = new GameObject("ResultText"); txtGO.transform.parent = result.transform;
        var txt = txtGO.AddComponent<UnityEngine.UI.Text>(); txt.text = "RESULT: -"; txt.fontSize = 36; txt.alignment = TextAnchor.MiddleCenter;
        txt.rectTransform.anchoredPosition = new UnityEngine.Vector2(0,0);

        // NavMesh
        var navObj = new GameObject("NavMeshSurface"); var surf = navObj.AddComponent<UnityEngine.AI.NavMeshSurface>();
        surf.BuildNavMesh();

        // Save scene
        Directory.CreateDirectory(Path.GetDirectoryName(scenePath));
        EditorSceneManager.SaveScene(scene, scenePath);
        AssetDatabase.SaveAssets(); AssetDatabase.Refresh();
        Debug.Log("Dhaka scene created: " + scenePath);
    }
}
#endif

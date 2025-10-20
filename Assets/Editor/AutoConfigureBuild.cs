#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.Collections.Generic;

public class AutoConfigureBuild
{
    [MenuItem("Tools/BanglaBattle/Configure PlayerSettings and Build (Android)")]
    public static void ConfigureAndBuild()
    {
        // Basic PlayerSettings configuration for Android builds
        PlayerSettings.companyName = "YourCompany";
        PlayerSettings.productName = "Bangla Battle 3D";
        PlayerSettings.applicationIdentifier = "com.yourcompany.banglabattle3d";
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel21; // API 21
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        PlayerSettings.bundleVersion = "0.1.0";

        // Add scenes from Assets/Scenes if present
        var scenes = new List<string>();
        foreach (var scene in AssetDatabase.FindAssets("t:Scene", new string[] { "Assets/Scenes" }))
        {
            var path = AssetDatabase.GUIDToAssetPath(scene);
            if (!scenes.Contains(path)) scenes.Add(path);
        }
        if (scenes.Count == 0)
        {
            Debug.LogWarning("No scenes found in Assets/Scenes. Please add your main scene to Assets/Scenes and try again.");
            return;
        }
        EditorBuildSettings.scenes = new EditorBuildSettingsScene[scenes.Count];
        for (int i = 0; i < scenes.Count; i++)
        {
            EditorBuildSettings.scenes[i] = new EditorBuildSettingsScene(scenes[i], true);
        }

        // Build player
        string buildPath = "Builds/Android/BanglaBattle3D.apk";
        var report = BuildPipeline.BuildPlayer(scenes.ToArray(), buildPath, BuildTarget.Android, BuildOptions.None);
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + buildPath);
        }
        else
        {
            Debug.LogError("Build failed: " + report.summary.result);
        }
    }
}
#endif

# CI / GitHub Actions - Unity Android Build (Added)

This project package has been updated to include a GitHub Actions workflow for automatic Android builds.
Files added:

- .github/workflows/unity-android-build.yml
- Assets/Editor/AutoConfigureBuild.cs

## How to use GitHub Actions build
1. Create a GitHub repository and push this project (all files) to the `main` branch.
2. Add required secrets in your repo settings -> Secrets:
   - UNITY_LICENSE : base64-encoded Unity license file (see Unity docs on activating license for CI)
   - ANDROID_KEYSTORE : base64-encoded keystore file (optional, for release builds)
   - ANDROID_KEYSTORE_PASSWORD
   - ANDROID_KEY_ALIAS
   - ANDROID_KEY_PASSWORD
3. The workflow uses `game-ci/unity-builder@v2` to perform the build on ubuntu-latest.
4. You can tweak the unityVersion field in .github/workflows/unity-android-build.yml to match your installed Unity version.

## How to run a local editor build
- Open Unity Editor, then go to Tools -> BanglaBattle -> Configure PlayerSettings and Build (Android).
- This will auto-add scenes found under Assets/Scenes to Build Settings and try to produce Builds/Android/BanglaBattle3D.apk.


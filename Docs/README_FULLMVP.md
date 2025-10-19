Bangla Battle 3D - Playable MVP with Photon Fusion integration placeholders
Created: 2025-10-19T13:23:44.433675 UTC

This package adds:
- Dhaka blockout scene generator (Tools > BanglaBattle > Generate Dhaka Playable Scene)
- More enemies (6 spawn points) and improved SpawnManager settings
- Respawn point and simple Result Screen placeholder
- PhotonFusionManager.cs: placeholder script showing how to start Host/Client with Fusion's NetworkRunner. This file will work once you import Photon Fusion SDK into the project.
- Docs/PHOTON_FUSION_SETUP.md with high-level steps (see official docs linked in chat for exact steps and latest guidance).

How to test multiplayer locally:
1. Import Photon Fusion SDK (see docs). Set your App ID in Tools > Fusion > Realtime Settings.
2. Create two builds or use editor + standalone build to test Host/Client.
3. Run StartHost() or StartClient() from PhotonFusionManager (example usage in code comments).

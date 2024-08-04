using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using Utilities;
using InputControls;
namespace Network
{
    public class NetworkSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPlayer m_PlayerPrefab;

        private CharacterInputHandler m_LocalCharacterInputHandler;

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                Debug.Log("[NetworkSpawner] OnPlayerJoined. We are server. Spawning player");
                runner.Spawn(m_PlayerPrefab, Utils.GetRandomSpawnPoint(),Quaternion.identity, player);
                return;
            }
            Debug.Log("[NetworkSpawner] OnPlayerJoined.");
        }

        public void OnInput(NetworkRunner runner, NetworkInput input) 
        {
            if(m_LocalCharacterInputHandler == null && NetworkPlayer.LocalPlayer != null)
                m_LocalCharacterInputHandler = NetworkPlayer.LocalPlayer.GetComponent<CharacterInputHandler>();

            if (m_LocalCharacterInputHandler != null)
                input.Set(m_LocalCharacterInputHandler.GetNetworkInput());
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            Debug.Log("[NetworkSpawner] OnConnectedToServer");
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            Debug.Log("[NetworkSpawner] OnConnectFailed");
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            Debug.Log("[NetworkSpawner] OnConnectRequest");
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            Debug.Log("[NetworkSpawner] OnDisconnectedFromServer");
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }


        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

        public void OnSceneLoadDone(NetworkRunner runner) { }

        public void OnSceneLoadStart(NetworkRunner runner) { }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            Debug.Log("[NetworkSpawner] OnShutdown");
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    }
}

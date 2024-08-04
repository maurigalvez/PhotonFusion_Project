using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
namespace Network
{
    public class NetworkRunnerHandler : MonoBehaviour
    {
        [SerializeField] private NetworkRunner m_NetworkRunnerPrefab;

        private NetworkRunner m_NetworkRunnerInstance;

        private void Start()
        {
            m_NetworkRunnerInstance = Instantiate(m_NetworkRunnerPrefab);
            m_NetworkRunnerInstance.name = "Network Runner";

            var clientTask = InitializeNetworkRunner(m_NetworkRunnerInstance, GameMode.AutoHostOrClient, NetAddress.Any(), SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), null);

            Debug.Log("[NetworkRunnerHandler] Network Runner Started!");
        }

        protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
        {
            var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
            if (sceneManager == null)
            {
                sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            }

            runner.ProvideInput = true;

            return runner.StartGame(new StartGameArgs
            {
                GameMode = gameMode,
                Address = address,
                Scene = scene,
                SessionName = "TestRoom",
                OnGameStarted = initialized,
                SceneManager = sceneManager
            });
        }
    }
}

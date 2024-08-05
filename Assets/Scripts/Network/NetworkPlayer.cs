using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Utilities;
namespace Network
{
    public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
    {
        public static NetworkPlayer LocalPlayer { get; private set; }

        [SerializeField] private Transform m_PlayerModel;
        private const string LOCALMODEL_LAYERNAME = "LocalPlayerModel";

        public override void Spawned()
        {
            /// If we don't check for this, it will run for every client!
            if(Object.HasInputAuthority)
            {
                LocalPlayer = this;
                // set layer for local player
                Utils.SetRenderLayerInChildren(m_PlayerModel, LayerMask.NameToLayer(LOCALMODEL_LAYERNAME));
                Debug.Log("[NetworkPlayer] Spawned Local Player");
                return;
            }
            var localCamera = GetComponentInChildren<Camera>();
            if(localCamera)
                localCamera.enabled = false;

            var audioListender = GetComponentInChildren<AudioListener>();
            if(audioListender)
                audioListender.enabled = false;

            Debug.Log("[NetworkPlayer] Spawned Remote Player");
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority)
                Runner.Despawn(Object);            
        }

    }
}

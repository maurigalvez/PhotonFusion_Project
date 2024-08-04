using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
namespace Network
{
    public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
    {
        public static NetworkPlayer LocalPlayer { get; private set; }

        public override void Spawned()
        {
            /// If we don't check for this, it will run for every client!
            if(Object.HasInputAuthority)
            {
                LocalPlayer = this;
                Debug.Log("[NetworkPlayer] Spawned Local Player");
                return;
            }
            Debug.Log("[NetworkPlayer] Spawned Remote Player");
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority)
                Runner.Despawn(Object);            
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
namespace Network
{
    /// <summary>
    /// It is recommended to use bits as it's lower size for the network
    /// But for this example, it will use floats and Vector3
    /// </summary>
    public struct NetworkInputData : INetworkInput
    {
        public Vector2 MovementInput;   /// Which direction is the character moving? Only X,Y
        public Vector3 AimForwardVector; /// Replaced rotation for this, as this will be used for aiming later
        public NetworkBool IsJumpPressed;
    }
}

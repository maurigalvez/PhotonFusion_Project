using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Custom;
using Utilities;
namespace Movement
{
    using Network;

    [RequireComponent(typeof(Fusion.Custom.NetworkCharacterController))]
    public class CharacterMovementHandler : NetworkBehaviour
    {
        private Fusion.Custom.NetworkCharacterController m_NetworkCharacterController;
        private Transform m_CharacterTransform;
        private Camera m_LocalPlayerCamera;
        private const float MAX_CAMERA_ROTATION = 90f;
        private const float FALL_HEIGHT_THRESHOLD = -12f;

        private void Awake()
        {
            m_NetworkCharacterController = GetComponent<Fusion.Custom.NetworkCharacterController>();
            m_CharacterTransform = transform;
            m_LocalPlayerCamera = GetComponentInChildren<Camera>();
        }

        public override void FixedUpdateNetwork()
        {
            if(GetInput(out NetworkInputData networkInputData))
            {
                // Rotate the view
                ///m_NetworkCharacterController.Rotate(networkInputData.RotationInput);
                m_CharacterTransform.forward = networkInputData.AimForwardVector;   /// This might be yarring, and might need some lerping depending on the case

                /// Cancel out rotation on X
                var rotationEuler = m_CharacterTransform.rotation.eulerAngles;
                rotationEuler.x = 0;
                m_CharacterTransform.rotation = Quaternion.Euler(rotationEuler);

                // Move player over network
                var moveDirection = m_CharacterTransform.forward * networkInputData.MovementInput.y 
                                      + m_CharacterTransform.right * networkInputData.MovementInput.x;

                moveDirection.Normalize();
                m_NetworkCharacterController.Move(moveDirection);

                // Jump
                if (networkInputData.IsJumpPressed)
                    m_NetworkCharacterController.Jump();

                CheckFailRespawn();
            }
        }

        private void CheckFailRespawn()
        {
            if (m_CharacterTransform.position.y < FALL_HEIGHT_THRESHOLD)
                m_CharacterTransform.position = Utils.GetRandomSpawnPoint();
        }
    }
}

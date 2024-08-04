using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Custom;
namespace Movement
{
    using Network;
    [RequireComponent(typeof(Fusion.Custom.NetworkCharacterController))]
    public class CharacterMovementHandler : NetworkBehaviour
    {
        private Fusion.Custom.NetworkCharacterController m_NetworkCharacterController;
        private Transform m_CharacterTransform;
        private Vector2 m_ViewInput;
        private float m_CameraRotationX = 0;
        private Camera m_LocalPlayerCamera;
        private const float MAX_CAMERA_ROTATION = 90f;

        public void SetViewInputVector(Vector2 viewInput)
        {
            m_ViewInput = viewInput;
        }

        private void Awake()
        {
            m_NetworkCharacterController = GetComponent<Fusion.Custom.NetworkCharacterController>();
            m_CharacterTransform = transform;
            m_LocalPlayerCamera = GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            m_CameraRotationX += Mathf.Clamp(m_ViewInput.y * Time.deltaTime * m_NetworkCharacterController.viewUpDownRotationSpeed, -MAX_CAMERA_ROTATION, MAX_CAMERA_ROTATION);
            m_LocalPlayerCamera.transform.localRotation = Quaternion.Euler(m_CameraRotationX, 0, 0);
        }

        public override void FixedUpdateNetwork()
        {
            if(GetInput(out NetworkInputData networkInputData))
            {
                // Rotate the view
                m_NetworkCharacterController.Rotate(networkInputData.RotationInput);

                // Move player over network
                Vector3 moveDirection = m_CharacterTransform.forward * networkInputData.MovementInput.y 
                                      + m_CharacterTransform.right * networkInputData.MovementInput.x;

                moveDirection.Normalize();
                m_NetworkCharacterController.Move(moveDirection);

                // Jump
                if (networkInputData.IsJumpPressed)
                    m_NetworkCharacterController.Jump();
            }
        }
    }
}

using CameraLogic;
using Movement;
using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InputControls
{
    [RequireComponent(typeof(CharacterMovementHandler))]
    public class CharacterInputHandler : MonoBehaviour
    {
        private Vector2 m_MoveInputVector = Vector2.zero;
        private Vector2 m_ViewInputVector = Vector2.zero;
        private bool m_IsJumpButtonPressed = false;
        private LocalCameraHandler m_LocalCameraHandler;

        private void Awake()
        {
            m_LocalCameraHandler = GetComponentInChildren<LocalCameraHandler>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // View input
            m_ViewInputVector.x = Input.GetAxis("Mouse X");
            m_ViewInputVector.y = Input.GetAxis("Mouse Y") * -1; // Invert mouse look

            m_LocalCameraHandler.SetViewInputVector(m_ViewInputVector);

            // Movement input
            m_MoveInputVector.x = Input.GetAxis("Horizontal");
            m_MoveInputVector.y = Input.GetAxis("Vertical");
            
            // We only set to true and reset in GetNetworkInput() so network has a chance to read the value of the var
            // and send it to other players.
            if(Input.GetButtonDown("Jump"))
                m_IsJumpButtonPressed = true;
        }

        public NetworkInputData GetNetworkInput()
        {
            NetworkInputData networkInput = new()
            {
                AimForwardVector = m_LocalCameraHandler.transform.forward,
                MovementInput = m_MoveInputVector,
                IsJumpPressed = m_IsJumpButtonPressed
            };
            // Reset now that it's been read by others
            m_IsJumpButtonPressed = false;
            return networkInput;
        }
    }
}

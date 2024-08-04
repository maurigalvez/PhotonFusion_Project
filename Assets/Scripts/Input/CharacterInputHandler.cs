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
        private CharacterMovementHandler m_MovementHandler;

        private void Awake()
        {
            m_MovementHandler = GetComponent<CharacterMovementHandler>();
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

            m_MovementHandler.SetViewInputVector(m_ViewInputVector);

            // Movement input
            m_MoveInputVector.x = Input.GetAxis("Horizontal");
            m_MoveInputVector.y = Input.GetAxis("Vertical");

            m_IsJumpButtonPressed = Input.GetButtonDown("Jump");
        }

        public NetworkInputData GetNetworkInput()
        {
            return new()
            {
                RotationInput = m_ViewInputVector.x,
                MovementInput = m_MoveInputVector,
                IsJumpPressed = m_IsJumpButtonPressed
            };
        }
    }
}

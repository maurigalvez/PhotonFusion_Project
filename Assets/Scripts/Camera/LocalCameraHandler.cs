using Fusion.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CameraLogic
{
    [RequireComponent(typeof(Camera))]
    public class LocalCameraHandler : MonoBehaviour
    {
        [SerializeField] private Transform m_CameraAnchorPoint;

        private Camera m_LocalCamera;
        private Transform m_LocalCameraTransform;
        private Vector2 m_ViewInput;
        private float m_CameraRotationX;
        private float m_CameraRotationY;
        private const float MAX_CAMERA_ROTATION_X = 90f;
        private NetworkCharacterController m_NetworkCharacterController;

        public void SetViewInputVector(Vector2 viewInput)
        {
            m_ViewInput = viewInput;
        }

        private void Awake()
        {
            m_LocalCamera = GetComponent<Camera>();
            m_LocalCameraTransform = m_LocalCamera.transform;
            m_NetworkCharacterController = GetComponentInParent<NetworkCharacterController>();
        }

        private void Start()
        {
            if (m_LocalCamera.enabled)
                m_LocalCamera.transform.SetParent(null);
        }

        private void LateUpdate()
        {
            if (m_CameraAnchorPoint == null)
                return;

            if (!m_LocalCamera.enabled)
                return;

            m_LocalCameraTransform.position = m_CameraAnchorPoint.position;

            m_CameraRotationX += m_ViewInput.y * Time.deltaTime * m_NetworkCharacterController.viewUpDownRotationSpeed;
            m_CameraRotationX = Mathf.Clamp(m_CameraRotationX, -MAX_CAMERA_ROTATION_X, MAX_CAMERA_ROTATION_X);

            m_CameraRotationY += m_ViewInput.x * Time.deltaTime * m_NetworkCharacterController.rotationSpeed;

            m_LocalCameraTransform.rotation = Quaternion.Euler(m_CameraRotationX, m_CameraRotationY,0);
        }
    }
}

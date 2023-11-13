using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float speed = 1f;
    public LayerMask groundLayer;

    [SerializeField]
    private XRNode inputSource;
    [SerializeField]
    private XROrigin m_XROrigin;
    [SerializeField]
    private CharacterController character;
    private Vector2 inputAxis;
    private float fallingSpeed;
    private float gravity;
    
    void Start()
    {
        fallingSpeed = 0;
        gravity = 5f;
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headDirection = Quaternion.Euler(0, m_XROrigin.GetComponentInChildren<Camera>().transform.eulerAngles.y, 0);
        if(inputAxis.y > 0)
        {
            Vector3 direction = headDirection * new Vector3(0, 0, inputAxis.y);
            character.Move(direction * speed * Time.deltaTime);
        }

        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.down * fallingSpeed * Time.fixedDeltaTime);
    }

    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }

    void CapsuleFollowHeadset()
    {
        character.height = m_XROrigin.CameraInOriginSpaceHeight + 0.2f;
        Vector3 capsuleCenter = transform.InverseTransformPoint(m_XROrigin.GetComponentInChildren<Camera>().transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}

//https://sneakydaggergames.medium.com/vr-in-unity-how-to-create-a-continuous-movement-system-track-real-space-movement-2bd6fe31df0a

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] private GameObject arm;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float acceleration = 2.0f;
    [SerializeField] private float deceleration = 2.0f;
    [SerializeField] private float bodyRotationSpeed = 200f;
    [SerializeField] private float armRotationSpeed = 100f;
    [SerializeField] private float minFOV = 60f;
    [SerializeField] private float maxFOV = 80f;
    [SerializeField] private float cameraSpeed = 10f;

    private Rigidbody2D rb;

    private Vector2 movementVec;
    private Vector2 armVec;
    private Vector2 velocity;
    private float armPos;

    private void Start()
    {
        Physics.gravity = new Vector3(0, 0, 9.81f);

        movementVec = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movementVec != Vector2.zero)
        {
            velocity = Vector2.MoveTowards(velocity, movementVec * movementSpeed, acceleration * Time.fixedDeltaTime);
            playerCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(playerCamera.m_Lens.OrthographicSize, maxFOV, cameraSpeed * Time.fixedDeltaTime);
        }
        else
        {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            playerCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(playerCamera.m_Lens.OrthographicSize, minFOV, cameraSpeed / 2 * Time.fixedDeltaTime);
        }
        rb.velocity = velocity;
        
        // Rotate body
        if (rb.velocity != Vector2.zero)
        {
            float targetBodyAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            float bodyAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetBodyAngle, bodyRotationSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, bodyAngle));
        }

        if (armVec != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(armVec.y, armVec.x) * Mathf.Rad2Deg;
            armPos = Mathf.MoveTowardsAngle(arm.transform.eulerAngles.z, targetAngle, armRotationSpeed * Time.fixedDeltaTime);
            arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, armPos));
        }
        arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, armPos));

    }

    public void OnMove(InputValue value)
    {
        movementVec = value.Get<Vector2>().normalized;
    }

    public void OnSwing(InputValue value)
    {
        armVec = value.Get<Vector2>().normalized;
    }

}

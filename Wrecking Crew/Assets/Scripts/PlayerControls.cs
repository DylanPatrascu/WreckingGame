using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float acceleration = 2.0f;
    [SerializeField] private float deceleration = 2.0f;
    [SerializeField] private float bodyRotationSpeed = 200f;

    [Header("Arm Settings")]
    [SerializeField] private GameObject arm;
    [SerializeField] private float armRotationSpeed = 100f;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float minFOV = 60f;
    [SerializeField] private float maxFOV = 80f;
    [SerializeField] private float shakeTime = 0.5f;
    [SerializeField] private float cameraSpeed = 10f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip engineSound;
    [SerializeField] private float minPitch = 1;
    [SerializeField] private float maxPitch = 4;

    private Rigidbody2D rb;

    private Vector2 movementVec;
    private Vector2 armVec;
    private Vector2 velocity;
    private float armPos;
    private bool shaking = false;


    private Coroutine cameraShakeCoroutine;

    private void OnEnable()
    {
        Ball.OnBallHit += ShakeCamera;
    }

    private void OnDisable()
    {
        Ball.OnBallHit -= ShakeCamera;
    }

    private void Start()
    {
        Physics.gravity = new Vector3(0, 0, 9.81f);
        Physics2D.gravity = new Vector3(0, 0, 9.81f);

        movementVec = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GameLogic.gameRunning)
        {
            if (movementVec != Vector2.zero)
            {
                velocity = Vector2.MoveTowards(rb.velocity, movementVec * movementSpeed, acceleration * Time.fixedDeltaTime);
                playerCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(playerCamera.m_Lens.OrthographicSize, maxFOV, cameraSpeed * Time.fixedDeltaTime);
            }
            else
            {
                velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
                playerCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(playerCamera.m_Lens.OrthographicSize, minFOV, cameraSpeed / 2 * Time.fixedDeltaTime);
            }
            
            rb.velocity = velocity;

            audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, rb.velocity.magnitude / 10);

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
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }

    }

    public void OnMove(InputValue value)
    {
        movementVec = value.Get<Vector2>().normalized;
    }

    public void OnSwing(InputValue value)
    {
        armVec = value.Get<Vector2>().normalized;
    }

    private void ShakeCamera()
    {
        if (!shaking)
            cameraShakeCoroutine = StartCoroutine(CameraShake());
    }

    private IEnumerator CameraShake()
    {
        shaking = true;
        float timeElapsed = 0;
        float t;
        float offsetAmount = 0.25f;
        CinemachineCameraOffset offset = playerCamera.gameObject.GetComponent<CinemachineCameraOffset>();

        while (timeElapsed < shakeTime)
        {
            t = (timeElapsed % (shakeTime / 2)) / (shakeTime / 2);


            if (shakeTime < shakeTime / 2)
            {
                offset.m_Offset = new Vector3(
                    Random.Range(0, Mathf.Lerp(0, offsetAmount, t)), 
                    Random.Range(0, Mathf.Lerp(0, offsetAmount, t)), 
                    Random.Range(0, Mathf.Lerp(0, offsetAmount, t))
                    );
            }
            else
            {
                offset.m_Offset = new Vector3(
                    Random.Range(0, Mathf.Lerp(offsetAmount, 0, t)),
                    Random.Range(0, Mathf.Lerp(offsetAmount, 0, t)),
                    Random.Range(0, Mathf.Lerp(offsetAmount, 0, t))
                    );
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        offset.m_Offset = Vector3.zero;
        shaking = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class FlightController : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    Transform shipMesh;

    [SerializeField]
    ParticleSystem thrusters;

    float acceleration;
    float flightSpeed;
    float maxFlightSpeed;
    float turnSpeed;

    [Header("Cam Settings")]
    float defaultFov;

    [Header("Current Val")]
    [SerializeField]
    float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
    }

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip boostOn;
    [SerializeField]
    AudioClip boostOff;

    Vector2 viewportOffset = new Vector2(-0.5f, -0.5f);
    Vector3 camOffset;
    Vector3 camRotOffset;

    public bool camReleased = false;
    public bool boosting;
    public bool slowing;

    void Start()
    {
        cam = Camera.main;
        camOffset = cam.transform.position - transform.position;
        camRotOffset = cam.transform.localRotation.eulerAngles - transform.localRotation.eulerAngles;
        defaultFov = cam.fieldOfView;
        Cursor.lockState = CursorLockMode.Confined;
        source = GetComponents<AudioSource>()[2];
        source.PlayOneShot(boostOn);

        Ship ship = GetComponent<PlayerShip>();
        acceleration = ship.acceleration;
        flightSpeed = ship.flightSpeed;
        maxFlightSpeed = ship.maxFlightSpeed;
        turnSpeed = ship.turnSpeed;
    }


    void Update()
    {
        // Move forward towards +z & account for acceleration
        if (Input.GetButton("Boost"))
        {
           
            CameraShaker.Instance.ShakeOnce(0.05f, 2f, 0.5f, 0.5f);
            if (!boosting) source.PlayOneShot(boostOn);
            thrusters.startSize = 0.5f;
            boosting = true;
            slowing = false;
            currentSpeed = Mathf.Clamp(currentSpeed + (acceleration * Time.deltaTime), 0, maxFlightSpeed);
        }
        else if (Input.GetButton("Break"))
        {
            if (!slowing) source.PlayOneShot(boostOff);
            thrusters.startSize = 0.1f;
            boosting = false;
            slowing = true;
            currentSpeed = Mathf.Clamp(currentSpeed + (-acceleration * Time.deltaTime), 0, maxFlightSpeed);
        }
        else
        {
            thrusters.startSize = 0.2f;
            boosting = false;
            slowing = false;
            // Slowly go to normal flight speed if not 'Boosting' or 'Breaking'
            currentSpeed = Mathf.LerpUnclamped(currentSpeed, flightSpeed, Time.deltaTime * acceleration);
        }



        transform.position += transform.forward * currentSpeed * Time.deltaTime;

        Vector3 currentForward = new Vector3(transform.forward.x, 0, transform.forward.z);
        float currentAngle = Vector3.Angle(currentForward, transform.forward); // Angle against transform's forward vector
        float maxAngle = 80f; // Max ascent/descent angle
        

        // Viewport Setup (experimental movement for mouse)
        // Instead of [0,0] on bottom left, and [1,1] on top right.. we set bottom left to [-1,-1] and top right to [1,1], where [0,0] is in the center
        Vector2 viewportPos = cam.ScreenToViewportPoint(Input.mousePosition);
        viewportPos += viewportOffset;
        viewportPos *= 2f;

        // Ship Mesh rotation (not to be confused with the object rotation which the camera follows
        float rollLockAngle = 20f;
        float pitchLockAngle = 5f;

        Quaternion meshRot = Quaternion.Euler(new Vector3(-viewportPos.y * pitchLockAngle, 0, -viewportPos.x * rollLockAngle));
        shipMesh.localRotation = meshRot;



        float edgeSmoothing = Mathf.Clamp((1 - (currentAngle / maxAngle)), 0.1f, 1f); // slow down as currentAngle reaches maxAngle so the 'stop' is not abupt
        float pitchDelta = -viewportPos.y * turnSpeed * Time.deltaTime;
        float yawDelta = viewportPos.x * turnSpeed * Time.deltaTime;
        float rollDelta = 0;


        Quaternion currentRotation = transform.rotation;
        Vector3 newEulerRot = new Vector3(pitchDelta, yawDelta, rollDelta);
        Quaternion pitchYawRollRot = Quaternion.Euler(newEulerRot);
        Quaternion newRotation = currentRotation * pitchYawRollRot;


        // Limit pitch so that no odd behaviours occur 
        // (the ship cannot flip upside down in any case for our control system so it would spin erratically at the 'tipping point')
        Vector3 newRotForward = newRotation * Vector3.forward;
        bool isOnEdge = (maxAngle - (Vector3.Angle(transform.forward, new Vector3(newRotForward.x, 0, newRotForward.z))) <= 1f);
        // Only apply edgeSmoothing when approaching maxAngle on the direction its moving to, but not when its going back.
        if (pitchDelta > 0 && Vector3.Angle(Vector3.up, transform.forward) <= 90) // If ship is pointing upwards max, only descent is possible
        { 
            transform.rotation = newRotation;
        }
        else if (pitchDelta < 0 && Vector3.Angle(Vector3.up, transform.forward) > 90) // If ship is pointing downwards max, only ascent is possible
        {
            transform.rotation = newRotation;
        }
        else
        {
            // Lock pitch on maxAngle
            if (isOnEdge)
            {
                newEulerRot.x = 0f;
            }
            newEulerRot.x *= edgeSmoothing;
            pitchYawRollRot = Quaternion.Euler(newEulerRot);
            newRotation = currentRotation * pitchYawRollRot;
            transform.rotation = newRotation;
        }


        Quaternion uprightRot = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0));
        transform.rotation = Quaternion.Lerp(transform.rotation, uprightRot, Time.deltaTime * turnSpeed);

        // Camera Boom
        
        Vector3 boomVector = camOffset.normalized;
        float boomLength = camOffset.magnitude;
        float boostBoom = ((currentSpeed-flightSpeed) / (maxFlightSpeed-flightSpeed)) * 5f;

        float boostFovIncrease = 20f;
        cam.fieldOfView = defaultFov + ((currentSpeed-flightSpeed)/(maxFlightSpeed-flightSpeed)) * boostFovIncrease;

        Quaternion boomRotation = Quaternion.Euler(boomVector);
        
        //if (!camReleased) 
        //{
            // cam.transform.localPosition = boomVector * (boomLength);
            // cam.transform.rotation = transform.rotation;
            CameraShaker.Instance.RestPositionOffset = boomVector * (boomLength);
            CameraShaker.Instance.RestRotationOffset = camRotOffset;
        //}

    }

    public IEnumerator ReleaseCam(float seconds)
    {
        camReleased = true;
        yield return new WaitForSeconds(seconds);
        camReleased = false;
    }


}

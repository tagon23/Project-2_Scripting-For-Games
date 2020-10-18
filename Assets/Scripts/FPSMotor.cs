using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSMotor : MonoBehaviour
{
    [SerializeField] Camera _camera = null;
    [SerializeField] float _cameraAngleLimit = 70f;
    [SerializeField] GroundDetector _groundDetector = null;
    [SerializeField] CharacterController playerController;
    [SerializeField] Transform debugHit;
    [SerializeField] ParticleSystem _gunFire = null;
    [SerializeField] AudioClip audioClip = null;
    [SerializeField] Camera CameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 20f;
    //light - can keep if want - will probably make particle
    //[SerializeField] GameObject visuallightObject;
    [SerializeField] GameObject playerObject;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] Transform hookshotTransform;
    float hookshotSize;
    bool _isGrounded = true;
    //stores raycast hit info
    RaycastHit objecthit;
    float speedforGrapple = 1f;
    float _turnAmountThisFrame = 0;
    float _lookAmountThisFrame = 0;
    //tracking camera angle
    private float _currentCameraRotationX = 0;
    private State state;
    private Vector3 hookshotPosition;
    Vector3 _movementThisFrame = Vector3.zero;

    Rigidbody _rigidbody = null;

private enum State
    {
        Normal,
        HookshotFly,
        HookshotThrow,
    }

private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        hookshotTransform.gameObject.SetActive(false);
    }

    public void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleFire();
                ApplyMovement(_movementThisFrame);
                ApplyTurn(_turnAmountThisFrame);
                ApplyLook(_lookAmountThisFrame);
                break;
            case State.HookshotThrow:
                HandleHookShotThrown();
                ApplyMovement(_movementThisFrame);
                ApplyTurn(_turnAmountThisFrame);
                ApplyLook(_lookAmountThisFrame);
                break;
            case State.HookshotFly:
                HookshotMovement();
                ApplyTurn(_turnAmountThisFrame);
                ApplyLook(_lookAmountThisFrame);
                HandleFire();
                break;
        }

    }

 private void FixedUpdate()
    {
        ApplyMovement(_movementThisFrame);
        ApplyTurn(_turnAmountThisFrame);
        ApplyLook(_lookAmountThisFrame);
    }

    private void OnEnable()
    {
        _groundDetector.GroundDetected += OnGroundDetected;
        _groundDetector.GroundVanished += OnGroundVanished;
    }

    private void OnDisable()
    {
        _groundDetector.GroundDetected -= OnGroundDetected;
        _groundDetector.GroundVanished -= OnGroundVanished;
    }

   void OnGroundDetected()
    {
        _isGrounded = true;
        //notify the others we have landed
        Land?.Invoke();
    }

    void OnGroundVanished()
    {
        _isGrounded = false;
    }

    public event Action Land = delegate { };

    

    public void Move(Vector3 requestsedMovement)
    {
        _movementThisFrame = requestsedMovement;
    }

    public void Turn(float turnAmount)
    {
        _turnAmountThisFrame = turnAmount;
    }

    public void Look(float lookAmount)
    {
        _lookAmountThisFrame = lookAmount;
    }

    public void Jump(float jumpForce)
    {
        //only allows if on the ground
        if (_isGrounded == false)
            return;

        _rigidbody.AddForce(Vector3.up * jumpForce);
    }

   
    public void ApplyMovement(Vector3 moveVector)
    {
        //confirm movement
        if (moveVector == Vector3.zero)
            return;
        //move
        _rigidbody.MovePosition(_rigidbody.position + moveVector);
        //clear
        _movementThisFrame = Vector3.zero;
    }

    void ApplyTurn(float rotateAmount)
    {
        //same as above
        if (rotateAmount == 0)
            return;
        Quaternion newRotation = Quaternion.Euler(0, rotateAmount, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * newRotation);
        _turnAmountThisFrame = 0;
    }

    void ApplyLook(float lookAmount)
    {
        if (lookAmount == 0)
            return;

        //calculate and clamp rotation before applying
        _currentCameraRotationX -= lookAmount;
        _currentCameraRotationX = Mathf.Clamp
            (_currentCameraRotationX, -_cameraAngleLimit, _cameraAngleLimit);//look this up later
        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
        _lookAmountThisFrame = 0;
    }



    public void HandleFire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    //fire using raycast
    public void Fire()
    {
        //play graphics
        _gunFire.Play();
        AudioHelper.PlayClip2D(audioClip, 1);
        //calculate direction + start Grapple
        Vector3 rayDirection = CameraController.transform.forward;
        StartGrapple();
        //cast debug
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.red, 1f);
        //fire raycast
        void StartGrapple()
        {
            if (Physics.Raycast(rayOrigin.position, rayDirection, out objecthit, shootDistance))
            {
                
                //hookshot movement
                hookshotPosition = objecthit.point;
                hookshotSize = 0;

                //this was here as a backup option if I didn't get hookshot working in time, but I have hookshot... mostly working.
                //apply damage
                //EnemyHealth enemyHealth = objecthit.transform.gameObject.GetComponent<EnemyHealth>();
                //if(enemyHealth != null)
                //{
                //enemyHealth.TakeDamage(weaponDamage);
                //}

            }
            else
            {
                
            }
            if (Physics.Raycast(playerObject.transform.position, playerObject.transform.forward, out objecthit))
            {
                debugHit.position = objecthit.point;
            }
            state = State.HookshotThrow;
            hookshotTransform.gameObject.SetActive(true);
        }
       // if (TestInputDownHookshot())
        //{
            //state = State.Normal;
        //}
    }

    private void HandleHookShotThrown()
    {
        hookshotTransform.LookAt(hookshotPosition);
            float hookshotThrowSpeed = 25f;
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        hookshotTransform.localScale = new Vector3(1,1,hookshotSize);
        if (hookshotSize >= Vector3.Distance(transform.position, hookshotPosition))
        {
            state = State.HookshotFly;
        }
    }

    private void HookshotMovement()
    {
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float hookshotSpeed = 7f;
        playerController.Move(hookshotDir * hookshotSpeed * Time.deltaTime);
        float reachedHookshotDist = 1f;
        if(Vector3.Distance(transform.position,hookshotPosition) < reachedHookshotDist)
        {
            state = State.Normal;
            Vector3 _movementThisFrame = Vector3.zero;
            float _turnAmountThisFrame = 0;
            float _lookAmountThisFrame = 0;
        }
        hookshotTransform.gameObject.SetActive(false);
    }

    private bool TestInputDownHookshot()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    //for stopping of movement
    public void StopGrapple()
    {

    }
    public void PleaseStop()
    {
        state = State.Normal;
    }
}

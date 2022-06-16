using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float turnSmoothTime;
 
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _turnSmooth;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool doubleJump;
    
    private CharacterController _controller;
    private Animator _animator;
    public Transform cam;
    private CinemachineFreeLook _cam2;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _cam2 = cam.GetComponentInChildren<CinemachineFreeLook>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && _velocity.y < 0)
        {
            _animator.SetBool("Jump", false);
            _velocity.y = -2f;
        }
        
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        switch (_cam2.m_XAxis.Value) 
        {
            case 0:
                _moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
                break;
            case 90:
                _moveDirection = new Vector3(verticalInput, 0, -horizontalInput).normalized;
                break;
            case -90:
                _moveDirection = new Vector3(-verticalInput, 0, horizontalInput).normalized;
                break;
            case 180:
                _moveDirection = new Vector3(-horizontalInput, 0, -verticalInput).normalized;
                break;
        }

        if (_moveDirection != Vector3.zero && !Input.GetKey(KeyCode.RightShift))
        {
            Walk();
            Turn();
        }
        else if (_moveDirection != Vector3.zero && Input.GetKey(KeyCode.RightShift))
        {
            Run();
            Turn();
        }
        else if (_moveDirection == Vector3.zero)
        {
            Stay();
        }

        _moveDirection *= moveSpeed;

        if (isGrounded)
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("is Grounded", true);
            doubleJump = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
            {
                doubleJump = false;
                Jump();
            } 
        }
        _controller.Move(_moveDirection * Time.deltaTime);

        _velocity.y += -2 * gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Stay()
    {
        _animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        _animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        _animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        _velocity.y = jumpHeight;
        _animator.SetBool("Jump", true);
        _animator.SetBool("is Grounded", false);
    }

    private void Turn()
    {
        float angle;
        float targetAngle;
        switch (_cam2.m_XAxis.Value) 
        {
            case 0:
                targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmooth, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                break;
            case 90:
                targetAngle = Mathf.Atan2(-_moveDirection.z, _moveDirection.x) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmooth, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                break;
            case -90:
                targetAngle = Mathf.Atan2(_moveDirection.z, -_moveDirection.x) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmooth, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                break;
            case 180:
                targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + _cam2.m_YAxis.Value;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmooth, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                break;
        }
    }
}

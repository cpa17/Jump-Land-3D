using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField] 
    private float jumpSpeed = 3f;
    [SerializeField] 
    private float gravity = 9.81f;
    [SerializeField] 
    private float doubleJump = 0.7f;
    
    private CharacterController _controller;
    private Vector3 _direction;
    private float _jump;
    private bool _canDoubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        
        _direction = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Move(horizontalInput, verticalInput);
        Jumping();
        
        _controller.Move(_direction * Time.deltaTime * speed);
    }
    
    void Move (float horizontalInput, float verticalInput)
    {
        _direction.Set(horizontalInput, 0, verticalInput);
    }
    
    private void Jumping()
    {
        if (_controller.isGrounded)
        {
            _canDoubleJump = true;
            
            if (Input.GetButtonDown("Jump"))
            {
                _jump = jumpSpeed;
            }  
        }
        else
        {
            if (Input.GetButtonDown("Jump") && _canDoubleJump)
            {
                _jump = jumpSpeed * doubleJump;
                _canDoubleJump = false;
            }
        }
        
        _jump -= gravity * Time.deltaTime;
        _direction.y = _jump;
    }
}

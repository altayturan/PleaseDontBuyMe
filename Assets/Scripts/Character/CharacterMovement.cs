using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject camera;

    [SerializeField] private float torque = 30;
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxVelocityX = 10;
    [SerializeField] private float maxVelocityZ = 10;
    [SerializeField] private float jumpForce=5;

    [SerializeField] private GameData _gameData;

    private bool _isGrounded = true;
    
    private Vector3 _clampedVelocity;

    [SerializeField] private AudioSource _audioSource;


    private void Start()
    {
        rigidbody.maxAngularVelocity = 5;
    }

    private void Update()
    {
        
        Debug.LogError(_isGrounded);
        if (Input.GetKey(KeyCode.W))
        {
            var targetVector = new Vector3(transform.position.x - camera.transform.position.x, 0,transform.position.z - camera.transform.position.z);
            rigidbody.AddForce(targetVector.normalized * speed);
        }
        
        
        
        if (Input.GetKey(KeyCode.S))
        {
            var targetVector = new Vector3(transform.position.x - camera.transform.position.x, 0,transform.position.z - camera.transform.position.z);
            rigidbody.AddForce(-targetVector.normalized * speed);
            
        }
        
        
        if (Input.GetKey(KeyCode.A))
        {
            var targetVector = new Vector3(transform.position.x - camera.transform.position.x, 0,transform.position.z - camera.transform.position.z);
            targetVector = Quaternion.AngleAxis(90, Vector3.up) * targetVector.normalized * torque;
            rigidbody.AddForce(-targetVector);
        }
        
        
        
        if (Input.GetKey(KeyCode.D))
        {
             var targetVector = new Vector3(transform.position.x - camera.transform.position.x, 0,transform.position.z - camera.transform.position.z);
             targetVector = Quaternion.AngleAxis(90, Vector3.up) * targetVector.normalized * torque;
            rigidbody.AddForce(targetVector);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isGrounded) return;
            _isGrounded = false;
            rigidbody.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        }
        
        

        _clampedVelocity.x = Mathf.Clamp(rigidbody.velocity.x, -maxVelocityX, maxVelocityX);
        _clampedVelocity.y = rigidbody.velocity.y;
        _clampedVelocity.z = Mathf.Clamp(rigidbody.velocity.z, -maxVelocityZ, maxVelocityZ);

        rigidbody.velocity = _clampedVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;

        if(!_audioSource.isPlaying) _audioSource.Play();

        if (collision.collider.CompareTag("Door"))
        {
            _gameData.winGame = true;
        }
        if (collision.collider.CompareTag("Ground"))
        {
            _gameData.loseGame = true;
            _gameData.falled = true;
        }
    }
}
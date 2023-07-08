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
    [SerializeField] private float maxVelocityX = 10;
    [SerializeField] private float maxVelocityZ = 10;
    
    private Vector3 _clampedVelocity;


    private void Start()
    {
        rigidbody.maxAngularVelocity = 5;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var targetX = Mathf.Clamp(transform.position.x - camera.transform.position.x, -5, 5);
            var targetZ = Mathf.Clamp(transform.position.z - camera.transform.position.z, -5, 5);
            
            Debug.LogError($"X: {targetX}  Z: {targetZ}");
            
            var targetVector = new Vector3(targetX, 0,targetZ);
            rigidbody.AddForce(targetVector);
        }
        
        
        
        if (Input.GetKey(KeyCode.S))
        {
            var targetVector = new Vector3(transform.position.x - camera.transform.position.x, 0,transform.position.z - camera.transform.position.z);
            rigidbody.AddForce(-targetVector);
            
        }
        
        
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddTorque(new Vector3(0,-1,0) * torque,ForceMode.Force);
        }
        
        
        
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddTorque(new Vector3(0,1,0) * torque,ForceMode.Force);
        }
        
        
        

        _clampedVelocity.x = Mathf.Clamp(rigidbody.velocity.x, -maxVelocityX, maxVelocityX);
        _clampedVelocity.y = rigidbody.velocity.y;
        _clampedVelocity.z = Mathf.Clamp(rigidbody.velocity.z, -maxVelocityZ, maxVelocityZ);

        rigidbody.velocity = _clampedVelocity;
    }
}
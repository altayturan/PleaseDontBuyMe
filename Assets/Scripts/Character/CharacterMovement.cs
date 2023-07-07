using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject debug;
    private Vector3 _clampedVelocity;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Physics.Raycast(camera.transform.position, transform.position - camera.transform.position, out var hit,30))
            {
                var targetVector = new Vector3(hit.collider.transform.position.x - camera.transform.position.x, 0,hit.collider.transform.position.z - camera.transform.position.z);
                debug.transform.position = hit.point;
                rigidbody.AddForceAtPosition(targetVector/2, hit.point);
            }
        }
        
        
        
        if (Input.GetKey(KeyCode.S))
        {
            if (Physics.Raycast(camera.transform.position, transform.position - camera.transform.position, out var hit,30))
            {
                var targetVector = new Vector3(hit.collider.transform.position.x - camera.transform.position.x, 0,hit.collider.transform.position.z - camera.transform.position.z);
                debug.transform.position = hit.point;
                rigidbody.AddForceAtPosition(-targetVector/2, hit.point);
            }
        }
        
        
        if (Input.GetKey(KeyCode.A))
        {
            if (Physics.Raycast(camera.transform.position, transform.position - camera.transform.position, out var hit,30))
            {
                var targetVector = new Vector3(hit.collider.transform.position.x - camera.transform.position.x, 0,hit.collider.transform.position.z - camera.transform.position.z);
                debug.transform.position = hit.point + new Vector3(-2,0,0);
                rigidbody.AddForceAtPosition(targetVector/4, hit.point + new Vector3(2,0,0));
            }
        }
        
        
        
        if (Input.GetKey(KeyCode.D))
        {
            if (Physics.Raycast(camera.transform.position, transform.position - camera.transform.position, out var hit,30))
            {
                var targetVector = new Vector3(hit.collider.transform.position.x - camera.transform.position.x, 0,hit.collider.transform.position.z - camera.transform.position.z);
                debug.transform.position = hit.point + new Vector3(2,0,0);
                rigidbody.AddForceAtPosition(targetVector/4, hit.point + new Vector3(-2,0,0));
            }
        }
        
        
        
        
        
        
        
        
        
        

        _clampedVelocity.x = Mathf.Clamp(rigidbody.velocity.x, -3, 3);
        _clampedVelocity.y = rigidbody.velocity.y;
        _clampedVelocity.z = Mathf.Clamp(rigidbody.velocity.z, -3, 3);

        rigidbody.velocity = _clampedVelocity;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MatchTransformWithPhysics : MonoBehaviour
{
    [Tooltip("The target to match the position and rotation from.")]
    [SerializeField] private Transform target;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(target.position);
        rb.MoveRotation(target.rotation);
    }
}

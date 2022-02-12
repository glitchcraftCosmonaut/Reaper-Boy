using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{

    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;

    [SerializeField] private LayerMask whatIsGround;

    public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }
}

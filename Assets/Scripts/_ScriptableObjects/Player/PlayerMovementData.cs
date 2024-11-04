using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerMovementStatsSO", menuName ="Player/PlayerMovementStats")]
public class PlayerMovementData : ScriptableObject
{
    [Header("Movement Settings")]
    public float speed;

    [Header("Dash Settings")]
    public float dashDuration;
    public float dashSpeed;
    public float dashCoolDown;
    public bool isDashing = false;

    [Header("Gravity Settings")]
    public float groundedGravity;
    public float gravity;

    [Header("Airborne Settings")]
    public float airSpeed;


    [Header("Jump Settings")]
    public float initialJumpVel;
    public bool isJumping = false;
}

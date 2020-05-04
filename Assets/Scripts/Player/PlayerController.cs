using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerBody;
    public GameObject playerOrigin;

    public Camera viewport;
    public GameObject viewportOrigin;
    public Animator viewportAnimator;

    public CharacterController characterController;

    public float mouseSensitivity = 15f;
    public float mouseVerticalMultiplier = 1.5f;
    public float maxViewAngle = 85f;
    public float fov = 80f;

    public float moveSpeed = 250f;
    public float jumpPower = 150f;
    public float sprintMultiplier = 2f;

    public float crouchMax = 2f;
    public float crouchSpeed = 10f;

    public float constantGroundedForce = 1f;
    public float gravity = 5f;
    public float maxGravity = 25f;

    public PlayerBindings controls = new PlayerBindings();

    public PlayerMovement playerMovement;
    public PlayerStance playerStance;
    public PlayerCamera playerCamera;

    void Start()
    {
        playerMovement = new PlayerMovement(this);
        playerStance = new PlayerStance(this);
        playerCamera = new PlayerCamera(this);
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.DoPlayerMovement();
        playerStance.DoPlayerStance();
        playerCamera.DoPlayerCameraMovement();
        playerCamera.DoPlayerCameraWalkAnimation();
    }
}

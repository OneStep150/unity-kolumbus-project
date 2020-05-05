using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement
    {
        private PlayerController playerController;
        private Dictionary<PlayerBindings.KeyBind, KeyCode> keybinds;

        private float currentGravity = 0f;

        public bool hasJumped = false;
        public bool isMoving = false;

        public PlayerMovement(PlayerController playerController)
        {
            this.playerController = playerController;
            keybinds = playerController.controls.keybinds;
        }

        public void DoPlayerMovement()
        {
            Vector3 moveVector = new Vector3(0, 0, 0);

            DoPlayerMovementLateral(ref moveVector);
            DoPlayerMovementVertical(ref moveVector);

            playerController.characterController.Move(moveVector);
        }
        private void DoPlayerMovementLateral(ref Vector3 moveVector)
        {
            isMoving = false;

            if (Input.GetKey(keybinds[PlayerBindings.KeyBind.PLAYER_MOVE_FORWARD]))
            {
                moveVector += playerController.playerOrigin.transform.forward * playerController.moveSpeed;
                isMoving = true;
            }

            if (Input.GetKey(keybinds[PlayerBindings.KeyBind.PLAYER_MOVE_BACKWARD]))
            {
                moveVector += -playerController.playerOrigin.transform.forward * playerController.moveSpeed;
                isMoving = true;
            }

            if (Input.GetKey(keybinds[PlayerBindings.KeyBind.PLAYER_MOVE_RIGHT]))
            {
                moveVector += playerController.playerOrigin.transform.right * playerController.moveSpeed;
                isMoving = true;
            }

            if (Input.GetKey(keybinds[PlayerBindings.KeyBind.PLAYER_MOVE_LEFT]))
            {
                moveVector += -playerController.playerOrigin.transform.right * playerController.moveSpeed;
                isMoving = true;
            }
            

            if (Input.GetKey(keybinds[PlayerBindings.KeyBind.PLAYER_MOVE_SPRINT]))
            {
                moveVector *= playerController.sprintMultiplier;
            }

            moveVector *= Time.deltaTime;
        }
        private void DoPlayerMovementVertical(ref Vector3 moveVector)
        {
            if (playerController.characterController.isGrounded)
            {
                currentGravity = playerController.gravity;
            }
            else
            {
                currentGravity += playerController.gravity * Time.deltaTime;
            }

            KeyCode jumpKey = keybinds[PlayerBindings.KeyBind.PLAYER_MOVE_JUMP];

            if (Input.GetKey(jumpKey) && playerController.characterController.isGrounded && !hasJumped)
            {
                currentGravity += playerController.jumpPower;
                hasJumped = true;
            }

            if (!Input.GetKey(jumpKey) && playerController.characterController.isGrounded)
            {
                hasJumped = false;
            }

            moveVector += playerController.playerOrigin.transform.up * currentGravity * Time.deltaTime;
        }
    }
}

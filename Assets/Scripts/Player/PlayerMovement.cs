using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerMovement
    {
        private PlayerController playerController;
        private Dictionary<PlayerBindings.KeyBind, KeyCode> keybinds;

        private float currentGravity = 0f;

        private bool hasJumped = false;

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
            KeyCode forwardKey;
            keybinds.TryGetValue(PlayerBindings.KeyBind.PLAYER_MOVE_FORWARD, out forwardKey);

            if (Input.GetKey(forwardKey))
            {
                moveVector += playerController.playerBody.transform.forward * playerController.moveSpeed;
            }

            KeyCode backwardKey;
            keybinds.TryGetValue(PlayerBindings.KeyBind.PLAYER_MOVE_BACKWARD, out backwardKey);

            if (Input.GetKey(backwardKey))
            {
                moveVector += -playerController.playerBody.transform.forward * playerController.moveSpeed;
            }

            KeyCode rightKey;
            keybinds.TryGetValue(PlayerBindings.KeyBind.PLAYER_MOVE_RIGHT, out rightKey);

            if (Input.GetKey(rightKey))
            {
                moveVector += playerController.playerBody.transform.right * playerController.moveSpeed;
            }

            KeyCode leftKey;
            keybinds.TryGetValue(PlayerBindings.KeyBind.PLAYER_MOVE_LEFT, out leftKey);

            if (Input.GetKey(leftKey))
            {
                moveVector += -playerController.playerBody.transform.right * playerController.moveSpeed;
            }

            KeyCode sprintKey;
            keybinds.TryGetValue(PlayerBindings.KeyBind.PLAYER_MOVE_SPRINT, out sprintKey);

            if (Input.GetKey(sprintKey))
            {
                moveVector *= playerController.sprintMultiplier;
            }

            moveVector *= Time.deltaTime;
        }
        private void DoPlayerMovementVertical(ref Vector3 moveVector)
        {
            currentGravity += -playerController.gravity * Time.deltaTime;

            if (playerController.characterController.isGrounded)
            {
                currentGravity = 0f;
            }

            KeyCode jumpKey;
            keybinds.TryGetValue(PlayerBindings.KeyBind.PLAYER_MOVE_JUMP, out jumpKey);

            if (Input.GetKey(jumpKey) && playerController.characterController.isGrounded && !hasJumped)
            {
                currentGravity += playerController.jumpPower;
                hasJumped = true;
            }

            if (!Input.GetKey(jumpKey) && playerController.characterController.isGrounded)
            {
                hasJumped = false;
            }


            if (currentGravity > playerController.maxGravity)
            {
                currentGravity = playerController.maxGravity;
            }

            moveVector += playerController.playerBody.transform.up * currentGravity * Time.deltaTime;
        }
    }
}

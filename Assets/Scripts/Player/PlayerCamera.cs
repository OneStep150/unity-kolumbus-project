using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerCamera
    {
        private PlayerController playerController;

        private bool mirrorAnimation = false;
        private bool mirrorSet = false;

        public PlayerCamera(PlayerController playerController)
        {
            this.playerController = playerController;
            playerController.viewport.fieldOfView = playerController.fov;
        }

        public void DoPlayerCameraMovement()
        {
            Quaternion horizontal = playerController.playerOrigin.transform.rotation;

            DoPlayerCameraMovementVertical();
            DoPlayerCameraMovementHorizontal();
        }

        public void DoPlayerCameraWalkAnimation()
        {
            Vector3 playerVelocity = playerController.characterController.velocity;
            float swaySpeedMultiplier = Math.Abs(playerVelocity.x) + Math.Abs(playerVelocity.z);
            swaySpeedMultiplier *= 0.25f;

            playerController.viewportAnimator.SetBool("isWalking", playerController.playerMovement.isMoving && 
                playerController.characterController.isGrounded);
            playerController.viewportAnimator.SetFloat("lateralSpeed", swaySpeedMultiplier);

            if (playerController.playerMovement.isMoving && !mirrorSet)
            {
                playerController.viewportAnimator.SetBool("mirrorAnimation", mirrorAnimation);
                mirrorSet = true;
            }

            if (!playerController.playerMovement.isMoving && mirrorSet)
            {
                mirrorAnimation = !mirrorAnimation;
                mirrorSet = false;
            }
        }

        private void DoPlayerCameraMovementVertical()
        {
            float currentY = UnityEditor.TransformUtils.GetInspectorRotation(playerController.viewportOrigin.transform).x;
            float mouseY = playerController.mouseSensitivity * playerController.mouseVerticalMultiplier * Input.GetAxis("Mouse Y");

            float newY = currentY - mouseY;
            newY = Mathf.Clamp(newY, -playerController.maxViewAngle, playerController.maxViewAngle);

            playerController.viewportOrigin.transform.localRotation = Quaternion.Euler(newY, 0, 0);
        }

        private void DoPlayerCameraMovementHorizontal()
        {
            float mouseX = playerController.mouseSensitivity * Input.GetAxis("Mouse X");

            playerController.gameObject.transform.Rotate(new Vector3(0, mouseX, 0));
        }
    }
}

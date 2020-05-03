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
        PlayerController playerController;

        public PlayerCamera(PlayerController playerController)
        {
            this.playerController = playerController;
            playerController.viewport.fieldOfView = playerController.fov;
        }

        public void DoPlayerCameraMovement()
        {
            Quaternion horizontal = playerController.playerBody.transform.rotation;

            DoPlayerCameraMovementVertical();
            DoPlayerCameraMovementHorizontal();
        }

        private void DoPlayerCameraMovementVertical()
        {
            float currentY = UnityEditor.TransformUtils.GetInspectorRotation(playerController.viewport.transform).x;
            float mouseY = playerController.mouseSensitivity * playerController.mouseVerticalMultiplier * Input.GetAxis("Mouse Y");

            float newY = currentY - mouseY;
            newY = Mathf.Clamp(newY, -playerController.maxViewAngle, playerController.maxViewAngle);

            playerController.viewport.transform.localRotation = Quaternion.Euler(newY, 0, 0);
        }

        private void DoPlayerCameraMovementHorizontal()
        {
            float mouseX = playerController.mouseSensitivity * Input.GetAxis("Mouse X");

            playerController.gameObject.transform.Rotate(new Vector3(0, mouseX, 0));
        }
    }
}

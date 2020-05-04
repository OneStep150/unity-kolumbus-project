using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerStance
    {
        private PlayerController playerController;
        private Dictionary<PlayerBindings.KeyBind, KeyCode> keybinds;

        private float originalHeight;
        private Vector3 originalCenter;
        
        private float crouchAmount = 0f;

        public PlayerStance(PlayerController playerController)
        {
            this.playerController = playerController;
            keybinds = playerController.controls.keybinds;

            originalHeight = playerController.characterController.height;
            originalCenter = playerController.characterController.center;
        }

        public void DoPlayerStance()
        {
            KeyCode crouchKey;
            keybinds.TryGetValue(PlayerBindings.KeyBind.PLAYER_MOVE_CROUCH, out crouchKey);

            if (Input.GetKey(crouchKey))
            {
                crouchAmount += playerController.crouchSpeed * Time.deltaTime;
            } else if (crouchAmount > 0)
            {
                RaycastHit hit;
                
                if (!Physics.Raycast(playerController.playerBody.transform.position, playerController.playerBody.transform.up, out hit, originalHeight + crouchAmount))
                {
                    crouchAmount -= playerController.crouchSpeed * Time.deltaTime;
                }
                
            }

            crouchAmount = Mathf.Clamp(crouchAmount, 0, 1);

            playerController.characterController.height = originalHeight - (crouchAmount / 2);
            playerController.characterController.center = originalCenter + new Vector3(0, (crouchAmount / 2), 0);
        }
    }
}

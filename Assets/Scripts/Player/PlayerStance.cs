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

        private float crouchAmount = 0f;
        private float originalHeight;

        public PlayerStance(PlayerController playerController)
        {
            this.playerController = playerController;
            keybinds = playerController.controls.keybinds;
            originalHeight = playerController.characterController.height;
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
                
                if (!Physics.Raycast(playerController.playerBody.transform.position, playerController.playerBody.transform.up, out hit, originalHeight - playerController.characterController.height))
                {
                    crouchAmount -= playerController.crouchSpeed * Time.deltaTime;
                }
                
            }

            crouchAmount = Mathf.Clamp(crouchAmount, 0, 1);

            playerController.characterController.height = Mathf.Lerp(originalHeight, originalHeight - playerController.crouchMax, 
                crouchAmount);

        }

    }
}

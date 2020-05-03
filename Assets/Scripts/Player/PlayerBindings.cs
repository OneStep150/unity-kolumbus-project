using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBindings
    {
        public enum KeyBind
        {
            PLAYER_MOVE_FORWARD,
            PLAYER_MOVE_BACKWARD,
            PLAYER_MOVE_RIGHT,
            PLAYER_MOVE_LEFT,
            PLAYER_MOVE_JUMP,
            PLAYER_MOVE_CROUCH,
            PLAYER_MOVE_PRONE,
            PLAYER_MOVE_LEANRIGHT,
            PLAYER_MOVE_LEANLEFT,
            PLAYER_MOVE_SPRINT,
            PLAYER_ACTION_PRIMARY,
            PLAYER_ACTION_SECONDARY,
            PLAYER_ACTION_RELOAD,
            PLAYER_ACTION_USE
        }

        public Dictionary<KeyBind, KeyCode> keybinds = new Dictionary<KeyBind, KeyCode>();

        public PlayerBindings()
        {
            keybinds.Add(KeyBind.PLAYER_MOVE_FORWARD, KeyCode.W);
            keybinds.Add(KeyBind.PLAYER_MOVE_BACKWARD, KeyCode.S);
            keybinds.Add(KeyBind.PLAYER_MOVE_RIGHT, KeyCode.D);
            keybinds.Add(KeyBind.PLAYER_MOVE_LEFT, KeyCode.A);
            keybinds.Add(KeyBind.PLAYER_MOVE_JUMP, KeyCode.Space);
            keybinds.Add(KeyBind.PLAYER_MOVE_CROUCH, KeyCode.LeftControl);
            keybinds.Add(KeyBind.PLAYER_MOVE_LEANRIGHT, KeyCode.E);
            keybinds.Add(KeyBind.PLAYER_MOVE_LEANLEFT, KeyCode.Q);
            keybinds.Add(KeyBind.PLAYER_MOVE_SPRINT, KeyCode.LeftShift);

            keybinds.Add(KeyBind.PLAYER_ACTION_PRIMARY, KeyCode.Mouse0);
            keybinds.Add(KeyBind.PLAYER_ACTION_SECONDARY, KeyCode.Mouse1);
            keybinds.Add(KeyBind.PLAYER_ACTION_RELOAD, KeyCode.R);
            keybinds.Add(KeyBind.PLAYER_ACTION_USE, KeyCode.F);
        }
    }
}

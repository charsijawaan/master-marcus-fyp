using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public SavedKeys savedKeys;
    public static bool InputIsDisabled = false;

    void Update()
    {

        if (!InputIsDisabled)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            float turbo = Input.GetAxis("Turbo");
    
            // Move Up
            if(y == 1) {
                VirtualInputManager.Instance.MoveUp = true;
            }
            else {
                VirtualInputManager.Instance.MoveUp = false;
            }
    
            // Move Down
            if(y == -1) {
                VirtualInputManager.Instance.MoveDown = true;
            }
            else {
                VirtualInputManager.Instance.MoveDown = false;
            }
    
            // Move Right
            if(x == 1) {
                VirtualInputManager.Instance.MoveRight = true;
            }
            else {
                VirtualInputManager.Instance.MoveRight = false;
            }
    
            // Move Left
            if(x == -1) {
                VirtualInputManager.Instance.MoveLeft = true;
            }
            else {
                VirtualInputManager.Instance.MoveLeft = false;
            }
    
            // Jump
            if(Input.GetButton("Jump")) {
                VirtualInputManager.Instance.Jump = true;
            }
            else {
                VirtualInputManager.Instance.Jump = false;
            }    
    
            // Turbo
            if(turbo == 1) {
                VirtualInputManager.Instance.Turbo = true;
            }   
            else {
                VirtualInputManager.Instance.Turbo = false;
            }     
    
            // Attack
            if(Input.GetButton("PrimaryAttack")) {
                VirtualInputManager.Instance.Attack = true;
            }
            else {
                VirtualInputManager.Instance.Attack = false;
            }
            
            // Attack2
            if (Input.GetButton("SecondaryAttack"))
            {
                VirtualInputManager.Instance.Attack2 = true;
            }
            else
            {
                VirtualInputManager.Instance.Attack2 = false;
            }
        }
        else
        {
            VirtualInputManager.Instance.MoveUp = false;
            VirtualInputManager.Instance.MoveDown = false;
            VirtualInputManager.Instance.MoveRight = false;
            VirtualInputManager.Instance.MoveLeft = false;
            VirtualInputManager.Instance.Attack = false;
            VirtualInputManager.Instance.Attack2 = false;
            VirtualInputManager.Instance.Turbo = false;
            VirtualInputManager.Instance.Jump = false;
        }
        

        // if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_TURBO]))
        // {
        //     VirtualInputManager.Instance.Turbo = true;
        // }
        // else
        // {
        //     VirtualInputManager.Instance.Turbo = false;
        // }

        // if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_MOVE_UP]))
        // {
        //     VirtualInputManager.Instance.MoveUp = true;
        // }
        // else
        // {
        //     VirtualInputManager.Instance.MoveUp = false;
        // }

        // if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_MOVE_DOWN]))
        // {
        //     VirtualInputManager.Instance.MoveDown = true;
        // }
        // else
        // {
        //     VirtualInputManager.Instance.MoveDown = false;
        // }

        // if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_MOVE_RIGHT]))
        // {
        //     VirtualInputManager.Instance.MoveRight = true;
        // }
        // else
        // {
        //     VirtualInputManager.Instance.MoveRight = false;
        // }

        // if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_MOVE_LEFT]))
        // {
        //     VirtualInputManager.Instance.MoveLeft = true;
        // }
        // else
        // {
        //     VirtualInputManager.Instance.MoveLeft = false;
        // }

        // if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_JUMP]))
        // {
        //     VirtualInputManager.Instance.Jump = true;
        // }
        // else
        // {
        //     VirtualInputManager.Instance.Jump = false;
        // }

        // if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_ATTACK]))
        // {
        //     VirtualInputManager.Instance.Attack = true;
        // }
        // else
        // {
        //     VirtualInputManager.Instance.Attack = false;
        // }

        // if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_ATTACK2]))
        // {
        //     VirtualInputManager.Instance.Attack2 = true;
        // }
        // else
        // {
        //     VirtualInputManager.Instance.Attack2 = false;
        // }
    }

}

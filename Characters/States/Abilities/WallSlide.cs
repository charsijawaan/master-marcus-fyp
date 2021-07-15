using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AbilityData/WallSlide")]
public class WallSlide : StateData
{
    public Vector3 MaxFallVelocity;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.characterControl.MoveLeft = false;
        characterState.characterControl.MoveRight = false;

        // NOT TESTED CODE
        characterState.characterControl.MoveDown = false;
        characterState.characterControl.MoveUp = false;
        // NOT TESTED CODE

        characterState.characterControl.animationProgress.AirMomentum = 0f;

        characterState.characterControl.animationProgress.MaxFallVelocity = MaxFallVelocity;
        characterState.characterControl.animationProgress.CanWallJump = false;
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {       
        if(!characterState.characterControl.Jump)
        {
            characterState.characterControl.animationProgress.CanWallJump = true;
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.characterControl.animationProgress.MaxFallVelocity = Vector3.zero;
    }
}

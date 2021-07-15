using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], false);
        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Attack], false);
        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
        characterState.characterControl.animationProgress.BlockingObj = null;
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

        //if (characterState.characterControl.animationProgress.AttackTriggered)
        //{
        //    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Attack], true);
        //}

        if (characterState.characterControl.Jump)
        {
            if (!characterState.characterControl.animationProgress.Jumped)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], true);
            }
        }
        else
        {
            if (!characterState.characterControl.animationProgress.IsRunning(typeof(Jump)))
            {
                characterState.characterControl.animationProgress.Jumped = false;
            }
        }

        if (characterState.characterControl.MoveLeft && characterState.characterControl.MoveRight)
        {
            //do nothing
        }
        if(characterState.characterControl.MoveUp && characterState.characterControl.MoveDown)
        {
            //do nothing
        }
        else if (characterState.characterControl.MoveRight)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
        }
        else if (characterState.characterControl.MoveLeft)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
        }
        else if (characterState.characterControl.MoveUp)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
        }
        else if (characterState.characterControl.MoveDown)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
        }

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //Debug.Log("Repositioning");
        //characterState.characterControl.collisionSpheres.Reposition_BackSpheres();
        //characterState.characterControl.collisionSpheres.Reposition_BottomSpheres();
        //characterState.characterControl.collisionSpheres.Reposition_FrontSpheres();
    }
}

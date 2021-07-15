using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AbilityData/CheckMovement")]
public class CheckMovement : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //CharacterControl control = characterState.GetCharacterControl(animator);

        if (characterState.characterControl.MoveLeft || characterState.characterControl.MoveRight)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
        }
        else
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}

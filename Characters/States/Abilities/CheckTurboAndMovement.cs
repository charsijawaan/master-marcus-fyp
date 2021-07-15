using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AbilityData/CheckTurboAndMovement")]
public class CheckTurboAndMovement : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //CharacterControl control = characterState.GetCharacterControl(animator);

        if ((characterState.characterControl.MoveLeft || characterState.characterControl.MoveRight || characterState.characterControl.MoveUp || characterState.characterControl.MoveDown) && characterState.characterControl.Turbo)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], true);
        }
        else
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}

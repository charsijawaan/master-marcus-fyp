using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AbilityData/Landing")]
public class Landing : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.characterControl.animationProgress.IsLanding = true;
        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], false);
        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.characterControl.animationProgress.IsLanding = false;
    }
}

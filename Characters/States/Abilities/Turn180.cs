using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AbilityData/Turn180")]
public class Turn180 : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //CharacterControl control = characterState.GetCharacterControl(animator);

        if (characterState.characterControl.IsFacingForward())
        {
            characterState.characterControl.FaceForward(false);
        }
        else
        {
            characterState.characterControl.FaceForward(true);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}

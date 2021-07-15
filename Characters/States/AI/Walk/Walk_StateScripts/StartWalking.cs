using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AI/StartWalking")]
public class StartWalking : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.characterControl.aiProgress.SetRandomFlyingKick();
        characterState.characterControl.aiController.WalkStraightToStartSphere();
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (characterState.characterControl.Attack)
        {
            return;
        }

        // Straight
        if (characterState.characterControl.aiProgress.AIDistanceToStartSphere() > 1.5f)
        {
            characterState.characterControl.Turbo = true;
        }
        else
        {
            characterState.characterControl.Turbo = false;
        }

        characterState.characterControl.aiController.WalkStraightToStartSphere();

        if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 1f)
        {                
            characterState.characterControl.Turbo = false;
            characterState.characterControl.MoveRight = false;
            characterState.characterControl.MoveLeft = false;
            characterState.characterControl.MoveUp = false;
            characterState.characterControl.MoveDown = false;
        }

        if (characterState.characterControl.aiProgress.TargetIsOnSamePlatform())
        {
            characterState.characterControl.aiProgress.RepositionDestination();
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.DicAITrans[AI_Walk_Transitions.jump_platform], false);
        animator.SetBool(HashManager.Instance.DicAITrans[AI_Walk_Transitions.fall_platform], false);
    }
}

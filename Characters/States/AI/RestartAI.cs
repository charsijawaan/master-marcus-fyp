﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AI/RestartAI")]
public class RestartAI : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        // walking
        if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 1f)
        {
            if (characterState.characterControl.aiProgress.TargetDistanceToEndSphere() > 0.5f)
            {
                if (characterState.characterControl.aiProgress.TargetIsGrounded())
                {
                    characterState.characterControl.aiController.InitializeAI();
                }
            }
        }

        // landing
        if (characterState.characterControl.animationProgress.IsLanding)
        {
            characterState.characterControl.Turbo = false;
            characterState.characterControl.Jump = false;
            characterState.characterControl.MoveUp = false;
            characterState.characterControl.aiController.InitializeAI();
        }

        // path is blocked
        characterState.characterControl.aiProgress.BlockingCharacter =
            CharacterManager.Instance.GetCharacter(characterState.characterControl.animationProgress.BlockingObj);

        if (characterState.characterControl.aiProgress.BlockingCharacter != null)
        {
            if (characterState.characterControl.animationProgress.Ground != null)
            {
                characterState.characterControl.Turbo = false;
                characterState.characterControl.Jump = false;
                characterState.characterControl.MoveUp = false;
                characterState.characterControl.MoveLeft = false;
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveDown = false;
                characterState.characterControl.aiController.InitializeAI();
            }
        }

        if (characterState.characterControl.animationProgress.Ground != null && !characterState.characterControl.animationProgress.IsRunning(typeof(Jump)) && !characterState.characterControl.animationProgress.IsRunning(typeof(WallJumpPrep)))
        {
            if (characterState.characterControl.aiProgress.GetStartSphereHeight() > 0.1f)
            {
                characterState.characterControl.Turbo = false;
                characterState.characterControl.Jump = false;
                characterState.characterControl.MoveUp = false;
                characterState.characterControl.MoveLeft = false;
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveDown = false;
                characterState.characterControl.aiController.InitializeAI();            
            }   
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AI/AITriggerAttack")]
public class AITriggerAttack : StateData
{
    delegate void GroundAttack(CharacterControl control);
    List<GroundAttack> ListGroundAttacks = new List<GroundAttack>();

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (ListGroundAttacks.Count == 0)
        {
            ListGroundAttacks.Add(NormalGroundAttack);
            ListGroundAttacks.Add(ForwardGroundAttack);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

        if (characterState.characterControl.aiProgress.TargetIsDead())
        {
            characterState.characterControl.Attack = false;
            return;
        }

        if (characterState.characterControl.Turbo &&
            characterState.characterControl.aiProgress.AIDistanceToTarget() < 2f)
        {
            FlyingKick(characterState.characterControl);
        }
        else if (!characterState.characterControl.Turbo &&
            characterState.characterControl.aiProgress.AIDistanceToTarget() < 1f)
        {
            ListGroundAttacks[Random.Range(0, ListGroundAttacks.Count)](characterState.characterControl);
        }
        else
        {
            characterState.characterControl.Attack = false;
        }
        characterState.characterControl.animationProgress.AttackTriggered = characterState.characterControl.Attack;
    }


    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public void NormalGroundAttack(CharacterControl control)
    {            
        if (control.aiProgress.TargetIsOnSamePlatform())
        {
            control.MoveRight = false;
            control.MoveLeft = false;
            control.MoveUp = false;
            control.MoveDown = false;
            control.Attack = true;
        }
        else
        {
            control.Attack = false;
        }
    }

    public void ForwardGroundAttack(CharacterControl control)
    {
        if (control.aiProgress.TargetIsOnSamePlatform())
        {
            if (control.aiProgress.TargetIsOnRightSide())
            {
                control.MoveRight = true;
                control.MoveLeft = false;
                control.MoveUp = false;
                control.MoveDown = false;
                control.Attack = true;
            }
            else if(control.aiProgress.TargetIsOnLeftSide())
            {
                control.MoveRight = false;
                control.MoveLeft = true;
                control.MoveUp = false;
                control.MoveDown = false;
                control.Attack = true;
            }
            else if(control.aiProgress.TargetIsOnUpSide())
            {
                control.MoveRight = false;
                control.MoveLeft = false;
                control.MoveUp = true;
                control.MoveDown = false;
                control.Attack = true;
            }
            else if(control.aiProgress.TargetIsOnDownSide())
            {
                control.MoveRight = false;
                control.MoveLeft = false;
                control.MoveUp = false;
                control.MoveDown = true;
                control.Attack = true;
            }
            else if(control.aiProgress.TargetIsOnUpRightSide())
            {
                control.MoveRight = true;
                control.MoveLeft = false;
                control.MoveUp = true;
                control.MoveDown = false;
                control.Attack = true;
            }
            else if(control.aiProgress.TargetIsOnUpLeftSide())
            {
                control.MoveRight = false;
                control.MoveLeft = true;
                control.MoveUp = true;
                control.MoveDown = false;
                control.Attack = true;
            }
            else if(control.aiProgress.TargetIsOnDownRightSide())
            {
                control.MoveRight = true;
                control.MoveLeft = false;
                control.MoveUp = false;
                control.MoveDown = true;
                control.Attack = true;
            }
            else if(control.aiProgress.TargetIsOnDownLeftSide())
            {
                control.MoveRight = false;
                control.MoveLeft = true;
                control.MoveUp = false;
                control.MoveDown = true;
                control.Attack = true;
            }
        }
        else
        {
            control.Attack = false;
        }
    }

    public void FlyingKick(CharacterControl control)
    {
        if (control.aiProgress.DoFlyingKick &&
            control.aiProgress.TargetIsOnSamePlatform())
        {
            control.Attack = true;
        }
        else
        {
            control.Attack = false;
        }
    }
}

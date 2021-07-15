using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TransitionConditionType
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    ATTACK,    
    JUMP,
    GRABBING_LEDGE,
    LEFT_OR_RIGHT,
    GROUNDED,
    MOVE_FORWARD,
    AIR,
    BLOCKED_BY_WALL,
    CAN_WALL_JUMP,
    NOT_GRABBING_LEDGE,
    IS_RUNNING,
    ATTACK2,
    //IS_PUSHING,
}

[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AbilityData/TransitionIndexer")]
public class TransitionIndexer : StateData
{
    public int Index;
    public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (MakeTransition(characterState.characterControl))
        {
            animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], Index);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (animator.GetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex]) == 0)
        {
            if (MakeTransition(characterState.characterControl))
            {
                animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], Index);
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], 0);
    }

    private bool MakeTransition(CharacterControl control)
    {
        foreach(TransitionConditionType c in transitionConditions)
        {
            switch (c)
            {
                case TransitionConditionType.UP:
                    {
                        if (!control.MoveUp)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.DOWN:
                    {
                        if (!control.MoveDown)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.LEFT:
                    {
                        if (!control.MoveLeft)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.RIGHT:
                    {
                        if (!control.MoveRight)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.ATTACK:
                    {
                        if (!control.animationProgress.AttackTriggered)
                        {
                            return false;
                        }
                    }
                    break;                
                case TransitionConditionType.JUMP:
                    {
                        if (!control.Jump)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.GRABBING_LEDGE:
                    {
                        if (!control.ledgeChecker.IsGrabbingLedge)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.LEFT_OR_RIGHT:
                    {
                        if (!control.MoveLeft && !control.MoveRight)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.GROUNDED:
                    {
                        if (control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]) == false)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.MOVE_FORWARD:
                    {
                        if(!(control.MoveLeft || control.MoveRight || control.MoveDown || control.MoveUp))
                        {
                            return false;
                        }
                    }
                    break;

                case TransitionConditionType.AIR:
                    {
                        if (!control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]) == false)
                        {
                            return false;
                        }
                    }
                    break;

                case TransitionConditionType.BLOCKED_BY_WALL:
                    {
                        if(control.animationProgress.BlockingObj == null)
                        {
                            return false;
                        }
                        else
                        {
                            if(CharacterManager.Instance.GetCharacter(control.animationProgress.BlockingObj) != null)
                            {
                                return false;
                            }
                        }
                    }
                    break;

                case TransitionConditionType.CAN_WALL_JUMP:
                    {
                        if (!control.animationProgress.CanWallJump)
                        {
                            return false;
                        }                            
                    }
                    break;

                case TransitionConditionType.NOT_GRABBING_LEDGE:
                    {
                        if (control.ledgeChecker.IsGrabbingLedge)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.IS_RUNNING:
                    {
                        if (!control.Turbo)
                        {
                            return false;
                        }
                    }
                    break;
                case TransitionConditionType.ATTACK2:
                    {
                        if (!control.animationProgress.Attack2Triggered)
                        {
                            return false;
                        }
                    }
                    break;
                //case TransitionConditionType.IS_PUSHING:
                //    {
                //        if (!control.grabChecker.IsGrabbing)
                //        {
                //            return false;
                //        }
                //    }
                //    break;
            }
        }

        return true;
    }
}

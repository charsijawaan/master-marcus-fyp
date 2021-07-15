using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "MasterMarcus/AbilityData/MoveForward")]
public class MoveForward : StateData
{
    public bool debug;

    public bool Constant;
    public AnimationCurve SpeedGraph;
    public float Speed;
    public float BlockDistance;

    [Header("IgnoreCharacterBox")]
    public bool IgnoreCharacterBox;
    public float IgnoreStartTime;
    public float IgnoreEndTime;

    [Header("Momentum")]
    public bool UseMomentum;
    public float StartingMomentum;
    public float MaxMomentum;
    public bool ClearMomentumOnExit;

    private List<GameObject> SpheresList;
    private float DirBlock;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.characterControl.animationProgress.LatestMoveForward = this;

        if (StartingMomentum > 0.001f)
        {
            if (characterState.characterControl.IsFacingForward())
            {
                characterState.characterControl.animationProgress.AirMomentum = StartingMomentum;
            }
            else
            {
                characterState.characterControl.animationProgress.AirMomentum = -StartingMomentum;
            }
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (debug)
        {
            Debug.Log(stateInfo.normalizedTime);
        }

        //characterState.characterControl.animationProgress.LockDirectionNextState = LockDirectionNextState;

        //if (characterState.characterControl.animationProgress.IsRunning(typeof(MoveForward)))
        //{
        //    return;
        //}

        if(characterState.characterControl.animationProgress.LatestMoveForward != this)
        {
            return;
        }

        if (characterState.characterControl.animationProgress.IsRunning(typeof(WallSlide)))
        {
            return;
        }

        if (characterState.characterControl.Jump)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], true);
        }

        if (characterState.characterControl.Turbo)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], true);
        }
        else
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], false);
        }

        if (UseMomentum)
        {
            UpdateMomentum(characterState.characterControl, stateInfo);
        }
        else
        {
            if (Constant)
            {
                ConstantMove(characterState.characterControl, animator, stateInfo);
            }
            else
            {
                ControlledMove(characterState.characterControl, animator, stateInfo);
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (ClearMomentumOnExit)
        {
            characterState.characterControl.animationProgress.AirMomentum = 0f;
        }
    }

    private void UpdateMomentum(CharacterControl control, AnimatorStateInfo stateInfo)
    {
        if (!control.animationProgress.RightSideIsBlocked())
        {
            if (control.MoveRight)
            {
                control.animationProgress.AirMomentum += SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
            }
        }
            
        if (!control.animationProgress.LeftSideIsBlocked())
        {
            if (control.MoveLeft)
            {
                control.animationProgress.AirMomentum -= SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
            }
        }
        if (!control.animationProgress.DownSideIsBlocked())
        {
            if (control.MoveDown)
            {
                control.animationProgress.AirMomentum += SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
            }
        }
        if (!control.animationProgress.UpSideIsBlocked())
        {
            if (control.MoveUp)
            {
                control.animationProgress.AirMomentum -= SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
            }
        }
        if (control.animationProgress.RightSideIsBlocked() || control.animationProgress.LeftSideIsBlocked() || control.animationProgress.DownSideIsBlocked() || control.animationProgress.UpSideIsBlocked())
        {
            control.animationProgress.AirMomentum =
                Mathf.Lerp(control.animationProgress.AirMomentum, 0f, Time.deltaTime * 1.5f);  
        }
            

        if (Mathf.Abs(control.animationProgress.AirMomentum) >= MaxMomentum)
        {
            if (control.animationProgress.AirMomentum > 0f)
            {
                control.animationProgress.AirMomentum = MaxMomentum;
            }
            else if (control.animationProgress.AirMomentum < 0f)
            {
                control.animationProgress.AirMomentum = -MaxMomentum;
            }
        }

        if (!IsBlocked(control, Speed, stateInfo))
        {
            control.MoveForward(Speed, Mathf.Abs(control.animationProgress.AirMomentum));
        }
    }

    private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (!IsBlocked(control, Speed, stateInfo))
        {
            control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
        }

        if (!control.MoveRight && !control.MoveLeft)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
        }
        else
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
        }
    }

    private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
    {
        // Cases to Ignore
        if(control.MoveLeft && control.MoveRight)
        {
            return;
        }

        if(control.MoveUp && control.MoveDown)
        {
            return;
        }

        // Diagonal Cases
        if (control.MoveUp && control.MoveRight)
        {                
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }
        else if (control.MoveUp && control.MoveLeft)
        {
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }
        else if (control.MoveDown && control.MoveRight)
        {
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }
        else if (control.MoveDown && control.MoveLeft)
        {
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        // Simple Cases
        else if (control.MoveRight)
        {
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        else if (control.MoveLeft)
        {
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        else if (control.MoveUp)
        {
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        else if (control.MoveDown)
        {
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }


        else
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
            return;
        }


        CheckTurn(control);
    }

    private void CheckTurn(CharacterControl control)
    {
        // Diagonal Cases
        if (control.MoveUp && control.MoveRight)
        {
            control.transform.rotation = Quaternion.Euler(0f, -45f, 0f);
        }
        else if (control.MoveUp && control.MoveLeft)
        {
            control.transform.rotation = Quaternion.Euler(0f, -135f, 0f);
        }

        else if (control.MoveDown && control.MoveRight)
        {
            control.transform.rotation = Quaternion.Euler(0f, 45f, 0f);
        }

        else if (control.MoveDown && control.MoveLeft)
        {
            control.transform.rotation = Quaternion.Euler(0f, 135f, 0f);
        }

        // Simple Cases
        else if (control.MoveRight)
        {
            control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        else if (control.MoveLeft)
        {
            control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        else if (control.MoveUp)
        {
            control.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }

        else if (control.MoveDown)
        {
            control.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }

    bool IgnoringCharacterBox(Collider col, AnimatorStateInfo stateInfo)
    {
        if (!IgnoreCharacterBox)
        {
            return false;
        }

        if (stateInfo.normalizedTime < IgnoreStartTime)
        {
            return false;
        }
        else if (stateInfo.normalizedTime > IgnoreEndTime)
        {
            return false;
        }

        if (col.transform.root.gameObject.GetComponent<CharacterControl>() != null)
        {
            return true;
        }

        return false;
    }

    bool IsBlocked(CharacterControl control, float speed, AnimatorStateInfo stateInfo)
    {
        if (speed > 0)
        {
            SpheresList = control.collisionSpheres.FrontSpheres;
            DirBlock = 0.3f;
        }
        else
        {
            SpheresList = control.collisionSpheres.BackSpheres;
            DirBlock = -0.3f;
        }

        foreach (GameObject o in SpheresList)
        {
            Debug.DrawRay(o.transform.position, control.transform.forward * DirBlock, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, control.transform.forward * DirBlock, out hit, BlockDistance))
            {
                if (!IsBodyPart(hit.collider, control) &&
                    !IgnoringCharacterBox(hit.collider, stateInfo) &&
                    !Ledge.IsLedge(hit.collider.gameObject) &&
                    !Ledge.IsLedgeChecker(hit.collider.gameObject) &&
                    !IsTrigger(hit.collider, control) &&
                    !Box.IsBox(hit.collider.gameObject) &&
                    !Box.IsGrabChecker(hit.collider.gameObject))
                {
                    control.animationProgress.BlockingObj = hit.collider.transform.root.gameObject;
                    return true;
                }
            }
        }

        control.animationProgress.BlockingObj = null;
        return false;
    }

    bool IsBodyPart(Collider col, CharacterControl control)
    {
        if (col.transform.root.gameObject == control.gameObject)
        {
            return true;
        }

        CharacterControl target = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

        if (target == null)
        {
            return false;
        }

        if (target.damageDetector.IsDead())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsTrigger(Collider col, CharacterControl control)
    {
        if(col.gameObject.name.Contains("Trigger"))
        {
            return true;
        }
        return false;
    }
}

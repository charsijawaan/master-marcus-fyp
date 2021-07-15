using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationProgress : MonoBehaviour
{
    public Dictionary<StateData, int> CurrentRunningAbilities = new Dictionary<StateData, int>();

    public bool CameraShaken;
    public List<PoolObjectType> SpawnedObjList = new List<PoolObjectType>();
    public bool RagdollTriggered;
    public MoveForward LatestMoveForward;

    [Header("Attack Button")]
    public bool AttackTriggered;
    public bool AttackButtonIsReset;
    public bool Attack2Triggered;

    [Header("GroundMovement")]
    public bool IsLanding;
    public bool isPushing;

    [Header("Colliding Objects")]
    public GameObject Ground;
    public GameObject BlockingObj;

    [Header("AirControl")]
    public bool Jumped;
    public float AirMomentum;
    public bool CancelPull;
    public Vector3 MaxFallVelocity;
    public bool CanWallJump;

    [Header("UpdateBoxCollider")]
    public bool UpdatingBoxCollider;
    public bool UpdatingSpheres;
    public Vector3 TargetSize;
    public float Size_Speed;
    public Vector3 TargetCenter;
    public float Center_Speed;

    [Header("Damage Info")]
    public Attack Attack;
    public CharacterControl Attacker;
    public TriggerDetector DamagedTrigger;
    public GameObject AttackingPart;

    private CharacterControl control;

    private void Awake()
    {
        control = GetComponent<CharacterControl>();
    }

    private void Update()
    {
        if (control.Attack)
        {
            if (AttackButtonIsReset)
            {
                AttackTriggered = true;
                AttackButtonIsReset = false;
            }
        }
        else
        {
            AttackButtonIsReset = true;
            AttackTriggered = false;
        }

        if(control.Attack2)
        {
            Attack2Triggered = true;
        }
        else
        {
            Attack2Triggered = false;
        }
    }

    public bool IsRunning(System.Type type)
    {
        foreach(KeyValuePair<StateData, int> data in CurrentRunningAbilities)
        {
            if(data.Key.GetType() == type)
            {
                return true;
            }
        }
        return false;
    }

    public bool RightSideIsBlocked()
    {
        if (BlockingObj == null)
        {
            return false;
        }

        if ((BlockingObj.transform.position - control.transform.position).z > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool LeftSideIsBlocked()
    {
        if (BlockingObj == null)
        {
            return false;
        }

        if ((BlockingObj.transform.position - control.transform.position).z < 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // NOT TESTED CODE
    public bool UpSideIsBlocked()
    {
        if (BlockingObj == null)
        {
            return false;
        }

        if ((BlockingObj.transform.position - control.transform.position).x < 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DownSideIsBlocked()
    {
        if (BlockingObj == null)
        {
            return false;
        }

        if ((BlockingObj.transform.position - control.transform.position).x > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // NOT TESTED CODE

}

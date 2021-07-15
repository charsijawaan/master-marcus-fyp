using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AI_TYPE
{
    NONE,
    WALK_AND_JUMP,
}


public class AIController : MonoBehaviour
{
    public AI_TYPE InitialAI;

    List<AISubset> AIList = new List<AISubset>();
    Coroutine AIRoutine;
    Vector3 TargetDir = new Vector3();
    CharacterControl control;

    private void Awake()
    {
        control = this.gameObject.GetComponentInParent<CharacterControl>();
    }

    private void Start()
    {
        InitializeAI();
    }

    public void InitializeAI()
    {
        if (AIList.Count == 0)
        {
            AISubset[] arr = this.gameObject.GetComponentsInChildren<AISubset>();

            foreach (AISubset s in arr)
            {
                if (!AIList.Contains(s))
                {
                    AIList.Add(s);
                    s.gameObject.SetActive(false);
                }
            }
        }

        AIRoutine = StartCoroutine(_InitAI());
    }

    private void OnEnable()
    {
        if (AIRoutine != null)
        {
            StopCoroutine(AIRoutine);
        }
    }

    private IEnumerator _InitAI()
    {
        yield return new WaitForEndOfFrame();

        TriggerAI(InitialAI);
    }

    public void TriggerAI(AI_TYPE aiType)
    {
        AISubset next = null;

        foreach(AISubset s in AIList)
        {
            s.gameObject.SetActive(false);

            if (s.aiType == aiType)
            {
                next = s;
            }
        }

        if (next != null)
        {
            next.gameObject.SetActive(true);
        }
    }

    public void WalkStraightToStartSphere()
    {
        Vector3 enemyLocation = control.transform.position;
        Vector3 destination = control.aiProgress.pathfindingAgent.StartSphere.transform.position;

        TargetDir = destination - enemyLocation;

        if(TargetDir.z < -1f)
        {
            control.MoveLeft = true;
            control.MoveRight = false;
        }
        if(TargetDir.z > 0f)
        {
            control.MoveLeft = false;
            control.MoveRight = true;
        }
        if (TargetDir.x < 0f)
        {
            control.MoveUp = true;
            control.MoveDown = false;
        }
        if (TargetDir.x > 0f)
        {
            control.MoveUp = false;
            control.MoveDown = true;
        }
        if ( (TargetDir.x < 0 && TargetDir.x > -0.25) || (TargetDir.z > 0 && TargetDir.z < 0.25))
        {
            control.MoveUp = false;
            control.MoveDown = false;
        }            
    }
}

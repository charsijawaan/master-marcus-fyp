using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HashManager : Singleton<HashManager>
{
    public Dictionary<TransitionParameter, int> DicMainParams = new Dictionary<TransitionParameter, int>();
    public Dictionary<AI_Walk_Transitions, int> DicAITrans = new Dictionary<AI_Walk_Transitions, int>();

    private void Awake()
    {
        // animation transitions
        TransitionParameter[] arrParam = System.Enum.GetValues(typeof(TransitionParameter)) as TransitionParameter[];

        foreach(TransitionParameter t in arrParam)
        {
            DicMainParams.Add(t, Animator.StringToHash(t.ToString()));
        }

        // AI transitions
        AI_Walk_Transitions[] arrAITrans = System.Enum.GetValues(typeof(AI_Walk_Transitions)) as AI_Walk_Transitions[];

        foreach (AI_Walk_Transitions t in arrAITrans)
        {
            DicAITrans.Add(t, Animator.StringToHash(t.ToString()));
        }
    }
}



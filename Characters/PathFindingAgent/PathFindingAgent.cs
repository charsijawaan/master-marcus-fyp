using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PathFindingAgent : MonoBehaviour
{
    public bool TargetPlayableCharacter;
    public GameObject target;
    NavMeshAgent navMeshAgent;
    Coroutine MoveRoutine;
    public GameObject StartSphere;
    public GameObject EndSphere;
    public bool StartWalk;

    public CharacterControl owner = null;

    private void Awake()
    {
        StartSphere.GetComponent<MeshRenderer>().enabled = false;
        EndSphere.GetComponent<MeshRenderer>().enabled = false;

        navMeshAgent = GetComponent<NavMeshAgent>();        
    }

    public void GoToTarget()
    {
        navMeshAgent.enabled = true;
        StartSphere.transform.parent = null;
        EndSphere.transform.parent = null;
        StartWalk = false;

        navMeshAgent.isStopped = false;

        if (TargetPlayableCharacter)
        {
            target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
        }

        navMeshAgent.SetDestination(target.transform.position);

        //if (!navMeshAgent.pathPending)
        //{
        MoveRoutine = StartCoroutine(_Move());    
        //}


    }

    private void OnEnable()
    {
        if (MoveRoutine != null)
        {
            StopCoroutine(MoveRoutine);
        }
    }

    IEnumerator _Move()
    {
        while (true)
        {
            Vector3 dist = transform.position - navMeshAgent.destination;
                
            if (Vector3.SqrMagnitude(dist) < 0.5f)
            {
                StartSphere.transform.position = navMeshAgent.destination;
                EndSphere.transform.position = navMeshAgent.destination;

                navMeshAgent.isStopped = true;
                StartWalk = true;
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        owner.navMeshObstacle.carving = true;
    }
}
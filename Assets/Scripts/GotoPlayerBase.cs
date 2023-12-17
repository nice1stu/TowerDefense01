using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GotoPlayerBase : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform baseMarker;

    void Update()
    {
        enemy.SetDestination(baseMarker.position);
        Debug.Log(baseMarker.position);
    }
}

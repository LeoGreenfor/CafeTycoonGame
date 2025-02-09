using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotBuyerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;

    private Transform _machineQueuePlace;
    private Transform _tablePlace;

    private bool _isBoughtItem;
    private bool _isCompleteSitting;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    public void SetPath(Transform machineQueue, Transform tablePlace)
    {
        agent = GetComponent<NavMeshAgent>();
        UpdQueuePlace(machineQueue);
        _tablePlace = tablePlace;
    }
    public void UpdQueuePlace(Transform machineQueue)
    {
        _machineQueuePlace = machineQueue;
        agent.SetDestination(_machineQueuePlace.position);
    }
}

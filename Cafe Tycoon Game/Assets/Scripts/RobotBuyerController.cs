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

    public bool _isBoughtItem;
    public bool IsBoughtItem
    {
        set
        {
            _isBoughtItem = value;
            if (value) GoToTheTable();
        }
        get {
            return _isBoughtItem;

        }
    }
    private bool _isCompleteSitting;
    public bool IsCompleteSitting
    {
        set
        {
            _isCompleteSitting = value;
            if (value) LeaveTable();
        }
        get
        {
            return _isCompleteSitting;

        }
    }
    public bool IsAtTable;
    private bool hasArrived = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (HasReachedDestination())
        {
            if (!hasArrived)
            {
                hasArrived = true;
                agent.isStopped = true;

                if (Vector3.Distance(agent.transform.position, _tablePlace.position) <= 0.2f)
                {
                    IsAtTable = true;
                    Debug.Log("✅ Робот прибув до столу!");
                }

                Debug.Log("✅ Агент досяг пункту призначення.");
            }
        }
        else
        {
            agent.isStopped = false;
            //Debug.Log("🔄 Агент рухається...");
        }
    }

    private bool HasReachedDestination()
    {
        if (agent.pathPending)
        {
            Debug.Log("⏳ Шлях ще обчислюється...");
            return false;
        }

        float distanceThreshold = 0.1f; // Малий запас
        bool hasStoppedMoving = agent.velocity.sqrMagnitude < 0.01f;

        bool hasArrived = agent.remainingDistance <= agent.stoppingDistance + distanceThreshold && hasStoppedMoving;

        //Debug.Log($"📏 Відстань до мети: {agent.remainingDistance}, Зупинений: {hasStoppedMoving}");

        return hasArrived;
    }


    public void SetPath(Transform machineQueue, Transform tablePlace)
    {
        UpdQueuePlace(machineQueue);
        _tablePlace = tablePlace;
    }
    public void UpdQueuePlace(Transform machineQueue)
    {
        _machineQueuePlace = machineQueue;
        MoveTo(_machineQueuePlace.position);
    }

    private void MoveTo(Vector3 destination)
    {
        hasArrived = false;
        agent.SetDestination(destination);
    }

    private void GoToTheTable()
    {
        //yield return new WaitForEndOfFrame();
        MoveTo(_tablePlace.position);
    }
    private void LeaveTable()
    {
        Destroy(gameObject);
    }
}

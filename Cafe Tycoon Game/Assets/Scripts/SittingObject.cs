using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingObject : InteractibleObject
{
    [SerializeField]
    private int maxNumberSittingPlaces;
    [SerializeField]
    private Transform[] sittingPlaces;
    [SerializeField]
    public int _currentNumberSittingPlaces = 0;
    public bool IsQueueFree => _currentNumberSittingPlaces < maxNumberSittingPlaces;

    private RobotBuyerController _previousFirstRobot;

    private void Start()
    {
        robots = new RobotBuyerController[maxNumberSittingPlaces];
    }

    public Transform GetFreeQueuePlace()
    {
        int index = Array.IndexOf(robots, null);
        return sittingPlaces[index];
    }
    public void UpdCurrentQueueSize(int index)
    {
        _currentNumberSittingPlaces += index;
    }

    public IEnumerator ServeClient(RobotBuyerController client)
    {
        yield return new WaitUntil(() => client.IsAtTable);
        yield return new WaitForSeconds(timeDelay);
        RemovePersonFromQueue(client);
        client.IsCompleteSitting = true;
    }

    public void AddPersonToQueue(RobotBuyerController robot)
    {
        int index = Array.IndexOf(robots, null);
        robots[index] = robot;
        UpdCurrentQueueSize(1);

        StartCoroutine(ServeClient(robot));
    }
    private void RemovePersonFromQueue(RobotBuyerController robot)
    {
        int index = Array.IndexOf(robots, robot);
        robots[index] = null;
        UpdCurrentQueueSize(-1);
    }
}

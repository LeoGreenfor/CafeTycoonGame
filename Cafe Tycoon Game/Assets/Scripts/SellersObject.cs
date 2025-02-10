using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SellersObject : InteractibleObject
{
    [SerializeField]
    private int maxQueueSize;
    public int _currentQueueSize = 0;
    [SerializeField]
    private Transform[] queuePlaces;
    [SerializeField]
    private ParticleSystem moneyParticleSystem;
    public bool IsQueueFree => _currentQueueSize < maxQueueSize;

    private RobotBuyerController _previousFirstRobot;

    private void Start()
    {
        robots = new RobotBuyerController[maxQueueSize];
    }

    public Transform GetFreeQueuePlace()
    {
        int index = Array.IndexOf(robots, null);
        return queuePlaces[index];
    }
    public void UpdCurrentQueueSize(int index)
    {
        _currentQueueSize += index;
    }

    public IEnumerator ServeClient(RobotBuyerController client)
    {
        yield return new WaitForSeconds(timeDelay);
        client.IsBoughtItem = true;
        RemoveFirstPersonFromQueue();
    }

    public void AddPersonToQueue(RobotBuyerController robot)
    {
        robots[_currentQueueSize] = robot;
        UpdCurrentQueueSize(1);

        CheckFirstElementChange();
    }
    private void RemoveFirstPersonFromQueue()
    {
        for (int i = 1; i < robots.Length; i++)
        {
            robots[i - 1] = robots[i];

            if (robots[i - 1] != null) // Prevent null reference
                robots[i - 1].UpdQueuePlace(queuePlaces[i - 1]);
        }

        robots[_currentQueueSize - 1] = null; // Set last element to null
        UpdCurrentQueueSize(-1);

        CheckFirstElementChange();
    }
    private void CheckFirstElementChange()
    {
        if (_currentQueueSize > 0 && robots[0] != _previousFirstRobot)
        {
            _previousFirstRobot = robots[0];

            if (robots[0] != null) // Ensure it's not null before calling ServeClient
                StartCoroutine(ServeClient(robots[0]));
        }
    }
}

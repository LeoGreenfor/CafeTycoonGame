using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MachineObject : InteractibleObject
{
    [SerializeField]
    private int level;
    [SerializeField]
    private int maxQueueSize;
    private int _currentQueueSize = 0;
    [SerializeField]
    private Transform[] queuePlaces;
    [SerializeField]
    private ParticleSystem moneyParticleSystem;
    public bool IsQueueFree => _currentQueueSize < maxQueueSize;
    public bool IsQueueFullLevelUp => maxQueueSize == queuePlaces.Length;

    private RobotBuyerController _previousFirstRobot;
    private float _currentPrice;

    private void Start()
    {
        robots = new List<RobotBuyerController>(new RobotBuyerController[maxQueueSize]);
        _currentPrice = basePrice;
    }

    #region Client Handle
    public Transform GetFreeQueuePlace()
    {
        int index = robots.IndexOf(null);
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
        for (int i = 1; i < robots.Count; i++)
        {
            robots[i - 1] = robots[i];

            if (robots[i - 1] != null)
                robots[i - 1].UpdQueuePlace(queuePlaces[i - 1]);
        }

        robots[_currentQueueSize - 1] = null;
        UpdCurrentQueueSize(-1);

        CheckFirstElementChange();
    }
    private void CheckFirstElementChange()
    {
        if (_currentQueueSize > 0 && robots[0] != _previousFirstRobot)
        {
            _previousFirstRobot = robots[0];

            if (robots[0] != null)
                StartCoroutine(ServeClient(robots[0]));
        }
    }
    #endregion

    protected override void ActionOnClick()
    {
        UIManager.Instance.SetAndShowMachinePopUp(description, level, this);
    }

    public void UpdMaxQueueSize()
    {
        queuePlaces[maxQueueSize].gameObject.SetActive(true);
        maxQueueSize++;
        robots.Add(null);
    }
    public void UpdPricePerItem()
    {
        _currentPrice++;
    }
    public float GetCurrentPrice()
    {
        return _currentPrice;
    }
}

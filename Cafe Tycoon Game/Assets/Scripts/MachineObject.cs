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
    private ParticleSystem moneyParticleSystem;

    private RobotBuyerController _previousFirstRobot;

    private void Start()
    {
        robots = new List<RobotBuyerController>(new RobotBuyerController[maxQueueSize]);
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
        moneyParticleSystem.Play();
        GameManager.Instance.Money += profits;
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
        base.ActionOnClick();
        UIManager.Instance.SetAndShowMachinePopUp(description, level, this);
    }
    protected override void OnIsAvailable()
    {
        base.OnIsAvailable();
        UpdMaxQueueSize(1);
    }

    public void UpdMaxQueueSize()
    {
        queuePlaces[maxQueueSize].gameObject.SetActive(true);
        maxQueueSize++;
        robots.Add(null);
    }

    public void UpdMaxQueueSize(int length)
    {
        for (int i = 0; i < length; i++)
        {
            queuePlaces[i].gameObject.SetActive(true);
            robots.Add(null);
        }
        maxQueueSize = length;
    }

    #region Economics

    [SerializeField]
    private float profits;
    [SerializeField]
    private float priceForProfitsLevelUp;
    [SerializeField]
    private float priceForQueueLevelUp;

    public void UpdProfits(float price)
    {
        profits = price;
    }
    public float GetProfit()
    {
        return profits;
    }
    public void UpdPriceForProfitsLevelUp(float price)
    {
        priceForProfitsLevelUp = price;
    }
    public float GetPriceForProfitsLevelUp() 
    {
        return priceForProfitsLevelUp;
    }
    public void UpdPriceForQueueLevelUp(float price)
    {
        priceForQueueLevelUp = price;
    }
    public float GetPriceForQueueLevelUp()
    {
        return priceForQueueLevelUp;
    }
    #endregion
}

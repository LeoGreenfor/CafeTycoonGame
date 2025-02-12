using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObject : InteractibleObject
{
    [SerializeField]
    private float priceForQueueLevelUp;

    private void Start()
    {
        robots = new List<RobotBuyerController>(new RobotBuyerController[maxQueueSize]);
    }

    #region Client Handler
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
        yield return new WaitUntil(() => client.IsAtTable);
        yield return new WaitForSeconds(timeDelay);
        RemovePersonFromQueue(client);
        client.IsCompleteSitting = true;
    }

    public void AddPersonToQueue(RobotBuyerController robot)
    {
        int index = robots.IndexOf(null);
        robots[index] = robot;
        UpdCurrentQueueSize(1);

        StartCoroutine(ServeClient(robot));
    }
    private void RemovePersonFromQueue(RobotBuyerController robot)
    {
        int index = robots.IndexOf(robot);
        robots[index] = null;
        UpdCurrentQueueSize(-1);
    }
    #endregion

    protected override void ActionOnClick()
    {
        base.ActionOnClick();
        UIManager.Instance.SetAndShowTablePopUp(description, this);
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
    public void UpdPriceForQueueLevelUp(float price)
    {
        priceForQueueLevelUp = price;
    }
    public float GetPriceForQueueLevelUp()
    {
        return priceForQueueLevelUp;
    }
    public int GetLastAddedChairNumber()
    {
        return maxQueueSize - 1;
    }
}

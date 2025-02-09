using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellersObject : InteractibleObject
{
    [SerializeField]
    private int maxQueueSize;
    private int _currentQueueSize;
    [SerializeField]
    private Transform[] queuePlaces;
    [SerializeField]
    private ParticleSystem moneyParticleSystem;

    public bool GetQueueCapability()
    {
        return _currentQueueSize < maxQueueSize; 
    }
    public Transform GetFreeQueuePlace()
    {
        return queuePlaces[_currentQueueSize];
    }
    public void UpdCurrentQueueSize(int index)
    {
        _currentQueueSize += index;
    }

    /*public void AddPersonToQueue(GameObject gameObject)
    {
        _currentQueueSize++;
        gameObject.transform = queuePlaces[_currentQueueSize];
    }*/
}

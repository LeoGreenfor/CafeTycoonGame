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
    private int _currentNumberSittingPlaces;


    public bool GetSittingCapability()
    {
        return _currentNumberSittingPlaces < maxNumberSittingPlaces;
    }
    public Transform GetFreeQueuePlace()
    {
        return sittingPlaces[_currentNumberSittingPlaces];
    }
    public void UpdCurrentQueueSize(int index)
    {
        _currentNumberSittingPlaces += index;
    }
}

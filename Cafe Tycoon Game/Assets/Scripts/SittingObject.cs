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
}

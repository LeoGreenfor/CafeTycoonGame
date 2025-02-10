using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{
    public bool IsAvaliable;
    [SerializeField]
    protected float price;
    [SerializeField]
    protected float timeDelay;
    [SerializeField]
    protected RobotBuyerController[] robots;
}

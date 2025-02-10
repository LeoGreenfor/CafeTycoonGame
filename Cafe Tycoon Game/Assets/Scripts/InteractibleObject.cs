using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractibleObject : MonoBehaviour
{
    public bool IsAvaliable;
    [SerializeField]
    protected float basePrice;
    [SerializeField]
    protected float timeDelay;
    [SerializeField]
    protected RobotBuyerController[] robots;
    [SerializeField]
    protected DescriptionBase description;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                DetectTap(touch.position);
            }
        }
    }

    private void DetectTap(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ActionOnClick();
            }
        }
    }

    protected virtual void ActionOnClick()
    {

    }
}

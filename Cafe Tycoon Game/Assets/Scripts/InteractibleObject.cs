using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractibleObject : MonoBehaviour
{
    public bool IsAvaliable
    {
        get
        {
            return _isAvaliable;
        }
        set
        {
            _isAvaliable = value;
            if (value) OnIsAvailable();
            Debug.Log("a");
        }
    }
    [SerializeField] 
    private bool _isAvaliable;
    [SerializeField]
    protected float itemPrice;
    [SerializeField]
    protected float timeDelay;
    [SerializeField]
    protected List<RobotBuyerController> robots;
    [SerializeField]
    protected DescriptionBase description;

    [Header("Visuals")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material unlockedMaterial;

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

    protected virtual void OnIsAvailable()
    {
        meshRenderer.material = unlockedMaterial;
    }
    public float GetItemPrice()
    {
        return itemPrice;
    }
}

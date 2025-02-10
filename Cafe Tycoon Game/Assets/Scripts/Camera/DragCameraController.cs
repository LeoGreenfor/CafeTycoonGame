using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCameraController : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 0.01f;
    [SerializeField] private float horizontalLimit;
    [SerializeField] private float verticalLimit;

    private Vector2 previousTouchPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - previousTouchPosition;

                Vector3 move = new Vector3(-delta.x * dragSpeed, 0, -delta.y * dragSpeed);

                transform.position += move;
                previousTouchPosition = touch.position;

                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(pos.x, -horizontalLimit, horizontalLimit);
                pos.z = Mathf.Clamp(pos.z, -verticalLimit, verticalLimit);
                transform.position = pos;
            }
        }
    }
}
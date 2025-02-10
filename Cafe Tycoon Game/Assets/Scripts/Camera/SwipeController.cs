using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    public float swipeThreshold = 50f; // ̳������� ������� ��� ��������� ������
    public float swipeMoveDistance = 1f; // ³������, �� ��� ����������� ������ �� ���� �����

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;

                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                if (Mathf.Abs(swipeDelta.x) > swipeThreshold)
                {
                    if (swipeDelta.x > 0)
                    {
                        Debug.Log("Swipe Right");
                        MoveCamera(Vector3.right); // ���������� ������ ������
                    }
                    else
                    {
                        Debug.Log("Swipe Left");
                        MoveCamera(Vector3.left); // ���������� ������ ����
                    }
                }
                else if (Mathf.Abs(swipeDelta.y) > swipeThreshold)
                {
                    if (swipeDelta.y > 0)
                    {
                        Debug.Log("Swipe Up");
                        MoveCamera(Vector3.up); // ���������� ������ �����
                    }
                    else
                    {
                        Debug.Log("Swipe Down");
                        MoveCamera(Vector3.down); // ���������� ������ ����
                    }
                }
            }
        }
    }

    // ����� ��� ���������� ������
    void MoveCamera(Vector3 direction)
    {
        Camera.main.transform.Translate(direction * swipeMoveDistance);
    }
}

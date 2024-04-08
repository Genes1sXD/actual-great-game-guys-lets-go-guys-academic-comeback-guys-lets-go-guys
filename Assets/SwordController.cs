using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Transform player;
    public float offsetDistance = 1f;

    private Vector3 originalScale;

    void Start()
    {

        originalScale = transform.localScale;
    }

    void Update()
    {
        PointTowardsMouseAndAdjustPosition();
    }

    void PointTowardsMouseAndAdjustPosition()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 directionToMouse = mouseWorldPosition - player.position;
        directionToMouse.z = 0;

        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        transform.position = player.position + directionToMouse.normalized * offsetDistance;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (mouseWorldPosition.x < player.position.x)
        {

            transform.localScale = new Vector3(originalScale.x, -Mathf.Abs(originalScale.y), originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, Mathf.Abs(originalScale.y), originalScale.z);
        }
    }
}

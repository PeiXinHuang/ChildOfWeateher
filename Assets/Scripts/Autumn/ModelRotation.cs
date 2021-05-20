using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{

    private Vector3 dragStart, dragCurrent;
    private Quaternion newRotation;
    [SerializeField] private float moveSpeed;
    private void Start()
    {

        newRotation = transform.rotation;

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            dragCurrent = Input.mousePosition;
            Vector3 difference = dragStart - dragCurrent;
            dragStart = dragCurrent;
            newRotation *= Quaternion.Euler(Vector3.up * -difference.x);
           // newRotation *= Quaternion.Euler(Vector3.right * difference.y);


        }
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, moveSpeed * Time.deltaTime);
    }


}

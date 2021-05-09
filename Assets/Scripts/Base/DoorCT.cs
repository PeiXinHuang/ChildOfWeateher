using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCT : ModelCT
{

    public bool isOpen = false;
    public float distance = 5.0f;

    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            if (isOpen)
            {
                this.transform.Rotate(Vector3.up, 90, Space.Self);
            }
            else
            {
                this.transform.Rotate(Vector3.up, -90, Space.Self);
            }
            isOpen = !isOpen;
        }

    }
}

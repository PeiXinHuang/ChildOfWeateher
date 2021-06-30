using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCT : MonoBehaviour
{
    [SerializeField] private Camera mapCam;
    [SerializeField] private GameObject player;

    public float height = 20.0f;
    private void Update()
    {
        //跟随角色移动
        mapCam.transform.position = player.transform.position + new Vector3(0, height, 0);
    }
}

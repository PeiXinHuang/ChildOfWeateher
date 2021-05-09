using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCT : ModelCT
{
    public float speed = 50.0f;

    private float offset = 0;
    private Material wallMat;

    private void Start()
    {
        wallMat = this.transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }
    private void Update()
    {
        offset += speed * Time.deltaTime;
        wallMat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}

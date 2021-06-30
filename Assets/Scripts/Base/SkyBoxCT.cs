using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxCT : MonoBehaviour
{
    public List<Material> materials = new List<Material>();
    private int currentSkyBoxIndex = 0;
    public Skybox skybox;
    public void ChanegSkyBox(int index)
    {
        skybox.material = materials[index];
    }


}

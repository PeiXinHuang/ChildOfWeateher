using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureCT : MonoBehaviour
{
    [SerializeField] private GameObject snow;
    [SerializeField] private GameObject rainPrefab;

    private GameObject rain;

    public void BeginSetSnow()
    {
        //rain.SetActive(false);
        if(rain)
            Destroy(rain);
        snow.SetActive(true);
    }

    public void BeginSetRain()
    {
        //rain.SetActive(true);
        if (rain)
            Destroy(rain);
        rain = GameObject.Instantiate(rainPrefab);
        snow.SetActive(false);
    }

    public void BeginSetSun()
    {
        if (rain)
            Destroy(rain);
        snow.SetActive(false);
    }
}

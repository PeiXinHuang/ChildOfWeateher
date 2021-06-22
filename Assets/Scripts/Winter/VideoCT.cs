using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCT : MonoBehaviour
{
    public PlayerCT player;
    public VideoPlayer videoPlayer;
    public string str;
    


    private void OnTriggerEnter(Collider other)
    {
        
        videoPlayer.Play();
        this.GetComponent<MeshRenderer>().material.color = Color.white;
        player.uiController.ShowMessage(player.characterName, str);
    }

    private void OnTriggerExit(Collider other)
    {
        videoPlayer.Pause();
        this.GetComponent<MeshRenderer>().material.color = Color.black;
        player.uiController.HideMessageImmediate();
    }




}

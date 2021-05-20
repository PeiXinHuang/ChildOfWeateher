using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCT : MonoBehaviour
{
    public PlayerCT player;
    public VideoPlayer videoPlayer;
    public float distance = 5.0f;
    public void Update()
    {
        if(Vector3.Distance(player.transform.position, this.transform.position) < distance)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
        }
    }


    public string str;

    public void OnMouseDown()
    {
        if(videoPlayer.isPlaying)
            player.uiController.ShowMessage(player.characterName, str);
    }

}

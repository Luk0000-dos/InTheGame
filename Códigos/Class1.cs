using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Class1 : MonoBehaviour
{
	public Class1()
	{
		public VideoPlayer videoPlayer; // Referência ao seu VideoPlayer

}
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("InteractiveObject"))
    {
        PlayVideo();
    }
}

void PlayVideo()
{
    // Inicia o vídeo
    if (videoPlayer != null)
    {
        videoPlayer.Play();
    }
}
}

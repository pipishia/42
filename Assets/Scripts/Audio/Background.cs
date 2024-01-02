using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private AudioSource bgMusicAudioSource;
    
    void OnEnable()
    {
		//在所有Game Object中找尋Background Music
        bgMusicAudioSource = GameObject.FindGameObjectWithTag ("Background Music").GetComponent<AudioSource> ();
        
		//暫停音樂
        bgMusicAudioSource.Pause ();
    }
    
    void OnDisable()
    {
		//繼續音樂
        bgMusicAudioSource.UnPause ();
    }
}

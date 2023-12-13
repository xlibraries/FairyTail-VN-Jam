using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Zigurous.Tweening;

[System.Serializable]
public struct MusicPair 
{
    public string name;
    public AudioClip music;
}

public class AudioManager : MonoBehaviour
{
    public List<MusicPair> musicList;
    public AudioSource mainAudioSource;

    public static AudioManager instance {get; private set;}


    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
        instance = this;
        mainAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }


    public void PlayMusicSimple(string name)
    {
        AudioClip c = musicList.Find(m => m.name == name).music;
        if(c != null)
        {
            mainAudioSource.clip = c;
            mainAudioSource.Play();
            mainAudioSource.loop = true;
        }
    }

    public void PlayMusic(string name)
    {
        AudioClip c = musicList.Find(m => m.name == name).music;
        Debug.Assert(c != null);

        if(mainAudioSource.isPlaying) //If music is already playing, crossfade and replace.
        {
            AudioSource secondaryAudioSource = gameObject.AddComponent<AudioSource>();
            secondaryAudioSource.clip = c;
            secondaryAudioSource.Play();
            secondaryAudioSource.volume = 0.0f;
            secondaryAudioSource.loop = true;

            mainAudioSource.TweenVolume(0.0f,2.0f);
            secondaryAudioSource.TweenVolume(1.0f,2.0f).OnComplete(()=>{
                Destroy(mainAudioSource);
                mainAudioSource = secondaryAudioSource;
                mainAudioSource.loop = true;
            });            
        }
        else //If music is not playing, just play the normal music.
        {
            mainAudioSource.clip = c;
            mainAudioSource.Play();
            mainAudioSource.loop = true;
        }
    }


}

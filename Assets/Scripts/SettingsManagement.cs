using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume",value);
        Debug.Log(value);
        AudioManager.instance.PlayMusicSimple("Forest");
    }
}

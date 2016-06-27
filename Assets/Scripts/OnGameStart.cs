using UnityEngine;
using System.Collections;

/// <summary>
/// This script is enabled when the game starts to set PlayerPrefs and load any needed data.
/// </summary>
public class OnGameStart : MonoBehaviour {

	void Awake()
    {
        int _music = PlayerPrefs.GetInt("music", -1);
        int _sounds = PlayerPrefs.GetInt("sound", -1);
        int _autofire = PlayerPrefs.GetInt("autofire", -1);

        //if any of these are -1, we need to set them to default values
        if (_music == -1)
            PlayerPrefs.SetInt("music", 1);
        if (_sounds == -1)
            PlayerPrefs.SetInt("sound", 1);
        if (_autofire == -1)
            PlayerPrefs.SetInt("autofire", 0);

        Destroy(gameObject);
    }

}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    const string _autofireName = "Autofire";
    const string _soundName = "Sound";
    const string _musicName = "Music";

    GameObject _autofireMain, _soundMain, _musicMain;
    Button _autofireButton, _soundButton, _musicButton;
    Text _autofireButtonText, _soundButtonText, _musicButtonText;

    int _autofire, _sounds, _music;

    void Start()
    {
        _autofireMain = GameObject.Find(_autofireName);
        _soundMain = GameObject.Find(_soundName);
        _musicMain = GameObject.Find(_musicName);
        _autofireButton = _autofireMain.transform.GetChild(0).GetComponent<Button>();
        _soundButton = _soundMain.transform.GetChild(0).GetComponent<Button>();
        _musicButton = _musicMain.transform.GetChild(0).GetComponent<Button>();
        _autofireButtonText = _autofireButton.transform.GetChild(0).GetComponent<Text>();
        _soundButtonText = _soundButton.transform.GetChild(0).GetComponent<Text>();
        _musicButtonText = _musicButton.transform.GetChild(0).GetComponent<Text>();
        _autofire = PlayerPrefs.GetInt("autofire", -1);
        _sounds = PlayerPrefs.GetInt("sound", -1);
        _music = PlayerPrefs.GetInt("music", -1);
        Debug.Log(string.Format("AutoFire:{0}, Sounds:{1}, Music:{2}", _autofire, _sounds, _music));
        OnStartChange();
    }

    void OnStartChange()
    {
        ColorBlock _colors = _autofireButton.colors;
        _colors.normalColor = _autofire == 1 ? Color.green : Color.red;
        _autofireButton.colors = _colors;
        _autofireButtonText.text = _autofire == 1 ? "Yes" : "No";

         _colors = _soundButton.colors;
        _colors.normalColor = _sounds == 1 ? Color.green : Color.red;
        _soundButton.colors = _colors;
        _soundButtonText.text = _sounds == 1 ? "Yes" : "No";

         _colors = _musicButton.colors;
        _colors.normalColor = _music == 1 ? Color.green : Color.red;
        _musicButton.colors = _colors;
        _musicButtonText.text = _music == 1 ? "Yes" : "No";
        gameObject.SetActive(false);
    }

    public void OnInfoChange(string title)
    {
        if (title == "autofire")
        {
            _autofire = _autofire == 1 ? 0 : 1;
            ColorBlock _colors = _autofireButton.colors;
            _colors.normalColor = _autofire == 1 ? Color.green : Color.red;
            _autofireButton.colors = _colors;
            _autofireButtonText.text = _autofire == 1 ? "Yes" : "No";
            PlayerPrefs.SetInt("autofire", _autofire);
        }
        else if (title == "sound")
        {
            _sounds = _sounds == 1 ? 0 : 1;
            ColorBlock _colors = _soundButton.colors;
            _colors.normalColor = _sounds == 1 ? Color.green : Color.red;
            _soundButton.colors = _colors;
            _soundButtonText.text = _sounds == 1 ? "Yes" : "No";
            PlayerPrefs.SetInt("sound", _sounds);
        }
        else if (title == "music")
        {
            _music = _music == 1 ? 0 : 1;
            ColorBlock _colors = _musicButton.colors;
            _colors.normalColor = _music == 1 ? Color.green : Color.red;
            _musicButton.colors = _colors;
            _musicButtonText.text = _music == 1 ? "Yes" : "No";
            PlayerPrefs.SetInt("music", _music);
        }
    }

}
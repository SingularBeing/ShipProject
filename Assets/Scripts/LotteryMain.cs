using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LotteryMain : MonoBehaviour {

    public string[] m_Prizes = new string[]
    {
        "Milk Boost",
        "Money",
        "Tomatoes"
    };

    private bool _showLottery;
    private string _currentPrize = string.Empty;
    private Text _title;
    private Button _roll, _done;

    void Start()
    {
        _title = transform.GetChild(0).GetComponent<Text>();
        _roll = transform.GetChild(1).GetComponent<Button>();
        _done = transform.GetChild(2).GetComponent<Button>();
    }

    public void OnLotterySpin()
    {
        RandomPrize();
        DisplayRoll();
        SetCurrentLottery();
    }

    public void OnLotteryDone()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentLevel"));
    }

    public void RandomPrize()
    {
        int _randNum = Random.Range(0, m_Prizes.Length);
        _currentPrize = m_Prizes[_randNum];
    }

    public void DisplayRoll()
    {
        //set the title
        _title.text = _currentPrize;
        //disable the roll button
        _roll.interactable = false;
        //enable the done button
        _done.interactable = true;
    }

    public void SetCurrentLottery()
    {
        PlayerPrefs.SetString("Lottery", _currentPrize);
    }


	public void OnLevelCompleted()
    {
        
    }

}
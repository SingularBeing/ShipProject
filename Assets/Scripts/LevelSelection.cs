using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

    public Image m_Level1, m_Level2, m_Level3;

    bool l1, l2, l3;

    void Start()
    {
        l1 = true;
        l2 = false;
        l3 = false;
        m_Level1.color = Color.red;
    }

	public void Load_World1()
    {
        l1 = true;
        l2 = false;
        l3 = false;
        m_Level1.color = Color.red;
        m_Level2.color = Color.white;
        m_Level3.color = Color.white;
    }

    public void Load_World2()
    {
        l1 = false;
        l2 = true;
        l3 = false;
        m_Level1.color = Color.white;
        m_Level2.color = Color.red;
        m_Level3.color = Color.white;
    }

    public void Load_World3()
    {
        l1 = false;
        l2 = false;
        l3 = true;
        m_Level1.color = Color.white;
        m_Level2.color = Color.white;
        m_Level3.color = Color.red;
    }

    public void Load_MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void LoadLevel()
    {
        if (l1)
        {
            PlayerPrefs.SetString("CurrentLevel", "Level_1");
        }
        else if (l2)
        {
            PlayerPrefs.SetString("CurrentLevel", "Level_2");
        }
        else if (l3)
        {
            PlayerPrefs.SetString("CurrentLevel", "Level_3");
        }

        SceneManager.LoadScene("Lottery");
    }

}
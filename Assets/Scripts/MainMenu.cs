﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    const string m_BeginningObject = "Beginning_Info";
    const string m_MainGUI = "Main_Info";
    const string m_CreditsGUI = "Credits";
    const string m_TipsGUI = "Tips";
    private GameObject m_BeginningObjectBackup;
    private GameObject m_MainGUIObject;
    private GameObject m_OptionsObject;
    private GameObject m_CreditsGUIObject;
    private GameObject m_TipsGUIObject;

    void Awake()
    {
        PlayerPrefs.DeleteKey("shownInfo");
        m_BeginningObjectBackup = GameObject.Find(m_BeginningObject);
        m_BeginningObjectBackup.SetActive(false);
        m_MainGUIObject = GameObject.Find(m_MainGUI);
        m_OptionsObject = GameObject.Find("Options");
        m_CreditsGUIObject = GameObject.Find("Credits");
        m_CreditsGUIObject.SetActive(false);
        m_TipsGUIObject = GameObject.Find("Tips");
        m_TipsGUIObject.SetActive(false);
    }

	public void Button_Play()
    {
        int shownInfo = PlayerPrefs.GetInt("shownInfo", -1);
        if (shownInfo != -1)
        {
            Button_GoToLevelSelect();
            return;
        }
        m_BeginningObjectBackup.SetActive(true);
        m_MainGUIObject.SetActive(false);
        PlayerPrefs.SetInt("shownInfo", 1);
    }

    public void Button_GoToLevelSelect()
    {
        SceneManager.LoadScene("Level_Selection");
    }

    public void Button_Options()
    {
        m_OptionsObject.SetActive(true);
        m_MainGUIObject.SetActive(false);
    }

    public void Button_Tips()
    {
        m_TipsGUIObject.SetActive(true);
        m_MainGUIObject.SetActive(false);
    }

    public void Button_Credits()
    {
        m_CreditsGUIObject.SetActive(true);
        m_MainGUIObject.SetActive(false);
    }

    public void Load_Main()
    {
        m_MainGUIObject.SetActive(true);
        m_OptionsObject.SetActive(false);
        m_BeginningObjectBackup.SetActive(false);
        m_CreditsGUIObject.SetActive(false);
        m_TipsGUIObject.SetActive(false);
    }

}
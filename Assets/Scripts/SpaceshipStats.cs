using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpaceshipStats : MonoBehaviour
{

	public int m_CurrentCoins = 0;
	public int m_CurrentScore = 0;
	public int m_CurrentLives = 3;
	public int m_CurrentHealth = 100;
	public int m_MaxHealth = 100;

	public Text m_TextScore;
	public Text m_TextLives;
	public Image m_ImageHealth;

	public string m_ScoreText {
		get {
			return m_TextScore.text;
		}set {
			m_TextScore.text = value;
		}
	}

	public string m_LivesText {
		get {
			return m_TextLives.text;
		}set {
			m_TextLives.text = value;
		}
	}

	public float m_HealthAmount {
		get {
			return m_ImageHealth.fillAmount;
		}set {
			m_ImageHealth.fillAmount = (value / m_MaxHealth);
		}
	}

	public int m_Health {
		get {
			return m_CurrentHealth;
		}set {
			m_CurrentHealth = (value > m_MaxHealth ? m_MaxHealth : value);
		}
	}

	void Update ()
	{
		m_ScoreText = string.Format ("Score: {0}", m_CurrentScore);
		m_LivesText = string.Format ("Lives: {0}", m_CurrentLives);
		m_HealthAmount = m_Health;
	}

}
using UnityEngine;
using System.Collections;

public class Master : MonoBehaviour
{

	public Spaceship m_PlayerShip;
	public SpaceshipStats m_PlayerStats;
	public static Master m_Instance;

	public GameObject m_CoinPrefab;
	public GameObject m_EnemyPrefab;
    public GameObject m_BulletPrefab;

	void Awake ()
	{
		m_Instance = this;
	}

	void Start ()
	{
		
	}

    void Update()
    {
        if (m_PlayerStats.m_Health <= 0)
        {
            //lower lives and respawn
            m_PlayerStats.m_CurrentLives -= 1;
            if (m_PlayerStats.m_CurrentLives <= 0)
            {
                m_PlayerStats.m_CurrentLives = 0;
                Destroy(m_PlayerShip.gameObject);
            }
            else
            {
                m_PlayerStats.m_Health = m_PlayerStats.m_MaxHealth;
            }
        }
    }

	public void SpawnCoin (Vector2 location)
	{
		GameObject coin = (GameObject)Instantiate (m_CoinPrefab, location, Quaternion.identity);
		coin.name = "New Coin (" + location + ")";
	}

	public void SpawnEnemy (Vector2 location)
	{
		GameObject enemy = (GameObject)Instantiate (m_EnemyPrefab, location, Quaternion.identity);
		enemy.name = "New Enemy (" + location + ")";
	}

	public void IncreaseScore (int amount)
	{
		m_PlayerStats.m_CurrentScore += amount;
	}

	public void DecreaseLives (int amount)
	{
		m_PlayerStats.m_CurrentLives -= amount;
	}

	public void DecreaseHealth (int amount)
	{
		m_PlayerStats.m_Health -= amount;
	}

	public void IncreaseHealth (int amount)
	{
		m_PlayerStats.m_Health += amount;
	}

    public void IncreaseCoins(int amount)
    {
        m_PlayerStats.m_CurrentCoins += amount;
    }

}
using UnityEngine;
using System.Collections;

public class Master : MonoBehaviour
{

	public Spaceship m_PlayerShip;
	public SpaceshipStats m_PlayerStats;
	public static Master m_Instance;
    public GameObject m_WorldCanvas;

    public GameObject m_CoinPrefab;
	public GameObject m_EnemyPrefab;
    public GameObject m_BulletPrefab;
    public GameObject m_EnemyHealthbarPrefab;

    public static int m_PlayerStandardBulletDamage = 10;
    public static int m_PlayerBonusBulletDamage = 100;

    public int m_CurrentEnemyHealth = 100;


	void Awake ()
	{
		m_Instance = this;
	}

	void Start ()
	{
		
	}

    bool _died;

    void Update()
    {
        if (m_PlayerStats.m_Health <= 0 && !_died)
        {
            //lower lives and respawn
            m_PlayerStats.m_CurrentLives -= 1;
            if (m_PlayerStats.m_CurrentLives <= 0)
            {
                m_PlayerStats.m_CurrentLives = 0;
                if (m_PlayerShip != null)
                    m_PlayerShip.gameObject.SetActive(false);

                //check for a prize
                if(PlayerPrefs.GetString("Lottery") != string.Empty)
                {
                    Debug.Log("You lost : " + PlayerPrefs.GetString("Lottery"));
                    PlayerPrefs.SetString("Lottery", string.Empty);
                }
                _died = true;
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
        enemy.GetComponent<Enemy>().m_CurrentHealth = m_CurrentEnemyHealth;
        enemy.GetComponent<Enemy>().m_MaxHealth = m_CurrentEnemyHealth;
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

    public GameObject CreateEnemyHealthbar()
    {
        GameObject _healthBar = (GameObject)Instantiate(m_EnemyHealthbarPrefab);
        _healthBar.AddComponent<EnemyHealthbar>();
        _healthBar.transform.SetParent(m_WorldCanvas.transform);
        return _healthBar;
    }

}
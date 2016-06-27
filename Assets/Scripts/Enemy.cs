using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public int m_CurrentHealth = 100;
	public int m_MaxHealth = 100;
    public float m_DefaultMovementSpeed = 3;
    public SplineWalk m_Walk;
    public GameObject _healthBar;
    private EnemyHealthbar _healthBarComponent;
    private bool _normalWalkMovement;

    public int m_Health
    {
        get
        {
            return m_CurrentHealth;
        }
        set
        {
            m_CurrentHealth = (value > m_MaxHealth ? m_MaxHealth : value);
        }
    }

    public float m_HealthAmount
    {
        get
        {
            return _healthBarComponent._bar.fillAmount;
        }
        set
        {
            _healthBarComponent._bar.fillAmount = (value / m_MaxHealth);
        }
    }

    public void Impact (int damage)
	{
		m_CurrentHealth -= damage;
		CheckForDeath ();
	}

    void Start()
    {
        //create the health bar
        _healthBar = Master.m_Instance.CreateEnemyHealthbar();
        _healthBarComponent = _healthBar.GetComponent<EnemyHealthbar>();
        m_Walk = GetComponent<SplineWalk>();
        int rand = Random.Range(0, 100);
        m_Walk.spline = (rand >= 0 && rand <= 29) ? GameObject.Find("Bezier_CShape").GetComponent<BezierSpline>() : (rand >= 30 && rand <= 50) ? GameObject.Find("Bezier_SShape").GetComponent<BezierSpline>() : null;
        if (m_Walk.spline == null)
        {
            m_Walk.enabled = false;
            _normalWalkMovement = true;
            //grab a random position on the Y axis
            float randomY = Random.Range(-4, 4);
            transform.position = new Vector3(transform.position.x, randomY, transform.position.z);
        }
        StartCoroutine("WaitToShoot");
    }

    void Update()
    {
        _healthBarComponent.SetPosition(transform.position - new Vector3(0,1.5f,0));
        m_HealthAmount = m_Health;
        //move the enemy if needed
        if (_normalWalkMovement)
        {
            transform.Translate(-transform.right * m_DefaultMovementSpeed * Time.deltaTime);
        }
    }

	void CheckForDeath ()
	{
		if (m_CurrentHealth <= 0) {
            //die
            Master.m_Instance.IncreaseScore (Random.RandomRange(50, 200));
            //spawn some coins
            Master.m_Instance.SpawnCoin(transform.position);
            Destroy (gameObject);
		}
	}

    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(2.5f);
        //shoot a bullet
        var pos = Master.m_Instance.m_PlayerShip.transform.position;

        var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
        GameObject go = (GameObject)Instantiate(Master.m_Instance.m_BulletPrefab, transform.position, q);
        go.GetComponent<Bullet>().m_BulletUser = Bullet.UserOfBullet.Enemey;
        go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 500.0f);
        StartCoroutine("WaitToShoot");
    }

    void OnDisable()
    {
        Destroy(_healthBar);
    }

}
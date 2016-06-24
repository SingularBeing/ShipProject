using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public int m_CurrentHealth = 100;
	public int m_MaxHealth = 100;

	public void Impact (int damage)
	{
		m_CurrentHealth -= damage;
		CheckForDeath ();
	}

    void Start()
    {
        StartCoroutine("WaitToShoot");
    }

	void CheckForDeath ()
	{
		if (m_CurrentHealth <= 0) {
            //die
            Master.m_Instance.IncreaseScore (200);
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

}
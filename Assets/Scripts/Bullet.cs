using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public int m_DamageValue = 10;

	public enum UserOfBullet
	{
		Player,
		Enemey
	}

	public UserOfBullet m_BulletUser;

    void Start()
    {
        if (m_BulletUser != UserOfBullet.Enemey)
            m_DamageValue = Master.m_PlayerStandardBulletDamage;
        StartCoroutine("WaitForDestroy");
    }

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll != null && m_BulletUser != UserOfBullet.Enemey) {
			if (coll.GetComponent<Enemy> () != null) {
				coll.GetComponent<Enemy> ().Impact (m_DamageValue);
			}
		}
        if (coll != null && m_BulletUser != UserOfBullet.Player)
        {
            if (coll.GetComponent<Spaceship>() != null)
            {
                Master.m_Instance.DecreaseHealth(m_DamageValue);
            }
        }
    }

	IEnumerator WaitForDestroy ()
	{
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
	}

}
using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{

	void Update ()
	{
        float _distanceFromPlayer = Vector3.Distance(Master.m_Instance.m_PlayerShip.transform.position, transform.position);
        if(_distanceFromPlayer < 0.8f)
        {
            //collect the coin
            Master.m_Instance.IncreaseCoins(Random.RandomRange(1, 10));
            Destroy(gameObject);
        }
	}

}
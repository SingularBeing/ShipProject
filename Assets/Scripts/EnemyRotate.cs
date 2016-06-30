using UnityEngine;
using System.Collections;

public class EnemyRotate : MonoBehaviour {

    public Transform m_Target;

    void Start()
    {
        m_Target = Master.m_Instance.m_PlayerShip.transform;
    }

    void Update()
    {
        transform.LookAt2D(m_Target);
    }

}
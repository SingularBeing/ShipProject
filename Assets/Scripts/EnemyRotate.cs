using UnityEngine;
using System.Collections;

public class EnemyRotate : MonoBehaviour {

    public Transform m_Target;

    void Update()
    {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(m_Target.position.y - transform.position.y, m_Target.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

}
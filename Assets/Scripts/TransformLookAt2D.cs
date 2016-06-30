using UnityEngine;
using System.Collections;

public static class TransformLookAt2D {

    /// <summary>
    /// Look at a specific Transform
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="target">Target</param>
	public static void LookAt2D(this Transform transform, Transform target)
    {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

}
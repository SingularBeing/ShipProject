using UnityEngine;
using System.Collections;

public class SpaceshipRotate : MonoBehaviour
{

	public float visualDeg;
	public bool facingBottomRight, facingBottomLeft, facingTopRight, facingTopLeft;

	void Update ()
	{
		Vector3 mousePosition = Input.mousePosition;           
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

		// Get Angle in Radians
		float AngleRad = Mathf.Atan2 (mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
		// Get Angle in Degrees
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		visualDeg = AngleDeg;
		// Rotate Object
		this.transform.rotation = Quaternion.Euler (0, 0, AngleDeg);
		if (visualDeg < 0 && visualDeg > -90) {
			facingBottomRight = true;
		} else {
			facingBottomRight = false;
		}

		if (visualDeg < -90 && visualDeg > -180) {
			facingBottomLeft = true;
		} else {
			facingBottomLeft = false;
		}

		if (visualDeg < 180 && visualDeg > 90) {
			facingTopLeft = true;
		} else {
			facingTopLeft = false;
		}

		if (visualDeg < 90 && visualDeg > 0) {
			facingTopRight = true;
		} else {
			facingTopRight = false;
		}

		if (facingTopLeft || facingBottomLeft) {
			GetComponent<SpriteRenderer> ().flipY = true;
		} else {
			GetComponent<SpriteRenderer> ().flipY = false;
		}
	}

}
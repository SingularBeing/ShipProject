using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour
{

	public float m_MovementSpeed = 3;
	public string m_FireButton = "Fire1";
	public GameObject prefab;
	public Transform shootLocation, shootLocation2;
	public SpaceshipRotate _shoot;

	void Update ()
	{
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		transform.Translate (input * Time.deltaTime * m_MovementSpeed);

		if (Input.GetButtonUp (m_FireButton)) {
			//shoot a bullet
			var pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint (pos);

			var q = Quaternion.FromToRotation (Vector3.up, pos - transform.position);
			GameObject go = (GameObject)Instantiate (prefab, _shoot.facingBottomLeft || _shoot.facingTopLeft ? shootLocation2.position : shootLocation.position, q);
			go.GetComponent<Bullet> ().m_BulletUser = Bullet.UserOfBullet.Player;
            go.GetComponent<Rigidbody2D> ().AddForce (go.transform.up * 500.0f);
		}
	}

}
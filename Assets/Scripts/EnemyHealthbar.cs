using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour {

    public Image _bar;

    void Start()
    {
        _bar = GetComponent<Image>();
    }

	public void SetPosition(Vector3 _position)
    {
        this.transform.position = _position;
    }

    public void SetHealth(int _health)
    {
        _bar.fillAmount = _health;
    }

}
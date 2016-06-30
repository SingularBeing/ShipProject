using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

    public float m_SecondsUntilReset = 5;
    private RectTransform _me;

    void Start()
    {
        _me = GetComponent<RectTransform>();
        StartCoroutine(WaitToReset());
    }

	void Update()
    {
        _me.position += new Vector3(0, 0.5f, 0);
    }

    IEnumerator WaitToReset()
    {
        yield return new WaitForSeconds(m_SecondsUntilReset);
        _me.localPosition = new Vector3(0, -200, 0);
        StartCoroutine(WaitToReset());
    }

}
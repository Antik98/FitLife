using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroy : MonoBehaviour
{
    private Renderer m_renderer;

	void Start() {
		m_renderer = GetComponent<Renderer>();
	}
    void Update()
    {
        if (!m_renderer.isVisible)
			Destroy(gameObject);
    }
}

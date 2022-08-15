using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaleManager : MonoBehaviour
{
    private Camera m_mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_mainCamera = Camera.main;
        transform.parent = m_mainCamera.transform;
        transform.localPosition = Vector3.forward * 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        const float ratio = 1920f / 1080f;
        float width = Screen.width;
        float height = Screen.height;
        float screenRatio = width / height;

        Vector2 withInWorld = m_mainCamera.ScreenToWorldPoint(new Vector2(width, height)) - m_mainCamera.ScreenToWorldPoint(Vector3.zero);
        float size = 1f;
        if (screenRatio > ratio)
        {
            size = withInWorld.x / (19.2f);
        }
        else
        {
            size = withInWorld.y / (10.8f);
            
        }
        transform.localScale = Vector3.one * size;
    }
}

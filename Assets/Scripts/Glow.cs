using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    [SerializeField] GameObject glowPoint;
    [SerializeField] float glowTime = 1f;

    bool isGlowing = false;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(glowPoint == null)
        {
            Debug.Log("Missing glowPoint on: " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGlowing)
        {
            timer += Time.deltaTime;
            if(timer >= glowTime)
            {
                GlowOff();
                timer = 0f;
            }
        }
    }

    public void GlowOn()
    {
        glowPoint.SetActive(true);
        isGlowing = true;
    }

    public void GlowOff()
    {
        glowPoint.SetActive(false);
        isGlowing = false;
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneControlScript : MonoBehaviour
{
    enum ZoneOwner
    {
        None, Player, Enemy
    }
    [SerializeField] GameObject BeamEffect;
    [SerializeField] float minScalingFactor = 1f;
    [SerializeField] float maxScalingFactor = 15f;
    Vector3 originalScale;
    [SerializeField] ParticleSystem aura;


    [SerializeField] ZoneOwner zoneOwner = ZoneOwner.None;

    [SerializeField] float playerControlPtPerSec = 1f;
    [SerializeField] float enemyControlPtPerSec = 1f;
    private float controlValue = 0;

    float timer = 0f;
    float timeBtwnChecks = 1f;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = new Vector3(1f, 1f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBtwnChecks)
        {
            CheckControlStatus();
            timer = 0f;
        }

        ResizeControlBeamEffects();
    }

    private void ResizeControlBeamEffects()
    {
        if (zoneOwner != ZoneOwner.Enemy)
        {
            BeamEffect.GetComponent<MeshRenderer>().enabled = true;
            float scaleFactor = minScalingFactor;
            scaleFactor = (controlValue / 10) * maxScalingFactor;
            BeamEffect.transform.localScale = scaleFactor * originalScale;

            ParticleSystem.ShapeModule shape = aura.shape;
            shape.radius = scaleFactor;
            if (!aura.isPlaying)
            {
                aura.Play();
            }
        }
        else
        {
            BeamEffect.GetComponent<MeshRenderer>().enabled = false;
            BeamEffect.transform.localScale = minScalingFactor * originalScale;

            aura.Stop();
        }

    }

    private void CheckControlStatus()
    {
        if (controlValue >= 10)
        {
            zoneOwner = ZoneOwner.Player;
        }
        else if (controlValue < 10 && controlValue > -10)
        {
            zoneOwner = ZoneOwner.None;
        }
        else if (controlValue <= -10)
        {
            zoneOwner = ZoneOwner.Enemy;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;
        if (timer >= timeBtwnChecks)
        {
            timer = 0f;
        }
        else
        {
            return;
        }


        if (other.gameObject.tag == "Player")
            {
                //Increase Points
                controlValue += playerControlPtPerSec;
                if (controlValue > 10)
                {
                    controlValue = 10;
                }
            }

            if (other.gameObject.tag == "Enemy")
            {
                //Decrease Points
                controlValue -= enemyControlPtPerSec;
                if (controlValue < -10)
                {
                    controlValue = -10;
                }
            }

           
    }
}

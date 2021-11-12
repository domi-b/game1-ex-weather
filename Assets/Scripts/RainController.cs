using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private WindZone windZone;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        windZone = GetComponentInChildren<WindZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var emission = particleSystem.emission;
            emission.rateOverTimeMultiplier = 50;

            windZone.windMain = 3;
        }
    }
}

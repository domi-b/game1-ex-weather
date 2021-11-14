using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    public float windStrength
    {
        get => windZone.windMain;
        set => windZone.windMain = value;
    }
    public float rainStrength
    {
        get => particleSystem.emission.rateOverTimeMultiplier;
        set {
            var emission = particleSystem.emission;
            emission.rateOverTimeMultiplier = value;
        }
    }

    [SerializeField]
    private GameObject waterLevel;

    private new ParticleSystem particleSystem;
    private WindZone windZone;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        windZone = GetComponentInChildren<WindZone>();

        if (waterLevel != null)
        {
            StartCoroutine(AnimateWeather());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rainStrength = 500;
            windStrength = 5;
        }
        else
        {
            rainStrength = 100;
            windStrength = 1;
        }
    }

    private IEnumerator AnimateWeather()
    {
        yield return new WaitForSeconds(1);
        const int steps = 50;
        for (var i = 0; i < steps; i++)
        {
            waterLevel.transform.position += Vector3.up * 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

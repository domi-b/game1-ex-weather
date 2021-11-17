using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    private LerpParam windStrength;
    private LerpParam rainStrength;
    private LerpParam waterLevel;

    [SerializeField]
    private new Renderer renderer;
    [SerializeField]
    private float lerpDuration = 2;
    [SerializeField]
    private float startRainStrength = 100;
    [SerializeField]
    private float startWindStrength = 1;
    [SerializeField]
    private float startWaterLevel = 0.4f;

    private new ParticleSystem particleSystem;
    private WindZone windZone;
    private int smoothnessMinClampParam;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        windZone = GetComponentInChildren<WindZone>();

        smoothnessMinClampParam = Shader.PropertyToID("SmoothnessMinClamp");

        windStrength = new LerpParam(value => windZone.windMain = value, lerpDuration, startWindStrength);
        rainStrength = new LerpParam(value => {
            var emission = particleSystem.emission;
            emission.rateOverTimeMultiplier = value;
        }, lerpDuration, startRainStrength);
        waterLevel = new LerpParam(value => renderer.material.SetFloat(smoothnessMinClampParam, value), lerpDuration, startWaterLevel);

        StartCoroutine(WeatherCycle());
    }

    // Update is called once per frame
    void Update()
    {
        windStrength.Update();
        rainStrength.Update();
        waterLevel.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 5;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
    }

    private IEnumerator WeatherCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            windStrength.SetTarget(5 * startWindStrength);
            yield return new WaitForSeconds(5);
            rainStrength.SetTarget(5 * startRainStrength);
            waterLevel.SetTarget(0.2f + startWaterLevel);

            yield return new WaitForSeconds(10);
            windStrength.SetTarget(startWindStrength);
            yield return new WaitForSeconds(5);
            rainStrength.SetTarget(startRainStrength);
            waterLevel.SetTarget(startWaterLevel);
        }
    }
}

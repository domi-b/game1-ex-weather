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
    private float _waterLevel;
    public float waterLevel
    {
        get => _waterLevel;
        set
        {
            renderer.material.SetFloat(smoothnessMinClampParam, value);
            _waterLevel = value;
        }
    }

    [SerializeField]
    private new Renderer renderer;
    [SerializeField]
    private float lerpDuration = 2;

    private new ParticleSystem particleSystem;
    private WindZone windZone;
    private int smoothnessMinClampParam;

    private float lerpStart;
    private float startRainStrength = 500;
    private float startWindStrength = 5;
    private float startWaterLevel = 0.6f;
    private float targetRainStrength = 500;
    private float targetWindStrength = 5;
    private float targetWaterLevel = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        windZone = GetComponentInChildren<WindZone>();

        smoothnessMinClampParam = Shader.PropertyToID("SmoothnessMinClamp");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetRainStrength = 500;
            targetWindStrength = 5;
            targetWaterLevel = 0.6f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            targetRainStrength = 100;
            targetWindStrength = 1;
            targetWaterLevel = 0.4f;
        }
        else
        {
            var t = (Time.time - lerpStart) / lerpDuration;
            rainStrength = Mathf.Lerp(startRainStrength, targetRainStrength, t);
            windStrength = Mathf.Lerp(startWindStrength, targetWindStrength, t);
            waterLevel = Mathf.Lerp(startWaterLevel, targetWaterLevel, t);
            return;
        }

        lerpStart = Time.time;
        startRainStrength = rainStrength;
        startWindStrength = windStrength;
        startWaterLevel = waterLevel;
    }
}

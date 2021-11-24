using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using static LerpParam;

public class RainController : MonoBehaviour
{
    public const float lerpDuration = 2;

    public float weakerRain = 0.33f;

    public LerpParam windStrength = new LerpParam(0.25f, 5, lerpDuration / 2);
    public LerpParam rainStrength = new LerpParam(0, 2000, lerpDuration);
    public LerpParam waterLevel = new LerpParam(0.4f, 0.6f, lerpDuration * 4);
    public LerpParam saturation = new LerpParam(0, -25, lerpDuration * 2);
    public LerpParam fogStrength = new LerpParam(500, 50, lerpDuration * 2);

    [SerializeField]
    private Volume volume;
    [SerializeField]
    private Material floorMaterial;

    private new ParticleSystem particleSystem;
    private WindZone windZone;
    private ColorAdjustments colorAdjustments;
    private Fog fog;
    private int smoothnessMinClampParam;

    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        windZone = GetComponentInChildren<WindZone>();

        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out fog);

        smoothnessMinClampParam = Shader.PropertyToID("SmoothnessMinClamp");

        StartCoroutine(WeatherCycle());
    }

    private void Update()
    {
        windZone.windMain = windStrength.GetValue();

        var emission = particleSystem.emission;
        emission.rateOverTimeMultiplier = rainStrength.GetValue();
        var mappedRotation =  Mathf.Lerp(0, -40, Mathf.InverseLerp(windStrength.start, windStrength.end, windZone.windMain));
        var rotation = particleSystem.rotationOverLifetime;
        rotation.z = mappedRotation * Mathf.Deg2Rad;

        floorMaterial.SetFloat(smoothnessMinClampParam, waterLevel.GetValue());
        colorAdjustments.saturation.value = saturation.GetValue();
        fog.meanFreePath.value = fogStrength.GetValue();
        
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
            yield return new WaitForSeconds(5);
            yield return SwitchToRain();

            yield return new WaitForSeconds(15);
            yield return SwitchToSunshine();

            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator SwitchToSunshine()
    {
        Debug.Log("Most effects off + Rain lower");
        windStrength.SetTarget(TargetMode.Start);
        saturation.SetTarget(TargetMode.Start);
        fogStrength.SetTarget(TargetMode.Start);
        SetWeakerRain();

        yield return new WaitForSeconds(3);
        Debug.Log("Rain off");
        rainStrength.SetTarget(TargetMode.Start);

        yield return new WaitForSeconds(3);
        Debug.Log("Waterlevel off");
        waterLevel.SetTarget(TargetMode.Start);
    }

    private IEnumerator SwitchToRain()
    {
        Debug.Log("Wind on");
        windStrength.SetTarget(TargetMode.End);

        yield return new WaitForSeconds(3);
        Debug.Log("Fog + Rain on");
        saturation.SetTarget(TargetMode.End);
        fogStrength.SetTarget(TargetMode.End);
        SetWeakerRain();

        yield return new WaitForSeconds(3);
        Debug.Log("Waterlevel + strong rain");
        rainStrength.SetTarget(TargetMode.End);
        waterLevel.SetTarget(TargetMode.End);
    }

    private void SetWeakerRain()
    {
        rainStrength.SetTarget(Mathf.Lerp(rainStrength.start, rainStrength.end, weakerRain));
    }
}

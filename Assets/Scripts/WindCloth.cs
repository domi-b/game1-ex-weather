using UnityEngine;

public class WindCloth : MonoBehaviour
{
    [SerializeField]
    private Cloth[] clothes;
    private WindZone windZone;

    [SerializeField]
    private float windMultiplier = 10;

    private void Start()
    {
        windZone = GetComponentInChildren<WindZone>();
    }

    private void Update()
    {
        var windDirection = windZone.transform.rotation * Vector3.forward;
        var wind = windMultiplier * windZone.windMain * StrengthVariation() * windDirection.normalized;

        foreach (var cloth in clothes)
        {
            cloth.externalAcceleration = wind;
        }
    }

    private static float StrengthVariation()
    {
        return Mathf.Abs(Mathf.Sin(Time.time * 2)) / 8f + 0.875f;
    }
}

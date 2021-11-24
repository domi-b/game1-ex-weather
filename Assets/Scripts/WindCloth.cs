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
        var wind = windMultiplier * windZone.windMain * windDirection.normalized;

        foreach (var cloth in clothes)
        {
            cloth.externalAcceleration = wind;
        }
    }
}

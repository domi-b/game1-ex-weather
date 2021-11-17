using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCloth : MonoBehaviour
{
    [SerializeField]
    private Cloth[] clothes;
    private WindZone windZone;

    /*private Vector3 TargetWindForce;

    public Vector3 MaxWindForce;
    public float WindForceIntervals = 1;
    public Vector3 NegativeWindForce;*/

    private void Start()
    {
        windZone = GetComponentInChildren<WindZone>();
    }

    private void Update()
    {
        var wind = (windZone.transform.rotation * Vector3.forward).normalized * 10 * windZone.windMain;
        Debug.Log(wind);

        //https://forum.unity.com/threads/cloth-with-wind-and-physics.321444/
        foreach (var cloth in clothes)
        {
            cloth.externalAcceleration = wind;
            /*cloth.externalAcceleration = Vector3.MoveTowards(cloth.externalAcceleration, TargetWindForce, WindForceIntervals * Time.deltaTime);

            if (cloth.externalAcceleration == MaxWindForce)
            {
                TargetWindForce = NegativeWindForce;
            }
            if (cloth.externalAcceleration == NegativeWindForce)
            {
                TargetWindForce = MaxWindForce;
            }*/
        }
    }
}

using System;
using UnityEngine;

public sealed class LerpParam
{
    private float _current;
    public float Current
    {
        get => _current;
        set
        {
            _current = value;
            onUpdate(value);
        }
    }

    private float start;
    private float target;
    private float lerpStart;
    private readonly float lerpDuration;
    private readonly Action<float> onUpdate;

    public LerpParam(Action<float> onUpdate, float lerpDuration, float current)
    {
        this.onUpdate = onUpdate;
        this.lerpDuration = lerpDuration;
        Current = current;
    }

    public void SetTarget(float target)
    {
        this.target = target;
        start = Current;
        lerpStart = Time.time;
    }

    public void Update()
    {
        var t = (Time.time - lerpStart) / lerpDuration;
        Current = Mathf.Lerp(start, target, t);
    }
}

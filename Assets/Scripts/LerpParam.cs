using System;
using UnityEngine;

[Serializable]
public sealed class LerpParam
{
    public enum TargetMode
    {
        Start,
        End
    }

    public float start;
    public float end;
    public float lerpDuration;

    private float value;
    private float lerpStart;
    private float lerpTarget;
    private float lerpStartTime;

    public LerpParam(float start, float end, float lerpDuration)
    {
        this.start = start;
        this.end = end;
        this.lerpDuration = lerpDuration;
        value = start;
        lerpStart = start;
        lerpTarget = start;
    }

    public void SetTarget(TargetMode targetMode)
    {
        var target = targetMode switch
        {
            TargetMode.Start => start,
            TargetMode.End => end,
            _ => throw new ArgumentException($"Target mode {targetMode} is invalid")
        };
        SetTarget(target);
    }

    public void SetTarget(float target)
    {
        lerpTarget = target;
        lerpStart = value;
        lerpStartTime = Time.time;
    }

    public float GetValue()
    {
        var t = (Time.time - lerpStartTime) / lerpDuration;
        value = Mathf.Lerp(lerpStart, lerpTarget, t);
        return value;
    }
}

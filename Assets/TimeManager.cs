using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using UnityEngine;


public class TimeManager : MonoSingleton<TimeManager>
{
    public AnimationCurve _lagTimeCurve;

    Coroutine _lagCoroutine;

    public bool canChangeTime = true;

    public void Init()
    {
        SetTime(1);
    }

    public void DoLagTime(float intensity = .2f, float timeBeforeNormalTime = .1f)
    {
        if (canChangeTime == false) return;

        if (_lagCoroutine != null)
        {
            StopCoroutine(_lagCoroutine);
            _lagCoroutine = null;
        }

        _lagCoroutine = StartCoroutine(LagCoroutine(intensity, timeBeforeNormalTime));
    }

    IEnumerator LagCoroutine(float intensity, float timeBeforeReset)
    {
        SetTime(intensity);

        yield return new WaitForSecondsRealtime(timeBeforeReset);

        SetTime(1);
    }

    public void SetTime(float intensity = .2f)
    {
        Time.timeScale = intensity;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}

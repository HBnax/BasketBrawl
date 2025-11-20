using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] float matchDuration = 60f;
    [SerializeField] private bool useUnscaledTime;

    private float remainingTime;
    private bool isTimerRunning;

    public event System.Action<float> OnTick;
    public event System.Action OnTimerEnd;

    Coroutine timerCoroutine;
    private void Awake()
    {
        remainingTime = matchDuration;
        OnTick?.Invoke(remainingTime);
    }

    public void StartTimer()
    {
        Debug.Log("TimerStarted");
        StopTimer();
        remainingTime = matchDuration;
        isTimerRunning = true;
        timerCoroutine = StartCoroutine(TimerLoop());
        OnTick?.Invoke(remainingTime);
    }

    public void ResetTimer(float newDuration)
    {
        if (newDuration > 0f)
        {
            matchDuration = newDuration;
        }
        StopTimer();
        remainingTime = matchDuration;
        OnTick?.Invoke(remainingTime);
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
        if (timerCoroutine == null)
        {
            timerCoroutine = StartCoroutine(TimerLoop());
        }
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    IEnumerator TimerLoop()
    {
        while (remainingTime >= 0f)
        {
            yield return new WaitForSeconds(1f);
            if (isTimerRunning)
            {
                
                remainingTime = Mathf.Max(0f, remainingTime - 1f);
                
                if (remainingTime <= 0f)
                {
                    remainingTime = 0f;
                    OnTimerEnd?.Invoke();
                    isTimerRunning = false;
                    timerCoroutine = null;
                    yield break;
                }
                OnTick?.Invoke(remainingTime);
                Debug.Log("Remaining Time: " + remainingTime);
            }
            yield return null;
        }
        timerCoroutine = null;
    }
}

using UnityEngine;
using UnityEngine.UI;
public class TimerHUD : MonoBehaviour
{
    public TimerController timer;
    public Text timerText;

    private void OnEnable()
    {
        if (!timer) return;
        timer.OnTick += HandleTick;
        HandleTick(timer.GetRemainingTime());
    }

    private void onDisable()
    {
        if (timer)
        {
            timer.OnTick -= HandleTick;
        }
    }

    private void HandleTick(float remainingTime)
    {
        timerText.text = remainingTime.ToString();
    }
}

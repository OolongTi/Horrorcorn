

using UnityEngine;

public class TimeManager
{
    private static BoolStack pauseStack = new BoolStack();
    
    private static float timeScale = 1f;

    public static void SetPause(GameObject obj)
    {
        pauseStack.Add(obj);
        UpdateTimeScale();
    }

    public static void RemovePause(GameObject obj)
    {
        pauseStack.Remove(obj);
        UpdateTimeScale();
    }

    private static void UpdateTimeScale()
    {
        if (pauseStack.IsSet)
        {
            Time.timeScale = 0f;
            Debug.Log(Time.timeScale);
        }
        else
        {
            Time.timeScale = timeScale;
            Debug.Log(Time.timeScale);
        }
    }
}

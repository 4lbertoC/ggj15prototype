using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{
    private float nextTransition = 0;
    private Quaternion from;
    private Quaternion to;
    private int rotationDegrees = 0;

    void Awake()
    {
        GameState.GetInstance().OnNewStandoff += NextPhase;
        from = Quaternion.Euler(0, rotationDegrees, 0);
        transform.rotation = from;

    }

    public void NextPhase()
    {
        from = this.gameObject.transform.rotation;
        rotationDegrees += 90;
        rotationDegrees %= 360;
        to = Quaternion.Euler(0, rotationDegrees, 0);
        nextTransition = 0.5f;
    }

    void Update()
    {
        if (nextTransition > 0)
        {
            nextTransition -= Time.deltaTime;
            transform.rotation = Quaternion.Lerp(from, to, (0.2f - nextTransition) / 0.2f);
        }
        else
        {
            transform.rotation = to;
        }
    }
}

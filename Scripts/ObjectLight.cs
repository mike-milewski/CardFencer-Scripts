using System.Collections;
using UnityEngine;

public class ObjectLight : MonoBehaviour
{
    [SerializeField]
    private Light pointLight;

    [SerializeField]
    private float lightRange, speed;

    private Coroutine enableLightRoutine, disableLightRoutine;

    public Light PointLight
    {
        get
        {
            return pointLight;
        }
        set
        {
            pointLight = value;
        }
    }

    public void EnableLight()
    {
        pointLight.enabled = true;

        enableLightRoutine = null;

        enableLightRoutine = StartCoroutine("TurnOnLight");
    }

    public void DisableLight()
    {
        disableLightRoutine = null;

        disableLightRoutine = StartCoroutine("TurnOffLight");
    }

    private IEnumerator TurnOnLight()
    {
        float t = 0;

        while(t < 1f)
        {
            t += Time.deltaTime;

            if(pointLight.range < lightRange)
            {
                pointLight.range += 5 * Time.deltaTime;
            }
            yield return null;
        }
        pointLight.range = lightRange;
    }

    private IEnumerator TurnOffLight()
    {
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;

            pointLight.range -= speed * Time.deltaTime;

            yield return null;
        }
        pointLight.range = 0;

        pointLight.enabled = false;
    }

    public void StopLightOnRoutine()
    {
        if(enableLightRoutine != null)
        {
            StopCoroutine(enableLightRoutine);

            enableLightRoutine = null;

            pointLight.enabled = true;

            pointLight.range = lightRange;
        }
    }

    public void StopLightOffRoutine()
    {
        if(disableLightRoutine != null)
        {
            StopCoroutine(disableLightRoutine);

            disableLightRoutine = null;

            pointLight.range = 0;

            pointLight.enabled = false;
        }
    }
}
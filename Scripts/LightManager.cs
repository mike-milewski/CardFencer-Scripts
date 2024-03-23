using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume currentVolume;

    [SerializeField]
    private PostProcessProfile defaultProfile, nightProfile;

    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    [SerializeField]
    private Light directionalLight;

    [SerializeField]
    private Light[] environmentLights;

    [SerializeField]
    private ParticleSystem[] fireFlyLights;

    [SerializeField]
    private float dayBloomIntensity, dayColorGradingTemperature, dayColorGradingSaturation, dayColorGradingContrast, dayRedValue;

    [SerializeField]
    private Color nightTimeDirectionalLightColor;

    private Coroutine dayRoutine, nightRoutine, enableLightsRoutine, disableLightsRoutine, ambienceRoutine;

    [SerializeField]
    private bool isDay, isNight;

    public bool IsDay => isDay;

    public bool IsNight => isNight;

    private void Awake()
    {
        SetDefaultDayProfile();
    }

    public void DayProfile()
    {
        if(dayRoutine != null)
        {
            StopCoroutine(dayRoutine);
        }
        if(ambienceRoutine != null)
        {
            StopCoroutine(ambienceRoutine);
        }

        dayRoutine = null;
        ambienceRoutine = null;

        dayRoutine = StartCoroutine("ChangeNightToDay");
        ambienceRoutine = StartCoroutine(AmbienceAudio(false));

        EndLights();
    }

    public void NightProfile()
    {
        if(nightRoutine != null)
        {
            StopCoroutine(nightRoutine);
        }
        if(ambienceRoutine != null)
        {
            StopCoroutine(ambienceRoutine);
        }

        nightRoutine = null;
        ambienceRoutine = null;

        nightRoutine = StartCoroutine("ChangeDayToNight");

        Scene scene = SceneManager.GetActiveScene();

        if(scene.name.Contains("Forest"))
        {
            ambienceRoutine = StartCoroutine(AmbienceAudio(true));
        }
        else
        {
            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.AmbientForeground.Stop();
        }

        StartLights();
    }

    private void StartLights()
    {
        for(int i = 0; i < fireFlyLights.Length; i++)
        {
            fireFlyLights[i].gameObject.SetActive(true);

            fireFlyLights[i].GetComponent<ObjectLight>().EnableLight();

            fireFlyLights[i].Play();
        }

        if(enableLightsRoutine != null)
        {
            StopCoroutine(enableLightsRoutine);
        }

        enableLightsRoutine = null;

        enableLightsRoutine = StartCoroutine("EnableLights");
    }

    private void EndLights()
    {
        for (int i = 0; i < fireFlyLights.Length; i++)
        {
            fireFlyLights[i].GetComponent<ObjectLight>().DisableLight();
        }

        if(disableLightsRoutine != null)
        {
            StopCoroutine(disableLightsRoutine);
        }

        disableLightsRoutine = null;

        disableLightsRoutine = StartCoroutine("DisableLights");
    }

    private void SetDefaultDayProfile()
    {
        if(worldEnvironmentData.changedDay)
        {
            isNight = true;
            isDay = false;

            SetNightTime();
        }
        else
        {
            isDay = true;
            isNight = false;

            SetDayTime();
        }
    }

    private void SetNightTime()
    {
        directionalLight.color = nightTimeDirectionalLightColor;

        Scene scene = SceneManager.GetActiveScene();

        if(scene.name.Contains("Forest"))
        {
            AudioManager.instance.PlayAmbient(AudioManager.instance.RainAudio);
            AudioManager.instance.PlayAmbientForeground(AudioManager.instance.NightForestAudio);
        }
        else
        {
            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.AmbientForeground.Stop();
        }

        for (int i = 0; i < fireFlyLights.Length; i++)
        {
            fireFlyLights[i].gameObject.SetActive(true);

            fireFlyLights[i].GetComponent<ObjectLight>().PointLight.enabled = true;
            fireFlyLights[i].GetComponent<ObjectLight>().PointLight.range = 2;
        }

        for(int i = 0; i < environmentLights.Length; i++)
        {
            environmentLights[i].enabled = true;
            environmentLights[i].range = 4;
        }
    }

    private void SetDayTime()
    {
        for (int i = 0; i < fireFlyLights.Length; i++)
        {
            fireFlyLights[i].GetComponent<ObjectLight>().PointLight.range = 0;
            fireFlyLights[i].GetComponent<ObjectLight>().PointLight.enabled = false;

            fireFlyLights[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < environmentLights.Length; i++)
        {
            environmentLights[i].range = 0;
            environmentLights[i].enabled = false;
        }
        directionalLight.color = Color.white;

        AudioManager.instance.Ambient.volume = 0;
        AudioManager.instance.Ambient.Stop();

        AudioManager.instance.AmbientForeground.volume = 0;
        AudioManager.instance.AmbientForeground.Stop();
    }

    private IEnumerator ChangeNightToDay()
    {
        float t = 0;

        isDay = true;
        isNight = false;

        if (PlayerPrefs.HasKey("BackgroundAudio"))
        {
            if (PlayerPrefs.GetFloat("BackgroundAudio") > 0)
            {
                while (t < 1f)
                {
                    t += Time.deltaTime;

                    directionalLight.color = Color.Lerp(nightTimeDirectionalLightColor, Color.white, t);

                    if(AudioManager.instance.BackgroundMusic.volume < PlayerPrefs.GetFloat("BackgroundAudio"))
                    {
                        AudioManager.instance.BackgroundMusic.volume += Time.deltaTime;
                    }

                    yield return null;
                }

                AudioManager.instance.BackgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundAudio");
            }
            else
            {
                AudioManager.instance.BackgroundMusic.volume = 0;

                while (t < 1f)
                {
                    t += Time.deltaTime;

                    directionalLight.color = Color.Lerp(nightTimeDirectionalLightColor, Color.white, t);

                    yield return null;
                }
            }
        }
        else
        {
            while (t < 1f)
            {
                t += Time.deltaTime;

                directionalLight.color = Color.Lerp(nightTimeDirectionalLightColor, Color.white, t);

                AudioManager.instance.BackgroundMusic.volume += Time.deltaTime;

                yield return null;
            }
        }

        if (PlayerPrefs.HasKey("BackgroundAudio"))
        {
            AudioManager.instance.BackgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundAudio");
        }
        else
        {
            AudioManager.instance.BackgroundMusic.volume = 1;
        }

        MenuController.instance.MenuPlayerLights.SetActive(false);
    }

    private IEnumerator ChangeDayToNight()
    {
        float t = 0;

        isNight = true;
        isDay = false;

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name.Contains("ForestField"))
        {
            if (PlayerPrefs.HasKey("BackgroundAudio"))
            {
                if (PlayerPrefs.GetFloat("BackgroundAudio") > 0.15f)
                {
                    while (t < 1f)
                    {
                        t += Time.deltaTime;

                        directionalLight.color = Color.Lerp(Color.white, nightTimeDirectionalLightColor, t);

                        if (AudioManager.instance.BackgroundMusic.volume > 0.15f)
                        {
                            AudioManager.instance.BackgroundMusic.volume -= Time.deltaTime;
                        }

                        yield return null;
                    }
                }
                else
                {
                    AudioManager.instance.BackgroundMusic.volume = 0;

                    while (t < 1f)
                    {
                        t += Time.deltaTime;

                        directionalLight.color = Color.Lerp(Color.white, nightTimeDirectionalLightColor, t);

                        yield return null;
                    }
                }
            }
            else
            {
                while (t < 1f)
                {
                    t += Time.deltaTime;

                    directionalLight.color = Color.Lerp(Color.white, nightTimeDirectionalLightColor, t);

                    if (AudioManager.instance.BackgroundMusic.volume > 0.15f)
                    {
                        AudioManager.instance.BackgroundMusic.volume -= Time.deltaTime;
                    }

                    yield return null;
                }
            }

            if (PlayerPrefs.HasKey("BackgroundAudio"))
            {
                if (PlayerPrefs.GetFloat("BackgroundAudio") > 0.15f)
                {
                    AudioManager.instance.BackgroundMusic.volume = 0.15f;
                }
            }
            else
            {
                AudioManager.instance.BackgroundMusic.volume = 0.15f;
            }
        }
        else
        {
            while (t < 1f)
            {
                t += Time.deltaTime;

                directionalLight.color = Color.Lerp(Color.white, nightTimeDirectionalLightColor, t);

                yield return null;
            }
        }

        MenuController.instance.MenuPlayerLights.SetActive(true);
    }

    private IEnumerator AmbienceAudio(bool increase)
    {
        float t = 0;

        if (increase)
        {
            if (PlayerPrefs.HasKey("BackgroundAudio"))
            {
                if (PlayerPrefs.GetFloat("BackgroundAudio") > 0)
                {
                    AudioManager.instance.PlayAmbientForeground(AudioManager.instance.NightForestAudio);

                    while (t < 1f)
                    {
                        t += Time.deltaTime;

                        AudioManager.instance.AmbientForeground.volume += Time.deltaTime;

                        yield return null;
                    }
                    AudioManager.instance.PlayAmbientForeground(AudioManager.instance.NightForestAudio);
                }
                else
                {
                    AudioManager.instance.AmbientForeground.volume = 0;
                    AudioManager.instance.AmbientForeground.Stop();
                }
            }
            else
            {
                AudioManager.instance.PlayAmbientForeground(AudioManager.instance.NightForestAudio);

                while (t < 1f)
                {
                    t += Time.deltaTime;

                    AudioManager.instance.AmbientForeground.volume += Time.deltaTime;

                    yield return null;
                }
                AudioManager.instance.PlayAmbientForeground(AudioManager.instance.NightForestAudio);
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("BackgroundAudio"))
            {
                if (PlayerPrefs.GetFloat("BackgroundAudio") > 0)
                {
                    while (t < 1f)
                    {
                        t += Time.deltaTime;

                        AudioManager.instance.AmbientForeground.volume -= Time.deltaTime;

                        yield return null;
                    }
                    AudioManager.instance.AmbientForeground.volume = 0;
                    AudioManager.instance.AmbientForeground.Stop();
                }
                else
                {
                    AudioManager.instance.AmbientForeground.volume = 0;
                    AudioManager.instance.AmbientForeground.Stop();
                }
            }
            else
            {
                while (t < 1f)
                {
                    t += Time.deltaTime;

                    AudioManager.instance.AmbientForeground.volume -= Time.deltaTime;

                    yield return null;
                }
                AudioManager.instance.AmbientForeground.volume = 0;
                AudioManager.instance.AmbientForeground.Stop();
            }
        }
    }

    private void EnableLights()
    {
        foreach (Light lit in environmentLights)
        {
            lit.GetComponent<ObjectLight>().EnableLight();
        }
    }

    private void DisableLights()
    {
        foreach(Light lit in environmentLights)
        {
            lit.GetComponent<ObjectLight>().DisableLight();
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeatherManager : MonoBehaviour
{
    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    [SerializeField]
    private ParticleSystem weatherParticle;

    private Coroutine weatherRoutine;

    public ParticleSystem WeatherParticle => weatherParticle;

    private void Start()
    {
        if(weatherParticle != null)
        {
            if (worldEnvironmentData.changedWeather)
            {
                weatherParticle.gameObject.SetActive(true);

                weatherParticle.Play();
            }
            else
            {
                weatherParticle.Stop();
            }
        }
    }

    public void ChangeWeather(bool toggle, bool loopParticle)
    {
        if(weatherParticle != null)
        {
            ParticleSystem.MainModule main = weatherParticle.main;

            main.loop = loopParticle;

            Scene scene = SceneManager.GetActiveScene();

            StartWeatherAudio(false);

            if (toggle)
            {
                weatherParticle.Play();

                if(scene.name.Contains("Forest"))
                   StartWeatherAudio(true);
            }
            else
            {
                weatherParticle.Stop();
            }
        }
    }

    private void StartWeatherAudio(bool increase)
    {
        weatherRoutine = null;

        weatherRoutine = StartCoroutine(WeatherAudio(increase));
    }

    private IEnumerator WeatherAudio(bool increase)
    {
        float t = 0;

        if(increase)
        {
            if (PlayerPrefs.HasKey("BackgroundAudio"))
            {
                if (PlayerPrefs.GetFloat("BackgroundAudio") > 0)
                {
                    AudioManager.instance.PlayAmbient(AudioManager.instance.RainAudio);

                    while (t < 1)
                    {
                        t += Time.deltaTime;

                        AudioManager.instance.Ambient.volume += 5 * Time.deltaTime;

                        yield return null;
                    }
                    AudioManager.instance.Ambient.volume = PlayerPrefs.GetFloat("BackgroundAudio");
                }
                else
                {
                    AudioManager.instance.Ambient.volume = 0;
                }
            }
            else
            {
                AudioManager.instance.PlayAmbient(AudioManager.instance.RainAudio);

                while (t < 1)
                {
                    t += Time.deltaTime;

                    AudioManager.instance.Ambient.volume += 5 * Time.deltaTime;

                    yield return null;
                }
                AudioManager.instance.Ambient.volume = 1;
            }
        }
        else
        {
            while (t < 1)
            {
                t += Time.deltaTime;

                AudioManager.instance.Ambient.volume -= Time.deltaTime;

                yield return null;
            }
            AudioManager.instance.Ambient.volume = 0;
        }
    }
}
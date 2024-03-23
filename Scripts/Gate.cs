using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private Transform gateOne, gateTwo;

    [SerializeField]
    private float gateOneYRotation, gateTwoYRotation, openSpeed;

    public void OpenGatesRoutine()
    {
        StartCoroutine(OpenGates());
    }

    private IEnumerator OpenGates()
    {
        float t = 0f;

        Quaternion gateOneTargetRotation = Quaternion.Euler(new Vector3(0, gateOneYRotation, 0));
        Quaternion gateTwoTargetRotation = Quaternion.Euler(new Vector3(0, gateTwoYRotation, 0));

        CheckAudio();

        while(t < 2)
        {
            t += Time.unscaledDeltaTime;

            if(gateOne != null)
               gateOne.rotation = Quaternion.Slerp(gateOne.transform.rotation, gateOneTargetRotation, openSpeed * Time.unscaledDeltaTime);

            if(gateTwo != null)
               gateTwo.rotation = Quaternion.Slerp(gateTwo.transform.rotation, gateTwoTargetRotation, openSpeed * Time.unscaledDeltaTime);

            yield return null;
        }
    }

    private void CheckAudio()
    {
        if(gameObject.GetComponent<AudioSource>())
        {
            if (PlayerPrefs.HasKey("SoundEffects"))
            {
                gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            }
            else
            {
                gameObject.GetComponent<AudioSource>().volume = 1.0f;
            }

            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
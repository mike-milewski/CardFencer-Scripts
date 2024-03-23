using System.Collections;
using UnityEngine;

public class TempParticle : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("IncrementParticle");
    }

    public IEnumerator IncrementParticle()
    {
        float t = 0;
        
        while(t < 2.8f)
        {
            t += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        gameObject.SetActive(false);
    }
}
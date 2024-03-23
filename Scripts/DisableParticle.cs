using UnityEngine;

public class DisableParticle : MonoBehaviour
{
    [SerializeField]
    private float disableTime;

    private float defaultTime;

    private void OnEnable()
    {
        defaultTime = disableTime;
    }

    private void Update()
    {
        defaultTime -= Time.deltaTime;
        if(defaultTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
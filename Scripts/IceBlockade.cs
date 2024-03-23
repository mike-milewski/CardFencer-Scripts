using UnityEngine;

public class IceBlockade : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blockades;

    [SerializeField]
    private GameObject particleObject;

    public void RemoveBlockades()
    {
        for(int i = 0; i < blockades.Length; i++)
        {
            blockades[i].SetActive(false);
        }

        particleObject.SetActive(true);

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            particleObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            particleObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            particleObject.GetComponent<AudioSource>().Play();
        }
    }
}
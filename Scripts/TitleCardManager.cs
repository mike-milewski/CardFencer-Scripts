using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCardManager : MonoBehaviour
{
    [SerializeField]
    private List<TitleCard> titleCard = new List<TitleCard>();

    [SerializeField]
    private float timeToStartTitleSequence;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToStartTitleSequence);
        StartSequence();
    }

    public void StartSequence()
    {
        if(titleCard.Count > 0)
        {
            int random = Random.Range(0, titleCard.Count);

            titleCard[random].AnimateCard();

            titleCard.RemoveAt(random);
        }
    }
}
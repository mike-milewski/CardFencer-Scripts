using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankBonusBattleInformation : MonoBehaviour
{
    [SerializeField]
    private Image bonusImage;

    [SerializeField]
    private Sprite extendedCardSprite, rankCardSprite;

    [SerializeField]
    private TextMeshProUGUI bonusText;

    [SerializeField]
    private ParticleSystem bonusParticle;

    public void SetBonusInfo(Sprite sprite, string bonusInfo)
    {
        bonusImage.sprite = sprite;

        bonusText.text = bonusInfo;

        if(sprite == extendedCardSprite || sprite == rankCardSprite)
        {
            RectTransform rectTrans = bonusImage.GetComponent<RectTransform>();

            rectTrans.sizeDelta = new Vector2(22, 30);
        }
    }

    public void PlayParticle()
    {
        bonusParticle.gameObject.SetActive(true);

        bonusParticle.Play();
    }
}
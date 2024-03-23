using UnityEngine;
using UnityEngine.UI;

public class TitleCard : MonoBehaviour
{
    [SerializeField]
    private TitleCardManager titleCardManager;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Color color;

    [SerializeField]
    private GameObject textObject, nameTextObject, cardTextObject, cardPortrait;

    [SerializeField]
    private ParticleSystem cardGlow;

    [SerializeField]
    private Image cardImage, crystalImage, maskImage;

    [SerializeField]
    private Sprite frontCardSprite;

    [SerializeField]
    private Color[] cardGlowColors;

    [SerializeField]
    private Sprite[] crystalSprites, maskSprites;

    public void ChangeCard()
    {
        cardImage.sprite = frontCardSprite;

        cardImage.color = color;

        textObject.SetActive(true);
        nameTextObject.SetActive(true);
        cardTextObject.SetActive(true);
        cardPortrait.SetActive(true);
        crystalImage.gameObject.SetActive(true);
        maskImage.gameObject.SetActive(true);

        var startClr = cardGlow.main;
        startClr.startColor = cardGlowColors[Random.Range(0, cardGlowColors.Length)];

        crystalImage.sprite = crystalSprites[Random.Range(0, crystalSprites.Length)];
        maskImage.sprite = maskSprites[Random.Range(0, maskSprites.Length)];
    }

    public void EnableParticle()
    {
        cardGlow.gameObject.SetActive(true);
    }

    public void AnimateCard()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CardAudio);

        animator.Play("TitleCard");
    }

    public void AnimateAnotherCard()
    {
        titleCardManager.StartSequence();
    }
}
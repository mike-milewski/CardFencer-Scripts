using UnityEngine;
using TMPro;

public class StickerPowerHolder : MonoBehaviour
{
    public static StickerPowerHolder instance = null;

    [SerializeField]
    private GameObject stickerMessagePrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CreateStickerMessage(StickerPower stickerPower)
    {
        var message = Instantiate(stickerMessagePrefab);

        AudioSource audioSource = message.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }

        message.transform.SetParent(transform, false);

        string powerName = message.GetComponentInChildren<TextMeshProUGUI>().text;

        switch (stickerPower)
        {
            case (StickerPower.ResourcefulSpoils):
                powerName = "Resourceful Spoils";
                break;
            case (StickerPower.Charity):
                powerName = "Charity";
                break;
            case (StickerPower.Mulligan):
                powerName = "Mulligan";
                break;
            case (StickerPower.MiracleCounter):
                powerName = "Miracle Counter";
                break;
            case (StickerPower.MiracleGuard):
                powerName = "Miracle Guard";
                break;
            case (StickerPower.PerfectEscape):
                powerName = "Escape Artist";
                break;
            case (StickerPower.PoisionImmune):
                powerName = "Anti-Venom";
                break;
            case (StickerPower.BurnsImmune):
                powerName = "Sunscreen";
                break;
            case (StickerPower.AcornCharm):
                powerName = "Acorn Charm";
                break;
            case (StickerPower.CpHealing):
                powerName = "Rejuvenate";
                break;
            case (StickerPower.HpHealing):
                powerName = "Regenerate";
                break;
            case (StickerPower.HpSteal):
                powerName = "Vampire";
                break;
            case (StickerPower.Wraith):
                powerName = "Wraith";
                break;
            case (StickerPower.IncreaedBasicAttack):
                powerName = "Natural Strength";
                break;
            case (StickerPower.Enrage):
                powerName = "Berserker";
                break;
            case (StickerPower.ExtraExp):
                powerName = "Gold Crown";
                break;
            case (StickerPower.ExtraGold):
                powerName = "Money Bags";
                break;
            case (StickerPower.Thorns):
                powerName = "Spiked Body";
                break;
            case (StickerPower.Pierce):
                powerName = "Pierce";
                break;
            case (StickerPower.Tripwire):
                powerName = "Tripwire";
                break;
            case (StickerPower.StrengthUp):
                powerName = "Power";
                break;
            case (StickerPower.Minimalist):
                powerName = "Minimalist";
                break;
            case (StickerPower.NegativeBenefits):
                powerName = "Negative Benefits";
                break;
            case (StickerPower.ParalysisImmune):
                powerName = "Lightning Rod";
                break;
            case (StickerPower.Purity):
                powerName = "Purity";
                break;
            case (StickerPower.Impervious):
                powerName = "Impervious";
                break;
            case (StickerPower.MysticFavored):
                powerName = "Mystic Favored";
                break;
            case (StickerPower.SecondChance):
                powerName = "Second Chance";
                break;
            case (StickerPower.BlindRage):
                powerName = "Blind Rage";
                break;
            case (StickerPower.DeckedOut):
                powerName = "Decked Out";
                break;
            case (StickerPower.EvenGround):
                powerName = "Even Ground";
                break;
            case (StickerPower.Focus):
                powerName = "Focus";
                break;
            case (StickerPower.ThrillOfTheHunt):
                powerName = "Thrilling Hunt";
                break;
            case (StickerPower.ForcefulStrike):
                powerName = "Forceful Strike";
                break;
            case (StickerPower.Hunter):
                powerName = "Hunter";
                break;
            case (StickerPower.Paralyze):
                powerName = "Paralyze";
                break;
            case (StickerPower.CrocodileTears):
                powerName = "Crocodile Tears";
                break;
            case (StickerPower.PerfectTiming):
                powerName = "Perfect Timing";
                break;
            case (StickerPower.Envenom):
                powerName = "Envenom";
                break;
            case (StickerPower.Sunburn):
                powerName = "Sunburn";
                break;
            case (StickerPower.Maximalist):
                powerName = "Maximalist";
                break;
            case (StickerPower.DispellingSword):
                powerName = "Dispelling Sword";
                break;
            case (StickerPower.LuckyGuard):
                powerName = "Lucky Guard";
                break;
            case (StickerPower.ShadowForm):
                powerName = "Shadow Form";
                break;
            case (StickerPower.HelpingHand):
                powerName = "Helping Hand";
                break;
            case (StickerPower.ElectricStorm):
                powerName = "Electric Storm";
                break;
            case (StickerPower.PoisonGas):
                powerName = "Poison Gas";
                break;
            case (StickerPower.SolarFlare):
                powerName = "Solar Flare";
                break;
            case (StickerPower.CourageUnderFire):
                powerName = "Courage Under Fire";
                break;
        }
        message.GetComponentInChildren<TextMeshProUGUI>().text = powerName;
    }
}
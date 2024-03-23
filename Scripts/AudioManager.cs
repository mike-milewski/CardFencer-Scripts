using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField]
    private BackgroundMusic backGroundMusic;

    [SerializeField]
    private AudioSource backgroundMusic, soundEffects, ambient, ambientForeground;

    [SerializeField]
    private AudioClip mainMenuTheme, forestAudio, battleAudio, bossTheme, finalBossTheme, forestOverWorldTheme, victoryAudio, cardAudio, battleTransitionAudio, selectedAudio, fireAudio, basicSwordAudio, slashAudio, punchAudio, levelUpAudio, expGainAudio, applauseAudio, playerJumpAudio, 
                      playerJumpLandAudio, cardDissolveAudio, cursorAudio, combinedCardAudio, healingPickUpAudio, gameOverAudio, errorMessageAudio, gotItemAudio, equipAudio, treasureChestAudio, desertOverWorldTheme, desertAudio, arcticOverWorldTheme, arcticAudio,
                      graveyardOverWorldTheme, graveyardAudio, casteOverWorldTheme, castleAudio, buyItemAudio, itemAppearAudio, openMenuAudio, closeMenuAudio, fountainUpgradeAudio, forestTownAudio, rainAudio, nightForestAudio,
                      nightForestStageAudio, messagePopupAudio, hpHealAudio, cpHealAudio, counterAudio, guardAudio, paralysisAudio, poisonAudio, spiderKingBattleCryAudio, spiderKingRightLeg, spiderKingLeftLeg,
                      missedAttackAudio, desertTown, arcticTown, graveyardTown, castleTown, nightDesertAudio, nightArcticAudio, endingTheme;

    private Coroutine fadeOutBGMRoutine;

    public Coroutine FadeOutBGMRoutine
    {
        get
        {
            return fadeOutBGMRoutine;
        }
        set
        {
            fadeOutBGMRoutine = value;
        }
    }

    public BackgroundMusic _backGroundMusic => backGroundMusic;

    public AudioClip MainMenuTheme => mainMenuTheme;

    public AudioClip ForestOverworldTheme => forestOverWorldTheme;

    public AudioClip ForestAudio => forestAudio;

    public AudioClip DesertOverWorldTheme => desertOverWorldTheme;

    public AudioClip DesertAudio => desertAudio;

    public AudioClip ArcticOverWorldTheme => arcticOverWorldTheme;

    public AudioClip ArcticAudio => arcticAudio;

    public AudioClip GraveyardOverWorldTheme => graveyardOverWorldTheme;

    public AudioClip GraveyardAudio => graveyardAudio;

    public AudioClip CastleOverWorldTheme => casteOverWorldTheme;

    public AudioClip CastleAudio => castleAudio;

    public AudioClip BattleAudio => battleAudio;

    public AudioClip BossTheme => bossTheme;

    public AudioClip FinalBossTheme => finalBossTheme;

    public AudioClip EndingTheme => endingTheme;

    public AudioClip CardAudio => cardAudio;

    public AudioClip BattleTransitionAudio => battleTransitionAudio;

    public AudioClip SelectedAudio => selectedAudio;

    public AudioClip FireAudio => fireAudio;

    public AudioClip BasicSwordAudio => basicSwordAudio;

    public AudioClip SlashAudio => slashAudio;

    public AudioClip PunchAudio => punchAudio;

    public AudioClip LevelUpAudio => levelUpAudio;

    public AudioClip ExpGainAudioClip => expGainAudio;

    public AudioClip ApplauseAudio => applauseAudio;

    public AudioClip PlayerJumpAudio => playerJumpAudio;

    public AudioClip PlayerJumpLandAudio => playerJumpLandAudio;

    public AudioClip CardDissolveAudio => cardDissolveAudio;

    public AudioClip CursorAudio => cursorAudio;

    public AudioClip CombinedCardAudio => combinedCardAudio;

    public AudioClip HealingPickUpAudio => healingPickUpAudio;

    public AudioClip GameOverAudio => gameOverAudio;

    public AudioClip ErrorMessageAudio => errorMessageAudio;

    public AudioClip GotItemAudio => gotItemAudio;

    public AudioClip EquipAudio => equipAudio;

    public AudioClip TreasureChestAudio => treasureChestAudio;

    public AudioClip BuyItemAudio => buyItemAudio;

    public AudioClip ItemAppearAudio => itemAppearAudio;

    public AudioClip OpenMenuAudio => openMenuAudio;

    public AudioClip CloseMenuAudio => closeMenuAudio;

    public AudioClip FountainAudio => fountainUpgradeAudio;

    public AudioClip ForestTownAudio => forestTownAudio;

    public AudioClip DesertTown => desertTown;

    public AudioClip ArcticTown => arcticTown;

    public AudioClip GraveyardTown => graveyardTown;

    public AudioClip CastleTown => castleTown;

    public AudioClip RainAudio => rainAudio;

    public AudioClip NightForestAudio => nightForestAudio;

    public AudioClip NightForestStageAudio => nightForestStageAudio;

    public AudioClip NightDesertAudio => nightDesertAudio;

    public AudioClip NightArcticAudio => nightArcticAudio;

    public AudioClip MessagePopupAudio => messagePopupAudio;

    public AudioClip HpHealAudio => hpHealAudio;

    public AudioClip CpHealAudio => cpHealAudio;

    public AudioClip CounterAudio => counterAudio;

    public AudioClip GuardAudio => guardAudio;

    public AudioClip ParalysisAudio => paralysisAudio;

    public AudioClip SpiderKingBattleCryAudio => spiderKingBattleCryAudio;

    public AudioClip PoisonAudio => poisonAudio;

    public AudioClip SpiderKingRightLeg => spiderKingRightLeg;

    public AudioClip SpiderKingLeftLeg => spiderKingLeftLeg;

    public AudioClip MissedAttackAudio => missedAttackAudio;

    public AudioSource BackgroundMusic
    {
        get
        {
            return backgroundMusic;
        }
        set
        {
            backgroundMusic = value;
        }
    }

    public AudioSource SoundEffects
    {
        get
        {
            return soundEffects;
        }
        set
        {
            soundEffects = value;
        }
    }

    public AudioSource Ambient
    {
        get
        {
            return ambient;
        }
        set
        {
            ambient = value;
        }
    }

    public AudioSource AmbientForeground
    {
        get
        {
            return ambientForeground;
        }
        set
        {
            ambientForeground = value;
        }
    }

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if(soundEffects != null)
        {
            soundEffects.clip = clip;

            if (PlayerPrefs.HasKey("SoundEffects"))
            {
                SoundEffects.volume = PlayerPrefs.GetFloat("SoundEffects");
            }
            else
            {
                SoundEffects.volume = 1.0f;
            }

            soundEffects.Play();
        }
    }

    public void PlayBGM(AudioClip audioClip)
    {
        backgroundMusic.clip = audioClip;

        if(PlayerPrefs.HasKey("BackgroundAudio"))
        {
            backgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundAudio");
        }
        else
        {
            backgroundMusic.volume = 1.0f;
        }

        backgroundMusic.Play();
    }

    public void PlayAmbient(AudioClip ambience)
    {
        ambient.clip = ambience;

        if(PlayerPrefs.HasKey("BackgroundAudio"))
        {
            ambient.volume = PlayerPrefs.GetFloat("BackgroundAudio");
        }
        else
        {
            ambient.volume = 1;
        }

        ambient.Play();
    }

    public void PlayAmbientForeground(AudioClip foreground)
    {
        ambientForeground.clip = foreground;

        if (PlayerPrefs.HasKey("BackgroundAudio"))
        {
            ambientForeground.volume = PlayerPrefs.GetFloat("BackgroundAudio");
        }
        else
        {
            ambientForeground.volume = 1;
        }

        ambientForeground.Play();
    }

    public void PlayVictoryBGM()
    {
        backgroundMusic.clip = victoryAudio;

        if (PlayerPrefs.HasKey("BackgroundAudio"))
        {
            backgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundAudio");
        }
        else
        {
            backgroundMusic.volume = 1f;
        }

        backgroundMusic.Play();
    }

    public void StopMusicFade()
    {
        StopCoroutine("FadeOutBGM");
    }

    public IEnumerator FadeOutBGM()
    {
        while(backgroundMusic.volume > 0)
        {
            backgroundMusic.volume -= 2 * Time.deltaTime;

            yield return null;
        }
        backgroundMusic.volume = 0;
    }

    public IEnumerator FadeOutAmbience()
    {
        if(ambient.clip != null)
        {
            while (ambient.volume > 0)
            {
                ambient.volume -= Time.deltaTime;

                yield return null;
            }
            ambient.volume = 0;
        }
    }

    public IEnumerator FadeOutAmbienceForeground()
    {
        if (ambientForeground.clip != null)
        {
            while (ambientForeground.volume > 0)
            {
                ambientForeground.volume -= Time.deltaTime;

                yield return null;
            }
            ambientForeground.volume = 0;
        }
    }
}
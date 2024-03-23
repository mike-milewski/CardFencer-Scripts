using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    private WorldEnvironmentData worldMapEnvironmentData;

    private Scene scene;

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        scene = SceneManager.GetActiveScene();

        switch(scene.name)
        {
            case ("MainMenu"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.MainMenuTheme;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("ForestField"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.ForestOverworldTheme;
                if(worldMapEnvironmentData.changedDay)
                {
                    CheckVolumeSettings(0.15f);
                }
                else
                {
                    CheckVolumeSettings(1f);
                }
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("ForestTown"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.ForestTownAudio;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("DesertTown"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.DesertTown;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("ArcticTown"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.ArcticTown;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("GraveyardTown"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.GraveyardTown;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("CastleTown"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.CastleTown;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("DesertField"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.DesertOverWorldTheme;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("ArcticField"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.ArcticOverWorldTheme;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("GraveyardField"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.GraveyardOverWorldTheme;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
            case ("CastleField"):
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.CastleOverWorldTheme;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
                break;
        }

        if(!worldMapEnvironmentData.changedWeather)
        {
            AudioManager.instance.Ambient.volume = 0;
            AudioManager.instance.Ambient.Stop();
        }

        if(scene.name.Equals("Forest_1") || scene.name.Equals("Forest_2") || scene.name.Equals("Secret_Wood_1"))
        {
            AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.ForestAudio;
            CheckVolumeSettings(1f);
            AudioManager.instance.BackgroundMusic.Play();
        }
        else if(scene.name.Equals("Forest_3") || scene.name.Equals("Forest_4") || scene.name.Equals("Forest_5") || scene.name.Equals("Secret_Wood_2") || scene.name.Equals("Secret_Wood_3"))
        {
            AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.NightForestStageAudio;
            CheckVolumeSettings(1f);
            AudioManager.instance.BackgroundMusic.Play();

            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.Ambient.Stop();
        }
        else if(scene.name.Equals("Desert_1") || scene.name.Equals("Desert_2") || scene.name.Equals("Desert_3") || scene.name.Equals("Secret_Desert_1"))
        {
            AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.DesertAudio;
            CheckVolumeSettings(1f);
            AudioManager.instance.BackgroundMusic.Play();

            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.Ambient.Stop();
        }
        else if (scene.name.Equals("Desert_4") || scene.name.Equals("Desert_5") || scene.name.Equals("Secret_Desert_2") || scene.name.Equals("Secret_Desert_3"))
        {
            AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.NightDesertAudio;
            CheckVolumeSettings(1f);
            AudioManager.instance.BackgroundMusic.Play();

            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.Ambient.Stop();
        }
        else if(scene.name.Equals("Secret_Arctic_1") || scene.name.Equals("Secret_Arctic_3") || scene.name.Equals("Arctic_3") || scene.name.Equals("Arctic_4") || scene.name.Equals("Arctic_5"))
        {
            AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.ArcticAudio;
            CheckVolumeSettings(1f);
            AudioManager.instance.BackgroundMusic.Play();

            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.Ambient.Stop();
        }
        else if (scene.name.Equals("Arctic_1") || scene.name.Equals("Arctic_2") || scene.name.Equals("Secret_Arctic_2"))
        {
            AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.NightArcticAudio;
            CheckVolumeSettings(1f);
            AudioManager.instance.BackgroundMusic.Play();

            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.Ambient.Stop();
        }
        else if(scene.name.Equals("Graveyard_1") || scene.name.Equals("Graveyard_2") || scene.name.Equals("Graveyard_3") || scene.name.Equals("Graveyard_4") || scene.name.Equals("Graveyard_5") ||
                scene.name.Equals("Secret_Graveyard_1") || scene.name.Equals("Secret_Graveyard_2") || scene.name.Equals("Secret_Graveyard_3"))
        {
            AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.GraveyardAudio;
            CheckVolumeSettings(1f);
            AudioManager.instance.BackgroundMusic.Play();

            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.Ambient.Stop();
        }
        else if(scene.name.Equals("Castle_1") || scene.name.Equals("Castle_2") || scene.name.Equals("Castle_3") || scene.name.Equals("Castle_4") || scene.name.Equals("Castle_5") ||
                scene.name.Equals("Secret_Castle_1") || scene.name.Equals("Secret_Castle_2") || scene.name.Equals("Secret_Castle_3"))
        {
            AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.CastleAudio;
            CheckVolumeSettings(1f);
            AudioManager.instance.BackgroundMusic.Play();

            AudioManager.instance.AmbientForeground.volume = 0;
            AudioManager.instance.Ambient.Stop();
        }

        if(scene.name.Contains("Battle"))
        {
            if(GameManager.instance.IsBossFight)
            {
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.BossTheme;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
            }
            else if(GameManager.instance.IsFinalBossFight)
            {
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.FinalBossTheme;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
            }
            else
            {
                AudioManager.instance.BackgroundMusic.clip = AudioManager.instance.BattleAudio;
                CheckVolumeSettings(1f);
                AudioManager.instance.BackgroundMusic.Play();
            }
        }

        if(scene.name.Contains("Boss"))
        {
            AudioManager.instance.BackgroundMusic.Stop();
        }
    }

    public void CheckVolumeSettings(float volume)
    {
        Scene scene = SceneManager.GetActiveScene();

        if(PlayerPrefs.HasKey("BackgroundAudio"))
        {
            if(scene.name.Contains("ForestField"))
            {
                if(worldMapEnvironmentData.changedDay)
                {
                    AudioManager.instance.BackgroundMusic.volume = volume;
                }
                else
                {
                    AudioManager.instance.BackgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundAudio");
                }
            }
            else
            {
                AudioManager.instance.BackgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundAudio");
            }
        }
        else
        {
            AudioManager.instance.BackgroundMusic.volume = volume;
        }
    }
}
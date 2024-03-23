using UnityEngine;
using TMPro;

public enum Objectives { NONE, FindExit, DefeatEnemies, collectItems };

public class StageObjectives : MonoBehaviour
{
    [SerializeField]
    private Objectives objectives, specialObjective;

    [SerializeField]
    private TextMeshProUGUI objectiveText, secretObjectiveText;

    [SerializeField]
    private Animator objectivePanelAnimator, specialObjectivePanelAnimator;

    [SerializeField]
    private GameObject exitWallObject;

    [SerializeField]
    private string enemyNameToDefeat, itemsToCollect;

    [SerializeField]
    private int enemiesToDefeat, amountOfItemsToCollect;

    private int totalEnemiesToDefeat;

    [SerializeField]
    private bool defeatAllEnemies, defeatAllEnemiesSecret, defeatSpecificEnemy, defeatSpecificEnemySecret;

    private bool clearedObjective, clearedSecretObjective;

    public Objectives _Objectives
    {
        get
        {
            return objectives;
        }
        set
        {
            objectives = value;
        }
    }

    public Animator ObjectivePanelAnimator => objectivePanelAnimator;

    public Objectives SpecialObjective => specialObjective;

    public bool DefeatAllEnemies => defeatAllEnemies;

    public bool DefeatAllEnemiesSecret => defeatAllEnemiesSecret;

    public bool ClearedSecretObjective => clearedSecretObjective;

    public int EnemiesToDefeat
    {
        get
        {
            return enemiesToDefeat;
        }
        set
        {
            enemiesToDefeat = value;
        }
    }

    public void SetMainObjective()
    {
        if (objectives != Objectives.NONE)
        {
            objectivePanelAnimator.gameObject.SetActive(true);
            objectivePanelAnimator.Play("StageObjective", -1, 0);
            PlayAudio(false);
            switch (objectives)
            {
                case (Objectives.FindExit):
                    objectiveText.text = "Head For The Exit.";
                    exitWallObject.SetActive(false);
                    break;
                case (Objectives.DefeatEnemies):
                    if (defeatAllEnemies)
                    {
                        objectiveText.text = "Defeat All Enemies. Remaining: " + enemiesToDefeat;
                    }
                    else if (defeatSpecificEnemy)
                    {
                        objectiveText.text = "Defeat The " + enemyNameToDefeat + ".";
                    }
                    else
                    {
                        if (enemiesToDefeat > 1)
                        {
                            objectiveText.text = "Defeat " + enemiesToDefeat + " Enemies.";
                        }
                        else
                        {
                            objectiveText.text = "Defeat " + enemiesToDefeat + " Enemy.";
                        }
                    }
                    exitWallObject.SetActive(true);
                    break;
                case (Objectives.collectItems):
                    if (amountOfItemsToCollect > 1)
                    {
                        objectiveText.text = "Collect " + amountOfItemsToCollect + " " + itemsToCollect + "s.";
                    }
                    else
                    {
                        objectiveText.text = "Collect " + amountOfItemsToCollect + " " + itemsToCollect + ".";
                    }
                    exitWallObject.SetActive(true);
                    break;
            }

            MenuController.instance.StageObjectivePanel.SetActive(true);
            MenuController.instance.StageObjectiveText.text = objectiveText.text;
        }
    }

    private void SetSecretObjective()
    {
        if (specialObjective != Objectives.NONE)
        {
            MenuController.instance.StageConnectsToSecret = true;
            if(HasSecretSeekerSticker())
            {
                specialObjectivePanelAnimator.gameObject.SetActive(true);
                specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
                PlayAudio(true);
            }
            
            switch (specialObjective)
            {
                case (Objectives.DefeatEnemies):
                    if (defeatAllEnemiesSecret)
                    {
                        secretObjectiveText.text = "Defeat All Enemies. Remaining: " + enemiesToDefeat;
                    }
                    else if (defeatSpecificEnemySecret)
                    {
                        secretObjectiveText.text = "Defeat The " + enemyNameToDefeat + ".";
                    }
                    else
                    {
                        totalEnemiesToDefeat = enemiesToDefeat;

                        if (enemiesToDefeat > 1)
                        {
                            secretObjectiveText.text = "Defeat " + enemiesToDefeat + " Enemies.";
                        }
                        else
                        {
                            secretObjectiveText.text = "Defeat " + enemiesToDefeat + " Enemy.";
                        }
                    }
                    break;
                case (Objectives.collectItems):
                    if (amountOfItemsToCollect > 1)
                    {
                        secretObjectiveText.text = "Collect " + amountOfItemsToCollect + " " + itemsToCollect + "s.";
                    }
                    else
                    {
                        secretObjectiveText.text = "Collect " + amountOfItemsToCollect + " " + itemsToCollect + ".";
                    }
                    break;
            }

            if(HasSecretSeekerSticker())
            {
                MenuController.instance.SecretStageObjectivePanel.SetActive(true);
            }
            
            MenuController.instance.SecretStageObjectiveText.text = secretObjectiveText.text;
        }
    }

    public void UpdateMainObjective()
    {
        if(objectives != Objectives.NONE)
        {
            if (!clearedObjective)
            {
                switch (objectives)
                {
                    case (Objectives.DefeatEnemies):
                        if (defeatAllEnemies)
                        {
                            enemiesToDefeat--;
                            objectivePanelAnimator.gameObject.SetActive(true);
                            objectivePanelAnimator.Play("StageObjective", -1, 0);
                            PlayAudio(false);
                            if (enemiesToDefeat <= 0)
                            {
                                objectiveText.text = "Head For The Exit.";
                                exitWallObject.SetActive(false);
                                clearedObjective = true;
                            }
                            else
                            {
                                objectiveText.text = "Defeat All Enemies. Remaining: " + enemiesToDefeat;
                            }
                        }
                        else if (defeatSpecificEnemy)
                        {
                            if (!string.IsNullOrEmpty(enemyNameToDefeat))
                            {
                                for (int i = 0; i < GameManager.instance.EnemyObject.Enemies.Count; i++)
                                {
                                    if (GameManager.instance.EnemyObject.Enemies[i].enemyName == enemyNameToDefeat)
                                    {
                                        objectiveText.text = "Head For The Exit.";
                                        objectivePanelAnimator.gameObject.SetActive(true);
                                        objectivePanelAnimator.Play("StageObjective", -1, 0);
                                        PlayAudio(false);
                                        exitWallObject.SetActive(false);
                                        clearedObjective = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            enemiesToDefeat--;
                            objectivePanelAnimator.gameObject.SetActive(true);
                            objectivePanelAnimator.Play("StageObjective", -1, 0);
                            PlayAudio(false);
                            if (enemiesToDefeat <= 0)
                            {
                                objectiveText.text = "Head For The Exit.";
                                exitWallObject.SetActive(false);
                                clearedObjective = true;
                            }
                            else
                            {
                                if (enemiesToDefeat > 1)
                                {
                                    objectiveText.text = "Defeat " + enemiesToDefeat + " Enemies.";
                                }
                                else
                                {
                                    objectiveText.text = "Defeat " + enemiesToDefeat + " Enemy.";
                                }
                            }
                        }
                        break;
                    case (Objectives.collectItems):
                        amountOfItemsToCollect--;
                        objectivePanelAnimator.gameObject.SetActive(true);
                        objectivePanelAnimator.Play("StageObjective", -1, 0);
                        PlayAudio(false);
                        if (amountOfItemsToCollect <= 0)
                        {
                            objectiveText.text = "Head For The Exit.";
                            exitWallObject.SetActive(false);
                            clearedObjective = true;
                        }
                        else
                        {
                            if (amountOfItemsToCollect > 1)
                            {
                                objectiveText.text = "Collect " + amountOfItemsToCollect + " " + itemsToCollect + "s.";
                            }
                            else
                            {
                                objectiveText.text = "Collect " + amountOfItemsToCollect + " " + itemsToCollect + ".";
                            }
                        }
                        break;
                }
                MenuController.instance.StageObjectiveText.text = objectiveText.text;
            }
        }
    }

    public void UpdateSecretObjective()
    {
        if(specialObjective != Objectives.NONE)
        {
            if (!clearedSecretObjective)
            {
                switch (specialObjective)
                {
                    case (Objectives.DefeatEnemies):
                        if (defeatAllEnemiesSecret)
                        {
                            enemiesToDefeat--;

                            if(HasSecretSeekerSticker())
                            {
                                specialObjectivePanelAnimator.gameObject.SetActive(true);
                                specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
                                PlayAudio(true);
                            }
                            
                            if (enemiesToDefeat <= 0)
                            {
                                secretObjectiveText.text = "COMPLETE! Defeated all enemies!";
                                NodeManager.instance.UnlockedSecretStage = true;
                                clearedSecretObjective = true;

                                if(!HasSecretSeekerSticker())
                                {
                                    specialObjectivePanelAnimator.gameObject.SetActive(true);
                                    specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
                                    PlayAudio(true);

                                    MenuController.instance.SecretStageObjectivePanel.SetActive(true);
                                }
                            }
                            else
                            {
                                secretObjectiveText.text = "Defeat All Enemies. Remaining: " + enemiesToDefeat;
                            }
                        }
                        else if (defeatSpecificEnemySecret)
                        {
                            if (!string.IsNullOrEmpty(enemyNameToDefeat))
                            {
                                for (int i = 0; i < GameManager.instance.EnemyObject.Enemies.Count; i++)
                                {
                                    if (GameManager.instance.EnemyObject.Enemies[i].enemyName == enemyNameToDefeat)
                                    {
                                        secretObjectiveText.text = "COMPLETE! Defeated the " + enemyNameToDefeat + "!";
                                        specialObjectivePanelAnimator.gameObject.SetActive(true);
                                        specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
                                        PlayAudio(true);
                                        NodeManager.instance.UnlockedSecretStage = true;
                                        clearedSecretObjective = true;

                                        if(!HasSecretSeekerSticker())
                                        {
                                            MenuController.instance.SecretStageObjectivePanel.SetActive(true);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            enemiesToDefeat--;

                            if(HasSecretSeekerSticker())
                            {
                                specialObjectivePanelAnimator.gameObject.SetActive(true);
                                specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
                                PlayAudio(true);
                            }
                            
                            if (enemiesToDefeat <= 0)
                            {
                                secretObjectiveText.text = "COMPLETE! Defeated " + totalEnemiesToDefeat + " enemies!";
                                clearedSecretObjective = true;

                                NodeManager.instance.UnlockedSecretStage = true;

                                if (!HasSecretSeekerSticker())
                                {
                                    specialObjectivePanelAnimator.gameObject.SetActive(true);
                                    specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
                                    PlayAudio(true);

                                    MenuController.instance.SecretStageObjectivePanel.SetActive(true);
                                }
                            }
                            else
                            {
                                if (enemiesToDefeat > 1)
                                {
                                    secretObjectiveText.text = "Defeat " + enemiesToDefeat + " Enemies.";
                                }
                                else
                                {
                                    secretObjectiveText.text = "Defeat " + enemiesToDefeat + " Enemy.";
                                }
                            }
                        }
                        break;
                    case (Objectives.collectItems):
                        amountOfItemsToCollect--;

                        if(HasSecretSeekerSticker())
                        {
                            specialObjectivePanelAnimator.gameObject.SetActive(true);
                            specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
                            PlayAudio(true);
                        }
                        
                        if (amountOfItemsToCollect <= 0)
                        {
                            secretObjectiveText.text = "COMPLETE! Collected all " + itemsToCollect + "s!";
                            clearedSecretObjective = true;

                            NodeManager.instance.UnlockedSecretStage = true;

                            if (!HasSecretSeekerSticker())
                            {
                                specialObjectivePanelAnimator.gameObject.SetActive(true);
                                specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
                                PlayAudio(true);

                                MenuController.instance.SecretStageObjectivePanel.SetActive(true);
                            }
                        }
                        else
                        {
                            if (amountOfItemsToCollect > 1)
                            {
                                secretObjectiveText.text = "Collect " + amountOfItemsToCollect + " " + itemsToCollect + "s.";
                            }
                            else
                            {
                                secretObjectiveText.text = "Collect " + amountOfItemsToCollect + " " + itemsToCollect + ".";
                            }
                        }
                        break;
                }
                MenuController.instance.SecretStageObjectiveText.text = secretObjectiveText.text;
            }
        }
    }

    private bool HasSecretSeekerSticker()
    {
        bool applies = false;

        if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.SecretSeeker))
        {
            applies = true;
        }

        return applies;
    }

    public void CheckEnemyObjectiveMain()
    {
        if (objectives == Objectives.DefeatEnemies)
            UpdateMainObjective();
    }

    public void CheckCollectObjectiveMain()
    {
        if (objectives == Objectives.collectItems)
            UpdateMainObjective();
    }

    public void CheckCollectObjectiveSecret()
    {
        if (specialObjective == Objectives.collectItems)
            UpdateSecretObjective();
    }

    public void CheckAllEnemiesDefeatedObjectiveSecret()
    {
        if (specialObjective == Objectives.DefeatEnemies && !defeatSpecificEnemySecret && defeatAllEnemies)
        {
            UpdateSecretObjective();
        }  
    }

    public void CheckEnemiesDefeatedObjectiveSecret()
    {
        if (specialObjective == Objectives.DefeatEnemies && !defeatSpecificEnemySecret && !defeatAllEnemies)
        {
            UpdateSecretObjective();
        }
    }

    public void PlayStageObjectiveAnimator()
    {
        if(objectives != Objectives.NONE)
        {
            objectivePanelAnimator.gameObject.SetActive(true);

            objectivePanelAnimator.Play("StageObjective", -1, 0);

            SetMainObjective();
        }
        if(specialObjective != Objectives.NONE)
        {
            PlaySecretStageObjectiveAnimator();
        }
    }

    private void PlaySecretStageObjectiveAnimator()
    {
        if(HasSecretSeekerSticker())
        {
            specialObjectivePanelAnimator.gameObject.SetActive(true);

            specialObjectivePanelAnimator.Play("StageObjective", -1, 0);
        }

        SetSecretObjective();
    }

    private void PlayAudio(bool secretPanel)
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            if (secretPanel)
            {
                specialObjectivePanelAnimator.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                specialObjectivePanelAnimator.GetComponent<AudioSource>().Play();
            }
            else
            {
                objectivePanelAnimator.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                objectivePanelAnimator.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if(secretPanel)
            {
                specialObjectivePanelAnimator.GetComponent<AudioSource>().Play();
            }
            else
            {
                objectivePanelAnimator.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void ResetObjectivesAnimator()
    {
        if(objectivePanelAnimator.gameObject.activeSelf)
        {
            objectivePanelAnimator.Play("Idle", -1, 0);
            objectivePanelAnimator.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
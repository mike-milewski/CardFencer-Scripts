using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sticker : MonoBehaviour
{
    [SerializeField]
    private StickerInformation stickerInformation;

    [SerializeField]
    private StickerPage stickerPage;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private PropogateDrag propogateDrag;

    [SerializeField]
    private ParticleSystem stickerEquipParticle;

    [SerializeField]
    private Animator stickerInfoPanelAnimator, mainMenuStickerInfoPanelAnimator, errorMessageAnimator;

    private Animator spoilsInfoPanelAnimator = null;

    [SerializeField]
    private RectTransform[] stickerSlots;

    [SerializeField]
    private RectTransform stickerRectTransform;

    [SerializeField]
    private Transform stickerListParent, equippedStickerParent;

    [SerializeField]
    private GameObject stickerPricePanel, newStickerText, stickerParticleObject, statusDurationPanel, mainMenuStickerDurationPanel;

    [SerializeField]
    private Button stickerButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Image stickerImage, mainStickerImage, statusEffectSpriteSticker, statusEffectSpriteMenu;

    [SerializeField]
    private TextMeshProUGUI stickerPointsText, equippedStickerPointsText, stickerPanelInfoText, stickerNameText, mainMenuStickerNameText, mainMenuStickerInfoText, stickerMenuStickerText, mainMainStickerText, errorText, stickerPriceText,
                            statusEffectStickerInfoStickerMenu, statusEffectStickerInfoMainMenu, statusInfoSticker, statusInfoMenu, statusDurationText, mainMenuStickerDurationText;

    private bool equipped, mainMenuView, isInShopMenu, isUndetectable;

    [SerializeField]
    private bool isInBattle;

    [SerializeField]
    private int stickerIndex;

    [SerializeField]
    private int stickerBuyValue;

    public StickerInformation _stickerInformation
    {
        get
        {
            return stickerInformation;
        }
        set
        {
            stickerInformation = value;
        }
    }

    public StickerPage _StickerPage => stickerPage;

    public Button StickerButton => stickerButton;

    public GameObject StickerPricePanel => stickerPricePanel;

    public GameObject NewStickerText => newStickerText;

    public TextMeshProUGUI StickerPointsText => stickerPointsText;

    public Animator SpoilsInfoPanelAnimator
    {
        get
        {
            return spoilsInfoPanelAnimator;
        }
        set
        {
            spoilsInfoPanelAnimator = value;
        }
    }

    public TextMeshProUGUI StickerPriceText
    {
        get
        {
            return stickerPriceText;
        }
        set
        {
            stickerPriceText = value;
        }
    }

    public PropogateDrag _PropogateDrag
    {
        get
        {
            return propogateDrag;
        }
        set
        {
            propogateDrag = value;
        }
    }

    public bool IsUndetectable => isUndetectable;

    public bool MainMenuView
    {
        get
        {
            return mainMenuView;
        }
        set
        {
            mainMenuView = value;
        }
    }

    public bool IsInShopMenu
    {
        get
        {
            return isInShopMenu;
        }
        set
        {
            isInShopMenu = value;
        }
    }

    public int StickerBuyValue
    {
        get
        {
            return stickerBuyValue;
        }
        set
        {
            stickerBuyValue = value;
        }
    }

    private void Awake()
    {
        SetUpStickerInformation();
    }

    public void SetUpStickerInformation()
    {
        stickerImage.sprite = stickerInformation.stickerSprite;

        stickerPointsText.text = stickerInformation.stickerCost + " SP";

        equippedStickerPointsText.text = stickerInformation.stickerCost + " SP";
    }

    public void SetStickerBuyValue()
    {
        stickerBuyValue = stickerInformation.buyValue;
    }

    public void EquipStatus()
    {
        if(!equipped)
        {
            EquipSticker();
        }
        else
        {
            UnEquipSticker();
        }
    }

    public void EquipSticker()
    {
        if(mainCharacterStats.currentPlayerStickerPoints >= stickerInformation.stickerCost)
        {
            if(playerMenuInfo.equippedStickers >= playerMenuInfo.currentStickerSlotSize)
            {
                ErrorMessage("You can't equip anymore Stickers!");
            }
            else
            {
                mainCharacterStats.currentPlayerStickerPoints -= stickerInformation.stickerCost;

                stickerMenuStickerText.text = mainCharacterStats.currentPlayerStickerPoints + "/" + mainCharacterStats.maximumStickerPoints;
                mainMainStickerText.text = mainCharacterStats.currentPlayerStickerPoints + "/" + mainCharacterStats.maximumStickerPoints;

                HideStickerParticle();

                if(newStickerText.activeSelf)
                {
                    newStickerText.SetActive(false);
                }

                stickerIndex = playerMenuInfo.equippedStickers;

                CreateStickerForDefaultMenu(stickerIndex);

                playerMenuInfo.equippedStickers++;

                AudioManager.instance.PlaySoundEffect(AudioManager.instance.EquipAudio);

                stickerRectTransform.SetParent(equippedStickerParent);

                stickerPointsText.gameObject.SetActive(false);
                equippedStickerPointsText.gameObject.SetActive(true);

                stickerEquipParticle.Stop();
                stickerEquipParticle.Play();

                equipped = true;

                GainStickerAbility();

                MenuController.instance._StickerMenu.AdjustEquippedStickerNavigations();
                MenuController.instance._StickerMenu.AdjustStickerNavigations();

                InputManager.instance.SetSelectedObject(MenuController.instance._StickerMenu.StickerList.Count > 0 ? MenuController.instance._StickerMenu.StickerList[0].gameObject :
                                                        MenuController.instance._StickerMenu.StickerNextArrow);
            }
        }
        else
        {
            ErrorMessage("Not enough Sticker Points!");
        }
    }

    public void UnEquipSticker()
    {
        playerMenuInfo.equippedStickers--;

        stickerPage.SetStickerCategory(this);

        stickerEquipParticle.Stop();
        stickerEquipParticle.Play();

        equipped = false;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.EquipAudio);

        mainCharacterStats.currentPlayerStickerPoints += stickerInformation.stickerCost;

        stickerMenuStickerText.text = mainCharacterStats.currentPlayerStickerPoints + "/" + mainCharacterStats.maximumStickerPoints;
        mainMainStickerText.text = mainCharacterStats.currentPlayerStickerPoints + "/" + mainCharacterStats.maximumStickerPoints;

        stickerPointsText.gameObject.SetActive(true);
        equippedStickerPointsText.gameObject.SetActive(false);

        LoseStickerAbility();

        DestroyStickerForDefaultMenu(stickerIndex);

        RepositionStickers();

        InputManager.instance.SetSelectedObject(MenuController.instance._StickerMenu.StickerParent.childCount > 0 ? MenuController.instance._StickerMenu.StickerParent.GetChild(0).gameObject :
                                                MenuController.instance._StickerMenu.StickerNextArrow);
    }

    private void RepositionStickers()
    {
        for(int i = 0; i < MenuController.instance._equippedStickers.Count; i++)
        {
            MenuController.instance._equippedStickers[i].stickerRectTransform.SetParent(MenuController.instance.StickerSlots[i]);

            MenuController.instance._equippedStickers[i].stickerIndex = i;

            MenuController.instance._equippedStickers[i].stickerRectTransform.anchoredPosition = new Vector2(0, 0);
        }

        for(int j = 0; j < equippedStickerParent.childCount; j++)
        {
            equippedStickerParent.GetChild(j).GetComponent<Sticker>().stickerIndex = j;
        }

        MenuController.instance._StickerMenu.AdjustEquippedStickerNavigations();
        MenuController.instance._StickerMenu.AdjustStickerNavigations();
    }

    public void ShowStickerParticle()
    {
        stickerParticleObject.SetActive(true);
    }

    public void HideStickerParticle()
    {
        stickerParticleObject.SetActive(false);
    }

    public void ShowBestiaryInfo()
    {
        MenuController.instance._BestiaryMenu._Sticker.gameObject.SetActive(true);

        MenuController.instance._BestiaryMenu._Sticker.stickerInformation = stickerInformation;

        MenuController.instance._BestiaryMenu._Sticker.SetUpStickerInformation();

        MenuController.instance._BestiaryMenu.ItemInfoPanel.SetActive(true);

        MenuController.instance._BestiaryMenu.ItemInfoNameText.text = stickerInformation.stickerName;

        if(stickerInformation.appliesStatusEffect)
        {
            MenuController.instance._BestiaryMenu.ItemInfoTextStickerStatus.gameObject.SetActive(true);
            MenuController.instance._BestiaryMenu.ItemInfoText.gameObject.SetActive(false);
            MenuController.instance._BestiaryMenu.StatusDurationPanel.SetActive(true);

            MenuController.instance._BestiaryMenu.StatusDurationText.text = stickerInformation.statusDuration == 0 ? "Duration: Infinite" : "Duration: " + stickerInformation.statusDuration + " Turn(s)";

            if(stickerInformation.statusPercentChance > 0)
            {
                MenuController.instance._BestiaryMenu.StatusDurationText.text += ", Success: " + stickerInformation.statusPercentChance + "%";
            }

            MenuController.instance._BestiaryMenu.StickerInfoStatusImage.sprite = stickerInformation.statusEffectSprite;

            MenuController.instance._BestiaryMenu.ItemInfoTextStickerStatus.text = stickerInformation.stickerDescription;

            MenuController.instance._BestiaryMenu.ItemInfoStickerStatusDescription.text = stickerInformation.statusEffectDescription;
        }
        else
        {
            MenuController.instance._BestiaryMenu.ItemInfoText.gameObject.SetActive(true);
            MenuController.instance._BestiaryMenu.ItemInfoTextStickerStatus.gameObject.SetActive(false);
            MenuController.instance._BestiaryMenu.StatusDurationPanel.SetActive(false);

            MenuController.instance._BestiaryMenu.ItemInfoText.text = stickerInformation.stickerDescription;
        }
    }

    public void HideBestiaryInfo()
    {
        MenuController.instance._BestiaryMenu._Sticker.gameObject.SetActive(false);
        MenuController.instance._BestiaryMenu.StatusDurationPanel.SetActive(false);
        MenuController.instance._BestiaryMenu.ItemInfoPanel.SetActive(false);
    }

    private void GainStickerAbility()
    {
        switch(stickerInformation.stickerPower)
        {
            case (StickerPower.HealthUp):
                bool fullHealth = false;
                if(mainCharacterStats.currentPlayerHealth >= mainCharacterStats.maximumHealth)
                {
                    fullHealth = true;
                }
                mainCharacterStats.maximumHealth += stickerInformation.statValueIncrease;
                if(fullHealth)
                {
                    mainCharacterStats.currentPlayerHealth = mainCharacterStats.maximumHealth;
                }
                break;
            case (StickerPower.CardPointsUp):
                bool fullCardPoints = false;
                if (mainCharacterStats.currentPlayerCardPoints >= mainCharacterStats.maximumCardPoints)
                {
                    fullCardPoints = true;
                }
                mainCharacterStats.maximumCardPoints += stickerInformation.statValueIncrease;
                if (fullCardPoints)
                {
                    mainCharacterStats.currentPlayerCardPoints = mainCharacterStats.maximumCardPoints;
                }
                break;
            case (StickerPower.StrengthUp):
                mainCharacterStats.strength += stickerInformation.statValueIncrease;
                break;
            case (StickerPower.DefenseUp):
                mainCharacterStats.defense += stickerInformation.statValueIncrease;
                break;
            case (StickerPower.IncreasedSpeed):
                mainCharacterStats.moveSpeed = mainCharacterStats.increasedMoveSpeed;
                break;
            case (StickerPower.SecretSeeker):

                Scene scene = SceneManager.GetActiveScene();

                if(MenuController.instance.StageConnectsToSecret)
                {
                    MenuController.instance.SecretStageObjectivePanel.SetActive(true);
                }

                if(!scene.name.Contains("Field") && !scene.name.Contains("Town") && !scene.name.Contains("Main"))
                {
                    if(GameManager.instance.SecretEnemyObject != null)
                    {
                        GameManager.instance.SecretEnemyObject.ShowTargetSymbol();
                    }
                }
                break;
            case (StickerPower.Undetectable):
                isUndetectable = true;
                break;
            case (StickerPower.Wraith):
                mainCharacterStats.cardPointHealPercentage = 0.2f;
                break;
        }

        if(stickerInformation.isFieldPower)
        {
            MenuController.instance._StickerPowerManager.FieldStickerPowers.Add(stickerInformation.stickerPower);

            MenuController.instance.UpdateCardPointHealInfo();
        }
        if(stickerInformation.isStartOfBattlePower)
        {
            MenuController.instance._StickerPowerManager.StartOfBattleStickerPowers.Add(stickerInformation.stickerPower);
        }
        if(stickerInformation.isConditionalBattlePower)
        {
            MenuController.instance._StickerPowerManager.MidBattleStickerPowers.Add(stickerInformation.stickerPower);
        }
    }

    public void LoseStickerAbility()
    {
        switch (stickerInformation.stickerPower)
        {
            case (StickerPower.HealthUp):
                mainCharacterStats.maximumHealth -= stickerInformation.statValueIncrease;
                if (mainCharacterStats.currentPlayerHealth > mainCharacterStats.maximumHealth)
                {
                    mainCharacterStats.currentPlayerHealth = mainCharacterStats.maximumHealth;
                }
                break;
            case (StickerPower.CardPointsUp):
                mainCharacterStats.maximumCardPoints -= stickerInformation.statValueIncrease;
                if (mainCharacterStats.currentPlayerCardPoints > mainCharacterStats.maximumCardPoints)
                {
                    mainCharacterStats.currentPlayerCardPoints = mainCharacterStats.maximumCardPoints;
                }
                break;
            case (StickerPower.StrengthUp):
                mainCharacterStats.strength -= stickerInformation.statValueIncrease;
                break;
            case (StickerPower.DefenseUp):
                mainCharacterStats.defense -= stickerInformation.statValueIncrease;
                break;
            case (StickerPower.IncreasedSpeed):
                mainCharacterStats.moveSpeed = 15;
                mainCharacterStats.increasedSprintSpeed = 3;
                break;
            case (StickerPower.SecretSeeker):

                Scene scene = SceneManager.GetActiveScene();

                if(!scene.name.Contains("Field") && !scene.name.Contains("Town"))
                {
                    if (!GameManager.instance._StageObjectives.ClearedSecretObjective)
                    {
                        MenuController.instance.SecretStageObjectivePanel.SetActive(false);
                    }

                    if(GameManager.instance.SecretEnemyObject != null)
                       GameManager.instance.SecretEnemyObject.HideTargetSymbol();
                }
                break;
            case (StickerPower.Undetectable):
                isUndetectable = false;
                break;
            case (StickerPower.Wraith):
                mainCharacterStats.cardPointHealPercentage = 0.1f;
                break;
        }

        if(stickerInformation.isFieldPower)
        {
            MenuController.instance._StickerPowerManager.FieldStickerPowers.Remove(stickerInformation.stickerPower);

            MenuController.instance.UpdateCardPointHealInfo();
        }
        if(stickerInformation.isStartOfBattlePower)
        {
            MenuController.instance._StickerPowerManager.StartOfBattleStickerPowers.Remove(stickerInformation.stickerPower);
        }
        if(stickerInformation.isConditionalBattlePower)
        {
            MenuController.instance._StickerPowerManager.MidBattleStickerPowers.Remove(stickerInformation.stickerPower);
        }
    }

    public void DisplayStickerInfo()
    {
        if(isInBattle)
        {
            spoilsInfoPanelAnimator.Play("OnPointerStatusPanel", -1, 0);

            SpoilsManager.instance.SpoilsTitleText.text = stickerInformation.stickerName;

            SpoilsManager.instance.StickerCostFrame.SetActive(true);

            SpoilsManager.instance.StickerCostText.text = stickerInformation.stickerCost + " SP";

            if (stickerInformation.appliesStatusEffect)
            {
                SpoilsManager.instance.SpoilsInfoText.gameObject.SetActive(false);
                SpoilsManager.instance.StickerStatusInfo.gameObject.SetActive(true);
                SpoilsManager.instance.DurationPanel.SetActive(true);

                SpoilsManager.instance.StickerStatusInfo.text = stickerInformation.stickerDescription;

                SpoilsManager.instance.StickerStatusImage.sprite = stickerInformation.statusEffectSprite;

                SpoilsManager.instance.StatusDurationText.text = stickerInformation.statusDuration == 0 ? "Duration: Infinite" : "Duration: " + stickerInformation.statusDuration + " Turn(s)";

                if (stickerInformation.statusPercentChance > 0)
                {
                    SpoilsManager.instance.StatusDurationText.text += ", Success: " + stickerInformation.statusPercentChance + "%";
                }

                SpoilsManager.instance.StickerStatusDescription.text = stickerInformation.statusEffectDescription;
            }
            else
            {
                SpoilsManager.instance.DurationPanel.SetActive(false);

                SpoilsManager.instance.SpoilsInfoText.text = stickerInformation.stickerDescription;
            }
        }
        else
        {
            if (!isInShopMenu)
            {
                if (!mainMenuView)
                {
                    stickerInfoPanelAnimator.Play("Show", -1, 0);

                    stickerNameText.text = stickerInformation.stickerName;

                    if (stickerInformation.appliesStatusEffect)
                    {
                        statusDurationPanel.SetActive(true);

                        statusEffectStickerInfoStickerMenu.gameObject.SetActive(true);

                        stickerPanelInfoText.gameObject.SetActive(false);

                        statusDurationText.text = stickerInformation.statusDuration == 0 ? "Duration: Infinite" : "Duration: " + stickerInformation.statusDuration + " Turn(s)";

                        if (stickerInformation.statusPercentChance > 0)
                        {
                            statusDurationText.text += ", Success: " + stickerInformation.statusPercentChance + "%";
                        }

                        statusEffectStickerInfoStickerMenu.text = stickerInformation.stickerDescription;

                        statusEffectSpriteSticker.sprite = stickerInformation.statusEffectSprite;

                        statusInfoSticker.text = stickerInformation.statusEffectDescription;
                    }
                    else
                    {
                        statusDurationPanel.SetActive(false);

                        stickerPanelInfoText.gameObject.SetActive(true);

                        statusEffectStickerInfoStickerMenu.gameObject.SetActive(false);

                        stickerPanelInfoText.text = stickerInformation.stickerDescription;
                    }
                }
                else
                {
                    mainMenuStickerInfoPanelAnimator.Play("Show", -1, 0);

                    mainMenuStickerNameText.text = stickerInformation.stickerName;

                    if (stickerInformation.appliesStatusEffect)
                    {
                        mainMenuStickerDurationPanel.SetActive(true);

                        statusEffectStickerInfoMainMenu.gameObject.SetActive(true);

                        mainMenuStickerInfoText.gameObject.SetActive(false);

                        mainMenuStickerDurationText.text = stickerInformation.statusDuration == 0 ? "Duration: Infinite" : "Duration: " + stickerInformation.statusDuration + " Turn(s)";

                        if (stickerInformation.statusPercentChance > 0)
                        {
                            mainMenuStickerDurationText.text += ", Success: " + stickerInformation.statusPercentChance + "%";
                        }

                        statusEffectStickerInfoMainMenu.text = stickerInformation.stickerDescription;

                        statusEffectSpriteMenu.sprite = stickerInformation.statusEffectSprite;

                        statusInfoMenu.text = stickerInformation.statusEffectDescription;
                    }
                    else
                    {
                        mainMenuStickerDurationPanel.SetActive(false);

                        mainMenuStickerInfoText.gameObject.SetActive(true);

                        statusEffectStickerInfoMainMenu.gameObject.SetActive(false);

                        mainMenuStickerInfoText.text = stickerInformation.stickerDescription;
                    }
                }
            }
            else
            {
                ShopMenu.instance.PlayStickerPanel(true, this);
            }
        }
    }

    public void PlayStickerAudio()
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void UpdateStickerPointsText()
    {
        stickerPointsText.text = stickerInformation.stickerCost + " SP";
    }

    public void HideStickerPanel()
    {
        if(isInBattle)
        {
            spoilsInfoPanelAnimator.Play("Reverse", -1, 0);
        }
        else
        {
            if (!isInShopMenu)
            {
                if (!mainMenuView)
                {
                    if(MenuController.instance.gameObject.activeSelf)
                       stickerInfoPanelAnimator.Play("Hide", -1, 0);
                }
                else
                {
                    mainMenuStickerInfoPanelAnimator.Play("Hide", -1, 0);
                }
            }
            else
            {
                ShopMenu.instance.PlayStickerPanel(false, this);
            }
        }
    }

    public void HideStickerPanelController()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if (isInBattle)
            {
                spoilsInfoPanelAnimator.Play("Reverse", -1, 0);
            }
            else
            {
                if (!isInShopMenu)
                {
                    if (!mainMenuView)
                    {
                        stickerInfoPanelAnimator.Play("Hide", -1, 0);
                    }
                    else
                    {
                        mainMenuStickerInfoPanelAnimator.Play("Hide", -1, 0);
                    }
                }
                else
                {
                    ShopMenu.instance.PlayStickerPanel(false, this);
                }
            }
        }
    }

    private void CreateStickerForDefaultMenu(int slotIndex)
    {
        var sticker = Instantiate(MenuController.instance.StickerPrefab, MenuController.instance.StickerSlots[slotIndex]);

        sticker.stickerIndex = playerMenuInfo.equippedStickers;

        sticker.mainMenuView = true;

        sticker.stickerRectTransform.SetAsFirstSibling();

        sticker.gameObject.SetActive(true);

        sticker.stickerRectTransform.sizeDelta = new Vector2(55f, 55f);

        sticker.stickerRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        sticker.stickerRectTransform.anchorMax = new Vector2(0.5f, 0.5f);

        sticker.stickerRectTransform.anchoredPosition = new Vector2(0, 0);

        sticker.stickerPointsText.gameObject.SetActive(false);

        sticker.mainStickerImage.raycastTarget = false;

        sticker.stickerInformation = stickerInformation;

        sticker.stickerImage.sprite = stickerInformation.stickerSprite;

        MenuController.instance._equippedStickers.Add(sticker);
    }

    private void DestroyStickerForDefaultMenu(int slotIndex)
    {
        MenuController.instance._equippedStickers.RemoveAt(slotIndex);

        Destroy(MenuController.instance.StickerSlots[slotIndex].GetChild(0).gameObject);
    }

    private void ErrorMessage(string message)
    {
        errorText.text = message;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.ErrorMessageAudio);

        errorMessageAnimator.Play("Message", -1, 0);
    }
}
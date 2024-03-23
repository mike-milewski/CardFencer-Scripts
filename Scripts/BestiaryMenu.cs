using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Steamworks;

public class BestiaryMenu : MonoBehaviour
{
    [SerializeField]
    private List<EnemyStats> enemies = new List<EnemyStats>();

    [SerializeField]
    private Transform[] enemyContainers;

    [SerializeField]
    private BestiaryButton bestiaryButtonPrefab;

    [SerializeField]
    private MenuCard menuCard, enemyInfoMenuCard;

    [SerializeField]
    private Sticker sticker, enemyInfoSticker;

    [SerializeField]
    private GameObject enemyInfoPanel, itemInfoPanel, statusDurationPanel, enemyList;

    [SerializeField]
    private Selectable forestSelectable, desertSelectable, arcticSelectable, graveyardSelectable, castleSelectable;

    [SerializeField]
    private Image stickerInfoStatusImage;

    [SerializeField]
    private ScrollRectEx scrollRect, desertScroll, arcticScroll, cemeteryScroll, castleScroll;

    [SerializeField]
    private TextMeshProUGUI enemyInfoText, itemInfoNameText, itemInfoText, itemInfoTextStickerStatus, itemInfoStickerStatusDescription, statusDurationText, enemyNameText, enemyHealthText, enemyStrengthText,
                            enemyDefenseText, bestiaryCountText;

    [SerializeField]
    private Transform enemyHolder, enemyContainerHolder;

    private Transform enemyContainer;

    private Selectable currentSelectable;

    public List<EnemyStats> Enemies => enemies;

    public Transform[] EnemyContainers => enemyContainers;

    public Transform EnemyHolder => enemyHolder;

    public BestiaryButton _BestiaryButtonPrefab => bestiaryButtonPrefab;

    public TextMeshProUGUI EnemyNameText
    {
        get
        {
            return enemyNameText;
        }
        set
        {
            enemyNameText = value;
        }
    }

    public TextMeshProUGUI EnemyInfoText
    {
        get
        {
            return enemyInfoText;
        }
        set
        {
            enemyInfoText = value;
        }
    }

    public TextMeshProUGUI ItemInfoNameText
    {
        get
        {
            return itemInfoNameText;
        }
        set
        {
            itemInfoNameText = value;
        }
    }

    public TextMeshProUGUI ItemInfoText
    {
        get
        {
            return itemInfoText;
        }
        set
        {
            itemInfoText = value;
        }
    }

    public TextMeshProUGUI ItemInfoTextStickerStatus
    {
        get
        {
            return itemInfoTextStickerStatus;
        }
        set
        {
            itemInfoTextStickerStatus = value;
        }
    }

    public TextMeshProUGUI StatusDurationText
    {
        get
        {
            return statusDurationText;
        }
        set
        {
            statusDurationText = value;
        }
    }

    public TextMeshProUGUI ItemInfoStickerStatusDescription
    {
        get
        {
            return itemInfoStickerStatusDescription;
        }
        set
        {
            itemInfoStickerStatusDescription = value;
        }
    }

    public TextMeshProUGUI EnemyHealthText
    {
        get => enemyHealthText;
        set => enemyHealthText = value;
    }

    public TextMeshProUGUI EnemyStrengthText
    {
        get => enemyStrengthText;
        set => enemyStrengthText = value;
    }

    public TextMeshProUGUI EnemyDefenseText
    {
        get => enemyDefenseText;
        set => enemyDefenseText = value;
    }

    public Transform EnemyContainer => enemyContainer;

    public GameObject EnemyInfoPanel => enemyInfoPanel;

    public GameObject StatusDurationPanel => statusDurationPanel;

    public GameObject ItemInfoPanel => itemInfoPanel;

    public GameObject EnemyList => enemyList;

    public Image StickerInfoStatusImage
    {
        get
        {
            return stickerInfoStatusImage;
        }
        set
        {
            stickerInfoStatusImage = value;
        }
    }

    public MenuCard _MenuCard
    {
        get
        {
            return menuCard;
        }
        set
        {
            menuCard = value;
        }
    }

    public Sticker _Sticker
    {
        get
        {
            return sticker;
        }
        set
        {
            sticker = value;
        }
    }

    public void CompareEnemies(EnemyStats enemyStats)
    {
        int sameName = 0;

        switch(enemyStats.worldLocation)
        {
            case (WorldLocation.Forest):
                enemyContainer = enemyContainers[0];
                break;
            case (WorldLocation.Desert):
                enemyContainer = enemyContainers[1];
                break;
            case (WorldLocation.Arctic):
                enemyContainer = enemyContainers[2];
                break;
            case (WorldLocation.Graveyard):
                enemyContainer = enemyContainers[3];
                break;
            case (WorldLocation.Castle):
                enemyContainer = enemyContainers[4];
                break;
        }

        if(enemyContainer.childCount > 0)
        {
            foreach(BestiaryButton bestiaryButton in enemyContainer.GetComponentsInChildren<BestiaryButton>())
            {
                if (enemyStats.enemyName == bestiaryButton._EnemyStats.enemyName)
                {
                    sameName++;
                }
            }

            if(sameName <= 0)
            {
                BestiaryButton monster = Instantiate(bestiaryButtonPrefab, enemyContainer);

                monster.gameObject.SetActive(true);

                monster._EnemyStats = enemyStats;

                monster.EnemyNameText.text = monster._EnemyStats.enemyName;

                CheckBestiaryAchievement();
            }
        }
        else
        {
            BestiaryButton monster = Instantiate(bestiaryButtonPrefab, enemyContainer);

            monster.gameObject.SetActive(true);

            monster._EnemyStats = enemyStats;

            monster.EnemyNameText.text = monster._EnemyStats.enemyName;

            CheckBestiaryAchievement();
        }
    }

    public void ResetBestiaryMenu()
    {
        for(int i = 0; i < enemyContainers.Length; i++)
        {
            if(enemyContainers[i].childCount > 0)
            {
                foreach(BestiaryButton button in enemyContainers[i].GetComponentsInChildren<BestiaryButton>())
                {
                    Destroy(button.gameObject);
                }
            }
        }
    }

    public void SetAllMonsters()
    {
        foreach(BestiaryButton bestiaryButton in enemyContainerHolder.GetComponentsInChildren<BestiaryButton>(true))
        {
            bestiaryButton.transform.SetParent(enemyHolder, false);
        }
    }

    public void ResetAllMonsters()
    {
        foreach(BestiaryButton bestiaryButton in enemyHolder.GetComponentsInChildren<BestiaryButton>(true))
        {
            switch (bestiaryButton._EnemyStats.worldLocation)
            {
                case WorldLocation.Forest:
                    bestiaryButton.transform.SetParent(EnemyContainers[0], true);
                    break;
                case WorldLocation.Desert:
                    bestiaryButton.transform.SetParent(EnemyContainers[1], true);
                    break;
                case WorldLocation.Arctic:
                    bestiaryButton.transform.SetParent(EnemyContainers[2], true);
                    break;
                case WorldLocation.Graveyard:
                    bestiaryButton.transform.SetParent(EnemyContainers[3], true);
                    break;
                case WorldLocation.Castle:
                    bestiaryButton.transform.SetParent(EnemyContainers[4], true);
                    break;
            }
        }
    }

    public void ClearEnemyList()
    {
        enemies.Clear();
    }

    public void SetCurrentSelectable(Selectable select)
    {
        currentSelectable = select;
    }

    public void SetButtonNavigations(Transform container)
    {
        enemyContainer = container;

        if(enemyContainer.childCount > 0)
        {
            Navigation forestNav = forestSelectable.navigation;
            Navigation desertNav = desertSelectable.navigation;
            Navigation arcticNav = arcticSelectable.navigation;
            Navigation graveyardNav = graveyardSelectable.navigation;
            Navigation castleNav = castleSelectable.navigation;

            forestNav.selectOnDown = enemyContainer.GetChild(0).GetComponent<Selectable>();
            desertNav.selectOnDown = enemyContainer.GetChild(0).GetComponent<Selectable>();
            arcticNav.selectOnDown = enemyContainer.GetChild(0).GetComponent<Selectable>();
            graveyardNav.selectOnDown = enemyContainer.GetChild(0).GetComponent<Selectable>();
            castleNav.selectOnDown = enemyContainer.GetChild(0).GetComponent<Selectable>();

            for (int i = 0; i < enemyContainer.childCount; i++)
            {
                Navigation nav = enemyContainer.GetChild(i).GetComponent<Selectable>().navigation;

                nav.selectOnUp = null;
                nav.selectOnDown = null;

                if (i == 0)
                {
                    if (enemyContainer.childCount > 1)
                    {
                        nav.selectOnDown = enemyContainer.GetChild(i + 1).GetComponent<Selectable>();
                    }

                    nav.selectOnUp = currentSelectable;
                }
                else if (i >= enemyContainer.childCount - 1)
                {
                    nav.selectOnDown = enemyContainer.GetChild(0).GetComponent<Selectable>();
                    nav.selectOnUp = enemyContainer.GetChild(i - 1).GetComponent<Selectable>();
                }
                else
                {
                    nav.selectOnDown = enemyContainer.GetChild(i + 1).GetComponent<Selectable>();
                    nav.selectOnUp = enemyContainer.GetChild(i - 1).GetComponent<Selectable>();
                }

                enemyContainer.GetChild(i).GetComponent<Selectable>().navigation = nav;
            }

            forestSelectable.navigation = forestNav;
            desertSelectable.navigation = desertNav;
            arcticSelectable.navigation = arcticNav;
            graveyardSelectable.navigation = graveyardNav;
            castleSelectable.navigation = castleNav;
        }
    }

    public void DisableEnemyInformation()
    {
        enemyInfoPanel.SetActive(false);
        menuCard.gameObject.SetActive(false);
        sticker.gameObject.SetActive(false);

        enemyInfoMenuCard.HideCardParticle();
        enemyInfoSticker.HideStickerParticle();

        InputManager.instance.SetSelectedObject(null);
    }

    public void FocusItem(RectTransform bestiaryButtton)
    {
        if (InputManager.instance.ControllerPluggedIn)
        {
            if(scrollRect.gameObject.activeSelf)
            {
                StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(scrollRect, bestiaryButtton, 3f));
            }
            else if (desertScroll.gameObject.activeSelf)
            {
                StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(desertScroll, bestiaryButtton, 3f));
            }
            else if (arcticScroll.gameObject.activeSelf)
            {
                StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(arcticScroll, bestiaryButtton, 3f));
            }
            else if (cemeteryScroll.gameObject.activeSelf)
            {
                StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(cemeteryScroll, bestiaryButtton, 3f));
            }
            else if (castleScroll.gameObject.activeSelf)
            {
                StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(castleScroll, bestiaryButtton, 3f));
            }
        }
    }

    public void UpdateBestiaryCount(Transform worldContainer)
    {
        var forestMons = Resources.LoadAll("Enemies/ForestWorld");
        var desertMons = Resources.LoadAll("Enemies/DesertWorld");
        var arcticMons = Resources.LoadAll("Enemies/ArcticWorld");
        var graveMons = Resources.LoadAll("Enemies/GraveyardWorld");
        var castleMons = Resources.LoadAll("Enemies/CastleWorld");

        if (scrollRect.gameObject.activeSelf)
        {
            bestiaryCountText.text = worldContainer.childCount + "/" + forestMons.Length;
        }
        else if(desertScroll.gameObject.activeSelf)
        {
            bestiaryCountText.text = worldContainer.childCount + "/" + desertMons.Length;
        }
        else if(arcticScroll.gameObject.activeSelf)
        {
            bestiaryCountText.text = worldContainer.childCount + "/" + arcticMons.Length;
        }
        else if(cemeteryScroll.gameObject.activeSelf)
        {
            bestiaryCountText.text = worldContainer.childCount + "/" + graveMons.Length;
        }
        else if(castleScroll.gameObject.activeSelf)
        {
            bestiaryCountText.text = worldContainer.childCount + "/" + castleMons.Length;
        }
    }

    public void CheckBestiaryAchievement()
    {
        if(SteamManager.Initialized)
        {
            var forestMons = Resources.LoadAll("Enemies/ForestWorld");
            var desertMons = Resources.LoadAll("Enemies/DesertWorld");
            var arcticMons = Resources.LoadAll("Enemies/ArcticWorld");
            var graveMons = Resources.LoadAll("Enemies/GraveyardWorld");
            var castleMons = Resources.LoadAll("Enemies/CastleWorld");

            int totalMons = forestMons.Length + desertMons.Length + arcticMons.Length + graveMons.Length + castleMons.Length;
            int currentMos = enemyContainers[0].childCount + enemyContainers[1].childCount + enemyContainers[2].childCount + enemyContainers[3].childCount + enemyContainers[4].childCount;

            SteamUserStats.GetAchievement("ACH_BANE_OF_BEASTS", out bool achievementCompleted);

            if (!achievementCompleted)
            {
                if (currentMos >= totalMons)
                {
                    SteamUserStats.SetAchievement("ACH_BANE_OF_BEASTS");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }

    public void SetDefaultBestiaryButton()
    {
        scrollRect.gameObject.SetActive(true);
        desertScroll.gameObject.SetActive(false);
        arcticScroll.gameObject.SetActive(false);
        cemeteryScroll.gameObject.SetActive(false);
        castleScroll.gameObject.SetActive(false);
    }
}
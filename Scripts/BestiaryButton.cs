using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestiaryButton : MonoBehaviour
{
    [SerializeField]
    private BestiaryMenu bestiaryMenu;

    [SerializeField]
    private MenuCard menuCard;

    [SerializeField]
    private Sticker sticker;

    [SerializeField]
    private TextMeshProUGUI enemyNameText;

    [SerializeField]
    private EnemyStats enemyStats;

    public EnemyStats _EnemyStats
    {
        get
        {
            return enemyStats;
        }
        set
        {
            enemyStats = value;
        }
    }

    public TextMeshProUGUI EnemyNameText => enemyNameText;

    public void ShowEnemyInfo()
    {
        bestiaryMenu.EnemyInfoPanel.SetActive(true);

        menuCard.gameObject.SetActive(false);
        sticker.gameObject.SetActive(false);

        bestiaryMenu.EnemyNameText.text = enemyStats.enemyName;

        if(enemyStats.cardDrop != null || enemyStats.stickerDrop != null)
        {
            Navigation nav = gameObject.GetComponent<Selectable>().navigation;

            if (enemyStats.dropsCard)
            {
                menuCard.gameObject.SetActive(true);

                menuCard._cardTemplate = enemyStats.cardDrop;

                menuCard.UpdateCardInformation();

                Navigation cardNav = menuCard.GetComponent<Selectable>().navigation;

                cardNav.selectOnLeft = gameObject.GetComponent<Selectable>();

                menuCard.GetComponent<Selectable>().navigation = cardNav;

                nav.selectOnRight = menuCard.GetComponent<Selectable>();

                gameObject.GetComponent<Selectable>().navigation = nav;
            }
            else
            {
                if (enemyStats.stickerDrop != null)
                {
                    sticker.gameObject.SetActive(true);

                    sticker._stickerInformation = enemyStats.stickerDrop;

                    sticker.SetUpStickerInformation();

                    Navigation stickerNav = sticker.GetComponent<Selectable>().navigation;

                    stickerNav.selectOnLeft = gameObject.GetComponent<Selectable>();

                    sticker.GetComponent<Selectable>().navigation = stickerNav;

                    nav.selectOnRight = sticker.GetComponent<Selectable>();

                    gameObject.GetComponent<Selectable>().navigation = nav;
                }
            }

            bestiaryMenu.EnemyInfoText.text = "EXP: " + enemyStats.exp + "\n" + "Gold: " + enemyStats.money + "\n\n" + "Drops: " + "       " + enemyStats.dropChance + "%";
        }
        else
        {
            bestiaryMenu.EnemyInfoText.text = "EXP: " + enemyStats.exp + "\n" + "Gold: " + enemyStats.money + "\n\n" + "Drops: Nothing";
        }

        bestiaryMenu.EnemyHealthText.text = enemyStats.health.ToString();
        bestiaryMenu.EnemyStrengthText.text = enemyStats.strength.ToString();
        bestiaryMenu.EnemyDefenseText.text = enemyStats.defense.ToString();
    }
}
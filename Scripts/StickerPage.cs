using UnityEngine;
using TMPro;

public class StickerPage : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] stickerPages, stickerCountPanelPages;

    [SerializeField]
    private TextMeshProUGUI[] stickerCountTexts;

    [SerializeField]
    private Transform stickerParent;

    [SerializeField]
    private TextMeshProUGUI pageText;

    [SerializeField]
    private int pageIndex;

    public void IncrementPageIndex()
    {
        pageIndex++;

        if (pageIndex > stickerPages.Length - 1)
        {
            pageIndex = 0;
        }

        if(pageIndex > 0)
        {
            stickerPages[pageIndex - 1].gameObject.SetActive(false);
            stickerCountPanelPages[pageIndex - 1].gameObject.SetActive(false);
        }
        else
        {
            stickerPages[stickerPages.Length - 1].gameObject.SetActive(false);
            stickerCountPanelPages[stickerPages.Length - 1].gameObject.SetActive(false);
        }

        stickerPages[pageIndex].gameObject.SetActive(true);
        stickerCountPanelPages[pageIndex].gameObject.SetActive(true);

        switch (pageIndex)
        {
            case (0):
                pageText.text = "ALL";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (1):
                pageText.text = "0 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 0)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (2):
                pageText.text = "1 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 1)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (3):
                pageText.text = "2 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 2)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (4):
                pageText.text = "3 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 3)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (5):
                pageText.text = "4 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 4)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (6):
                pageText.text = "5 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 5)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (7):
                pageText.text = "6 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 6)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (8):
                pageText.text = "7 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 7)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (9):
                pageText.text = "8 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 8)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (10):
                pageText.text = "9 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 9)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (11):
                pageText.text = "10 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 10)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
        }
    }

    public void DecrementPageIndex()
    {
        pageIndex--;

        if (pageIndex < 0)
        {
            pageIndex = stickerPages.Length - 1;
        }

        if(pageIndex < stickerPages.Length - 1)
        {
            stickerPages[pageIndex + 1].gameObject.SetActive(false);
            stickerCountPanelPages[pageIndex + 1].gameObject.SetActive(false);
        }
        else
        {
            stickerPages[0].gameObject.SetActive(false);
            stickerCountPanelPages[0].gameObject.SetActive(false);
        }

        stickerPages[pageIndex].gameObject.SetActive(true);
        stickerCountPanelPages[pageIndex].gameObject.SetActive(true);

        switch (pageIndex)
        {
            case (0):
                pageText.text = "ALL";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (1):
                pageText.text = "0 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 0)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (2):
                pageText.text = "1 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 1)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (3):
                pageText.text = "2 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 2)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (4):
                pageText.text = "3 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 3)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (5):
                pageText.text = "4 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 4)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (6):
                pageText.text = "5 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 5)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (7):
                pageText.text = "6 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 6)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (8):
                pageText.text = "7 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 7)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (9):
                pageText.text = "8 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 8)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (10):
                pageText.text = "9 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 9)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
            case (11):
                pageText.text = "10 SP";
                foreach (Sticker sticker in stickerParent.GetComponentsInChildren<Sticker>(true))
                {
                    if (sticker._stickerInformation.stickerCost == 10)
                        sticker.transform.SetParent(stickerPages[pageIndex].transform);
                }
                UpdateStickerCountText();
                break;
        }
    }

    public void UpdateStickerCountText()
    {
        switch(pageIndex)
        {
            case 0:
                var allStickers = Resources.LoadAll("Stickers/");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + (allStickers.Length - 1);
                break;
            case 1:
                var zeroCostStickers = Resources.LoadAll("Stickers/ZeroCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + zeroCostStickers.Length;
                break;
            case 2:
                var oneCostStickers = Resources.LoadAll("Stickers/OneCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + oneCostStickers.Length;
                break;
            case 3:
                var twoCostStickers = Resources.LoadAll("Stickers/TwoCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + twoCostStickers.Length;
                break;
            case 4:
                var threeCostStickers = Resources.LoadAll("Stickers/ThreeCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + threeCostStickers.Length;
                break;
            case 5:
                var fourCostStickers = Resources.LoadAll("Stickers/FourCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + fourCostStickers.Length;
                break;
            case 6:
                var fiveCostStickers = Resources.LoadAll("Stickers/FiveCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + fiveCostStickers.Length;
                break;
            case 7:
                var sixCostStickers = Resources.LoadAll("Stickers/SixCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + sixCostStickers.Length;
                break;
            case 8:
                var sevenCostStickers = Resources.LoadAll("Stickers/SevenCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + sevenCostStickers.Length;
                break;
            case 9:
                var eightCostStickers = Resources.LoadAll("Stickers/EightCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + eightCostStickers.Length;
                break;
            case 10:
                var nineCostStickers = Resources.LoadAll("Stickers/NineCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + nineCostStickers.Length;
                break;
            case 11:
                var tenCostStickers = Resources.LoadAll("Stickers/TenCost");
                stickerCountTexts[pageIndex].text = stickerPages[pageIndex].childCount + "/" + tenCostStickers.Length;
                break;
        }
    }

    public void ResetStickerPage()
    {
        pageIndex = 0;

        pageText.text = "ALL";

        var allStickers = Resources.LoadAll("Stickers/");
        stickerCountTexts[0].text = stickerPages[0].childCount + "/" + (allStickers.Length - 1);

        foreach (RectTransform trans in stickerPages)
        {
            trans.gameObject.SetActive(false);
        }

        foreach(RectTransform trans in stickerCountPanelPages)
        {
            trans.gameObject.SetActive(false);
        }

        stickerPages[0].gameObject.SetActive(true);
        stickerCountPanelPages[0].gameObject.SetActive(true);
    }

    public Transform SetDefaultStickerCategory(Sticker sticker)
    {
        Transform trans = sticker.transform;

        trans = stickerPages[0].transform;

        sticker.HideStickerParticle();
        sticker.HideStickerPanel();

        sticker.transform.SetParent(trans, false);

        return trans;
    }

    public Transform SetStickerCategory(Sticker sticker)
    {
        Transform trans = sticker.transform;

        if(stickerPages[0].gameObject.activeSelf)
        {
            trans = stickerPages[0].transform;
        }
        else
        {
            switch (sticker._stickerInformation.stickerCost)
            {
                case (0):
                    trans = stickerPages[1].transform;
                    break;
                case (1):
                    trans = stickerPages[2].transform;
                    break;
                case (2):
                    trans = stickerPages[3].transform;
                    break;
                case (3):
                    trans = stickerPages[4].transform;
                    break;
                case (4):
                    trans = stickerPages[5].transform;
                    break;
                case (5):
                    trans = stickerPages[6].transform;
                    break;
                case (6):
                    trans = stickerPages[7].transform;
                    break;
                case (7):
                    trans = stickerPages[8].transform;
                    break;
                case (8):
                    trans = stickerPages[9].transform;
                    break;
                case (9):
                    trans = stickerPages[10].transform;
                    break;
                case (10):
                    trans = stickerPages[11].transform;
                    break;
                case (11):
                    trans = stickerPages[12].transform;
                    break;
                case (12):
                    trans = stickerPages[13].transform;
                    break;
                case (13):
                    trans = stickerPages[14].transform;
                    break;
                case (14):
                    trans = stickerPages[15].transform;
                    break;
                case (15):
                    trans = stickerPages[16].transform;
                    break;
            }
        }

        sticker.HideStickerParticle();
        sticker.HideStickerPanel();

        sticker.transform.SetParent(trans, false);

        return trans;
    }

    public int GetStickerPageChildCount()
    {
        return stickerPages[0].childCount;
    }
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopInformation", menuName = "ScriptableObjects/ShopInfo", order = 1)]
public class ShopInformation : ScriptableObject
{
    [Header("Starting Forest Town Shop")]
    public List<CardTemplate> startingCardShopForest;

    public List<StickerInformation> startingStickerShopForest;

    [Header("Starting Desert Town Shop")]
    public List<CardTemplate> startingCardShopDesert;

    public List<StickerInformation> startingStickerShopDesert;

    [Header("Starting Arctic Town Shop")]
    public List<CardTemplate> startingCardShopArctic;

    public List<StickerInformation> startingStickerShopArctic;

    [Header("Starting Graveyard Town Shop")]
    public List<CardTemplate> startingCardShopGraveyard;

    public List<StickerInformation> startingStickerShopGraveyard;

    [Header("Starting Castle Shop")]
    public List<CardTemplate> startingCardShopCastle;

    public List<StickerInformation> startingStickerShopCastle;

    [Header("Forest Town Shop")]
    public List<CardTemplate> cardShopForest;

    public List<StickerInformation> stickerShopForest;

    [Header("Desert Town Shop")]
    public List<CardTemplate> cardShopDesert;

    public List<StickerInformation> stickerShopDesert;

    [Header("Arctic Town Shop")]
    public List<CardTemplate> cardShopArctic;

    public List<StickerInformation> stickerShopArctic;

    [Header("Graveyard Town Shop")]
    public List<CardTemplate> cardShopGraveyard;

    public List<StickerInformation> stickerShopGraveyard;

    [Header("Castle Shop")]
    public List<CardTemplate> cardShopCastle;

    public List<StickerInformation> stickerShopCastle;
}
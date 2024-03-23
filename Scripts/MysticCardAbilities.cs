using UnityEngine;

public class MysticCardAbilities : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private Card card;

    public void CheckMysticCardEffect()
    {
        switch(card._cardTemplate.cardEffect)
        {
            case (CardEffect.ReturnCard):
                battleSystem.OpenGraveyard(card);
                break;
            case (CardEffect.AddCard):
                battleSystem.OpenDeck(card);
                break;
            case (CardEffect.CopyCard):
                battleSystem.OpenHandPanel(card);
                break;
        }
    }
}
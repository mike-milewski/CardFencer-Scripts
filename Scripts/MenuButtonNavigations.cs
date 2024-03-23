using UnityEngine;
using UnityEngine.UI;

public enum SelectableDirection { UP, DOWN, RIGHT, LEFT}

[System.Serializable]
public class Navigations
{
    [SerializeField]
    private Selectable selectableToAdd;

    [SerializeField]
    private SelectableDirection selectableDirection;

    public SelectableDirection _SelectableDirection => selectableDirection;

    public Selectable SelectableToAdd
    {
        get => selectableToAdd;
        set => selectableToAdd = value;
    }
}

public class MenuButtonNavigations : MonoBehaviour
{
    [SerializeField]
    private Navigations[] navigations;

    [SerializeField]
    private Selectable selectable;

    public void ChangeSelectableButtons()
    {
        for(int i = 0; i < MenuController.instance.ButtonAnimators.Length; i++)
        {
            Button menuButton = MenuController.instance.ButtonAnimators[i].GetComponent<Button>();

            for(int j = 0; j < navigations.Length; j++)
            {
                if(menuButton.interactable && menuButton.GetComponent<MenuButtonNavigations>())
                {
                    if(navigations[j].SelectableToAdd.GetComponent<Button>().interactable)
                    {
                        Navigation nav = selectable.navigation;

                        switch(navigations[j]._SelectableDirection)
                        {
                            case (SelectableDirection.UP):
                                nav.selectOnUp = navigations[j].SelectableToAdd;
                                break;
                            case (SelectableDirection.DOWN):
                                nav.selectOnDown = navigations[j].SelectableToAdd;
                                break;
                            case (SelectableDirection.RIGHT):
                                nav.selectOnRight = navigations[j].SelectableToAdd;
                                break;
                            case (SelectableDirection.LEFT):
                                nav.selectOnLeft = navigations[j].SelectableToAdd;
                                break;
                        }

                        selectable.navigation = nav;
                    }
                }
            }
        }
    }

    public void ChangeWorldSelectableButtons()
    {
        for (int i = 0; i < navigations.Length; i++)
        {
            if (navigations[i].SelectableToAdd.GetComponent<Button>().interactable)
            {
                Navigation nav = selectable.navigation;

                switch (navigations[i]._SelectableDirection)
                {
                    case (SelectableDirection.UP):
                        nav.selectOnUp = navigations[i].SelectableToAdd;
                        break;
                    case (SelectableDirection.DOWN):
                        nav.selectOnDown = navigations[i].SelectableToAdd;
                        break;
                    case (SelectableDirection.RIGHT):
                        nav.selectOnRight = navigations[i].SelectableToAdd;
                        break;
                    case (SelectableDirection.LEFT):
                        nav.selectOnLeft = navigations[i].SelectableToAdd;
                        break;
                }

                selectable.navigation = nav;
            }
        }
    }
}
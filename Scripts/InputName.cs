using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputName : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private Animator errorMessageAnimator;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameObject fakeConfirmNameButton;

    [SerializeField]
    private TextMeshProUGUI errorMessageText;

    [SerializeField]
    private TMP_InputField nameText;

    [SerializeField]
    private Selectable nameInputField, cancelNameSelectable;

    public void CheckButton()
    {
        if (!string.IsNullOrEmpty(nameText.text))
        {
            fakeConfirmNameButton.SetActive(false);

            SetTextFieldNavigation(false);
        }
        else
        {
            fakeConfirmNameButton.SetActive(true);

            SetTextFieldNavigation(true);
        }
    }

    public void CheckNameText(SceneNameToLoad sceneName)
    {
        canvasGroup.interactable = false;

        ApplyName();

        sceneName.FadeOutScene();
    }

    private void ApplyName()
    {
        mainCharacterStats.playerName = nameText.text;
    }

    private void SetTextFieldNavigation(bool emptyField)
    {
        if(emptyField)
        {
            Navigation nav = nameInputField.navigation;

            nav.selectOnUp = fakeConfirmNameButton.GetComponent<Button>();
            nav.selectOnDown = fakeConfirmNameButton.GetComponent<Button>();

            nameInputField.navigation = nav;

            Navigation navTwo = cancelNameSelectable.navigation;

            navTwo.selectOnLeft = fakeConfirmNameButton.GetComponent<Button>();
            navTwo.selectOnRight = fakeConfirmNameButton.GetComponent<Button>();

            cancelNameSelectable.navigation = navTwo;

            InputManager.instance.CurrentSelectedObject = fakeConfirmNameButton;
        }
        else
        {
            Navigation nav = nameInputField.navigation;

            nav.selectOnUp = GetComponent<Button>();
            nav.selectOnDown = GetComponent<Button>();

            nameInputField.navigation = nav;

            Navigation navTwo = cancelNameSelectable.navigation;

            navTwo.selectOnLeft = GetComponent<Button>();
            navTwo.selectOnRight = GetComponent<Button>();

            cancelNameSelectable.navigation = navTwo;

            InputManager.instance.CurrentSelectedObject = gameObject;
        }
    }

    public void ErrorMessage(string message)
    {
        errorMessageText.text = message;

        errorMessageAnimator.Play("Message", -1, 0);
    }
}
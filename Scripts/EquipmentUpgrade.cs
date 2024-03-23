using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EquipmentUpgrade : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private MenuButtonNavigations weaponPanelNavigation, shieldPanelNavigation;

    [SerializeField]
    private Button confirmButton;

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private PlayerFieldSwordAndShield fieldSwordAndShield;

    [SerializeField]
    private Animator fadedBackgroundPanelAnimator;

    [SerializeField]
    private Image weaponImage, shieldImage;

    [SerializeField]
    private TextMeshProUGUI weaponNameText, shieldNameText, weaponInformationText, shieldInformationText, confirmButtonText;

    [SerializeField]
    private Sprite[] weaponSprites, shieldSprites;

    [SerializeField]
    private GameObject selectedWeapon, selectedShield, weaponPanel, shieldPanel;

    [SerializeField]
    private int[] strengthIncrements, defenseIncrements;

    int weaponRank, shieldRank;

    private bool playedPanelAnimation, choseWeaponUpgrade;

    public bool ChoseWeaponUpgrade
    {
        get
        {
            return choseWeaponUpgrade;
        }
        set
        {
            choseWeaponUpgrade = value;
        }
    }

    public void SetUpgradeInformation()
    {
        weaponRank = playerMenuInfo.weaponIndex + 1;
        shieldRank = playerMenuInfo.shieldIndex + 1;

        if (weaponRank >= playerMenuInfo.weaponSprite.Length && shieldRank >= playerMenuInfo.shieldSprite.Length) return;

        DisableConfirmButton();

        if(weaponRank >= playerMenuInfo.weaponSprite.Length)
        {
            weaponPanel.SetActive(false);
        }
        else
        {
            weaponImage.sprite = weaponSprites[weaponRank];
            weaponNameText.text = playerMenuInfo.weaponName[weaponRank];

            if(weaponRank == 1)
            {
                weaponInformationText.text = "Strength +" + strengthIncrements[weaponRank] + "\n<size=8>Destroys mid-tier blockades.";
            }
            else if(weaponRank == 2)
            {
                weaponInformationText.text = "Strength +" + strengthIncrements[weaponRank] + "\n<size=8>Destroys high-tier blockades.";
            }
        }

        if(shieldRank >= playerMenuInfo.shieldSprite.Length)
        {
            shieldPanel.SetActive(false);
        }
        else
        {
            shieldImage.sprite = shieldSprites[shieldRank];
            shieldNameText.text = playerMenuInfo.shieldName[shieldRank];

            if(shieldRank == 1)
            {
                shieldInformationText.text = "Defense +" + defenseIncrements[shieldRank] + "\n<size=8>Moves mid-tier blockades.";
            }
            else if(shieldRank == 2)
            {
                shieldInformationText.text = "Defense +" + defenseIncrements[shieldRank] + "\n<size=8>Moves high-tier blockades.";
            }
        }

        weaponPanelNavigation.ChangeSelectableButtons();
        shieldPanelNavigation.ChangeSelectableButtons();

        StartCoroutine(EquipUpgradeRoutine());
    }

    private IEnumerator EquipUpgradeRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        DisableControls();

        yield return new WaitForSeconds(1);

        ShowFadedBackground();
    }

    private void DisableConfirmButton()
    {
        confirmButton.interactable = false;

        confirmButton.GetComponent<Animator>().SetTrigger("Disabled");
        confirmButton.GetComponent<Animator>().Play("Disabled");

        confirmButtonText.color = new Color(1, 1, 1, 0.5f);
    }

    public void EnableConfirmButton()
    {
        confirmButton.interactable = true;

        confirmButton.GetComponent<Animator>().SetTrigger("Normal");
        confirmButton.GetComponent<Animator>().Play("Normal");

        confirmButtonText.color = new Color(1, 1, 1, 1);

        SetEquipmentPanelNavigations();
    }

    private void SetEquipmentPanelNavigations()
    {
        Navigation weaponNav = weaponPanel.GetComponent<Selectable>().navigation;
        Navigation shieldNav = shieldPanel.GetComponent<Selectable>().navigation;
        Navigation confirmNav = confirmButton.GetComponent<Selectable>().navigation;

        if(weaponPanel.activeSelf)
        {
            confirmNav.selectOnUp = weaponPanel.GetComponent<Selectable>();
        }
        else if(shieldPanel.activeSelf)
        {
            confirmNav.selectOnUp = weaponPanel.GetComponent<Selectable>();
        }

        weaponNav.selectOnDown = confirmButton;
        shieldNav.selectOnDown = confirmButton;

        weaponPanel.GetComponent<Selectable>().navigation = weaponNav;
        shieldPanel.GetComponent <Selectable>().navigation = shieldNav;
        confirmButton.GetComponent <Selectable>().navigation = confirmNav;
    }

    public void EnablePanelParticle(ParticleSystem particleEffect)
    {
        particleEffect.gameObject.SetActive(true);

        if(!particleEffect.isPlaying)
        {
            particleEffect.Play();
        }
    }

    public void DisablePanelParticle(ParticleSystem particleEffect)
    {
        if (particleEffect.isPlaying)
        {
            particleEffect.Stop();
        }

        particleEffect.gameObject.SetActive(false);
    }

    private void DisableControls()
    {
        cameraFollow.enabled = false;
        playerMovement.enabled = false;
        fieldSwordAndShield.enabled = false;

        MenuController.instance.CanToggleMenu = false;
    }

    public void EnableControls()
    {
        if(playedPanelAnimation)
        {
            cameraFollow.enabled = true;
            playerMovement.enabled = true;
            fieldSwordAndShield.enabled = true;

            MenuController.instance.CanToggleMenu = true;
            GameManager.instance.IsBossFight = false;
        }
    }

    private void ShowFadedBackground()
    {
        fadedBackgroundPanelAnimator.Play("Show");
    }

    public void PlayOpenMenuSoundEffect()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);
    }

    public void AttachObjectToInputManager()
    {
        if(!playedPanelAnimation)
        {
            if (weaponPanel.activeSelf)
            {
                InputManager.instance.SetSelectedObject(selectedWeapon);
            }
            else
            {
                InputManager.instance.SetSelectedObject(selectedShield);
            }

            if (!InputManager.instance.ControllerPluggedIn)
            {
                InputManager.instance.ForceCursorOn = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            playedPanelAnimation = true;
        }
    }

    private void HideFadedBackground()
    {
        fadedBackgroundPanelAnimator.Play("Hide");
    }

    public void UpgradeEquipment()
    {
        if(choseWeaponUpgrade)
        {
            mainCharacterStats.strength += strengthIncrements[weaponRank];

            playerMenuInfo.weaponIndex++;
        }
        else
        {
            mainCharacterStats.defense += defenseIncrements[shieldRank];

            playerMenuInfo.shieldIndex++;
        }

        GameManager.instance.SetPlayerFieldEquipment();

        HideFadedBackground();
        DisableConfirmButton();

        InputManager.instance.ForceCursorOn = false;

        if (!InputManager.instance.ControllerPluggedIn)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        InputManager.instance.SetSelectedObject(null);

        MenuController.instance.UpdatePlayerEquipment();
    }
}
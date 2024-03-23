using System.Collections;
using UnityEngine;

public enum EquipSymbol { VeteranSword, EliteSword, VeteranShield, EliteShield };

public class SecretSymbol : MonoBehaviour
{
    [SerializeField]
    private EquipSymbol equipSymbol;

    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private Light pointLight;

    [SerializeField]
    private MeshRenderer symbolMeshRenderer, availableEquipmentRenderer;

    [SerializeField]
    private Material veteranSwordMaterial, eliteSwordMaterial, veteranShieldMaterial, eliteShieldMaterial, interactableMaterial, uninteractableMaterial;

    private Coroutine lightOnRoutine, lightOffRoutine;

    private void Awake()
    {
        switch (equipSymbol)
        {
            case EquipSymbol.VeteranSword:
                symbolMeshRenderer.material = veteranSwordMaterial;
                CheckWeaponRank(1);
                break;
            case EquipSymbol.EliteSword:
                symbolMeshRenderer.material = eliteSwordMaterial;
                CheckWeaponRank(2);
                break;
            case EquipSymbol.VeteranShield:
                symbolMeshRenderer.material = veteranShieldMaterial;
                CheckShieldRank(1);
                break;
            case EquipSymbol.EliteShield:
                symbolMeshRenderer.material = eliteShieldMaterial;
                CheckShieldRank(2);
                break;
        }

        if (worldEnvironmentData.changedDay)
        {
            pointLight.gameObject.SetActive(true);

            pointLight.range = 3.5f;
        }
        else
        {
            pointLight.range = 0;

            pointLight.gameObject.SetActive(true);
        }
    }

    private void CheckWeaponRank(int rank)
    {
        if(playerMenuInfo.weaponIndex < rank)
        {
            availableEquipmentRenderer.material = uninteractableMaterial;
        }
        else
        {
            availableEquipmentRenderer.material = interactableMaterial;
        }
    }

    private void CheckShieldRank(int rank)
    {
        if (playerMenuInfo.shieldIndex < rank)
        {
            availableEquipmentRenderer.material = uninteractableMaterial;
        }
        else
        {
            availableEquipmentRenderer.material = interactableMaterial;
        }
    }

    public void TurnOnLight()
    {
        if (lightOnRoutine != null)
        {
            StopCoroutine(lightOnRoutine);
        }

        lightOnRoutine = null;

        lightOnRoutine = StartCoroutine("EnableLight");
    }

    public void TurnOffLight()
    {
        if (lightOffRoutine != null)
        {
            StopCoroutine(lightOffRoutine);
        }

        lightOffRoutine = null;

        lightOffRoutine = StartCoroutine("DisableLight");
    }

    private IEnumerator EnableLight()
    {
        float t = 0;

        pointLight.enabled = true;

        pointLight.range = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            if (pointLight.range < 3.5f)
            {
                pointLight.range += 5 * Time.deltaTime;
            }

            yield return null;
        }
        pointLight.range = 3.5f;
    }

    private IEnumerator DisableLight()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            pointLight.range -= 5 * Time.deltaTime;

            yield return null;
        }
        pointLight.enabled = false;
    }

    public void StopLightEnableRoutine()
    {
        if(lightOnRoutine != null)
        {
            StopCoroutine(lightOnRoutine);

            lightOnRoutine = null;

            pointLight.enabled = true;

            pointLight.range = 3.5f;
        }
    }

    public void StopLightDisableRoutine()
    {
        if(lightOffRoutine != null)
        {
            StopCoroutine(lightOffRoutine);

            lightOffRoutine = null;

            pointLight.range = 0;

            pointLight.enabled = false;
        }
    }
}
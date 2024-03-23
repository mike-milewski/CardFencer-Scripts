using System.Collections;
using UnityEngine;

public enum HealType { HP, CP };

public class HealingFieldItem : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private GameObject lightObjectHolder;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private HealType healType;

    [SerializeField]
    private MeshRenderer meshRenderer, backFaceMeshRenderer;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private Material[] healingObjectMaterials;

    public HealType _HealType => healType;

    private void OnEnable()
    {
        rigidBody.AddForce(Vector3.up * 7f, ForceMode.Impulse);

        rigidBody.velocity = new Vector3(Random.Range(3, -3), 0, 0);

        boxCollider.enabled = false;

        StartCoroutine("ReEnableCollider");

        RandomizeHealingObject();
    }

    private IEnumerator ReEnableCollider()
    {
        yield return new WaitForSeconds(1);
        boxCollider.enabled = true;
    }

    public void RandomizeHealingObject()
    {
        int rand = Random.Range(0, healingObjectMaterials.Length);

        if(rand == 0)
        {
            healType = HealType.HP;
        }
        else
        {
            healType = HealType.CP;
        }

        meshRenderer.material = healingObjectMaterials[rand];
        backFaceMeshRenderer.material = healingObjectMaterials[rand];
    }

    public void Recover()
    {
        if(healType == HealType.HP)
        {
            mainCharacterStats.currentPlayerHealth++;
            if(mainCharacterStats.currentPlayerHealth > mainCharacterStats.maximumHealth)
            {
                mainCharacterStats.currentPlayerHealth = mainCharacterStats.maximumHealth;
            }
        }
        else
        {
            mainCharacterStats.currentPlayerCardPoints++;
            if (mainCharacterStats.currentPlayerCardPoints > mainCharacterStats.maximumCardPoints)
            {
                mainCharacterStats.currentPlayerCardPoints = mainCharacterStats.maximumCardPoints;
            }
        }
    }

    public void EnableLights()
    {
        lightObjectHolder.SetActive(true);
    }
}
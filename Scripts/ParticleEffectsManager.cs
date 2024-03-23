using UnityEngine;

public class ParticleEffectsManager : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private ParticleSystem hpParticle, cpParticle, strengthUpParticle, defenseUpParticle, strengthDownParticle, defenseDownParticle, healParticle, hpRegenParticle;

    public ParticleSystem HpParticle => hpParticle;

    public ParticleSystem CpParticle => cpParticle;

    public void PlayParticle(ParticleSystem particle, Vector3 pos)
    {
        particle.gameObject.SetActive(true);
        particle.gameObject.transform.position = pos;
        particle.Play();
    }

    public void SpawnParticle(string particleName, Vector3 pos, bool multiTarget)
    {
        if(!multiTarget)
        {
            switch (particleName)
            {
                case ("StrengthUp"):
                    strengthUpParticle.gameObject.SetActive(true);
                    strengthUpParticle.Play();
                    if (!strengthUpParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        strengthUpParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    strengthUpParticle.transform.position = pos;
                    break;
                case ("StrengthDown"):
                    strengthDownParticle.gameObject.SetActive(true);
                    strengthDownParticle.Play();
                    if (!strengthDownParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        strengthDownParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    strengthDownParticle.transform.position = pos;
                    break;
                case ("DefenseUp"):
                    defenseUpParticle.gameObject.SetActive(true);
                    defenseUpParticle.Play();
                    if (!defenseUpParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        defenseUpParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    defenseUpParticle.transform.position = pos;
                    break;
                case ("DefenseDown"):
                    defenseDownParticle.gameObject.SetActive(true);
                    defenseDownParticle.Play();
                    if (!defenseDownParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        defenseDownParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    defenseDownParticle.transform.position = pos;
                    break;
                case ("Mend"):
                    healParticle.gameObject.SetActive(true);
                    healParticle.Play();
                    if(!healParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        healParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    healParticle.transform.position = pos;
                    break;
                case ("Cure"):
                    healParticle.gameObject.SetActive(true);
                    healParticle.Play();
                    if (!healParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        healParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    healParticle.transform.position = pos;
                    break;
                case ("HpRegen"):
                    hpRegenParticle.gameObject.SetActive(true);
                    hpRegenParticle.Play();
                    if(!hpRegenParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        hpRegenParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    hpRegenParticle.transform.position = pos;
                    break;
                case ("CrackedShield"):
                    defenseDownParticle.gameObject.SetActive(true);
                    defenseDownParticle.Play();
                    if (!defenseDownParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        defenseDownParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    defenseDownParticle.transform.position = pos;
                    break;
                case ("DullBlade"):
                    defenseDownParticle.gameObject.SetActive(true);
                    defenseDownParticle.Play();
                    if(!defenseDownParticle.GetComponent<AudioSource>().isPlaying)
                    {
                        defenseDownParticle.GetComponent<CardParticleAudio>().CheckAudio();
                    }
                    defenseDownParticle.transform.position = pos;
                    break;
            }
        }
        else
        {
            for(int i = 0; i < battleSystem.Enemies.Count; i++)
            {
                switch (particleName)
                {
                    case ("StrengthUp"):
                        var strUpParticle = Instantiate(strengthUpParticle);
                        strUpParticle.gameObject.SetActive(true);
                        strUpParticle.Play();
                        strUpParticle.transform.position = new Vector3(battleSystem.Enemies[i].transform.position.x, battleSystem.Enemies[i].transform.position.y + 0.7f, battleSystem.Enemies[i].transform.position.z);
                        strUpParticle.gameObject.AddComponent<DestroyParticle>();
                        strUpParticle.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
                        break;
                    case ("StrengthDown"):
                        var strDownParticle = Instantiate(strengthDownParticle);
                        strDownParticle.gameObject.SetActive(true);
                        strDownParticle.Play();
                        strDownParticle.transform.position = new Vector3(battleSystem.Enemies[i].transform.position.x, battleSystem.Enemies[i].transform.position.y + 0.7f, battleSystem.Enemies[i].transform.position.z);
                        strDownParticle.gameObject.AddComponent<DestroyParticle>();
                        strDownParticle.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
                        break;
                    case ("DefenseUp"):
                        var defUpParticle = Instantiate(defenseUpParticle);
                        defUpParticle.gameObject.SetActive(true);
                        defUpParticle.Play();
                        defUpParticle.transform.position = new Vector3(battleSystem.Enemies[i].transform.position.x, battleSystem.Enemies[i].transform.position.y + battleSystem.Enemies[i]._EnemyStats.buffOffSetY, 
                                                                       battleSystem.Enemies[i].transform.position.z);
                        defUpParticle.gameObject.AddComponent<DestroyParticle>();
                        defUpParticle.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
                        break;
                    case ("DefenseDown"):
                        var defDownParticle = Instantiate(defenseDownParticle);
                        defDownParticle.gameObject.SetActive(true);
                        defDownParticle.Play();
                        defDownParticle.transform.position = new Vector3(battleSystem.Enemies[i].transform.position.x, battleSystem.Enemies[i].transform.position.y + 0.7f, battleSystem.Enemies[i].transform.position.z);
                        defDownParticle.gameObject.AddComponent<DestroyParticle>();
                        defDownParticle.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
                        break;
                    case ("Mend"):
                        var healingParticle = Instantiate(healParticle);
                        healingParticle.gameObject.SetActive(true);
                        healingParticle.Play();
                        healingParticle.transform.position = new Vector3(battleSystem.Enemies[i].transform.position.x, battleSystem.Enemies[i].transform.position.y + 0.7f, battleSystem.Enemies[i].transform.position.z);
                        healingParticle.gameObject.AddComponent<DestroyParticle>();
                        healingParticle.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
                        break;
                    case ("HpRegen"):
                        var regenParticle = Instantiate(hpRegenParticle);
                        regenParticle.gameObject.SetActive(true);
                        regenParticle.Play();
                        regenParticle.transform.position = new Vector3(battleSystem.Enemies[i].transform.position.x, battleSystem.Enemies[i].transform.position.y + 0.7f, battleSystem.Enemies[i].transform.position.z);
                        regenParticle.gameObject.AddComponent<DestroyParticle>();
                        regenParticle.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
                        break;
                    case ("Cure"):
                        var cureParticle = Instantiate(hpRegenParticle);
                        cureParticle.gameObject.SetActive(true);
                        cureParticle.Play();
                        cureParticle.transform.position = new Vector3(battleSystem.Enemies[i].transform.position.x, battleSystem.Enemies[i].transform.position.y + 0.7f, battleSystem.Enemies[i].transform.position.z);
                        cureParticle.gameObject.AddComponent<DestroyParticle>();
                        cureParticle.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
                        break;
                }
            }
        }
    }
}
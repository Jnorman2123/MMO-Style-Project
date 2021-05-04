using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOfFireController : MonoBehaviour
{
    // Declare variables for cast time, mana cost, and blast of fire cooldown
    private float castingTime;
    public int manaCost;
    private float cooldownTime;
    private bool bofCooldown;
    // Declare variables for min, max, and base spell power, and spell power modifier
    private int minSpellPower;
    private int maxSpellPower;
    private int baseSpellPower;
    private float spellPowerModifier;
    // Declare variable for target and target resistance
    private GameObject target;
    private int targetResistance;
    // Declare variable for the chat ui window and combatMessage
    private GameObject chatUIWindow;
    private string combatMessage;
    // Declare variables for the target and caster names
    private string targetName;
    private string casterName;
    // Start is called before the first frame update
    void Start()
    {
        // Set casting time and cooldown time
        castingTime = 2.5f;
        cooldownTime = 6.0f;
        // Set cooldown to false
        bofCooldown = false;
        // Set chatUIWindow
        chatUIWindow = GameObject.Find("Chat UI Window");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Create method to cast Blast of Fire
    public void CastBlastOfFire()
    {
        // Start the BeginCasting coroutine
        StartCoroutine(BeginCasting());
    }
    // Create coroutine to begin casting
    IEnumerator BeginCasting()
    {
        // Set the target based on character type
        if (CompareTag("Player"))
        {
            target = GetComponent<TargetingController>().target;
        } else if (CompareTag("Enemy")) {
            target = GetComponent<EnemyController>().enemyTarget;
        }
        // Don't cast and give message if no proper target
        if (target == null)
        {
            combatMessage = "You have no target.";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
        }
        else if (target.CompareTag("Interactable"))
        {
            combatMessage = "You can not use this spell on your target.";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
        }
        // Don't cast and give message if insufficient mana
        else if (manaCost > GetComponent<ManaController>().currentMana)
        {
            combatMessage = "You have insufficient mana!";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
        } else if (bofCooldown)
        {
            if (CompareTag("Player"))
            {
                combatMessage = "Blast of Fire cannot be used at this time!";
                // Log the combat message
                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            } else if (CompareTag("Enemy"))
            {
                StopCoroutine(BeginCasting());
            }
        }
        else if (manaCost <= GetComponent<ManaController>().currentMana)
        {
                    // Set the target resistance
                    targetResistance = target.GetComponent<ResistanceController>().resistance;
            // Set the target and caster name
            if (CompareTag("Player"))
            {
                targetName = target.name.Replace("(Clone)", "").Trim();
                casterName = gameObject.name;
            } else if (CompareTag("Enemy"))
            {
                targetName = target.name;
                casterName = gameObject.name.Replace("(Clone)", "").Trim();
            }
            // Set the power and manaCost
            minSpellPower = 5;
            maxSpellPower = 10;
            baseSpellPower = Random.Range(minSpellPower, maxSpellPower);
            spellPowerModifier = GetComponent<StatsController>().intelligence;
            spellPowerModifier /= 100;
            spellPowerModifier *= baseSpellPower;
            int netDamagePower = Mathf.RoundToInt(spellPowerModifier) + baseSpellPower - targetResistance;
            manaCost = 10;
            // Set cooldown to true
            bofCooldown = true;
            // Set casting to true and stop auto attacking
            if (CompareTag("Player"))
            {
                GetComponent<SpellsController>().casting = true;
                if (GetComponent<CombatController>().autoAttacking == true)
                {
                    GetComponent<CombatController>().autoAttacking = false;
                    GetComponent<CombatController>().AutoAttack();
                }
            } else if (CompareTag("Enemy"))
            {
                GetComponent<EnemySpellsController>().casting = true;
                if (GetComponent<EnemyCombatController>().autoAttacking == true)
                {
                    GetComponent<EnemyCombatController>().autoAttacking = false;
                    GetComponent<EnemyCombatController>().AutoAttack();
                }
            }
            // Log you are casting to the chat window
            combatMessage = casterName + " begins to cast Blast of Fire on " + targetName + ".";
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // After casting time deal damage to the target
            yield return new WaitForSeconds(castingTime);
            GetComponent<ManaController>().UseMana(manaCost);
            target.GetComponent<HealthController>().TakeDamage(netDamagePower, gameObject, target);
            // Start the OnCooldown coroutine
            StartCoroutine(OnCooldown());
            // Set combat message
            combatMessage = casterName + "'s Blast of Fire has hit "
                            + targetName + " for " + netDamagePower + " points of damage!";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // Set casting to false and autoAttacking to true and inCombat to true
            if (CompareTag("Player"))
            {
                GetComponent<SpellsController>().casting = false;
                GetComponent<CombatController>().inCombat = true;
                GetComponent<CombatController>().autoAttacking = true;
                // Stop damage spell coroutine and start auto attacking again
                StopCoroutine(BeginCasting());
                GetComponent<CombatController>().AutoAttack();
            } else if (CompareTag("Enemy"))
            {
                GetComponent<EnemySpellsController>().casting = false;
                GetComponent<EnemyCombatController>().inCombat = true;
                GetComponent<EnemyCombatController>().autoAttacking = true;
                // Start auto attacking again
                GetComponent<EnemyCombatController>().AutoAttack();
                // Stop damage spell coroutine 
                StopCoroutine(BeginCasting());
            }
        }
    }
    // Create a coroutine to put the spell on cooldown
    IEnumerator OnCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        // Set cooldown to false
        bofCooldown = false;
        // Stop the OnCooldown coroutine
        StopCoroutine(OnCooldown());
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOfFireController : MonoBehaviour
{
    // Declare variables for cast time and mana cost
    private float castingTime;
    private int manaCost;
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
    // Start is called before the first frame update
    void Start()
    {
        // Set casting time
        castingTime = 2.5f;
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
        // Set the target
        target = GetComponent<TargetingController>().target;
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
        }
        else if (manaCost < GetComponent<ManaController>().currentMana)
        {
            // Set the target resistance
            targetResistance = target.GetComponent<ResistanceController>().resistance;
            // Set the target and caster name
            string targetName = target.name.Replace("(Clone)", "").Trim();
            string casterName = gameObject.name;
            // Set the power and manaCost
            minSpellPower = 5;
            maxSpellPower = 10;
            baseSpellPower = Random.Range(minSpellPower, maxSpellPower);
            spellPowerModifier = GetComponent<StatsController>().intelligence;
            spellPowerModifier /= 100;
            spellPowerModifier *= baseSpellPower;
            int netDamagePower = Mathf.RoundToInt(spellPowerModifier) + baseSpellPower - targetResistance;
            manaCost = 10;
            // Set casting to true and stop auto attacking
            GetComponent<SpellsController>().casting = true;
            if (GetComponent<CombatController>().autoAttacking == true)
            {
                GetComponent<CombatController>().autoAttacking = false;
                GetComponent<CombatController>().AutoAttack();
            }
            // Log you are casting to the chat window
            combatMessage = casterName + " begins to cast damage spell on " + targetName + ".";
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // After casting time deal damage to the target
            yield return new WaitForSeconds(castingTime);
            GetComponent<ManaController>().UseMana(manaCost);
            target.GetComponent<HealthController>().TakeDamage(netDamagePower, gameObject, target);
            // Set combat message
            combatMessage = casterName + "'s damage spell has hit "
                            + targetName + " for " + netDamagePower + " points of damage!";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // Set casting to false and autoAttacking to true and inCombat to true
            GetComponent<SpellsController>().casting = false;
            GetComponent<CombatController>().inCombat = true;
            GetComponent<CombatController>().autoAttacking = true;
            // Stop damage spell coroutine and start auto attacking again
            StopCoroutine(BeginCasting());
            GetComponent<CombatController>().AutoAttack();
        }
    }
}
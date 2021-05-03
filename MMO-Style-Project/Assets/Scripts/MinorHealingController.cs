using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorHealingController : MonoBehaviour
{
    // Declare variables for cast time and mana cost
    private float castingTime;
    private int manaCost;
    // Declare variables for min, max, and base spell power, and spell power modifier
    private int minSpellPower;
    private int maxSpellPower;
    private int baseSpellPower;
    private float spellPowerModifier;
    // Declare variable for target
    private GameObject target;
    // Declare variable for the chat ui window and combatMessage
    private GameObject chatUIWindow;
    private string combatMessage;
    // Declare variable for already attacking
    private bool alreadyAttacking;
    // Start is called before the first frame update
    void Start()
    {
        // Set casting time
        castingTime = 3.0f;
        // Set chatUIWindow
        chatUIWindow = GameObject.Find("Chat UI Window");
        // Set already atacking to false
        alreadyAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Create method to cast minor healing
    public void CastMinorHealing()
    {
        // Start the BeginCasting coroutine
        StartCoroutine(BeginCasting());
    }
    // Create the begin casting coroutine
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
            Debug.Log("casting");
            // Set the target and caster name
            string targetName = target.name.Replace("(Clone)", "").Trim();
            string casterName = gameObject.name;
            // Set the power and manaCost
            minSpellPower = 5;
            maxSpellPower = 10;
            baseSpellPower = Random.Range(minSpellPower, maxSpellPower);
            spellPowerModifier = GetComponent<StatsController>().wisdom;
            spellPowerModifier /= 100;
            spellPowerModifier *= baseSpellPower;
            int netHealPower = Mathf.RoundToInt(spellPowerModifier) + baseSpellPower;
            manaCost = 10;
            // Set casting to true and stop auto attacking
            GetComponent<SpellsController>().casting = true;
            if (GetComponent<CombatController>().autoAttacking == true)
            {
                alreadyAttacking = true;
                GetComponent<CombatController>().autoAttacking = false;
                GetComponent<CombatController>().AutoAttack();
            }
            // Log you are casting to the chat window
            combatMessage = casterName + " begins to cast heal spell on " + targetName + ".";
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // Wait for casting time and then heal the target
            yield return new WaitForSeconds(castingTime);
            target.GetComponent<HealthController>().TakeDamage(-netHealPower, gameObject, target);
            // Set combat message
            combatMessage = casterName + "'s heal spell has healed "
                            + targetName + " for " + netHealPower + " points of damage!";
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // Set casting to false and autoAttacking to true and inCombat to true
            GetComponent<SpellsController>().casting = false;
            if (alreadyAttacking == true)
            {
                GetComponent<CombatController>().inCombat = true;
                GetComponent<CombatController>().autoAttacking = true;
            }
            // Stop damage spell coroutine and start auto attacking again
            StopCoroutine(BeginCasting());
            GetComponent<CombatController>().AutoAttack();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour
{
    // Declare variable for casting time and casting
    private float castingTime;
    public bool casting;
    // Declare variable for the target and target resistance
    private GameObject target;
    private int targetResistance;
    // Declare variable for the chat ui window and combatMessage
    private GameObject chatUIWindow;
    private string combatMessage;
    // Declare variables for power and power modifier
    private int minSpellPower;
    private int maxSpellPower;
    private int baseSpellPower;
    private float spellPowerModifier;
    // Declare variable for the mana cost
    public int manaCost;
    // Start is called before the first frame update
    void Start()
    {
        // Set castingTime and casting
        castingTime = 2.0f;
        casting = false;
        // Set chatUIWindow
        chatUIWindow = GameObject.Find("Chat UI Window");
    }

    // Update is called once per frame
    void Update()
    {
        // Cast damage spell method when 2 is pressed and not currently casting
        if (CompareTag("Player") & Input.GetKeyDown(KeyCode.Alpha2) & !casting)
        {
            StartCoroutine(CastSpell("damage"));
        }
        // Cast heal spell method when 3 is pressed and not currently casting
        if (CompareTag("Player") & Input.GetKeyDown(KeyCode.Alpha3) & !casting)
        {
            StartCoroutine(CastSpell("heal"));
        }
    }
    // Create coroutine to cast a damage spell when 2 is pressed
    IEnumerator CastSpell(string spellType)
    {
        // Set the target and target resistance
        target = GetComponent<TargetingController>().target;
        targetResistance = target.GetComponent<ResistanceController>().resistance;
        // Set the power and manaCost
        minSpellPower = 5;
        maxSpellPower = 10;
        baseSpellPower = Random.Range(minSpellPower, maxSpellPower);
        if (spellType == "damage")
        {
            spellPowerModifier = GetComponent<StatsController>().intelligence;
        } else if (spellType == "heal")
        {
            spellPowerModifier = GetComponent<StatsController>().wisdom;
        }

        spellPowerModifier /= 100;
        spellPowerModifier *= baseSpellPower;
        int netDamagePower = Mathf.RoundToInt(spellPowerModifier) + baseSpellPower - targetResistance;
        int netHealPower = Mathf.RoundToInt(spellPowerModifier) + baseSpellPower;
        manaCost = 10;
        // Don't cast and give message if no proper target
        if (target == null)
        {
            combatMessage = "You have no target.";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
        }  else if (target.CompareTag("Interactable"))
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
        } else if (manaCost < GetComponent<ManaController>().currentMana)
        {
            // Set casting to true and stop auto attacking
            casting = true;
            if (GetComponent<CombatController>().autoAttacking == true)
            {
                GetComponent<CombatController>().autoAttacking = false;
                GetComponent<CombatController>().AutoAttack();
            }
            if (spellType == "damage")
            {
                // Log you are casting to the chat window
                combatMessage = "You begin to cast damage spell!";
            } else if (spellType == "heal")
            {
                // Log you are casting to the chat window
                combatMessage = "You begin to cast heal spell!";
            }
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // After casting time deal damage to the target or heal the target
            yield return new WaitForSeconds(castingTime);
            GetComponent<ManaController>().UseMana(manaCost);
            if (spellType == "damage")
            {
                target.GetComponent<HealthController>().TakeDamage(netDamagePower, gameObject, target);
                // Set combat message
                combatMessage = gameObject.name.Replace("(Clone)", "").Trim() + "'s damage spell has hit " 
                                + target.name.Replace("(Clone)", "").Trim() + " for " + netDamagePower + " points of damage!";
            } else if (spellType == "heal")
            {
                target.GetComponent<HealthController>().TakeDamage(-netHealPower, gameObject, target);
                // Set combat message
                combatMessage = gameObject.name.Replace("(Clone)", "").Trim() + "'s heal spell has healed " 
                                + target.name.Replace("(Clone)", "").Trim() + " for " + netHealPower + " points of damage!";
            }
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // Set casting to false and autoAttacking to true and inCombat to true
            casting = false;
            GetComponent<CombatController>().inCombat = true;
            GetComponent<CombatController>().autoAttacking = true;
            // Stop damage spell coroutine and start auto attacking again
            StopCoroutine("CastSpell");
            GetComponent<CombatController>().AutoAttack();
        }    
    }
}

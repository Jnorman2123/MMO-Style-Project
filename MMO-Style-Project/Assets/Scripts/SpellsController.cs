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
    // Declare variables for damage and damage modifier
    private int minSpellDamage;
    private int maxSpellDamage;
    private int baseSpellDamage;
    private int spellDamageModifier;
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
        // Start the DamageSpell coroutine when 2 is pressed and not currently casting
        if (Input.GetKeyDown(KeyCode.Alpha2) & !casting)
        {
            StartCoroutine("DamageSpell");
        }
    }
    // Create coroutine to cast a damage spell when 2 is pressed
    IEnumerator DamageSpell()
    {
        // Set the target and target resistance
        target = GetComponent<TargetingController>().target;
        targetResistance = target.GetComponent<ResistanceController>().resistance;
        Debug.Log("resistance " + targetResistance);
        // Set the damage and manaCost
        minSpellDamage = 5;
        maxSpellDamage = 10;
        baseSpellDamage = Random.Range(minSpellDamage, maxSpellDamage);
        Debug.Log("base damage " + baseSpellDamage);
        spellDamageModifier = Mathf.RoundToInt(GetComponent<StatsController>().intelligence / 10);
        Debug.Log("damage modifier " + spellDamageModifier);
        int netDamage = baseSpellDamage + spellDamageModifier - targetResistance;
        manaCost = 10;
        // Don't cast and give message if no proper target
        if (target == null)
        {
            combatMessage = "You have no target.";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
        } else if (target == gameObject)
        {
            combatMessage = "You can not use this spell on yourself";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
        } else if (target.CompareTag("Interactable"))
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
            // Log you are casting to the chat window
            combatMessage = "You beging to cast!";
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // After casting time deal damage to the target
            yield return new WaitForSeconds(castingTime);
            GetComponent<ManaController>().UseMana(manaCost);
            target.GetComponent<HealthController>().TakeDamage(netDamage, gameObject, target);
            // Set combat message
            combatMessage = "Your damage spell has hit " + target.name.Replace("(Clone)", "").Trim()
                            + " for " + netDamage + " points of damage!";
            // Log the combat message
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // Set casting to false and autoAttacking to true and inCombat to true
            casting = false;
            GetComponent<CombatController>().inCombat = true;
            GetComponent<CombatController>().autoAttacking = true;
            // Stop damage spell coroutine and start auto attacking again
            StopCoroutine("DamageSpell");
            GetComponent<CombatController>().AutoAttack();
        }    
    }
}

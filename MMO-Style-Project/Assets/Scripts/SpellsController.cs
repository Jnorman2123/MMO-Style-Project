using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour
{
    // Declare variable for casting time and casting
    private float castingTime;
    public bool casting;
    // Declare variable for the chat ui window and combatMessage
    private GameObject chatUIWindow;
    private string combatMessage;
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
        // Set the target
        GameObject target = GetComponent<TargetingController>().target;
        // Set the damage and manaCost
        int minDamage = 5;
        int maxDamage = 10;
        int baseDamage = Random.Range(minDamage, maxDamage);
        int damageModifier = Mathf.RoundToInt(GetComponent<StatsController>().intelligence / 100);
        int netDamage = baseDamage + damageModifier;
        int manaCost = 10;
        // Don't cast and give message if no proper target
        if (target == null)
        {
            combatMessage = "You have no target.";
        } else if (target == gameObject)
        {
            combatMessage = "You can not use this spell on yourself";
        } else if (target.CompareTag("Interactable"))
        {
            combatMessage = "You can not use this spell on your target.";
        }
        // Don't cast and give message if insufficient mana
        else if (manaCost > GetComponent<ManaController>().currentMana)
        {
            combatMessage = "You have insufficient mana!";
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
            // Set casting to false and autoAttacking to true and inCombat to true
            casting = false;
            GetComponent<CombatController>().inCombat = true;
            GetComponent<CombatController>().autoAttacking = true;
            StopCoroutine("DamageSpell");
            GetComponent<CombatController>().AutoAttack();
        }
        // Log the combat message
        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
    }
}

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
        // Set casting to true and stop auto attacking
        casting = true;
        if (GetComponent<CombatController>().autoAttacking == true)
        {
            GetComponent<CombatController>().autoAttacking = false;
        }
        // Log you are casting to the chat window
        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText("You beging to cast!");
        // Set the target
        GameObject target = GetComponent<TargetingController>().target;
        // Set the damage
        int damage = 10;
        // After casting time deal damage to the target
        yield return new WaitForSeconds(castingTime);
        target.GetComponent<HealthController>().TakeDamage(damage, gameObject, target);
        // Set combat message and log it to the chat window
        combatMessage = "Your damage spell has hit " + target.name.Replace("(Clone)", "").Trim()
                        + " for " + damage + " points of damage!";
        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
        // Set casting to false and autoAttacking to true and inCombat to true
        casting = false;
        GetComponent<CombatController>().inCombat = true;
        GetComponent<CombatController>().autoAttacking = true;
        StopCoroutine("DamageSpell");
        GetComponent<CombatController>().AutoAttack();
    }
}

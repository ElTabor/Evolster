using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public IAbility ability;
    public bool abilityAvailable;

    private void Update()
    {
        transform.position = GameManager.instance.player.GetComponent<PlayerController>().spellController.transform.position;
        abilityAvailable = PlayerController.instance.manaController.ManageAbilityAvailability();
        ability.Update();
    }

    public void SetAbility(IAbility newAbility)
    {
        ability = newAbility;
        Debug.Log($"NewAbility set: {newAbility.abilityData.uniqueAbilityName}");
        ability.Start();
    }
}

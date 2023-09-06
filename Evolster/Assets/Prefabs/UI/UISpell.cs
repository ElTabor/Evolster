using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISpell : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spellNameText;
    [SerializeField] TextMeshProUGUI spellTypeText;
    [SerializeField] TextMeshProUGUI spellDescriptionText;
    [SerializeField] TextMeshProUGUI spellDamageText;
    [SerializeField] TextMeshProUGUI spellSpeedText;
    [SerializeField] TextMeshProUGUI spellLevelText;
    [SerializeField] TextMeshProUGUI spellLifeTimeText;

    public void UpdateSpellUI(SpellsData spellData)
    {
        spellNameText.text = spellData.Name;
        spellTypeText.text = spellData.Type;
        spellDescriptionText.text = spellData.Description;
        spellDamageText.text = spellData.Damage.ToString();
        spellSpeedText.text = spellData.Speed.ToString();
        spellLevelText.text = spellData.Level.ToString();
        spellLifeTimeText.text = spellData.LifeTime.ToString();

    }
}

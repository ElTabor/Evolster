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
        spellNameText.text = spellData.spellName;
        spellTypeText.text = spellData.spellType;
        spellDescriptionText.text = spellData.spellDescription;
        spellDamageText.text = spellData.spellDamage.ToString();
        spellSpeedText.text = spellData.spellSpeed.ToString();
        spellLevelText.text = spellData.spellLevel.ToString();
        spellLifeTimeText.text = spellData.spellLifeTime.ToString();
    }
}

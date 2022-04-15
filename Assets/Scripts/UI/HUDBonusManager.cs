using System;
using UnityEngine;

public class HUDBonusManager : MonoBehaviour
{
    public Transform root;
    public GameObject speedBonusIconPrefab;
    public GameObject shieldBonusIconPrefab;
    
    public void AddBonus(BonusScript.Bonus bonus)
    {
        GameObject go;
        switch (bonus.type)
        {
            case BonusScript.Bonus.BonusType.Point:
                return;
            case BonusScript.Bonus.BonusType.Speed:
                go = Instantiate(speedBonusIconPrefab, root);
                break;
            case BonusScript.Bonus.BonusType.Shield:
                go = Instantiate(shieldBonusIconPrefab, root);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        go.GetComponent<HUDBonusScript>().maxDuration = bonus.duration;
    }
    
    public void ResetBonuses()
    {
        foreach (Transform child in root)
        {
            Destroy(child.gameObject);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBonusApplicable
{
    public List<BonusScript.Bonus> BonusList {get;}
    public BonusScript.Bonus.BonusType[] ApplicableBonusTypes {get;}

    public void ApplyBonus(BonusScript.Bonus bonus)
    {
        if (Array.Exists(ApplicableBonusTypes, type => bonus.type == type))
        {
            BonusList.Add(bonus);
        }
    }

    public void ResetBonuses()
    {
        BonusList.Clear();
    }
}
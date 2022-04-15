using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBonusApplicable
{
    public void OnBonusAdded(BonusScript.Bonus bonus)
    {
        
    }
    
    public void OnBonusDeleted(BonusScript.Bonus bonus)
    {
        
    }
}
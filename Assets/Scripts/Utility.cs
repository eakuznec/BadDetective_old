using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Utility : MonoBehaviour
    {
        public static Money SetMoney(Money money)
        {
            Money newMoney = new Money();
            int extraLibra = money.penny / Constant.pennyInLibra;
            newMoney.penny = money.penny - extraLibra * Constant.pennyInLibra;
            int extraCrown = (money.libra + extraLibra) / Constant.libraInCrown;
            newMoney.libra = money.libra + extraLibra - extraCrown * Constant.libraInCrown;
            newMoney.crown = money.crown + extraCrown;
            return newMoney;
        }
    }
}

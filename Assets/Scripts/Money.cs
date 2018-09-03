using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    [System.Serializable]
    public struct Money
    {
        public int crown;
        public int libra;
        public int penny;

        private static Money recheck(int p, int l, int c)
        {
            if (p < 0)
            {
                while (p < 0)
                {
                    l--;
                    p += Constant.pennyInLibra;
                }
            }
            else if (p >= Constant.pennyInLibra)
            {
                while (p >= Constant.pennyInLibra)
                {
                    l++;
                    p -= Constant.pennyInLibra;
                }
            }

            if (l < 0)
            {
                while (l < 0)
                {
                    c--;
                    l += Constant.libraInCrown;
                }
            }
            else if (l >= Constant.libraInCrown)
            {
                while (l >= Constant.libraInCrown)
                {
                    c++;
                    l -= Constant.libraInCrown;
                }
            }

            return new Money()
            {
                penny = p,
                libra = l,
                crown = c
            };
        }

        public static Money operator +(Money m1, Money m2)
        {
            int p = m1.penny + m2.penny;
            int l = m1.libra + m2.libra;
            int c = m1.crown + m2.crown;
            return recheck(p,l,c);
        }

        public static Money operator -(Money m1, Money m2)
        {
            int p = m1.penny - m2.penny;
            int l = m1.libra - m2.libra;
            int c = m1.crown - m2.crown;
            return recheck(p, l, c);
        }

        public static Money operator *(Money m, float value)
        {
            int pennyCount = (m.crown * Constant.libraInCrown + m.libra) * Constant.pennyInLibra + m.penny;
            int p = (int)(pennyCount * value);
            return recheck(p, 0, 0);
        }

        public static Money operator *(Money m, int value)
        {
            int pennyCount = (m.crown * Constant.libraInCrown + m.libra) * Constant.pennyInLibra + m.penny;
            int p = pennyCount * value;
            return recheck(p, 0, 0);
        }

        public static Money operator /(Money m, int value)
        {
            int pennyCount = (m.crown * Constant.libraInCrown + m.libra) * Constant.pennyInLibra + m.penny;
            int p = pennyCount / value;
            return recheck(p, 0, 0);
        }

        public static float operator /(Money m1, Money m2)
        {
            int pennyCount1 = (m1.crown * Constant.libraInCrown + m1.libra) * Constant.pennyInLibra + m1.penny;
            int pennyCount2 = (m2.crown * Constant.libraInCrown + m2.libra) * Constant.pennyInLibra + m2.penny;
            return ((float)pennyCount1 / (float)pennyCount2);
        }

        public override string ToString()
        {
            string retVal = "";
            if (crown != 0)
            {
                retVal += string.Format("{0} crown ", crown);
            }
            if (libra != 0)
            {
                retVal +=string.Format("{0} libra ", libra);
            }
            if (penny != 0)
            {
                retVal += string.Format("{0} penny ", penny);
            }
            if(retVal == "")
            {
                retVal = "0 penny";
            }
            return retVal;
        }
    }
}

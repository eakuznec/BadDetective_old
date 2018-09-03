using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Detective : Character
    {
        public float maxHealth = 100;
        public float minHealth = 0;
        public float curHealth = 100;
        public float maxStress = 100;
        public float minStress = 0;
        public float curStress = 0;
        public float maxLoyalty = 100;
        public float minLoyalty = 0;
        public float curLoyalty;
        public int maxItemSlots = 4;
        public List<Method> methods = new List<Method>
    {
        Method.Brutal,
        Method.Careful,
        Method.Diplomatic,
        Method.Scientific
    };
        public int[] methodsValues = new int[4];
        public int[] maxMethodsValues = new int[4] { 100, 100, 100, 100 };

        public List<Trait> traits = new List<Trait>();
        private List<Equipment> _equipments = new List<Equipment>();

        private Money _salary;
        public DetectiveHome home;
        public WayType priorityWay;
        public Color wayColor;
        [HideInInspector]
        public iActivityPlace activityPlace;
        [HideInInspector]
        public DetectiveActivity activity;

        public bool checkGoHome;

        private void Start()
        {
            //Инициализация трейтов
            for (int i = 0; i < traits.Count; i++)
            {
                Trait trait = traits[i];
                if (trait == null)
                {
                    traits.RemoveAt(i);
                    i--;
                }
                else
                {
                    traits[i] = Instantiate(trait, this.transform);
                    traits[i].owner = this;
                    traits[i].AttachEffects();
                }
            }
            //Инициализация дома
            home = Instantiate(home, transform);
            home.owner = this;
            //Учет выплаты в таймлайне
            CreateSalaryAction();
            //
            ChangeActivity(DetectiveActivity.IN_OFFICE);
            BadDetective.UI.InterfaceManager.GetInstantiate().detectiveRow.ResetRow();
        }

        public void ChangeMethodValue(Method method, int value)
        {
            int i = methods.IndexOf(method);
            methodsValues[i] += value;
            if (methodsValues[i] < 0)
            {
                methodsValues[i] = 0;
            }
            else if (methodsValues[i] > maxMethodsValues[i])
            {
                methodsValues[i] = maxMethodsValues[i];
            }
        }

        public int GetMethodValue(Method method, Tag tag = Tag.NULL)
        {
            int retVal = 0;
            int index = methods.IndexOf(method);
            retVal = methodsValues[index];
            if (tag != Tag.NULL)
            {
                if (method == Method.Brutal)
                {
                    if (tag == Tag.battle)
                    {
                        retVal += GetBattleBonus();
                    }
                    else if (tag == Tag.brawl || tag == Tag.melee_weapon || tag == Tag.firearms)
                    {
                        retVal += GetBattleBonus(tag);
                    }
                    else if (tag == Tag.intimidation)
                    {
                        foreach (Trait trait in traits)
                        {
                            if (trait.tags.Contains(Tag.intimidation))
                            {
                                retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                            }
                        }
                    }
                }
                else if (method == Method.Careful)
                {

                }
                else if (method == Method.Diplomatic)
                {
                    if (tag == Tag.steppe)
                    {
                        foreach (Trait trait in traits)
                        {
                            if (trait.tags.Contains(tag))
                            {
                                retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_DIPLOMATIC);
                            }
                        }
                    }
                }
                else if (method == Method.Scientific)
                {

                }
            }
            return retVal;
        }

        public int GetMaxMethodValue(Method method)
        {
            int index = methods.IndexOf(method);
            return maxMethodsValues[index];
        }

        public void SetMethodValue(Method method, int value)
        {
            int i = methods.IndexOf(method);
            methodsValues[i] = value;
        }

        public void ChangeMaxMethodValue(Method method, int value)
        {
            int i = methods.IndexOf(method);
            maxMethodsValues[i] += value;
            if (methodsValues[i] > maxMethodsValues[i])
            {
                SetMethodValue(method, maxMethodsValues[i]);
            }
        }

        public Money salary
        {
            get
            {
                return _salary;
            }
            set
            {
                _salary = Utility.SetMoney(value);
            }
        }

        public Equipment GetMainWeapon(out int maxBonus, Tag weaponType = Tag.NULL)
        {
            Equipment retVal = null;
            maxBonus = 0;
            foreach (Equipment equipment in _equipments)
            {
                if (equipment.trait != null && equipment.trait.tags.Contains(Tag.weapon))
                {
                    if (weaponType == Tag.brawl && !equipment.trait.tags.Contains(Tag.brawl))
                    {
                        continue;
                    }
                    else if (weaponType == Tag.melee_weapon && !equipment.trait.tags.Contains(Tag.melee_weapon))
                    {
                        continue;
                    }
                    else if (weaponType == Tag.firearms && !equipment.trait.tags.Contains(Tag.firearms))
                    {
                        continue;
                    }

                    int bonus = equipment.trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                    if (retVal == null)
                    {
                        retVal = equipment;
                        maxBonus = bonus;
                    }
                    else if (maxBonus < bonus)

                    {
                        retVal = equipment;
                        maxBonus = bonus;
                    }
                }
            }
            return retVal;
        }

        public int GetBattleBonus(Tag weaponType = Tag.NULL)
        {
            int retVal = 0;
            int weaponBonus = 0;
            Equipment weapon = GetMainWeapon(out weaponBonus, weaponType);
            retVal += weaponBonus;
            if (weapon == null && (weaponType == Tag.NULL || weaponType == Tag.brawl))
            {
                foreach (Trait trait in traits)
                {
                    if (trait.tags.Contains(Tag.brawl) && !trait.tags.Contains(Tag.weapon))
                    {
                        retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                    }
                }
            }
            else if (weapon != null)
            {
                if ((weaponType == Tag.NULL && weapon.trait.tags.Contains(Tag.brawl)) || (weaponType == Tag.brawl))
                {
                    foreach (Trait trait in traits)
                    {
                        if (trait.tags.Contains(Tag.brawl) && !trait.tags.Contains(Tag.weapon))
                        {
                            retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                        }
                    }
                }
                else if ((weaponType == Tag.NULL && weapon.trait.tags.Contains(Tag.melee_weapon)) || (weaponType == Tag.melee_weapon))
                {
                    foreach (Trait trait in traits)
                    {
                        if (trait.tags.Contains(Tag.melee_weapon) && !trait.tags.Contains(Tag.weapon))
                        {
                            retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                        }
                    }
                }
                else if ((weaponType == Tag.NULL && weapon.trait.tags.Contains(Tag.firearms)) || (weaponType == Tag.firearms))
                {
                    foreach (Trait trait in traits)
                    {
                        if (trait.tags.Contains(Tag.firearms) && !trait.tags.Contains(Tag.weapon))
                        {
                            retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                        }
                    }
                }
            }
            foreach (Trait trait in traits)
            {
                if (trait.tags.Contains(Tag.battle))
                {
                    retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                }
            }
            return retVal;
        }

        public bool HaveTag(Tag tag)
        {
            foreach (Trait trait in traits)
            {
                foreach (Tag t in trait.tags)
                {
                    if (t == tag)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ChangeCurHealth(float value)
        {
            curHealth += value;
            if (curHealth > maxHealth)
            {
                curHealth = maxHealth;
            }
            else if (curHealth < minHealth)
            {
                curHealth = minHealth;
                Debug.Log(string.Format("{0} is dead!!!", characterName), this);
            }
        }

        public void ChangeCurStress(float value)
        {
            curStress += value;
            if (curStress > maxStress)
            {
                curStress = maxStress;
                Debug.Log(string.Format("{0} is madness!!!", characterName), this);
            }
            else if (curStress < minStress)
            {
                curStress = minStress;
            }
        }


        private void CheckGoHome()
        {
            //if (curStress > maxStress * 0.25)
            //{
            //    checkGoHome = true;
            //    List<Detective> detectives = new List<Detective> { this };
            //    Agency.GetInstantiate().
            //    GoTo(home, priorityWay, true);
            //}
        }

        private void Update()
        {
            //if(activity == DetectiveActivity.IN_OFFICE && !checkGoHome)
            //{
            //    CheckGoHome();
            //}
        }

        public void CreateSalaryAction()
        {
            Timeline timeline = Timeline.GetInstantiate();
            GameObject goAction = new GameObject(string.Format("TimelineAction_DetectiveSalary_{0}", characterName));
            goAction.transform.parent = timeline.transform;
            TimelineAction salaryAction = goAction.AddComponent<TimelineAction>();
            salaryAction.actionType = TimelineActionType.DETECTIVE_SALARY;
            salaryAction.detective = this;
            salaryAction.timer = timeline.GetTime() + 24 * 7; //выплата через неделю.
            timeline.RegistrateAction(salaryAction);
        }

        public void ChangeActivity(DetectiveActivity newActivity)
        {
            if (activity != newActivity)
            {
                Office office = Agency.GetInstantiate().GetOffice();
                if (activity == DetectiveActivity.IN_OFFICE)
                {
                        office.detectivesInOffice.Remove(this);
                }
                else if (activity == DetectiveActivity.IN_HOME)
                {
                        home.characterInHome = false;
                        checkGoHome = false;
                }
                else if (activity == DetectiveActivity.IN_WAY)
                {

                }
                activity = newActivity;
                if (activity == DetectiveActivity.IN_OFFICE)
                {
                        office.detectivesInOffice.Add(this);
                        activityPlace = office;
                }
                else if (activity == DetectiveActivity.IN_HOME)
                {
                        home.characterInHome = true;
                        activityPlace = home;
                }
                else if (activity == DetectiveActivity.IN_WAY)
                {

                }
            }
        }
    }
}

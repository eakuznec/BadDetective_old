using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Detective : Character
    {
        public Temper temper;
        public float maxHealth = 100;
        public float minHealth = 0;
        public float curHealth = 100;
        public float maxStress = 100;
        public float minStress = 0;
        public float curStress = 0;
        public float maxLoyalty = 100;
        public float minLoyalty = 0;
        public float curLoyalty;
        public float minConfidence = 0;
        public float maxConfidence = 100;
        public float curConfidence;
        private int maxItemSlots = 4;
        public int blockedSlots;
        public float speedMod = 1;

        public List<Method> methods = new List<Method>
    {
        Method.Brutal,
        Method.Careful,
        Method.Diplomatic,
        Method.Scientific
    };
        public int[] methodsValues = new int[4];
        public int[] maxMethodsValues = new int[4] { 100, 100, 100, 100 };

        public List<TraitContainer> traits = new List<TraitContainer>();
        private List<EquipmentContainer> _equipments = new List<EquipmentContainer>();
        private List<Item> items = new List<Item>();
        public List<FileNoteContainer> notes = new List<FileNoteContainer>();

        private Money _salary;
        public DetectiveHome home;
        public WayType priorityWay;
        public Color wayColor;
        [HideInInspector]
        public iActivityPlace activityPlace;
        [HideInInspector]
        public DetectiveActivity activity;

        public bool checkGoHome;

        private GameObject equipmentFolder;
        private GameObject traitFolder;

        private void Start()
        {
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
                }
                else if (method == Method.Careful)
                {

                }
                else if (method == Method.Diplomatic)
                {
                    if (tag == Tag.steppefolk)
                    {
                        foreach(TraitContainer trait in traits)
                        {
                            if (trait.trait.tags.Contains(tag))
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

        public List<EquipmentContainer> GetEquipment()
        {
            return _equipments;
        }

        public EquipmentContainer GetMainWeapon(out int maxBonus, Tag weaponType = Tag.NULL)
        {
            EquipmentContainer retVal = null;
            maxBonus = 0;
            foreach (EquipmentContainer equipment in _equipments)
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
            EquipmentContainer weapon = GetMainWeapon(out weaponBonus, weaponType);
            retVal += weaponBonus;
            if (weapon == null && (weaponType == Tag.NULL || weaponType == Tag.brawl))
            {
                foreach (TraitContainer trait in traits)
                {
                    if (trait.GetTags().Contains(Tag.brawl) && !trait.trait.tags.Contains(Tag.weapon))
                    {
                        retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                    }
                }
            }
            else if (weapon != null)
            {
                if ((weaponType == Tag.NULL && weapon.trait.tags.Contains(Tag.brawl)) || (weaponType == Tag.brawl))
                {
                    foreach (TraitContainer trait in traits)
                    {
                        if (trait.trait.tags.Contains(Tag.brawl) && !trait.trait.tags.Contains(Tag.weapon))
                        {
                            retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                        }
                    }
                }
                else if ((weaponType == Tag.NULL && weapon.trait.tags.Contains(Tag.melee_weapon)) || (weaponType == Tag.melee_weapon))
                {
                    foreach (TraitContainer trait in traits)
                    {
                        if (trait.GetTags().Contains(Tag.melee_weapon) && !trait.GetTags().Contains(Tag.weapon))
                        {
                            retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                        }
                    }
                }
                else if ((weaponType == Tag.NULL && weapon.trait.tags.Contains(Tag.firearms)) || (weaponType == Tag.firearms))
                {
                    foreach (TraitContainer trait in traits)
                    {
                        if (trait.GetTags().Contains(Tag.firearms) && !trait.GetTags().Contains(Tag.weapon))
                        {
                            retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                        }
                    }
                }
            }
            foreach (TraitContainer trait in traits)
            {
                if (trait.GetTags().Contains(Tag.battle))
                {
                    retVal += trait.GetTotalEffectValue(TraitEffectType.CHANGE_BRUTAL);
                }
            }
            return retVal;
        }

        public bool HaveTag(Tag tag)
        {
            foreach (TraitContainer trait in traits)
            {
                foreach (Tag t in trait.trait.tags)
                {
                    if (t == tag)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HaveHiddenTag(Tag tag, bool hidden)
        {
            foreach (TraitContainer trait in traits)
            {
                foreach (Tag t in trait.trait.tags)
                {
                    if (t == tag && trait.isHidden == hidden)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public TraitContainer GetTrait(Trait trait)
        {
            foreach (TraitContainer container in traits)
            {
                if (container.trait == trait)
                {
                    return container;
                }
            }
            return null;
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

        public void ChangeMaxHealth(float value)
        {
            maxHealth += value;
            if (curHealth > maxHealth)
            {
                curHealth = maxHealth;
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

        public void ChangeMinStress(float value)
        {
            minStress += value;
            if (curStress < minStress)
            {
                curStress = minStress;
            }
        }

        public void ChangeCurLoyalty(float value)
        {
            curLoyalty += value;
            if (curLoyalty > maxLoyalty)
            {
                curLoyalty = maxLoyalty;
            }
            else if (curLoyalty < minLoyalty)
            {
                curHealth = minLoyalty;
            }
        }

        public void ChangeCurConfidence(float value)
        {
            curConfidence += value;
            if (curConfidence > maxConfidence)
            {
                curConfidence = maxConfidence;
            }
            else if (curConfidence < minConfidence)
            {
                curConfidence = minConfidence;
            }
        }


        public float confidence
        {
            get
            {
                return curConfidence;
            }
            set
            {
                curConfidence += value;
                if(curConfidence < 0)
                {
                    curConfidence = 0;
                }
                else if (curConfidence > maxConfidence)
                {
                    curConfidence = maxConfidence;
                }
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

        public Method GetPriorityMethod(bool brutal, bool careful, bool diplomat, bool science)
        {
            List<Method> m = new List<Method>();
            List<int> mV = new List<int>();
            for(int i=0; i< methods.Count; i++)
            {
                m.Add(methods[i]);
                mV.Add(methodsValues[i]);
            }
            for (int i=0; i< mV.Count-1; i++)
            {
                for(int j=i+1; j< mV.Count; j++)
                {
                    if (mV[i] < mV[j])
                    {
                        m.Insert(i, m[j]);
                        m.RemoveAt(j + 1);
                        mV.Insert(i, mV[j]);
                        mV.RemoveAt(j + 1);
                    }
                }
            }
            foreach(Method method in m)
            {
                if(method == Method.Brutal && brutal)
                {
                    return Method.Brutal;
                }
                else if (method == Method.Careful && careful)
                {
                    return Method.Careful;
                }
                else if (method == Method.Diplomatic && diplomat)
                {
                    return Method.Diplomatic;
                }
                else if (method == Method.Scientific && science)
                {
                    return Method.Scientific;
                }
            }
            return m[0];
        }

        public Temper GetPriorityTemper(bool rude, bool prudent, bool merciful, bool cruel, bool mercantile, bool principled)
        {
            if(rude && temper == Temper.RUDE)
            {
                return Temper.RUDE;
            }
            else if(prudent && temper == Temper.PRUDENT)
            {
                return Temper.PRUDENT;
            }
            else if (merciful && temper == Temper.MERCIFUL)
            {
                return Temper.MERCIFUL;
            }
            else if (cruel && temper == Temper.CRUEL)
            {
                return Temper.CRUEL;
            }
            else if (mercantile && temper == Temper.MERCANTILE)
            {
                return Temper.MERCANTILE;
            }
            else if (principled && temper == Temper.PRINCIPLED)
            {
                return Temper.PRINCIPLED;
            }
            else
            {
                List<Temper> tempers = new List<Temper>();
                if (rude)
                {
                    tempers.Add(Temper.RUDE);
                }
                else if (prudent)
                {
                    tempers.Add(Temper.PRUDENT);
                }
                else if (merciful)
                {
                    tempers.Add(Temper.MERCIFUL);
                }
                else if (cruel)
                {
                    tempers.Add(Temper.CRUEL);
                }
                else if (mercantile)
                {
                    tempers.Add(Temper.MERCANTILE);
                }
                else if (principled)
                {
                    tempers.Add(Temper.PRINCIPLED);
                }
                if(temper == Temper.RUDE)
                {
                    tempers.Remove(Temper.PRUDENT);
                }
                else if (temper == Temper.PRUDENT)
                {
                    tempers.Remove(Temper.RUDE);
                }
                else if (temper == Temper.MERCIFUL)
                {
                    tempers.Remove(Temper.CRUEL);
                }
                else if (temper == Temper.CRUEL)
                {
                    tempers.Remove(Temper.MERCIFUL);
                }
                else if (temper == Temper.MERCANTILE)
                {
                    tempers.Remove(Temper.PRINCIPLED);
                }
                else if (temper == Temper.PRINCIPLED)
                {
                    tempers.Remove(Temper.MERCANTILE);
                }
                return tempers[Random.Range(0, tempers.Count)];
            }
        }

        public string GetHealthDescription()
        {
            if (curHealth < 10)
            {
                return "присмерти";
            }
            else if (curHealth < 25)
            {
                return "изувечен";
            }
            else if (curHealth < 50)
            {
                return "серьезные ранения";
            }
            else if (curHealth< 75)
            {
                return "ранен";
            }
            else if (curHealth<90)
            {
                return "потрепан";
            }
            else
            {
                return "здоров";
            }
        }

        public string GetStressDescription()
        {
            if (maxStress - curStress < 10)
            {
                return "невменяем";
            }
            else if (maxStress - curStress < 25)
            {
                return "на грани";
            }
            else if (maxStress - curStress < 50)
            {
                return "не в себе";
            }
            else if (maxStress - curStress < 75)
            {
                return "изнеможден";
            }
            else if (maxStress - curStress < 90)
            {
                return "утомлен";
            }
            else
            {
                return "сосредоточен";
            }
        }

        public string GetMethodDescription(Method method)
        {
            if (GetMethodValue(method) < 10)
            {
                return "отвратительно";
            }
            else if (GetMethodValue(method) < 25)
            {
                return "плохо";
            }
            else if (GetMethodValue(method) < 50)
            {
                return "нормально";
            }
            else if (GetMethodValue(method) < 75)
            {
                return "хорошо";
            }
            else if (GetMethodValue(method) < 90)
            {
                return "отлично";
            }
            else
            {
                return "фантастически";
            }
        }
        
        public float GetTotalConfidece()
        {
            return curConfidence;
        }

        public bool IsObedient()
        {
            return curLoyalty >= GetTotalConfidece();
        }

        public void AddTrait(Trait trait)
        {
            GameObject goTrait = new GameObject(trait.name);
            goTrait.transform.parent = GetTraitFolder().transform;
            TraitContainer traitContainer = goTrait.AddComponent<TraitContainer>();
            traitContainer.trait = trait;
        }

        public void AddItem(Item newItem)
        {
            if(newItem is Equipment)
            {
                if (maxItemSlots - blockedSlots - _equipments.Count > 0)
                {
                    AddEquipment((Equipment)newItem);
                }
                else
                {
                    items.Add(newItem);
                }
            }
            else
            {
                items.Add(newItem);
            }
        }

        public void AddEquipment(Equipment newEquipment)
        {
            GameObject goEquipment;
            if (newEquipment != null)
            {
                goEquipment = new GameObject(newEquipment.name);
            }
            else
            {
                goEquipment = new GameObject("Equipment_");
            }
            goEquipment.transform.parent = GetEquipmentFolder().transform;
            EquipmentContainer equipment = goEquipment.AddComponent<EquipmentContainer>();
            equipment.equipment = newEquipment;
            equipment.owner = this;
            _equipments.Add(equipment);
        }

        public void ChangeBlockedSlot(int value)
        {
            blockedSlots += value;
        }

        public GameObject GetEquipmentFolder()
        {
            if(equipmentFolder == null)
            {
                if (transform.Find("Equipments"))
                {
                    equipmentFolder = transform.Find("Equipments").gameObject;
                }
            }
            if (equipmentFolder == null)
            {
                equipmentFolder = new GameObject("Equipments");
                equipmentFolder.transform.parent = transform;
            }
            return equipmentFolder;
        }

        public GameObject GetTraitFolder()
        {
            if (traitFolder == null)
            {
                if (transform.Find("Traits"))
                {
                    traitFolder = transform.Find("Traits").gameObject;
                }
            }
            if (traitFolder == null)
            {
                traitFolder = new GameObject("Traits");
                traitFolder.transform.parent = transform;
            }
            return traitFolder;
        }

        public int GetMaxItemSlot()
        {
            return maxItemSlots;
        }

        public void ChangeMaxItemSlot(int value)
        {
            maxItemSlots += value;
            int dif = GetEquipment().Count - (maxItemSlots - blockedSlots);
            if (dif > 0)
            {
                for(int i=0; i<dif; i++)
                {
                    EquipmentContainer equipmentContainer = GetEquipment()[GetEquipment().Count - 1 - dif];
                    items.Add(equipmentContainer.equipment);
                    Destroy(equipmentContainer.gameObject);
                    GetEquipment().RemoveAt(GetEquipment().Count - 1 - dif);
                }
            }
        }
    }

    public enum Temper
    {
        RUDE,
        PRUDENT,
        MERCIFUL,
        CRUEL,
        MERCANTILE,
        PRINCIPLED,
        NULL
    }
}

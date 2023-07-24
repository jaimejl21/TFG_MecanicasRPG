using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class FightCharacter : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject select, lifeBar, specialBar, deBuffsUI;
    GameObject targets;
    FightController fightCntrl;
    public SpriteRenderer sr;
    Character.Info charInfo;
    ComboController cc;
    Color typeColor;

    public int position, atkBuffTurns = 0, atkDebuffTurns = 0, defBuffTurns = 0, defDebuffTurns = 0;
    int target, effectiveType, weakType, charType;
    public bool type, specialActivated = false;
    public string abilityType;
    public float life, special, attack, defense;
    float maxLife, maxSpecial, scaleI, atkBuff, atkDebuff, defBuff, defDebuff;

    public List<Character.Info> allCharList;

    private  void Start()
    {
        fightCntrl = FindObjectOfType<FightController>();
        cc = fightCntrl.comboCntrl;
        allCharList = GameManager.allChar.ToList();

        charInfo = transform.GetComponent<Character>().info;
        gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = "" + charInfo.id;
        charType = transform.GetComponent<Character>().info.type;
        SetTypeStats();

        scaleI = lifeBar.transform.localScale.x;
        maxLife = charInfo.stats.hp;
        life = maxLife;

        maxSpecial = maxLife;
        special = 0;

        attack = charInfo.stats.atk;
        defense = charInfo.stats.def;

        SetUlti(charInfo.special);

        if (type)
        {
            targets = GameObject.Find("Enemies");
        }
        else
        {
            targets = GameObject.Find("Players");
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!type)
        {
            fightCntrl.enemySelect = position;
            fightCntrl.pointerEnemy = fightCntrl.enemiesPositions.IndexOf(position);

            for (int i=0; i<fightCntrl.enemiesPositions.Count; i++)
            {
                fightCntrl.enemies.transform.GetChild(fightCntrl.enemiesPositions[i]).GetChild(0).GetComponent<FightCharacter>().Select(false);
            }
            fightCntrl.enemies.transform.GetChild(fightCntrl.enemySelect).GetChild(0).GetComponent<FightCharacter>().Select(true);
            Debug.Log("enemySelect: " + fightCntrl.enemySelect);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {}

    public void OnPointerUp(PointerEventData eventData){}

    public void Attack()
    {
        StartCoroutine(AnimAttack());
        if(type)
        {
            target = fightCntrl.enemySelect;
        }
        else
        {
            target = fightCntrl.playerSelect;
        }
        if(fightCntrl.enemiesN >= 0 && fightCntrl.playersN >= 0)
        {
            float typeBonus = GetTypeBonus(targets.transform.GetChild(target).GetChild(0).GetComponent<Character>().info.type);
            if(type)
            {
                targets.transform.GetChild(target).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack * cc.nameAtkVar + cc.timesAtkVar);
            }
            else
            {
                targets.transform.GetChild(target).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack);
            }
        }
        AddSpecial(attack * 2);
    }

    public void SetUlti(int sp)
    {
        switch (sp)
        {
            case 0:
                abilityType = "healAll";
                break;
            case 1:
                abilityType = "heal";
                break;
            case 2:
                abilityType = "attackAll";
                break;
            case 3:
                abilityType = "attackRow";
                break;
            case 4:
                abilityType = "attackColumn";
                break;
            case 5:
                abilityType = "buffAtkAll";
                break;
            case 6:
                abilityType = "debuffAtkAll";
                break;
            case 7:
                abilityType = "buffDefAll";
                break;
            case 8:
                abilityType = "debuffDefAll";
                break;
            case 9:
                abilityType = "buffAtk";
                break;
            case 10:
                abilityType = "debuffAtk";
                break;
            case 11:
                abilityType = "buffDef";
                break;
            case 12:
                abilityType = "debuffDef";
                break;
            default:
                break;
        }
    }

    public void SpecialAbility()
    {
        switch (abilityType)
        {
            case "healAll":
                for (int i = 0; i < 6; i++)
                {
                    HealCharacters(i, ((70f/100f) * attack));
                }
                break;
            case "heal":
                if(type)
                {
                    HealCharacters(fightCntrl.playerSelect, attack);
                }
                else
                {
                    HealCharacters(position, attack);
                }
                break;
            case "attackAll":
                StartCoroutine(AnimAttack());
                for(int i = 0; i < 6; i++)
                {
                    DamageCharacters(i, ((60f / 100f) * attack));
                }
                break;
            case "attackRow":
                StartCoroutine(AnimAttack());
                if(type)
                {
                    if (fightCntrl.enemySelect == 0 || fightCntrl.enemySelect == 3)
                    {
                        DamageCharacters(0, ((80f / 100f) * attack));
                        DamageCharacters(3, ((80f / 100f) * attack));
                    }
                    else if (fightCntrl.enemySelect == 1 || fightCntrl.enemySelect == 4)
                    {
                        DamageCharacters(1, ((80f / 100f) * attack));
                        DamageCharacters(4, ((80f / 100f) * attack));
                    }
                    else
                    {
                        DamageCharacters(2, ((80f / 100f) * attack));
                        DamageCharacters(5, ((80f / 100f) * attack));
                    }
                }
                else
                {
                    if (fightCntrl.playerSelect == 0 || fightCntrl.playerSelect == 3)
                    {
                        DamageCharacters(0, ((80f / 100f) * attack));
                        DamageCharacters(3, ((80f / 100f) * attack));
                    }
                    else if (fightCntrl.playerSelect == 1 || fightCntrl.playerSelect == 4)
                    {
                        DamageCharacters(1, ((80f / 100f) * attack));
                        DamageCharacters(4, ((80f / 100f) * attack));
                    }
                    else
                    {
                        DamageCharacters(2, ((80f / 100f) * attack));
                        DamageCharacters(5, ((80f / 100f) * attack));
                    }
                }
                
                break;
            case "attackColumn":
                StartCoroutine(AnimAttack());
                if(type)
                {
                    if (fightCntrl.enemySelect == 0 || fightCntrl.enemySelect == 1 || fightCntrl.enemySelect == 2)
                    {
                        DamageCharacters(0, ((70f / 100f) * attack));
                        DamageCharacters(1, ((70f / 100f) * attack));
                        DamageCharacters(2, ((70f / 100f) * attack));
                    }
                    else
                    {
                        DamageCharacters(3, ((70f / 100f) * attack));
                        DamageCharacters(4, ((70f / 100f) * attack));
                        DamageCharacters(5, ((70f / 100f) * attack));
                    }
                }
                else
                {
                    if (fightCntrl.playerSelect == 0 || fightCntrl.playerSelect == 1 || fightCntrl.playerSelect == 2)
                    {
                        DamageCharacters(0, ((70f / 100f) * attack));
                        DamageCharacters(1, ((70f / 100f) * attack));
                        DamageCharacters(2, ((70f / 100f) * attack));
                    }
                    else
                    {
                        DamageCharacters(3, ((70f / 100f) * attack));
                        DamageCharacters(4, ((70f / 100f) * attack));
                        DamageCharacters(5, ((70f / 100f) * attack));
                    }
                }
                break;
            case "buffAtkAll":
                for (int i = 0; i < 6; i++)
                {
                    DeBuffStatChars(i, true, true, 3, ((30f / 100f) * attack));
                }                   
                break;
            case "debuffAtkAll":
                for (int i = 0; i < 6; i++)
                {
                    DeBuffStatChars(i, false, true, 3, ((30f / 100f) * attack));
                }                   
                break;
            case "buffDefAll":
                for (int i = 0; i < 6; i++)
                {
                    DeBuffStatChars(i, true, false, 3, ((30f / 100f) * attack));
                }                   
                break;
            case "debuffDefAll":
                for (int i = 0; i < 6; i++)
                {
                    DeBuffStatChars(i, false, false, 3, ((30f / 100f) * attack));
                }                
                break;
            case "buffAtk":
                if(type)
                {
                    DeBuffStatChars(fightCntrl.playerSelect, true, true, 3, ((30f / 100f) * attack));

                }
                else
                {
                    DeBuffStatChars(position, true, true, 3, ((30f / 100f) * attack));
                }               
                break;
            case "debuffAtk":
                if (!type)
                {
                    DeBuffStatChars(fightCntrl.playerSelect, false, true, 3, ((30f / 100f) * attack));
                }
                else
                {
                    DeBuffStatChars(fightCntrl.enemySelect, false, true, 3, ((30f / 100f) * attack));
                }
                break;
            case "buffDef":
                if (type)
                {
                    DeBuffStatChars(fightCntrl.playerSelect, true, false, 3, ((30f / 100f) * attack));
                }
                else
                {
                    DeBuffStatChars(position, true, false, 3, ((30f / 100f) * attack));
                }
                break;
            case "debuffDef":
                if (!type)
                {
                    DeBuffStatChars(fightCntrl.playerSelect, false, false, 3, ((30f / 100f) * attack));
                }
                else
                {
                    DeBuffStatChars(fightCntrl.enemySelect, false, false, 3, ((30f / 100f) * attack));
                }
                break;
            default:
                break;
        }
        ResetSpecial();
    }

    public void ResetSpecial()
    {
        special = 0;
        specialBar.transform.localScale = new Vector3(0f, specialBar.transform.localScale.y, specialBar.transform.localScale.z);
        if(type)
        {
            int index = fightCntrl.atkBtnsIds.IndexOf(gameObject.GetComponent<Character>().info.id);
            fightCntrl.listAttackButtons[index].GetComponent<AttackButton>().specialActivated = false;
            fightCntrl.listAttackButtons[index].GetComponent<AttackButton>().specialButton.interactable = false;
        }
        specialActivated = false;
    }

    public void DamageCharacters(int position, float attack)
    {
        if (type)
        {
            if (fightCntrl.enemiesPositions.Contains(position))
            {
                float typeBonus = GetTypeBonus(GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<Character>().info.type);
                if (type)
                {
                    GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack * cc.nameAtkVar + cc.timesAtkVar);
                }
                else
                {
                    GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack);
                }
            }
        }
        else
        {
            if (fightCntrl.playersPositions.Contains(position))
            {
                float typeBonus = GetTypeBonus(GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<Character>().info.type);
                if (type)
                {
                    GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack * cc.nameAtkVar + cc.timesAtkVar);
                }
                else
                {
                    GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack);     
                }
            }
        } 
    }

    public void HealCharacters(int position, float amount)
    {
        amount = (float)Math.Round(amount, 0);
        if (type)
        {
            if (fightCntrl.playersPositions.Contains(position))
            {
                GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Heal(amount);
            }
        }
        else
        {
            if (fightCntrl.enemiesPositions.Contains(position))
            {
                GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Heal(amount);
            }
        }
    }

    public void Heal(float amount)
    {
        amount = (float)Math.Round(amount, 0);
        //Debug.Log("Amount heal: " + amount);
        if (life < maxLife)
        {
            StartCoroutine(AnimHeal(amount));
            if ((life + amount) > maxLife)
            {
                life = maxLife;
            }
            else
            {
                life += amount;
            }
        }
    }

    public void DeBuffStat(bool buff, bool atk, int turns, float amount)
    {
        amount = (float)Math.Round(amount, 0);
        //Debug.Log("Amount deBuff: " + amount);
        if (buff)
        {
            if(atk)
            {
                attack += amount;
                atkBuff = amount;
                if (atkBuffTurns <= 0)
                {
                    atkBuffTurns = turns;
                }
                else
                {
                    atkBuffTurns += turns;
                }
                //Debug.Log("Id: " + charInfo.id + " Buff atk " + atkBuff + " " + atkBuffTurns + " turns");
            }
            else
            {
                defense += amount;
                defBuff = amount;
                if (defBuffTurns <= 0)
                {
                    defBuffTurns = turns;
                }
                else
                {
                    defBuffTurns += turns;
                }
                //Debug.Log("Id: " + charInfo.id + " Buff def " +defBuff + " " + defBuffTurns + " turns");
            }
        }
        else
        {
            if (atk)
            {
                if((attack - amount) >= 0)
                {
                    attack -= amount;
                    atkDebuff = amount;
                }
                else
                {
                    atkDebuff = (attack - 1);
                    attack = 1;
                }
                if (atkDebuffTurns <= 0)
                {
                    atkDebuffTurns = turns;
                }
                else
                {
                    atkDebuffTurns += turns;
                }
                //Debug.Log("Id: " + charInfo.id + " Debuff atk " + atkDebuff + " " + atkDebuffTurns + " turns");
            }
            else
            {
                if ((defense - amount) >= 0)
                {
                    defense -= amount;
                    defDebuff = amount;
                }
                else
                {
                    defDebuff = (defense - 1);
                    defense = 1;
                }
                if (defDebuffTurns <= 0)
                {
                    defDebuffTurns = turns;
                }
                else
                {
                    defDebuffTurns += turns;
                }
                //Debug.Log("Id: " + charInfo.id + " Debuff def " + defDebuff + " " + defDebuffTurns + " turns");
            }
        }
        DeBuffsUIManager(true, atk, buff);
    }

    void DeBuffStatChars(int position, bool buff, bool atk, int turns, float amount)
    {
        if (type)
        {
            if (buff)
            {
                if (fightCntrl.playersPositions.Contains(position))
                {
                    GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().DeBuffStat(buff, atk, turns, amount);
                }
            }
            else
            {
                if (fightCntrl.enemiesPositions.Contains(position))
                {
                    GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().DeBuffStat(buff, atk, turns, amount);
                }
            }
        }
        else
        {
            if (buff)
            {
                if (fightCntrl.enemiesPositions.Contains(position))
                {
                    GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().DeBuffStat(buff, atk, turns, amount);
                }
            }
            else
            {
                if (fightCntrl.playersPositions.Contains(position))
                {
                    GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().DeBuffStat(buff, atk, turns, amount);
                }
            }
        }
    }

    public void DeBuffsUIManager(bool active, bool atk, bool buff)
    {
        if(atk)
        {
            if (active) { deBuffsUI.transform.GetChild(0).gameObject.SetActive(true); }

            if (buff)
            {
                deBuffsUI.transform.GetChild(0).GetChild(1).gameObject.SetActive(active);
            }
            else
            {
                deBuffsUI.transform.GetChild(0).GetChild(2).gameObject.SetActive(active);
            }
        }
        else
        {
            if (active) { deBuffsUI.transform.GetChild(1).gameObject.SetActive(true); }

            if (buff)
            {
                deBuffsUI.transform.GetChild(1).GetChild(1).gameObject.SetActive(active);
            }
            else
            {
                deBuffsUI.transform.GetChild(1).GetChild(2).gameObject.SetActive(active);
            }
        }
    }

    public void CheckDeBuffsTurns()
    {
        CheckDeBuffsTurnsAux(true, true);
        CheckDeBuffsTurnsAux(false, true);
        CheckDeBuffsTurnsAux(true, false);
        CheckDeBuffsTurnsAux(false, false);
    }

    public void CheckDeBuffsTurnsAux(bool buff, bool atk)
    {
        if(buff)
        {
            if(atk)
            {
                if (atkBuffTurns > 1)
                {
                    atkBuffTurns--;
                }
                else if (atkBuffTurns == 1)
                {
                    atkBuffTurns--;
                    attack -= atkBuff;
                    DeBuffsUIManager(false, atk, buff);
                    if (atkBuffTurns == 0 && atkDebuffTurns == 0)
                    {
                        deBuffsUI.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (defBuffTurns == 0 && defDebuffTurns == 0)
                    {
                        deBuffsUI.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (defBuffTurns > 1)
                {
                    defBuffTurns--;
                }
                else if (defBuffTurns == 1)
                {
                    defBuffTurns--;
                    defense -= defBuff;
                    DeBuffsUIManager(false, atk, buff);
                    if (atkBuffTurns == 0 && atkDebuffTurns == 0)
                    {
                        deBuffsUI.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (defBuffTurns == 0 && defDebuffTurns == 0)
                    {
                        deBuffsUI.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (atk)
            {
                if (atkDebuffTurns > 1)
                {
                    atkDebuffTurns--;
                }
                else if (atkDebuffTurns == 1)
                {
                    atkDebuffTurns--;
                    attack += atkDebuff;
                    DeBuffsUIManager(false, atk, buff);
                    if (atkBuffTurns == 0 && atkDebuffTurns == 0)
                    {
                        deBuffsUI.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (defBuffTurns == 0 && defDebuffTurns == 0)
                    {
                        deBuffsUI.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (defDebuffTurns > 1)
                {
                    defDebuffTurns--;
                }
                else if (defDebuffTurns == 1)
                {
                    defDebuffTurns--;
                    defense += defDebuff;
                    DeBuffsUIManager(false, atk, buff);
                    if (atkBuffTurns == 0 && atkDebuffTurns == 0)
                    {
                        deBuffsUI.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (defBuffTurns == 0 && defDebuffTurns == 0)
                    {
                        deBuffsUI.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void AddSpecial(float amount)
    {
        amount = (float)Math.Round(amount, 0);
        if (amount < 5) amount = 5;
        if (special < maxSpecial)
        {
            StartCoroutine(AnimChargeSpecial(amount));
            if ((special + amount) > maxSpecial)
            {
                special = maxSpecial;
            }
            else
            {
                special += amount;
            }
        }
        if(special >= maxSpecial)
        {
            if(type)
            {
                int index = fightCntrl.atkBtnsIds.IndexOf(gameObject.GetComponent<Character>().info.id);
                fightCntrl.listAttackButtons[index].GetComponent<AttackButton>().specialActivated = true;
            }
            specialActivated = true;
        }
    }

    public void Damage(float amount)
    {
        amount = (float)Math.Round(amount, 0);
        amount -= defense;
        if (amount <= 0) amount = 1;
        //Debug.Log("Amount dmg: " + amount);
        fightCntrl.typeBonusTxt.text = amount.ToString();
        fightCntrl.dmgTxtAnim.SetBool("Dmg", true);
        life -= amount;
        //Debug.Log("life = " + life);
        StartCoroutine(AnimDamage(amount));
        if(life <= 0)
        {
            //Debug.Log("Dead // life = " + life);
            if(type)
            {
                if (fightCntrl.playersN == 0)
                {
                    fightCntrl.playersN--;
                    cc.SetAttackingFalse();
                    fightCntrl.SetResult();
                }
                else
                {
                    fightCntrl.playersPositions.RemoveAt(fightCntrl.playersPositions.IndexOf(position));
                    fightCntrl.playerSelect = fightCntrl.playersPositions[0];
                    fightCntrl.pointerPlayer = 0;

                    int index = fightCntrl.atkBtnsIds.IndexOf(gameObject.GetComponent<Character>().info.id);
                    fightCntrl.listAttackButtons[index].GetComponent<AttackButton>().isAlive = false;

                    fightCntrl.playersN--;
                }
            }
            else
            {
                if (fightCntrl.enemiesN == 0)
                {
                    fightCntrl.enemiesN--;
                    cc.SetAttackingFalse();
                    fightCntrl.SetResult();
                }
                else
                {
                    fightCntrl.enemiesPositions.RemoveAt(fightCntrl.enemiesPositions.IndexOf(position));
                    fightCntrl.enemySelect = fightCntrl.enemiesPositions[0];
                    fightCntrl.pointerEnemy = 0;
                    for (int i = 0; i < fightCntrl.enemiesPositions.Count; i++)
                    {
                        fightCntrl.enemies.transform.GetChild(fightCntrl.enemiesPositions[i]).GetChild(0).GetComponent<FightCharacter>().Select(false);
                    }
                    fightCntrl.enemies.transform.GetChild(fightCntrl.enemySelect).GetChild(0).GetComponent<FightCharacter>().Select(true);
                    fightCntrl.enemiesN--;
                }
            }
            Destroy(gameObject);
        }
    }

    //IEnumerator WaitTime(float time)
    //{
    //    yield return new WaitForSecondsRealtime(time);
    //}

    IEnumerator AnimAttack()
    {
        float mov = 0.3f;
        if(!type)
        {
            mov *= -1;
        }
        transform.position = new Vector3(transform.position.x + mov, transform.position.y, transform.position.z);
        yield return new WaitForSecondsRealtime(0.2f);
        transform.position = new Vector3(transform.position.x - mov, transform.position.y, transform.position.z);
    }

    IEnumerator AnimDamage(float damage)
    {
        //Debug.Log("damage " + damage + "/ maxLife " + maxLife + " = " + damage / maxLife);
        lifeBar.transform.localScale = new Vector3(lifeBar.transform.localScale.x - ((float)Math.Round(damage / maxLife, 2)) * scaleI, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        for(int i=0; i<10; i++)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    IEnumerator AnimChargeSpecial(float amount)
    {
        if ((special + amount) > maxSpecial)
        {
            amount = maxSpecial - special;
        }
        yield return new WaitForSecondsRealtime(0.2f);
        specialBar.transform.localScale = new Vector3(specialBar.transform.localScale.x + ((float)Math.Round(amount / maxLife, 2)) * scaleI, specialBar.transform.localScale.y, specialBar.transform.localScale.z);
    }

    IEnumerator AnimHeal(float heal)
    {
        if ((life + heal) > maxLife)
        {
            heal = maxLife - life;
        }
        lifeBar.transform.localScale = new Vector3(lifeBar.transform.localScale.x + ((float)Math.Round(heal / maxLife, 2)) * scaleI, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        for (int i = 0; i < 4; i++)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    public void Select(bool select)
    {
        this.select.SetActive(select);
        //Debug.Log("Select: " + select);
    }

    void SetTypeStats()
    {
        switch (charType)
        {
            case 0:
                //Ancestral
                typeColor = Color.white;
                effectiveType = 1;
                weakType = 2;
                break;
            case 1:
                //Oscura
                typeColor = new Color(.5f, .2f, .6f, 1f);
                effectiveType = 2;
                weakType = 0;
                break;
            case 2:
                //Naturaleza
                typeColor = new Color(.5f, .3f, 0f, 1f);
                effectiveType = 0;
                weakType = 1;
                break;
            case 3:
                //Astucia
                typeColor = Color.green;
                effectiveType = 4;
                weakType = 6;
                break;
            case 4:
                //Vitalidad
                typeColor = Color.yellow;
                effectiveType = 5;
                weakType = 3;
                break;
            case 5:
                //Velocidad
                typeColor = Color.blue;
                effectiveType = 6;
                weakType = 4;
                break;
            case 6:
                //Fuerza
                typeColor = Color.red;
                effectiveType = 3;
                weakType = 5;
                break;
            default:
                break;
        }
        gameObject.transform.GetComponent<SpriteRenderer>().color = typeColor;
    }

    float GetTypeBonus(int objType)
    {
        if (objType == effectiveType)
        {
            fightCntrl.typeBonusTxt.color = Color.green;
            return 2f;
        }
        else if (objType == weakType)
        {
            fightCntrl.typeBonusTxt.color = Color.red;
            return 0.5f;
        }
        else
        {
            fightCntrl.typeBonusTxt.color = Color.white;
            return 1f;
        }
    }
}

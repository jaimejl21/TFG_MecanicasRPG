using System;
using System.Collections;
using System.Collections.Generic;
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
    public float life, special;
    float maxLife, maxSpecial, attack, defense, scaleI, atkBuff, atkDebuff, defBuff, defDebuff;

    private  void Start()
    {
        fightCntrl = FindObjectOfType<FightController>();
        cc = fightCntrl.comboCntrl;

        charInfo = transform.GetComponent<Character>().info;
        gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = "" + charInfo.id;
        charType = transform.GetComponent<Character>().info.type;
        SetTypeStats();

        scaleI = lifeBar.transform.localScale.x;
        maxLife = charInfo.stats.hp;

        maxSpecial = maxLife;
        special = 0;

        attack = charInfo.stats.atk;
        defense = charInfo.stats.def;

        SetUlti(true, "");

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
                fightCntrl.typeBonusTxt.text = (typeBonus * attack * cc.nameAtkVar + cc.timesAtkVar).ToString();
            }
            else
            {
                targets.transform.GetChild(target).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack);
                fightCntrl.typeBonusTxt.text = (typeBonus * attack).ToString();
            }
        }
        AddSpecial();
    }

    public void SetUlti(bool rdm, string name)
    {
        if(rdm)
        {
            int rand = new Random().Next(0, 13);
            switch(rand)
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
        else
        {
            abilityType = name;
        }
    }

    public void SpecialAbility()
    {
        switch (abilityType)
        {
            case "healAll":
                for (int i = 0; i < 6; i++)
                {
                    HealCharacters(i, 10f);
                }
                break;
            case "heal":
                if(type)
                {
                    HealCharacters(fightCntrl.playerSelect, 10f);
                }
                else
                {
                    HealCharacters(fightCntrl.enemySelect, 10f);
                }
                break;
            case "attackAll":
                StartCoroutine(AnimAttack());
                for(int i = 0; i < 6; i++)
                {
                    DamageCharacters(i);
                }
                break;
            case "attackRow":
                StartCoroutine(AnimAttack());
                if(type)
                {
                    if (fightCntrl.enemySelect == 0 || fightCntrl.enemySelect == 3)
                    {
                        DamageCharacters(0);
                        DamageCharacters(3);
                    }
                    else if (fightCntrl.enemySelect == 1 || fightCntrl.enemySelect == 4)
                    {
                        DamageCharacters(1);
                        DamageCharacters(4);
                    }
                    else
                    {
                        DamageCharacters(2);
                        DamageCharacters(5);
                    }
                }
                else
                {
                    if (fightCntrl.playerSelect == 0 || fightCntrl.playerSelect == 3)
                    {
                        DamageCharacters(0);
                        DamageCharacters(3);
                    }
                    else if (fightCntrl.playerSelect == 1 || fightCntrl.playerSelect == 4)
                    {
                        DamageCharacters(1);
                        DamageCharacters(4);
                    }
                    else
                    {
                        DamageCharacters(2);
                        DamageCharacters(5);
                    }
                }
                
                break;
            case "attackColumn":
                StartCoroutine(AnimAttack());
                if(type)
                {
                    if (fightCntrl.enemySelect == 0 || fightCntrl.enemySelect == 1 || fightCntrl.enemySelect == 2)
                    {
                        DamageCharacters(0);
                        DamageCharacters(1);
                        DamageCharacters(2);
                    }
                    else
                    {
                        DamageCharacters(3);
                        DamageCharacters(4);
                        DamageCharacters(5);
                    }
                }
                else
                {
                    if (fightCntrl.playerSelect == 0 || fightCntrl.playerSelect == 1 || fightCntrl.playerSelect == 2)
                    {
                        DamageCharacters(0);
                        DamageCharacters(1);
                        DamageCharacters(2);
                    }
                    else
                    {
                        DamageCharacters(3);
                        DamageCharacters(4);
                        DamageCharacters(5);
                    }
                }
                break;
            case "buffAtkAll":
                for (int i = 0; i < 6; i++)
                {
                    DeBuffStatChars(i, true, ref attack, ref atkBuff, ref atkBuffTurns, 10f, true);
                }                   
                break;
            case "debuffAtkAll":
                for (int i = 0; i < 6; i++)
                {
                    DeBuffStatChars(i, false, ref attack, ref atkDebuff, ref atkDebuffTurns, 10f, true);
                }                   
                break;
            case "buffDefAll":
                for (int i = 0; i < 6; i++)
                {
                    DeBuffStatChars(i, true, ref defense, ref defBuff, ref defBuffTurns, 10f, false);
                }                   
                break;
            case "debuffDefAll":
                for (int i = 0; i < 6; i++)
                {
                    DeBuffStatChars(i, false, ref defense, ref defDebuff, ref defDebuffTurns, 10f, false);
                }                
                break;
            case "buffAtk":
                if(type)
                {
                    DeBuffStatChars(fightCntrl.playerSelect, true, ref attack, ref atkBuff, ref atkBuffTurns, 10f, true);

                }
                else
                {
                    DeBuffStatChars(fightCntrl.enemySelect, true, ref attack, ref atkBuff, ref atkBuffTurns, 10f, true);
                }               
                break;
            case "debuffAtk":
                if (!type)
                {
                    DeBuffStatChars(fightCntrl.playerSelect, false, ref attack, ref atkDebuff, ref atkDebuffTurns, 10f, true);
                }
                else
                {
                    DeBuffStatChars(fightCntrl.enemySelect, false, ref attack, ref atkDebuff, ref atkDebuffTurns, 10f, true);
                }
                break;
            case "buffDef":
                if (type)
                {
                    DeBuffStatChars(fightCntrl.playerSelect, true, ref defense, ref defBuff, ref defBuffTurns, 10f, false);
                }
                else
                {
                    DeBuffStatChars(fightCntrl.enemySelect, true, ref defense, ref defBuff, ref defBuffTurns, 10f, false);
                }
                break;
            case "debuffDef":
                if (!type)
                {
                    DeBuffStatChars(fightCntrl.playerSelect, false, ref defense, ref defDebuff, ref defDebuffTurns, 10f, false);
                }
                else
                {
                    DeBuffStatChars(fightCntrl.enemySelect, false, ref defense, ref defDebuff, ref defDebuffTurns, 10f, false);
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

    public void DamageCharacters(int position)
    {
        if(type)
        {
            if (fightCntrl.enemiesPositions.Contains(position))
            {
                float typeBonus = GetTypeBonus(GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<Character>().info.type);
                if (type)
                {
                    GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack * cc.nameAtkVar + cc.timesAtkVar);
                    fightCntrl.typeBonusTxt.text = (typeBonus * attack * cc.nameAtkVar + cc.timesAtkVar).ToString();
                }
                else
                {
                    GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack);
                    fightCntrl.typeBonusTxt.text = (typeBonus * attack).ToString();
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
                    fightCntrl.typeBonusTxt.text = (typeBonus * attack * cc.nameAtkVar + cc.timesAtkVar).ToString();
                }
                else
                {
                    GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().Damage(typeBonus * attack);
                    fightCntrl.typeBonusTxt.text = (typeBonus * attack).ToString();
                }
            }
        } 
    }

    public void HealCharacters(int position, float amount)
    {
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

    public void DeBuffStat(bool buff, ref float stat, ref float statInc, ref int deBuffTurns, float amount, bool atk)
    {
        if(buff)
        {
            stat += amount;
            statInc = amount;
        }
        else
        {
            if((stat - amount) >= 0)
            {
                stat -= amount;
                statInc = amount;
            }
            else
            {
                statInc = stat;
                stat = 0;
            }
        }
        if(deBuffTurns <= 0)
        {
            deBuffTurns = 3;
        }
        else
        {
            deBuffTurns += 3;
        }
        DeBuffsUIManager(true, atk, buff);

        Debug.Log("Buff: " + buff + " Atk: " + atk + " Turns: " + deBuffTurns);
    }

    void DeBuffStatChars(int position, bool buff, ref float stat, ref float statInc, ref int deBuffTurns, float amount, bool atk)
    {
        if (type)
        {
            if(buff)
            {
                if (fightCntrl.playersPositions.Contains(position))
                {
                    GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().DeBuffStat(buff, ref stat, ref statInc, ref deBuffTurns, amount, atk);
                }
            }
            else
            {
                if (fightCntrl.enemiesPositions.Contains(position))
                {
                    GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().DeBuffStat(buff, ref stat, ref statInc, ref deBuffTurns, amount, atk);
                }                  
            }           
        }
        else
        {
            if (buff)
            {
                if (fightCntrl.enemiesPositions.Contains(position))
                {
                    GameObject.Find("Enemies").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().DeBuffStat(buff, ref stat, ref statInc, ref deBuffTurns, amount, atk);
                }
            }
            else
            {
                if (fightCntrl.playersPositions.Contains(position))
                {
                    GameObject.Find("Players").transform.GetChild(position).GetChild(0).GetComponent<FightCharacter>().DeBuffStat(buff, ref stat, ref statInc, ref deBuffTurns, amount, atk);
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
        CheckDeBuffsTurnsAux(true, ref attack, ref atkBuffTurns, ref atkBuff, true);
        CheckDeBuffsTurnsAux(false, ref attack, ref atkDebuffTurns, ref atkDebuff, true);
        CheckDeBuffsTurnsAux(true, ref defense, ref defBuffTurns, ref defBuff, false);
        CheckDeBuffsTurnsAux(false, ref defense, ref defDebuffTurns, ref defDebuff, false);
    }

    public void CheckDeBuffsTurnsAux(bool buff, ref float stat, ref int deBuffTurns, ref float statInc, bool atk)
    {       
        if(deBuffTurns > 1)
        {
            deBuffTurns--;
        }
        else if(deBuffTurns == 1)
        {
            deBuffTurns--;
            if(buff)
            {
                stat -= statInc;
            }
            else
            {
                stat += statInc;
            }
            DeBuffsUIManager(false, atk, buff);
            if(atkBuffTurns == 0  && atkDebuffTurns == 0)
            {                
                deBuffsUI.transform.GetChild(0).gameObject.SetActive(false);
            }
            if (defBuffTurns == 0 && defDebuffTurns == 0)
            {
                deBuffsUI.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        Debug.Log("Buff: " + buff + "Stat: " + nameof(stat) + " Turns: " + deBuffTurns);
    }

    public void AddSpecial()
    {
        int amount = 50;
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
        amount -= defense;
        life -= amount;
        StartCoroutine(AnimDamage(amount));
        if(life <= 0)
        {
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
        fightCntrl.typeBonusTxt.text = "";
    }

    IEnumerator AnimDamage(float damage)
    {
        lifeBar.transform.localScale = new Vector3(lifeBar.transform.localScale.x - (damage / maxLife) * scaleI, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
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
        specialBar.transform.localScale = new Vector3(specialBar.transform.localScale.x + (amount / maxSpecial) * scaleI, specialBar.transform.localScale.y, specialBar.transform.localScale.z);
    }

    IEnumerator AnimHeal(float heal)
    {
        if ((life + heal) > maxLife)
        {
            heal = maxLife - life;
        }
        lifeBar.transform.localScale = new Vector3(lifeBar.transform.localScale.x + (heal / maxLife) * scaleI, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        for (int i = 0; i < 10; i++)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSecondsRealtime(0.05f);
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

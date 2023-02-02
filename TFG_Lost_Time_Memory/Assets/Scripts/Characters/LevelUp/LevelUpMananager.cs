using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelUpMananager : MonoBehaviour
{
    public GameObject charGO, pool, selectedPool, materialGO;
    public Transform charPos;
    public TextMeshProUGUI[] statsTxt, statsBonusTxt;
    public TextMeshProUGUI lvTxt;

    public int idToEquip, amountC, amountR, amountSR;
    int selectedC, selectedR, selectedSR, exp, maxExp, level;

    public List<Character.Info> allCharList;
    public ExpSlider expSlider;

    private void Start()
    {
        allCharList = GameManager.allChar;

        pool = GameObject.FindGameObjectWithTag("Pool");
        selectedPool = GameObject.Find("SelectedPool");

        idToEquip = GameManager.inst.charToEquipGear;
        charGO.transform.GetComponent<Character>().info = GameManager.inst.GetCharInfoById(idToEquip);
        Instantiate(charGO, charPos);

        level = charGO.transform.GetComponent<Character>().info.level;
        exp = charGO.transform.GetComponent<Character>().info.exp;
        maxExp = charGO.transform.GetComponent<Character>().info.expNextLv;

        lvTxt.text = "Lv. " + level;

        SetAmounts();
        MaterialsInventory();
        InitBaseStatsTxt();
    }

    void SetAmounts()
    {
        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("amountC"))
        {
            amountC = PlayerPrefs.GetInt("amountC");
        }
        else
        {
            amountC = 10;
        }

        if (PlayerPrefs.HasKey("amountR"))
        {
            amountR = PlayerPrefs.GetInt("amountR");
        }
        else
        {
            amountR = 10;
        }

        if (PlayerPrefs.HasKey("amountSR"))
        {
            amountSR = PlayerPrefs.GetInt("amountSR");
        }
        else
        {
            amountSR = 10;
        }
    }

    void MaterialsInventory()
    {
        SetMaterials(0, amountC);
        SetMaterials(1, amountR);
        SetMaterials(2, amountSR);
    }

    void SetMaterials(int type, int amount)
    {
        if (amount != 0)
        {
            materialGO.GetComponent<LevelUpItem>().type = type;
            materialGO.GetComponent<LevelUpItem>().amount = amount;
            materialGO.GetComponent<LevelUpItem>().selected = false;
            Instantiate(materialGO, pool.transform);
        }  
    }

    public void SelectMaterial(bool selecType, int type, int expAm)
    {
        switch(type)
        {
            case 0:
                SelectionManager(selecType, type, expAm, ref selectedC, ref amountC);
                break;
            case 1:
                SelectionManager(selecType, type, expAm, ref selectedR, ref amountR);
                break;
            case 2:
                SelectionManager(selecType, type, expAm, ref selectedSR, ref amountSR);
                break;
            default:
                break;
        }
    }

    void SelectionManager(bool selecType, int type, int expAm, ref int selectedAm, ref int amount)
    {
        if(!selecType)
        {
            if (selectedAm == 0)
            {
                selectedAm++;
                materialGO.GetComponent<LevelUpItem>().type = type;
                materialGO.GetComponent<LevelUpItem>().amount = selectedAm;
                materialGO.GetComponent<LevelUpItem>().selected = true;
                Instantiate(materialGO, selectedPool.transform);
                amount--;
                ChangeAmounts(pool, type, amount);
            }
            else if(amount == 1)
            {
               amount--;
                for (int i = 0; i < pool.transform.childCount; i++)
                {
                    if (pool.transform.GetChild(i).transform.GetComponent<LevelUpItem>().type == type)
                    {
                        Destroy(pool.transform.GetChild(i).gameObject);
                    }
                }
                selectedAm++;
                ChangeAmounts(selectedPool, type, selectedAm);
            }
            else
            {
                selectedAm++;
                ChangeAmounts(selectedPool, type, selectedAm);
                amount--;
                ChangeAmounts(pool, type, amount);
            }
            UpdateExp(selecType, expAm);
        }
        else
        {
            if (selectedAm == 1)
            {
                selectedAm--;
                for (int i = 0; i < selectedPool.transform.childCount; i++)
                {
                    if (selectedPool.transform.GetChild(i).transform.GetComponent<LevelUpItem>().type == type)
                    {
                        Destroy(selectedPool.transform.GetChild(i).gameObject);
                    }
                }
                amount++;
                ChangeAmounts(pool, type, amount);
            }
            else if (amount == 0)
            {
                amount++;
                materialGO.GetComponent<LevelUpItem>().type = type;
                materialGO.GetComponent<LevelUpItem>().amount = amount;
                materialGO.GetComponent<LevelUpItem>().selected = false;
                Instantiate(materialGO, pool.transform);
                selectedAm--;
                ChangeAmounts(selectedPool, type, selectedAm);
            }
            else
            {
                selectedAm--;
                ChangeAmounts(selectedPool, type, selectedAm);
                amount++;
                ChangeAmounts(pool, type, amount);
            }
            UpdateExp(selecType, expAm);
        }
    }

    void ChangeAmounts(GameObject poolType, int type, int amount)
    {
        for (int i = 0; i < poolType.transform.childCount; i++)
        {
            if (poolType.transform.GetChild(i).transform.GetComponent<LevelUpItem>().type == type)
            {
                poolType.transform.GetChild(i).transform.GetComponent<LevelUpItem>().SetAmount(amount);
            }
        }
    }

    void UpdateExp(bool selectType, int expAm)
    {
        if (selectType)
        {
            exp -= expAm;
            if (exp < 0)
            {
                level--;
                lvTxt.text = "Lv. " + level;
                maxExp -= 320;
                exp = maxExp + exp;
            }
        }
        else
        {
            exp += expAm;
            if (exp >= maxExp)
            {
                level++;
                lvTxt.text = "Lv. " + level;
                exp -= maxExp;
                maxExp += 320;
            }
        }
        expSlider.UpdateValues(exp, maxExp);
    }

    public void UpdateCharStats(bool add, Gear.Info ginfo)
    {
        if(add)
        {
            if (ginfo.objType == 0 || ginfo.objType == 3)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraAtk += ginfo.statAmount;
            }
            else if (ginfo.objType == 1 || ginfo.objType == 4)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraDef += ginfo.statAmount;
            }
            else if (ginfo.objType == 2 || ginfo.objType == 5)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraHp += ginfo.statAmount;
            }
        }
        else
        {
            if (ginfo.objType == 0 || ginfo.objType == 3)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraAtk -= ginfo.statAmount;
            }
            else if (ginfo.objType == 1 || ginfo.objType == 4)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraDef -= ginfo.statAmount;
            }
            else if (ginfo.objType == 2 || ginfo.objType == 5)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraHp -= ginfo.statAmount;
            }
        }
        charGO.transform.GetComponent<Character>().info.stats.UpdateStats();
        UpdateStatsTxt();
    }

    void InitBaseStatsTxt()
    {
        statsTxt[0].text = "ATK: " + charGO.transform.GetComponent<Character>().info.stats.baseAtk;
        statsTxt[1].text = "DEF: " + charGO.transform.GetComponent<Character>().info.stats.baseDef;
        statsTxt[2].text = "HP: " + charGO.transform.GetComponent<Character>().info.stats.baseHp;
    }

    void UpdateStatsTxt()
    {
        statsBonusTxt[0].text = "+" + charGO.transform.GetComponent<Character>().info.stats.extraAtk;
        statsBonusTxt[1].text = "+" + charGO.transform.GetComponent<Character>().info.stats.extraDef;
        statsBonusTxt[2].text = "+" + charGO.transform.GetComponent<Character>().info.stats.extraHp;
    }

    //void AddSubtractStats(int statType, int amount)
    //{
    //    if (statType == 0)
    //    {
    //        charGO.transform.GetComponent<Character>().info.stats.extraAtk += amount;
    //    }
    //    else if (statType == 1)
    //    {
    //        charGO.transform.GetComponent<Character>().info.stats.extraDef += amount;
    //    }
    //    else if (statType == 2)
    //    {
    //        charGO.transform.GetComponent<Character>().info.stats.extraHp += amount;
    //    }
    //}

    public void SaveAmounts()
    {
        PlayerPrefs.SetInt("amountC", amountC);
        PlayerPrefs.SetInt("amountR", amountR);
        PlayerPrefs.SetInt("amountSR", amountSR);

        for (int i = 0; i < allCharList.Count; i++)
        {
            if (allCharList[i].id == idToEquip)
            {
                allCharList[i].level = charGO.transform.GetComponent<Character>().info.level;
            }
        }

        GameManager.inst.SaveListsToJson();
    }
}

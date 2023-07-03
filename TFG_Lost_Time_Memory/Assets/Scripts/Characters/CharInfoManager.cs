using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class CharInfoManager : MonoBehaviour
{
    public GameObject charGO;
    public Transform charPos;
    public TextMeshProUGUI[] txts;

    public int idToEquip;
    
    string typeName, weaponName, specialName;

    public List<Character.Info> allCharList;

    private void Start()
    {
        allCharList = GameManager.allChar.ToList();

        idToEquip = GameManager.inst.charToEquipGear;
        charGO.transform.GetComponent<Character>().info = GameManager.inst.GetCharInfoById(idToEquip);
        Instantiate(charGO, charPos);

        SetSpecial(charGO.transform.GetComponent<Character>().info.special);
        SetType();
        SetWeapon();
        InitTxts();
    }

    void InitTxts()
    {
        txts[0].text = "Nombre" + charGO.transform.GetComponent<Character>().info.name;
        txts[1].text = "Nivel: " + charGO.transform.GetComponent<Character>().info.level;
        txts[2].text = "Tipo: " + typeName;
        txts[3].text = "Arma: " + weaponName;
        txts[4].text = "Atq especial: " + specialName;
        txts[5].text = "Atq: " + charGO.transform.GetComponent<Character>().info.stats.baseAtk;
        txts[6].text = "Def: " + charGO.transform.GetComponent<Character>().info.stats.baseDef;
        txts[7].text = "Vida: " + charGO.transform.GetComponent<Character>().info.stats.baseHp;
        txts[8].text = "Character Info";
    }

    void SetType()
    {
        switch (charGO.transform.GetComponent<Character>().info.type)
        {
            case 0:
                typeName = "Magia ancestral";
                //typeColor = Color.white;
                break;
            case 1:
                typeName = "Magia oscura";
                //typeColor = new Color(.5f, .2f, .6f, 1f);
                break;
            case 2:
                typeName = "Magia de naturaleza";
                //typeColor = new Color(.5f, .3f, 0f, 1f);
                break;
            case 3:
                typeName = "Atucia";
                //typeColor = Color.green;
                break;
            case 4:
                typeName = "Vitalidad";
                //typeColor = Color.yellow;
                break;
            case 5:
                typeName = "Velocidad";
                //typeColor = Color.blue;
                break;
            case 6:
                typeName = "Fuerza";
                //typeColor = Color.red;
                break;
            default:
                break;
        }
    }

    void SetWeapon()
    {
        switch (charGO.transform.GetComponent<Character>().info.weapon)
        {
            case 6:
                weaponName = "Espada";
                break;
            case 7:
                weaponName = "Lanza";
                break;
            case 8:
                weaponName = "Guadaña";
                break;
            case 9:
                weaponName = "Daga";
                break;
            case 10:
                weaponName = "Bastón";
                break;
            case 11:
                weaponName = "Arco";
                break;
            case 12:
                weaponName = "Hacha";
                break;
            default:
                break;
        }
    }

    void SetSpecial(int sp)
    {
        switch (sp)
        {
            case -1:
                int rand = new Random().Next(0, 13);
                charGO.transform.GetComponent<Character>().info.special = rand;
                for (int i = 0; i < allCharList.Count; i++)
                {
                    if (allCharList[i].id == idToEquip)
                    {
                        allCharList[i] = charGO.transform.GetComponent<Character>().info;
                    }
                }
                GameManager.allChar = allCharList;
                GameManager.inst.SaveListsToJson();
                SetSpecial(rand);
                break;
            case 0:
                specialName = "healAll";
                break;
            case 1:
                specialName = "heal";
                break;
            case 2:
                specialName = "attackAll";
                break;
            case 3:
                specialName = "attackRow";
                break;
            case 4:
                specialName = "attackColumn";
                break;
            case 5:
                specialName = "buffAtkAll";
                break;
            case 6:
                specialName = "debuffAtkAll";
                break;
            case 7:
                specialName = "buffDefAll";
                break;
            case 8:
                specialName = "debuffDefAll";
                break;
            case 9:
                specialName = "buffAtk";
                break;
            case 10:
                specialName = "debuffAtk";
                break;
            case 11:
                specialName = "buffDef";
                break;
            case 12:
                specialName = "debuffDef";
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemyTeamGenerator : MonoBehaviour
{
    Gear.Info gi;
    List<Character.Info> auxCharList;
    List<int> posPos;

    float pAtk, pDef, pHp, eAtk, eDef, eHp;
    int pNum, eNum;

    void Start()
    {
        gi = new Gear.Info(-1, 10, -1, -1, 0, 0, 0, false, -1);

        auxCharList = new List<Character.Info>();
        auxCharList = GameManager.allChar.ToList();
        posPos = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

        pAtk = 0;
        pDef = 0; 
        pHp = 0;
        eAtk = 0;
        eDef = 0;
        eHp = 0;
        pNum = 0; 
        eNum = 0;

        GetStatsFromChars();
    }

    public List<Character.Info> GenerateEnemyTeam(int enemyTeam)
    {
        // id  type  race  pos  inTeam  List<Gear.Info> gear  level  exp  expNextLv  Stats stats

        List<Character.Info> enemyTeamList = new List<Character.Info>();
        int type;

        switch (enemyTeam)
        {
            case 0:
                ////Humanos random
                //for (int i = 0; i < 6; i++)
                //{
                //    type = new Random().Next(0, 7);
                //    enemyTeamList.Add(new Character.Info(i, "Humano " + i, type, 0, -1, -1, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                //}
                for (int i = 0; i < eNum; i++)
                {
                    type = new Random().Next(0, 7);
                    enemyTeamList.Add(new Character.Info(i, "Humano " + i, type, 0, -1, -1, RandomPos(eNum), false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats(eAtk, eDef, eHp, 0, 0, 0)));
                }
                break;
            case 1:
                //Orcos random
                //for (int i = 0; i < 6; i++)
                //{
                //    type = new Random().Next(0, 7);
                //    enemyTeamList.Add(new Character.Info(i, "Orco " + i, type, 1, -1, -1, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                //}
                for (int i = 0; i < eNum; i++)
                {
                    type = new Random().Next(0, 7);
                    enemyTeamList.Add(new Character.Info(i, "Orco " + i, type, 1, -1, -1, RandomPos(eNum), false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats(eAtk, eDef, eHp, 0, 0, 0)));
                }
                break;
            case 2:
                //Herrero mercader random
                type = new Random().Next(0, 7);
                enemyTeamList.Add(new Character.Info(0, "Humano", type, 0, -1, -1, 1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                break;
            case 3:
                type = new Random().Next(0, 7);
                enemyTeamList.Add(new Character.Info(0, "Orco", type, 1, -1, -1, 1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                break;
            case 4:
                break;
            case 5:  
                break;
            case 6:
                break;
            default:
                break;
        }

        return enemyTeamList;
    }

    void GetStatsFromChars()
    {
        for (int i = 0; i < auxCharList.Count; i++)
        {
            if (auxCharList[i].inTeam)
            {
                pNum++;
                pAtk += auxCharList[i].stats.baseAtk;
                pDef += auxCharList[i].stats.baseDef;
                pHp += auxCharList[i].stats.baseHp;
            }
        }

        eNum = new Random().Next(pNum, 7);

        eAtk = pAtk / (float) eNum;
        eDef = pDef / (float) eNum;
        eHp = pHp / (float) eNum;
    }

    int RandomPos(int max)
    {
        if (max == 1)
        {
            return 1;
        }
        else
        {
            int pos = -1;
            for (int i = 0; i < max; i++)
            {
                if (i == 0)
                {
                    posPos.RemoveAll(item => item == 1);
                    pos = 1;
                }
                else
                {
                    int pos1 = new Random().Next(0, posPos.Count);
                    int pos2 = posPos[pos1];
                    posPos.RemoveAll(item => item == pos2);
                    pos =  pos2;
                }
            }
            return pos;
        }
    }
}

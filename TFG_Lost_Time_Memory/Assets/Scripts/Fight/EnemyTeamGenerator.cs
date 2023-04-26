using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeamGenerator : MonoBehaviour
{
    int enemyTeam;

    public List<Character.Info> enemyTeamList;
    Gear.Info gi;

    void Start()
    {
        enemyTeam = GameManager.inst.enemyTeam;
        enemyTeamList = new List<Character.Info>();
        gi = new Gear.Info(-1, 10, -1, -1, 0, 0, 0, false, -1);
    }

    void GenerateEnemyTeam()
    {
        // id  type  race  pos  inTeam  List<Gear.Info> gear  level  exp  expNextLv  Stats stats;

        int type;

        switch (enemyTeam)
        {
            case 0:
                for (int i = 0; i < 39; i++)
                {
                    type = Random.Range(0, 7);
                    enemyTeamList.Add(new Character.Info(i, type, 0, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                }
                break;
            case 1:
                for (int i = 0; i < 39; i++)
                {
                    type = Random.Range(0, 7);
                    enemyTeamList.Add(new Character.Info(i, type, 1, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                }
                break;
            case 2:
                enemyTeamList.Add(new Character.Info(0, 0, 0, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                break;
            case 3:
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
    }
}

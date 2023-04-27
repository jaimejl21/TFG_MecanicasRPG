using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeamGenerator : MonoBehaviour
{
    Gear.Info gi;

    void Start()
    {
        gi = new Gear.Info(-1, 10, -1, -1, 0, 0, 0, false, -1);
    }

    public List<Character.Info> GenerateEnemyTeam(int enemyTeam)
    {
        // id  type  race  pos  inTeam  List<Gear.Info> gear  level  exp  expNextLv  Stats stats;

        List<Character.Info> enemyTeamList = new List<Character.Info>();
        int type;

        switch (enemyTeam)
        {
            case 0:
                for (int i = 0; i < 6; i++)
                {
                    type = Random.Range(0, 7);
                    enemyTeamList.Add(new Character.Info(i, type, 0, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                }
                break;
            case 1:
                for (int i = 0; i < 6; i++)
                {
                    type = Random.Range(0, 7);
                    enemyTeamList.Add(new Character.Info(i, type, 1, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                }
                break;
            case 2:
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

        return enemyTeamList;
    }
}

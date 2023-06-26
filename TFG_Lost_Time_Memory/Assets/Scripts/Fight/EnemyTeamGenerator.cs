using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyTeamGenerator : MonoBehaviour
{
    Gear.Info gi;

    void Start()
    {
        gi = new Gear.Info(-1, 10, -1, -1, 0, 0, 0, false, -1);
    }

    public List<Character.Info> GenerateEnemyTeam(int enemyTeam)
    {
        // id  type  race  pos  inTeam  List<Gear.Info> gear  level  exp  expNextLv  Stats stats

        List<Character.Info> enemyTeamList = new List<Character.Info>();
        int type;

        switch (enemyTeam)
        {
            case 0:
                //Humanos random
                for (int i = 0; i < 6; i++)
                {
                    type = new Random().Next(0, 7);
                    enemyTeamList.Add(new Character.Info(i, type, 0, -1, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                }
                break;
            case 1:
                //Orcos random
                for (int i = 0; i < 6; i++)
                {
                    type = new Random().Next(0, 7);
                    enemyTeamList.Add(new Character.Info(i, type, 1, -1, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                }
                break;
            case 2:
                //Herrero mercader random
                type = new Random().Next(0, 7);
                enemyTeamList.Add(new Character.Info(0, type, 0, -1, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                break;
            case 3:
                type = new Random().Next(0, 7);
                enemyTeamList.Add(new Character.Info(0, type, 1, -1, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
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

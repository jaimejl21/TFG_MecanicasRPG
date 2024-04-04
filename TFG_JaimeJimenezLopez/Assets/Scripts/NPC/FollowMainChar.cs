using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainChar : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;

    private Transform target; //Target to follow 
    [SerializeField]
    private Transform follower; //Follower object that have transform
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(follower.position,target.position) > stoppingDistance)
        {
            follower.position = Vector2.MoveTowards(follower.position, target.position, speed * Time.deltaTime);
        }
    }
    
}

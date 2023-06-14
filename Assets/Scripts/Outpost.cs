using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outpost : MonoBehaviour
{
    [SerializeField]
    private float flagTop;
    [SerializeField]
    private float flagBottom;
    private SkinnedMeshRenderer flag; //this is the flag we are going to move
    internal int team = -1;//this keeps track of the current team (-1 being no one). internal is public to the project

    //when a unit enters and stays within our area, after a certain interval we want to allow them to start collecting points (essentially, they captured the flag)
    private float timer; //this will keep track of the time within the outpost
    [SerializeField]
    private float scoreInterval = 5.0f;//how many seconds until someone gets a point
    [SerializeField] float valueIncrease = 0.005f;//how long for them to increase a value
    internal float currentValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        flag = GetComponentInChildren<SkinnedMeshRenderer>(); //this is the flag we are going to move and update
    }

    // Update is called once per frame
    void Update()
    {
        if(team != -1) //recall -1 is the nothing value
        {
            Color teamColor = GameManager.Instance.teams[team];//how would we get the current team's color?
                                                               //we don't want to just immediately set the teamColor as soon as we arrive 
                                                               //hint: our value between 0 and 1 is our currentValue
            flag.material.color = Color.Lerp(Color.white, teamColor, currentValue);
            flag.transform.parent.localPosition = new Vector3(0, Mathf.Lerp(flagBottom, flagTop, currentValue), 0);
            //once our current value is 1, we want to start collecting points
            if(currentValue == 1)
            {
                timer += Time.deltaTime; //change in time in our scene
                if (timer >= scoreInterval)
                {
                    timer = 0;
                    ScoreManager.Instance.scores[team]++; //add 1 to the current team's score
                }
                //we want to have our points for a particular team updated after a scoreInterval
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //on trigger stay means that s=omeone has triggered the collision and remains there
        Unit unit = other.GetComponent<Unit>();// we need to see if what collided is actually a unit
        if( unit != null) //we only care about things that trigger this that are units
        {
            Debug.Log("We are here to stay");
            if(unit.Team == team)
            {
                //this is for when our current team is staying there
                currentValue += valueIncrease;
                if(currentValue >= 1)
                {

                    currentValue = 1;
                }
            }
            else
            {
                //this is for when a new team enters. They immediately decrease the currentValue
                currentValue -= valueIncrease;
                if(currentValue <= 0)
                {
                    team = unit.Team;
                    currentValue = 0;
                }
            }
        }
    }
}

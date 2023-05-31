using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Unit
{
    //we need to have a navigation mesh for our AI; we also need our AI to be in a series of different states
    //from last semester, and what is typically important for AI in games: Idle, Patrolling/Moving, Chasing
    // Start is called before the first frame update
    private enum State
    {
        Idle,
        MovingToOutpost,
        Chasing
    }
    private State currentState; //this keeps track of the current state
    private NavMeshAgent agent; //this is our navmesh agent
    private Unit currentEnemy; //current enemy
    private Outpost currentOutpost; //the current outpost we are focused on
    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>(); //this is how we grab the navmesh agent component (will return an error if we haven't defined
        SetState(State.Idle);
    }
    private void SetState(State newState)
    {
        //what we want to do here is look at the newstater, compare it to the enumvalues, and then figure out what to do based on that.
        //set state will only be called when a state changes
        currentState = newState;
        StopAllCoroutines();//stop the previous coroutines so they aren't operating at the same time
        switch (currentState)
        {
            case State.Idle:
                StartCoroutine(OnIdle());
                //do some work
                break;
            case State.MovingToOutpost:
                StartCoroutine(OnMovingToOutpost());
                //do some work
                break;
            case State.Chasing:
                StartCoroutine(OnChasing());
                //do some work
                break;
            default:
                break;
        }
                    ///
    }
    private IEnumerator OnIdle() //handles our idle state
    {
        //when idling, we should probably do some work and look for an outpost
        while(currentOutpost == null)
        {
            LookForOutposts(); //if we ever find an outpost, and the currentOutpost changes, we will leave this loop
            yield return null; 
        }
        SetState(State.MovingToOutpost); //we found an outpost, we now need to move
        //this will change
    }
    private IEnumerator OnMovingToOutpost()
    {
        agent.SetDestination(currentOutpost.transform.position);
        while(!(currentOutpost.team == Team  && currentOutpost.currentValue == 1))
        {
            //look for enemies
            yield return null;
        }//we move towards an outpost as long as we are not the team possessing it
        currentOutpost = null;
        SetState(State.Idle);
    }
    private IEnumerator OnChasing()
    {
        yield return null;
    }

    private void LookForOutposts()
    {
        int r = Random.Range(0, GameManager.Instance.outposts.Length);//find a random outpost
        currentOutpost = GameManager.Instance.outposts[r];
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("VerticalSpeed", agent.velocity.magnitude);
    }
}

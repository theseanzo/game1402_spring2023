using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //this is going to be the base class for both our PlayerController, and our eventual AIController. This is going to be shared properties between our PlayerController and our AI
    // Start is called before the first frame update
    protected Animator animator;
    //some things all units have: health, how much damage they do, their current team, a viewing angle, a rigid body, where they begin
    [SerializeField]
    int fullHealth = 100;
    [SerializeField]
    int team; //the team our unit belongs to
    [SerializeField]
    int health; //the current health value of our unit 
    [SerializeField]
    int damage = 10;
    private const float RAYCAST_LENGTH = 0.3f;
    protected Rigidbody rb;
    protected virtual void Start() //the reason this is virtual is because we need to override it (and virtual enforces that we can't have a Start function on the children without an override
    {
        animator = GetComponent<Animator>();
    }
    public int Team
    {
        get
        {
            return team;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool IsGrounded() //we want to figure out if our character is on the ground or not
    {
        Vector3 origin = transform.position;//this is where our character begins
        origin.y += RAYCAST_LENGTH * 0.5f;
        LayerMask mask = LayerMask.GetMask("Terrain");
        return Physics.Raycast(origin, Vector3.down, RAYCAST_LENGTH, mask);
    }
}

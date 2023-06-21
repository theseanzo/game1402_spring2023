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
    private Color myColor;
    protected const float DISTANCE_LASER_IF_NO_HIT = 500.0f;
    [SerializeField]
    Laser laserPrefab;
    private Eye[] eyes = new Eye[2]; //we need to keep track of the eyes of our Unit
    public float viewAngle = 80; //the angle with which we can see things

    internal bool isAlive = true; //internal means public to that project, and private to everyone else
    Vector3 startPos;
    public float respawnTime = 5.0f;
    protected virtual void Start() //the reason this is virtual is because we need to override it (and virtual enforces that we can't have a Start function on the children without an override
    {
        animator = GetComponent<Animator>();
        eyes = GetComponentsInChildren<Eye>();
        myColor = GameManager.Instance.teams[team];//get the color for our team
        transform.Find("Teddy_Body").GetComponent<SkinnedMeshRenderer>().material.color = myColor; //change the bear color according to that team
        startPos = this.transform.position;
        Respawn();
    }
    public int Team
    {
        get
        {
            return team;
        }
    }
    protected virtual void OnHit(Unit attacker)
    {
        Debug.Log("Ow");
        health -= attacker.damage; //take some damage from the attacker
        if(health <= 0)
        {
            Die();
            //we will do death code later
        }
    }
    protected bool CanSee(Transform target, Vector3 targetPosition)
    {
        Vector3 startPos = GetEyesPosition();//where we do get the starting position of our vision?
        Vector3 dir = targetPosition - startPos;
        //We now need to change if our angle is greater than the viewing angle, and, if so, return false
        if (Vector3.Angle(transform.forward, dir) > viewAngle)
            return false;

        Ray ray = new Ray(startPos, dir);
        LayerMask mask = ~LayerMask.GetMask("Outpost");//make sure that we don't care about our Outposts when looking for things
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if(hit.transform != target)
            {
                return false;
            }
        }
        return true;
    }
    protected Vector3 GetEyesPosition()
    {
        return (eyes[0].transform.position + eyes[1].transform.position) / 2.0f;
    }
    protected void ShootAt(RaycastHit hit)
    {
        //we only want to shoot at units 
        Unit unit = hit.transform.GetComponent<Unit>(); //let's see if we get a unit
        if(unit != null)
        {
            //do some work
            if(unit.team != team)
            {
                
                unit.OnHit(this);//we are telling a unit that we have done some damage to it
                ShowLasers(hit.point);
            }
            
        }
    }
    protected void ShowLasers(Vector3 targetPosition) //the target position is what we are aiming for
    {
        foreach(Eye eye in eyes)
        {
            Laser laser = Instantiate(laserPrefab) as Laser; //the "as Laser" casts the game object to a laser; this is a technique we can use if we know we are creating a game object of a specific type (in this case, we know the laserPrefab is going to be a Laser)
            laser.Init(myColor, eye.transform.position, targetPosition);
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
    protected virtual void Die() //even for teddy bears, the Grim Reaper comes
    {
        if (!isAlive)
            return; //this is a mistake clearly because we are already dead
        gameObject.layer = LayerMask.NameToLayer("DeadTeddy");

        isAlive = false;
        Debug.Log("Die");

        animator.SetBool("Dead", true);
        Invoke("Respawn", respawnTime);
    }
    protected virtual void Respawn()
    {
        isAlive = true;
        gameObject.layer = LayerMask.NameToLayer("LiveTeddy");
        health = fullHealth;
        this.transform.position = startPos;
        animator.SetBool("Dead", false);
        //when we respawn, what do we need to do?
        //1) Change the layer
        //2) Health back to max
        //3) Move back to spawn location
        //4) Set them back to being alive
    }
}

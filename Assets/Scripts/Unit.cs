using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //base class and AI controller and shares between player controller ans AI
    protected Animator animator;

    // All units have: health , damage , current team , veiw angle , rigged body , spawn 
    [SerializeField]
    int fullHealth = 100;
    [SerializeField]
    int team;
    [SerializeField]
    int health;
    [SerializeField]
    int damage = 10;

    protected const float RAYCAST_LENGTH = 0.3f;
    protected Rigidbody rb;

    void Start()
    {
        
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

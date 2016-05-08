using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuPlayerController : MonoBehaviour
{
    // script is very similar to PlayerController, but with unnecessary components removed


    // components
    public Rigidbody rb;
    public Animator anim;

    // firing
    public float pulseRate;
    public float burstRate;

    // other attributes
    private float x_dir;
    private float z_dir;
    public float speed;
    private float nextPulse;
    private float nextBurst;

    void Start()
    {
        // direction initialization
        anim.SetFloat("x-dir", 0f);
        anim.SetFloat("z-dir", -1f);
    }

    void Update()
    {
        // pulse attack
        if (Input.GetAxis("Fire1") != 0 && Time.time > nextPulse)
        {
            nextPulse = Time.time + pulseRate;
            anim.SetTrigger("pulse");
        }

        // burst attack
        else if (Input.GetAxis("Fire2") != 0 && Time.time > nextBurst)
        {
            nextBurst = Time.time + burstRate;
            anim.SetTrigger("burst");
        }

        // movement
        else
        {
            Vector3 movement = new Vector3();
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
            movement.y = 0;
            rb.velocity = movement * speed;

            // not moving
            if (movement.x == 0 && movement.z == 0)
            { anim.SetBool("moving", false); }

            // moving
            else
            {
                anim.SetBool("moving", true);
                anim.SetFloat("x-speed", rb.velocity.x);
                anim.SetFloat("z-speed", rb.velocity.z);

                // set x-direction
                if (movement.x > 0)
                {
                    anim.SetFloat("x-dir", 1f);
                    x_dir = 1f;
                }
                else if (movement.x < 0)
                {
                    anim.SetFloat("x-dir", -1f);
                    x_dir = -1f;
                }
                else
                {
                    anim.SetFloat("x-dir", 0f);
                    x_dir = 0f;
                }

                // set z-direction
                if (movement.z > 0)
                {
                    anim.SetFloat("z-dir", 1f);
                    z_dir = 1f;
                }
                else if (movement.z < 0)
                {
                    anim.SetFloat("z-dir", -1f);
                    z_dir = -1f;
                }
                else
                {
                    anim.SetFloat("z-dir", 0f);
                    z_dir = 0f;
                }
            }
        }
    }

    // pulse attack, with sprite, speed, etc. determined by direction player is facing
    public void Pulse(){}

    // burst attack, calls all four directions in succession
    public void Burst(){}
}
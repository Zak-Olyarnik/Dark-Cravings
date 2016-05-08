using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // components
    public Rigidbody rb;
    public SpriteRenderer rend;
    public GameObject shadow;
    public Animator anim;

    // firing
    public GameObject[] pulse;
    public GameObject[] burst;
    public float pulseRate;
    public float burstRate;
    public float fireSpeed;
    public float burstSpeed;
    public float damageRate;

    // other attributes
    private float x_dir;
    private float z_dir;
    public float speed;
    private float nextPulse;
    private float nextBurst;
    public bool invincible;
    public static int health = 10;
    public static bool hasKey = false;
    public static bool won = false;
    Vector3 movement = new Vector3();

    // UI
    public GameObject[] healthUI;
    public GameObject[] staminaUI;
    public GameObject keyUI;
    public Sprite end;
    public Sprite full;
    public Sprite half;
    public Sprite halfEnd;
    public Sprite empty;
    public Sprite emptyEnd;
    
    // singleton
    static public Transform p;
    public static PlayerController instance;


    // singleton
    public static PlayerController getInstance()
    { return instance; }

    void Start()
    {
        // singleton
        p = this.transform;
        instance = this;
        
        // direction initialization
        anim.SetFloat("x-dir", 0f); 
        anim.SetFloat("z-dir", -1f);
        
        // UI initialization
        healthUI[8].SetActive(false);
        healthUI[7].SetActive(false);
        healthUI[6].SetActive(false);
        healthUI[5].SetActive(false);
        healthUI[4].GetComponent<Image>().sprite = end;
        keyUI.SetActive(false);

        foreach (GameObject s in staminaUI)
        { s.SetActive(false); }
    }

    void Update()
    {
        // return to menu on death
        if (health <= 0 || won)
        { Application.LoadLevel("menu"); }

        // update Key UI
        if (hasKey)
        { keyUI.SetActive(true); }
        else
        { keyUI.SetActive(false); }

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
            //Vector3 movement = new Vector3();
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
                else if(movement.x < 0)
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

    // enemy collisions
    IEnumerator OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!invincible)
            {
                health -= 1;
                UpdateHealthUI();
                invincible = true;

                // makes sprite flash
                for (int i=0; i< 4; i++)
                {
                    shadow.GetComponent<SpriteRenderer>().enabled = false;
                    rend.enabled = false;
                    Invoke("Visible", .25f);
                    yield return new WaitForSeconds(.5f);
                }
                invincible = false;
            }
        }
    }

// enemy collisions
IEnumerator OnCollisionStay(Collision other)
{
if (other.gameObject.tag == "Enemy")
{
if (!invincible)
{
health -= 1;
UpdateHealthUI();
invincible = true;

// makes sprite flash
for (int i=0; i< 4; i++)
{
shadow.GetComponent<SpriteRenderer>().enabled = false;
rend.enabled = false;
Invoke("Visible", .25f);
yield return new WaitForSeconds(.5f);
}
invincible = false;
}
}
}

    // makes visible again (flashes when taking damage)
    public void Visible()
    {
        rend.enabled = true;
        shadow.GetComponent<SpriteRenderer>().enabled = true;
    }

    // pulse attack, with sprite, speed, etc. determined by direction player is facing (or moving diagonally)
    public void Pulse()
    {
        GameObject p = null;
        Animator pulseAnim;

        if (x_dir > 0)
        {
            p = Instantiate(pulse[3], new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z), transform.rotation) as GameObject;
            pulseAnim = p.GetComponent<Animator>();
            pulseAnim.SetFloat("x-dir", x_dir);
            if (movement.z != 0)
            { p.GetComponent<Rigidbody>().velocity = movement * fireSpeed; }
            else
            { p.GetComponent<Rigidbody>().velocity = transform.right * fireSpeed / 1.1f; }
            
        }
        else if (x_dir < 0)
        {
            p = Instantiate(pulse[2], new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z), transform.rotation) as GameObject;
            pulseAnim = p.GetComponent<Animator>();
            pulseAnim.SetFloat("x-dir", x_dir);
            if (movement.z != 0)
            { p.GetComponent<Rigidbody>().velocity = movement * fireSpeed; }
            else
            { p.GetComponent<Rigidbody>().velocity = -transform.right * fireSpeed / 1.1f; }
        }
        else if (z_dir > 0)
        {
            p = Instantiate(pulse[0], new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z), transform.rotation) as GameObject;
            pulseAnim = p.GetComponent<Animator>();
            pulseAnim.SetFloat("z-dir", z_dir);
            if (movement.x != 0)
            { p.GetComponent<Rigidbody>().velocity = movement * fireSpeed; }
            else
            { p.GetComponent<Rigidbody>().velocity = transform.forward * 1.2f * fireSpeed; }
        }
        else if (z_dir < 0)
        {
            p = Instantiate(pulse[1], new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z), transform.rotation) as GameObject;
            pulseAnim = p.GetComponent<Animator>();
            pulseAnim.SetFloat("z-dir", z_dir);
            if (movement.x != 0)
            { p.GetComponent<Rigidbody>().velocity = movement * fireSpeed; }
            else
            { p.GetComponent<Rigidbody>().velocity = -transform.forward * 1.2f * fireSpeed; }
        }
        Destroy(p, 3f);
    }

    // burst attack, calls all four directions in succession
    public void Burst()
    {
        BurstRight();
        BurstDown();
        BurstLeft();
        BurstUp();
    }

    public void BurstUp()
    {
        GameObject p = Instantiate(burst[0], transform.position, transform.rotation) as GameObject;
        p.GetComponent<Rigidbody>().velocity = transform.forward * 1.2f * burstSpeed;
        Destroy(p, 0.3f);
    }

    public void BurstDown()
    {
        GameObject p = Instantiate(burst[1], new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z), transform.rotation) as GameObject;
        p.GetComponent<Rigidbody>().velocity = -transform.forward * 1.2f * burstSpeed;
        Destroy(p, 0.3f);
    }

    public void BurstLeft()
    {
        GameObject p = Instantiate(burst[2], transform.position, transform.rotation) as GameObject;
        p.GetComponent<Rigidbody>().velocity = -transform.right * burstSpeed;
        Destroy(p, 0.3f);
    }

    public void BurstRight()
    {
        GameObject p = Instantiate(burst[3], transform.position, transform.rotation) as GameObject;
        p.GetComponent<Rigidbody>().velocity = transform.right * burstSpeed;
        Destroy(p, 0.3f);
    }

    // each health value has a corresponding, unique UI display
    public void UpdateHealthUI()
    {
        switch (health)
        {
            case 10:
                healthUI[4].GetComponent<Image>().sprite = end;
                break;
            case 9:
                healthUI[4].GetComponent<Image>().sprite = halfEnd;
                break;
            case 8:
                healthUI[4].GetComponent<Image>().sprite = emptyEnd;
                healthUI[3].GetComponent<Image>().sprite = full;
                break;
            case 7:
                healthUI[3].GetComponent<Image>().sprite = half;
                break;
            case 6:
                healthUI[3].GetComponent<Image>().sprite = empty;
                healthUI[2].GetComponent<Image>().sprite = full;
                break;
            case 5:
                healthUI[2].GetComponent<Image>().sprite = half;
                break;
            case 4:
                healthUI[2].GetComponent<Image>().sprite = empty;
                healthUI[1].GetComponent<Image>().sprite = full;
                break;
            case 3:
                healthUI[1].GetComponent<Image>().sprite = half;
                break;
            case 2:
                healthUI[1].GetComponent<Image>().sprite = empty;
                healthUI[0].GetComponent<Image>().sprite = full;
                break;
            case 1:
                healthUI[0].GetComponent<Image>().sprite = half;
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    public Image healthbar;
    public int health;
    public int maxhealth;
    public int bombs;
    public int MaxBombs;
    Vector2 velocity;
    Rigidbody2D rb;
    public float hsped;
    public float timer;
    public float timerog;
    public GameObject Laserpf;
    public GameObject puasemenu;
    public GameObject DedLOL;
    public Image Thing;
    public float score;
    public Text scoreText, HighScoreText, bombt;
    public AudioClip[] soundfx;
    Animator anim;
    public bool invince;
    public GameObject slowdownanim, slowdownef;
    public GameObject flash;
    

    // Start is called before the first frame update
    void Start()
    {
        bombt.text = "b: " + bombs;
        rb = GetComponent<Rigidbody2D>();
        FindObjectOfType<Sceneloader>().setTimeScale(1);
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {

       
        Scoring();
        if (health <= 0)
        {
            DedLOL.SetActive(true);
            // FindObjectOfType<Sceneloader>().setTimeScale(0);
            Time.timeScale = 0;
            print("i did it");
        }
        if (Input.GetButtonDown("Pause"))
        {
            if (puasemenu.activeSelf == false)
            {
                puasemenu.SetActive(true);
                FindObjectOfType<Sceneloader>().setTimeScale(0);
            }
            else
            {
                puasemenu.SetActive(false);
                FindObjectOfType<Sceneloader>().setTimeScale(1);
                print("stooop time!!!!!");

            }
        }

        float xIn = Input.GetAxisRaw("Horizontal");
        velocity.x = xIn * hsped;
        if (!puasemenu.activeSelf)
        {
            if (health > 0)
            {
                if(Input.GetMouseButtonDown(1) )
                {
                    slowdownanim.SetActive(true);
                    slowdownef.SetActive(true);
                    foreach (GameObject g in GameObject.FindGameObjectsWithTag("shield"))
                    {
                        g.GetComponent<SpriteRenderer>().enabled = false;
                        g.GetComponent<CircleCollider2D>().enabled = false;
                    }
                }
                if (Input.GetMouseButtonUp(1))
                {
                    slowdownanim.SetActive(false);
                    slowdownef.SetActive(false);
                    foreach (GameObject g in GameObject.FindGameObjectsWithTag("shield"))
                    {
                        g.GetComponent<SpriteRenderer>().enabled = true;
                        g.GetComponent<CircleCollider2D>().enabled = true;
                    }
                }
                if (Input.GetMouseButton(1) || Input.GetKey("z"))
                {

                    if (timer > 0)
                    {
                        Time.timeScale = .5f;//we slow down time here!!!<---------

                        timer = timer - Time.deltaTime * 40;
                        Thing.fillAmount = timer / 100;

                    }
                    else
                    {

                        Time.timeScale = 1;
                        slowdownanim.SetActive(false);
                        slowdownef.SetActive(false);
                        foreach (GameObject g in GameObject.FindGameObjectsWithTag("shield"))
                        {
                            g.GetComponent<SpriteRenderer>().enabled = true;
                            g.GetComponent<CircleCollider2D>().enabled = true;
                        }
                        if (timer < 100)
                        {
                            if (timer != 0)
                            {
                                Time.timeScale = 1;
                                slowdownanim.SetActive(false);
                                slowdownef.SetActive(false);
                                foreach (GameObject g in GameObject.FindGameObjectsWithTag("shield"))
                                {
                                    g.GetComponent<SpriteRenderer>().enabled = true;
                                    g.GetComponent<CircleCollider2D>().enabled = true;
                                }
                                timer = timer + Time.deltaTime * 5;
                                Thing.fillAmount = timer / 100;
                            }
                        }
                    }

                }
                else
                {

                    Time.timeScale = 1;
                    slowdownanim.SetActive(false);
                    slowdownef.SetActive(false);
                    foreach (GameObject g in GameObject.FindGameObjectsWithTag("shield"))
                    {
                        g.GetComponent<SpriteRenderer>().enabled = true;
                        g.GetComponent<CircleCollider2D>().enabled = true;
                    }
                    if (timer < 100)
                    {
                        if (timer != 0)
                        {
                            Time.timeScale = 1;
                            slowdownanim.SetActive(false);
                            slowdownef.SetActive(false);
                            foreach (GameObject g in GameObject.FindGameObjectsWithTag("shield"))
                            {
                                g.GetComponent<SpriteRenderer>().enabled = true;
                                g.GetComponent<CircleCollider2D>().enabled = true;
                            }
                            timer = timer + Time.deltaTime * 5;
                            Thing.fillAmount = timer / 100;
                        }
                    }
                }
                Thing.fillAmount = timer / 100;
            }
            else
            {
               
            }


        }
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            if (timer >= 10)
            { 
            Instantiate(Laserpf, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(soundfx[0], Camera.main.transform.position);
            timer -= 10;
        }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (bombs > 0)
            {
                bombs--;
                enemHolder.eh.bomb();
                bombt.text = "b: " + bombs;
                  AudioSource.PlayClipAtPoint(soundfx[1], Camera.main.transform.position);
                StartCoroutine(flashdelay());
            }
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + velocity * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("BombAdd"))
        {
            if (bombs < MaxBombs)
            {
                bombs++;
                bombt.text = "b: " + bombs;
                AudioSource.PlayClipAtPoint(soundfx[2], Camera.main.transform.position);
            }
            Destroy(col.gameObject);

        }
        if (col.CompareTag("en") || col.CompareTag("enlazer") || col.CompareTag("shield"))
        {
            if (!invince)
            {
                print("i took damge");
                if (health >= 1)
                {
                    health--;
                    AudioSource.PlayClipAtPoint(soundfx[3], Camera.main.transform.position);
                    StartCoroutine(animwait());
                }
                else
                {
                    //this is where I will put the death thing :]
                }
            }
         
        }
        if (col.CompareTag("healthpack"))
        {
            if (health < maxhealth)
            {
                health++;
                AudioSource.PlayClipAtPoint(soundfx[2], Camera.main.transform.position);
                print("Gain health");
            }
            Destroy(col.gameObject);
        }
        upadatehealthUI();
    }

    public void upadatehealthUI()
    {
        float pc = (float)health / (float)maxhealth;
        print(pc);
        healthbar.fillAmount = pc;

    }
    public void Scoring()
    {
        score += Time.deltaTime;
        scoreText.text = "score: " + score.ToString("F0");
        float Highscore = PlayerPrefs.GetFloat("Highscore", 0);
        if (score > Highscore)
        {
            Highscore = score;
            PlayerPrefs.SetFloat("Highscore", Highscore);

        }
        HighScoreText.text = "HighScore: " + Highscore.ToString("F0");
    }
    public IEnumerator animwait()
    {
        invince = true;
        anim.SetBool("gothit", true);

        yield return new WaitForSeconds(GetClipTime(anim, "playerDamage"));
        invince = false;
        anim.SetBool("gothit", false);

    }
    public float GetClipTime(Animator anim, string aName)
    {
        float time = 0;
        foreach (AnimationClip c in anim.runtimeAnimatorController.animationClips)
        {
            if (c.name == aName)
            {
                time = c.length;
            }
        }
        print(time);
        return time;
    }
    public IEnumerator flashdelay()
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(.35f);
        flash.SetActive(false);
    }
}

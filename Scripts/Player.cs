using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public FixedJoystick joystick;
    public GameObject popup;
    private bool pause = false;
    private Rigidbody heroRb;

    private float moveSpeed = 6f;
    

    //Timer Object
    public TextMeshProUGUI timer;

    public GameObject gameOver;
    

    

    //Healthbar
    [SerializeField] public GameObject healthbar;
    private Slider health;

    [SerializeField] public GameObject levelBar;
    private Slider level;

    //Weapons
    public GameObject fireOrbs;

    void Start()
    {
        timer.text = "Time: "+ 0;
        StartCoroutine(Timer());
        heroRb = GetComponent<Rigidbody>();
        health = healthbar.GetComponent<Slider>();
        level = levelBar.GetComponent<Slider>();

        //Menu
        popup.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!pause)
        {
            heroRb.velocity = new Vector3(joystick.Horizontal * moveSpeed, heroRb.velocity.y, joystick.Vertical * moveSpeed);
        }
        MenuView();
        Timer();
        GameOver();
    

       

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goblin"))
        {
            health.value = health.value - 0.005f;
        
        }

        if (collision.gameObject.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
            level.value = level.value + 0.1f;
        
        }
    
    }

    public void MenuView()
    {
        if (level.value >= 0.99f)
        {
            pause = true;
            popup.gameObject.SetActive(true);
            joystick.gameObject.SetActive(false);
        
        }
    
    }

    public void FireOrbs()
    {
        pause = false;
        fireOrbs.gameObject.SetActive(true);
        popup.gameObject.SetActive(false);
        joystick.gameObject.SetActive(true);
        level.value = 0;

    }

    public void HealthPotion()
    {
        pause = false;
        popup.gameObject.SetActive(false);
        joystick.gameObject.SetActive(true);
        health.value += 0.5f;
        level.value = 0;

    }
    public void Arrow()
    {
        pause = false;
        popup.gameObject.SetActive(false);
        joystick.gameObject.SetActive(true);



    }

    private IEnumerator Timer()
    {
        int countdownTime = 60;
        while (countdownTime > 0)
        {
            
            yield  return new WaitForSeconds(1f);
            countdownTime--;
            timer.text = "Time:" + countdownTime;

        }

        if (countdownTime == 0)
        {
            pause = true;
            popup.gameObject.SetActive(true);
            joystick.gameObject.SetActive(false);
            level.value = 0;
            health.value = 1;
            Timer();

        }
       

    }

    public void GameOver()
    {
        if (health.value <= 0.15)
        {
            pause = true;
            joystick.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
            level.value = 0;

        }
    
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(0);
    }

}

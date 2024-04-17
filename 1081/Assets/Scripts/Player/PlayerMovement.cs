using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 3f;
    private int shieldLevel;
    private int staminaLevel;
    public int currentCoins = 0;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isSprinting = false;
    private bool usingShield = false;
    private float shieldTimer = 0f;
    //default shield duration is 5 seconds
    private float shieldDuration = 5f;
    private float maxTime;
    public Image staminaBar;
    public Image shieldBar;
    public Image shieldBarBackground;
    public Image Shield;
    // private Coroutine recharge;


    //stamina system
    public float defaultStamina = 10f;
    public float defaultStaminaRegen = 1f;
    public float maxStamina;
    public float currentStamina;
    public float currentStaminaRegen;
    public float staminaConsum = 2.5f;

    [SerializeField] private TMP_Text _shieldDuration;
    [SerializeField] private TMP_Text _shieldCount;
    [SerializeField] private TMP_Text _staminaTracker;

    // Shield/Coin Manager

    private ShieldManager shieldManager;
    private CoinCollectionManager coinManager;
    private int shieldCount;

    public AudioSource coinAudio;
    public AudioSource shieldAudio;
 
    private void Start(){
        shieldManager = FindObjectOfType<ShieldManager>();
        coinManager = FindObjectOfType<CoinCollectionManager>();
        staminaLevel = PlayerPrefs.GetInt("StaminaLevel");
        currentStamina = defaultStamina + (2f * (staminaLevel - 1));
        maxStamina = defaultStamina + (2f * (staminaLevel - 1));
        currentStaminaRegen = defaultStaminaRegen + (0.5f * (staminaLevel - 1));
        shieldLevel = PlayerPrefs.GetInt("ShieldLevel", 1);
        shieldCount = PlayerPrefs.GetInt("ShieldNumber");
        maxTime = shieldDuration + (2f * (shieldLevel - 1));
        _shieldCount.text = shieldCount.ToString();
        Shield.gameObject.SetActive(false);
        shieldBar.gameObject.SetActive(false);
        shieldBarBackground.gameObject.SetActive(false);
        _shieldDuration.gameObject.SetActive(false);
        _shieldCount.gameObject.SetActive(false);
        _staminaTracker.text = string.Format("{0}/{1}", (int)currentStamina, (int)maxStamina);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // enemyAI = enemy.GetComponent<EnemyAI>();
    }

    public void OnMovement(InputAction.CallbackContext ctxt)
    {
        movement = ctxt.ReadValue<Vector2>();

        // if (enemyAI.caughtPlayer){
        //     animator.SetBool("isArrested", true);
        //     rb.velocity = Vector2.zero;
        //     Debug.Log("Caught");
        // }
        // else{
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("X", movement.x);
                animator.SetFloat("Y", movement.y);
            }
            else
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsSprinting", false);
                animator.SetBool("ShieldWalking", false);
                animator.SetBool("ShieldSprinting", false);
            }
        // }
    }

    public void OnSprint(InputAction.CallbackContext ctxt)
    {
        if (ctxt.action.triggered && ctxt.ReadValue<float>() > 0)
        {
            if(currentStamina >2.5f){
                isSprinting = true;
            }
        }
        else
        {
            isSprinting = false;
            animator.SetBool("IsSprinting", false);
        }
    }

    public void ConsumeStamina(float amount){
        currentStamina -= amount;
        // currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        if (currentStamina < 0)
            currentStamina = 0;
        staminaBar.fillAmount = currentStamina / maxStamina;
        _staminaTracker.text = string.Format("{0}/{1}", (int)currentStamina, (int)maxStamina);

    }

    public void RegenerateStamina (float amount){
        if (currentStamina < maxStamina){
            currentStamina += amount;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            staminaBar.fillAmount = currentStamina / maxStamina;
            _staminaTracker.text = string.Format("{0 }/{1}", (int)currentStamina, (int)maxStamina);
        }
    }

    public void OnShield(InputAction.CallbackContext ctxt){
        //still need to add a logic that decreases the shield count if press

        if (ctxt.performed)
        {
            if(PlayerPrefs.GetInt("ShieldNumber") > 0){
                _shieldCount.gameObject.SetActive(true);
                Shield.gameObject.SetActive(true);
                shieldBar.gameObject.SetActive(true);
                shieldBarBackground.gameObject.SetActive(true);
                _shieldDuration.gameObject.SetActive(true);
                //testing purposes
                // PlayerPrefs.SetInt("ShieldLevel", 1);
                PlayerPrefs.SetInt("ShieldNumber", PlayerPrefs.GetInt("ShieldNumber") - 1);
                PlayerPrefs.Save();
                _shieldCount.text = PlayerPrefs.GetInt("ShieldNumber").ToString();
                usingShield = true;
                Debug.Log("Shield Activated");
                animator.SetBool("UsingShield", true);
                
                animator.SetInteger("ShieldLevel", shieldLevel);
                // Calculate delay based on shieldLevelIndex
                shieldTimer = shieldDuration + (2f * (shieldLevel - 1));

                animator.SetBool("IsWalking", false);
                animator.SetBool("IsSprinting", false);
            }
            else{
                Debug.Log("No shield available for use");
            }
        }
    }


    private void FixedUpdate()
    {
        shieldTimer -= Time.fixedDeltaTime; // Decrease shield timer
        //check if shield is got any seconds left;
        if(shieldTimer <= 0f){
            _shieldCount.gameObject.SetActive(false);
            Shield.gameObject.SetActive(false);
            shieldBar.gameObject.SetActive(false);
            shieldBarBackground.gameObject.SetActive(false);
            _shieldDuration.gameObject.SetActive(false);
            usingShield = false;
            animator.SetBool("UsingShield", false);
            animator.SetBool("ShieldSprinting", false);
            animator.SetBool("ShieldWalking", false);
        }

        StartCoroutine(UpdateUITimer(shieldTimer, maxTime));

        if (!animator.GetBool("hasEnergy")){
            if  (currentStamina > 5f)
                animator.SetBool("hasEnergy", true);
            else
                isSprinting = false;
        }

        if (usingShield){
            //change speed;
            speed = 2f;
            //conditions
            if (isSprinting){
                if (movement.x != 0 || movement.y != 0) 
                {
                    if (currentStamina <= 0){
                        animator.SetBool("hasEnergy", false);
                        animator.SetBool("IsSprinting", false);
                        StartCoroutine(SprintThreshold());
                        RegenerateStamina(currentStaminaRegen * Time.deltaTime);
                    }
                    else{
                        ConsumeStamina(staminaConsum * Time.deltaTime);
                        animator.SetBool("ShieldSprinting", true);
                        rb.MovePosition(rb.position + movement * (speed*2) * Time.fixedDeltaTime);
                    }
                }
                else
                {
                    animator.SetBool("ShieldSprinting", false);
                    RegenerateStamina(currentStaminaRegen * Time.deltaTime);
                }
            }
            else{
                if (movement.x != 0 || movement.y != 0)
                {
                    animator.SetBool("ShieldWalking", true);
                    rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                    RegenerateStamina(currentStaminaRegen * Time.deltaTime);

                }
                else
                {
                    animator.SetBool("ShieldWalking", false);
                    RegenerateStamina(currentStaminaRegen * Time.deltaTime);
                }
            }
        }
        else{
            //default speed
            speed = 3f;
                if (isSprinting)
                {
                    if (movement.x != 0 || movement.y != 0)
                    {
                        if (currentStamina <= 0){
                            animator.SetBool("hasEnergy", false);
                            animator.SetBool("IsSprinting", false);
                            StartCoroutine(SprintThreshold());
                            RegenerateStamina(currentStaminaRegen * Time.deltaTime);
                        }
                        else{
                            ConsumeStamina(staminaConsum * Time.deltaTime);
                            animator.SetBool("IsSprinting", true);
                            rb.MovePosition(rb.position + movement * (speed * 2) * Time.fixedDeltaTime);
                        }
                    }
                    else
                    {
                        animator.SetBool("IsSprinting", false);
                        RegenerateStamina(currentStaminaRegen * Time.deltaTime);
                    }
                }
                else
                {
                    if (movement.x != 0 || movement.y != 0)
                    {
                        animator.SetBool("IsWalking", true);
                        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                        RegenerateStamina(currentStaminaRegen * Time.deltaTime);
                    }
                    else
                    {
                        animator.SetBool("IsWalking", false);
                        RegenerateStamina(currentStaminaRegen * Time.deltaTime);
                    }
                }
        }
    }

    private IEnumerator UpdateUITimer (float timer, float maxTime){
        if (timer >= 0){
            int roundedTimer = Mathf.RoundToInt(timer);
            _shieldDuration.text = roundedTimer.ToString();
            shieldBar.fillAmount = timer / maxTime;
        }
        else{
            _shieldDuration.text = "0";
            Shield.gameObject.SetActive(false);
            shieldBar.gameObject.SetActive(false);
            shieldBarBackground.gameObject.SetActive(false);
            _shieldDuration.gameObject.SetActive(false);
            _shieldCount.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator SprintThreshold(){
        yield return new WaitForSeconds(3f);
    }

    void OnTriggerEnter2D(Collider2D other){
        int coinCount = PlayerPrefs.GetInt("PlayerCoins", 200);
        int shieldCount = PlayerPrefs.GetInt("ShieldNumber", 0);
        if(other.gameObject.CompareTag("Coin"))
        {   Coin coinScript = other.gameObject.GetComponent<Coin>();
            int coinID = coinScript.getCoinID();
            Destroy(other.gameObject);
            currentCoins++;
            coinAudio.Play();
            Debug.Log("Coin: " + currentCoins);
            // coinManager.ShowCollectedCoinIDs();
            // coinManager.ResetCollectedCoins();
        }
        else 
        if(other.gameObject.CompareTag("Shield")){
            Shield shieldScript = other.gameObject.GetComponent<Shield>();
            int shieldID = shieldScript.getShieldID();
            Destroy(other.gameObject);
            shieldCount++;
            PlayerPrefs.SetInt("ShieldNumber", shieldCount);
            PlayerPrefs.Save();
            Debug.Log("Shield: " + shieldCount);
            Debug.Log("ShieldID is : " + shieldID);
            shieldManager.addshieldToList(shieldID);
            // shieldManager.ShowCollectedshieldIDs();
            shieldAudio.Play();
        }
    }
}

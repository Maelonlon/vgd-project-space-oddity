using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

//per far funzionare gli UnityEvent passando un argomento

public class PlayerMovement : MonoBehaviour
{
    #region Public Members
    public GameObject rocketPivot;
    public GameObject rocketEnding;
    public Transform playerModel;
    public ParticleSystem particles;
    public MagneticBoots magneticBoots;

    public float rocketRotationSpeed = 10f;
    public float rocketPower = 10f;
    public float dampingRocketPower = 0.5f;

    public float maxFuel = 100f;
    public float fuelDepletionSpeed = 10f;
    public float fuelReplenishSpeed = 20f;


    public delegate void ChangeValue(float newValue);

    public event ChangeValue OnFuelChanged;

    #endregion

    #region Private Members
    //variabili che usiamo per interpolare la rotazione
    float targetRocketRotation = 0f;
    float rocketRotation = 0f;

    Rigidbody2D body;

    //when rocket needs to give force to the player
    bool rocketOn = false;


    float turnSmoothVelocity;
    public float turnSmoothTime = 0.2f;

    private float currentFuel;

    private bool isAdjustingPlayerRotation = true;

    private Vector3 originalScale;

    #endregion
    PlayerControls playerControls;

    public bool isInvulnerable = false;
    public GameObject protectionBubble;

    #region Unity Callbacks


    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Awake()
    {


        playerControls = new PlayerControls();
        playerControls.Player.RotateRocket.started += OnMoveRocket;
        playerControls.Player.RotateRocket.performed += OnMoveRocket;
        playerControls.Player.RotateRocket.canceled += OnMoveRocket;
        playerControls.Player.ActivateMagneticBoots.started += OnActivateMagneticBoots;
        playerControls.Player.FireRocket.started += OnFireRocket;
        playerControls.Player.FireRocket.canceled += OnFireRocket;


        originalScale = transform.localScale;
        body = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;

        if (SaveLoadUtils.GetEasyMode() == 1)
        {
            protectionBubble.SetActive(true);
            isInvulnerable = true;
        }
    }


    public void SetInvulnerabilityOff()
    {
        StartCoroutine(SetInvulnerabilityOffAfter(1.5f));
    }



    IEnumerator SetInvulnerabilityOffAfter(float seconds)
    {
        float timer = seconds;
        float stepTime = seconds / 20f;
        do
        {
            protectionBubble.SetActive(!protectionBubble.activeInHierarchy);
            yield return new WaitForSeconds(stepTime);
            timer -= stepTime;
        } while (timer > 0f);

        protectionBubble.SetActive(false);
        isInvulnerable = false;
    }


    private void Update()
    {
        RotateRocket();
        if (isAdjustingPlayerRotation)
            AdjustPlayerRotation();

        if (resetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime);
            if (transform.localScale == originalScale)
                resetScale = false;
        }
    }

    void RotateRocket()
    {
        rocketRotation = Mathf.LerpAngle(rocketRotation, targetRocketRotation, rocketRotationSpeed * Time.deltaTime);
        Vector3 angles = rocketPivot.transform.eulerAngles;
        angles.z = rocketRotation;
        rocketPivot.transform.eulerAngles = angles;
    }

    void AdjustPlayerRotation()
    {

        float targetAngle = Mathf.Atan2(Vector2.up.x, Vector2.up.y) * Mathf.Rad2Deg;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle,
            ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FixedUpdate()
    {
        if (rocketOn && currentFuel > 0f)
        {
            //ottengo la direzione inversa (simuliamo la terza legge di newton) in cui spinge il razzo
            Vector2 direction = (rocketPivot.transform.position - rocketEnding.transform.position).normalized;
            AddForce(direction * rocketPower, rocketPivot.transform.position);

            DepleteFuel();
        }
        if (!rocketOn && currentFuel <= maxFuel)
        {
            ReplenishFuel();
        }
        if (currentFuel <= 0f)
        {
            StopRocket();
        }




    }

    public void FlipScale()
    {

        Vector3 targetScale = playerModel.localScale;
        targetScale.x = -1;
        playerModel.localScale = targetScale;
    }



    void DepleteFuel()
    {
        currentFuel -= fuelDepletionSpeed * Time.fixedDeltaTime;
        OnFuelChanged.Invoke(currentFuel);
    }

    void ReplenishFuel()
    {
        currentFuel += fuelReplenishSpeed * Time.fixedDeltaTime;
        OnFuelChanged.Invoke(currentFuel);
    }
    void StopRocket()
    {
        particles.Stop();
        rocketOn = false;
    }


    public void AddForce(Vector3 forceVector, Vector3 position, bool scalePlayer = false)
    {

        body.AddForceAtPosition(forceVector, position);
        if (scalePlayer)
        {
            Vector3 newScale = transform.localScale;
            float xScaleMultiplier = Mathf.Abs(forceVector.x) - Mathf.Abs(forceVector.y);
            float yScaleMultiplier = Mathf.Abs(forceVector.y) - Mathf.Abs(forceVector.x);
            newScale.x = Mathf.Lerp(newScale.x, originalScale.x + xScaleMultiplier * 0.03f, Time.fixedDeltaTime);
            newScale.y = Mathf.Lerp(newScale.y, originalScale.y + yScaleMultiplier * 0.03f, Time.fixedDeltaTime);
            transform.localScale = newScale;

        }

        Debug.DrawLine(position, position + forceVector, Color.magenta, 0.05f);

    }

    bool resetScale = false;

    public void ResetScale()
    {
        resetScale = true;
    }



    public void AddTorque(float torque)
    {
        body.AddTorque(torque);
        Debug.DrawLine(transform.position, transform.position + Vector3.right * torque * 0.05f, Color.green, 0.05f);
    }

    public void SetAdjustingPlayerRotation(bool value)
    {
        isAdjustingPlayerRotation = value;
    }

    public void ChangeVelocity(float factor)
    {
        body.velocity *= factor;
    }


    #endregion

    #region Player Input Methods

    public float controlSnappiness = 5.0f; // play with this value... smaller is mooshier
    Vector3 persistentInput; // this is the "accumulator"
    public void OnMoveRocket(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        //fixing snapback
        persistentInput = Vector3.Lerp(persistentInput, input, controlSnappiness);

        if (input != Vector2.zero)
        {
            //ottengo l'angolo verso cui il razzo deve puntare
            targetRocketRotation = Vector2.SignedAngle(persistentInput, Vector2.down);
        }

    }




    public void OnFireRocket(InputAction.CallbackContext ctx)
    {
        if (ctx.started && currentFuel > 0f)
        {
            particles.Play();
            rocketOn = true;
            ChangeVelocity(dampingRocketPower);
        }
        else if (ctx.canceled)
        {
            particles.Stop();
            rocketOn = false;
        }

    }

    public void OnActivateMagneticBoots(InputAction.CallbackContext ctx)
    {
        if (magneticBoots.isActive)
            magneticBoots.Deactivate();
        else
            magneticBoots.Activate();

    }

    #endregion




}

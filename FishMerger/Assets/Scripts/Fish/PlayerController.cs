using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public float impulseForceMin = 1f;
    public float impulseForceMax = 10f;
    public float forceTime = 4f;

    public Vector3 impulseDirection = new Vector3(1, 1, 0);
    private bool isPressing = false;
    private float pressStartTime;
    public Player player;
    public HealthBar forceSlider;
    public Chaser chaser;
    public GameObject MugilWin;
    public bool hasControl = true;

    private float currentForce = 0f;

    public Vector3 lastGoodPosition;
    public float spawnOffsetX = 1f;
    public float spawnOffsetY = 1f;
    public Transform testCube;
    public float spawnCooldown = 1f;
    private bool isStart = true;

    public GameObject startTooltip;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Уничтожить новый экземпляр, если синглтон уже существует
        }
    }

    public void Start()
    {
        forceSlider.SetValues(impulseForceMin, impulseForceMax);
        startTooltip.SetActive(true);
        hasControl = false;
        player.DeactivateRb();
    }

    public void StartGame()
    {
        chaser.StartChase();
        hasControl = true;
        player.ActivateRb();
        startTooltip.SetActive(false);
    }

    public void WinGame()
    {
        player.gameObject.SetActive(false);
        Instantiate(MugilWin, player.transform.position, Quaternion.identity );
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            WinGame();
        }
        if (isStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStart = false;
                StartGame();
            }
        }
        else
        {
            HandleInput();
        }
        
    }


    private void HandleInput()
    {
        bool isGrounded = player.isGrounded;
        if (hasControl)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPressing = true;
                pressStartTime = Time.time;
            }

            if (isPressing && Input.GetKey(KeyCode.Space))
            {
                float heldTime = Time.time - pressStartTime;
                float t = Mathf.Clamp01(heldTime / forceTime);
                currentForce = Mathf.Lerp(impulseForceMin, impulseForceMax, t);
                forceSlider.SetForce(currentForce);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isPressing = false;
                if (isGrounded)
                {
                    player.Push(impulseDirection * currentForce);
                    lastGoodPosition = player.pos;
                    testCube.position = lastGoodPosition;
                }

                currentForce = impulseForceMin;
                forceSlider.SetForce(currentForce);
            }
        }
    }

    public void RespawnToNearPoint()
    {
        StartCoroutine(CooldownRoutine());
        player.gameObject.transform.position = new Vector3(lastGoodPosition.x - spawnOffsetX,
            lastGoodPosition.y + spawnOffsetY, lastGoodPosition.z);
    }

    private IEnumerator CooldownRoutine()
    {
        player.DeactivateRb();
        yield return new WaitForSeconds(spawnCooldown);
        player.ActivateRb();
    }

    public void CatchFish()
    {
        hasControl = false;
        player.DeactivateRb();
    }
}
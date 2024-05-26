using UnityEngine;
using System.Collections;

public class Chaser : MonoBehaviour
{
    // Переменные для движения рыбака
    public float fishermanSpeed = 5f;
    public float fishermanMaxSpeed = 10f;
    public float accelerationTime = 1f;
    public float maxDistanceToFish = 5f;
    [Space(10)]
    public float safeDistanceToCatchFish = 3f;
    public Vector3 direction = Vector3.forward;
    public bool isChasing = true;
    public Transform fish;

    // Переменные для анимации и ловли рыбы
    public Animator animator;
    public FishermanHand fishermanHand;

    // Приватные переменные
    private float currentSpeed;
    private float targetSpeed;
    private float speedChangeRate;
    private bool wasBeyondMaxDistance;
    private bool isOnCooldown = false;

    private bool isCatch = false;
    
    

    void Start()
    {
        currentSpeed = fishermanSpeed;
        wasBeyondMaxDistance = false;

        isChasing = false;
        animator.speed = 0;
    }

    public void StartChase()
    {
        animator.speed = 1;
        isChasing = true;
    }

    public void StopChase()
    {
        isChasing = false;
        if (animator != null)
        {
            animator.SetBool("isCrouch", true);
        }
    }

    void Update()
    {
        if (isChasing && fish != null)
        {
            float distanceToFish = Vector3.Distance(transform.position, fish.position);

            if (distanceToFish > maxDistanceToFish)
            {
                targetSpeed = fishermanMaxSpeed;
                speedChangeRate = (fishermanMaxSpeed - fishermanSpeed) / accelerationTime;

                if (!wasBeyondMaxDistance)
                {
                    wasBeyondMaxDistance = true;
                }
            }
            else
            {
                targetSpeed = fishermanSpeed;
                speedChangeRate = (fishermanMaxSpeed - fishermanSpeed) / (accelerationTime / 2);

                if (wasBeyondMaxDistance)
                {
                    wasBeyondMaxDistance = false;
                }
            }

            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, speedChangeRate * Time.deltaTime);
            Vector3 normalizedDirection = direction.normalized;
            Vector3 newPosition = transform.position + normalizedDirection * currentSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
        
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     CheckAndCatchFish();
        // }
    }

    public void CheckAndCatchFish()
    {
        if (isCatch)
            return;
        
        float distanceToFish = transform.position.x - fish.position.x;
        Debug.Log(distanceToFish);
        if (distanceToFish > 0.7f)
        {
            if (distanceToFish > safeDistanceToCatchFish) //здесь рыба прыгнула очень далеко и мы даём шанс ещё раз прыгнуть
            {
                Debug.Log("раз");
                float moveDirection = Mathf.Sign(fish.position.x - transform.position.x);
                transform.position = new Vector3(fish.position.x - 10, transform.position.y, transform.position.z);
                return;
            }
            else //здесь мы поймали рыбу
            {
                Debug.Log("два");
                float moveDirection = Mathf.Sign(fish.position.x - transform.position.x);
                transform.position += new Vector3(-moveDirection * (0.7f - distanceToFish), 0f, 0f);
                if (animator != null)
                {
                    animator.SetBool("isCrouch", true);
                }
                PlayerController.Instance.CatchFish();
                fishermanHand.CatchFish();
                isChasing = false;
                isCatch = true;
            }
        }
        else //здесь мы поймали рыбу
        {
            Debug.Log("три");
            float moveDirection = Mathf.Sign(fish.position.x - transform.position.x);
            transform.position += new Vector3(-moveDirection * (0.7f - distanceToFish), 0f, 0f);
            if (animator != null)
            {
                animator.SetBool("isCrouch", true);
            }
            PlayerController.Instance.CatchFish();
            fishermanHand.CatchFish();
            isChasing = false;
            isCatch = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOnCooldown && other.CompareTag("Player"))
        {
            CheckAndCatchFish();
            StartCoroutine(CooldownRoutine()); //корутина чтобы метод ловли не срабатывал на каждый коллайдер рыбы
        }
    }
    
    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(0.3f);
        isOnCooldown = false;
    }
}

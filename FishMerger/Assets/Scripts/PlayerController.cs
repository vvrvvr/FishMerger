using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float impulseForceOnce = 5f; // Сила импульса при одном нажатии
    public float impulseForceMin = 1f; // Минимальная сила импульса при удерживании
    public float impulseForceMax = 10f; // Максимальная сила импульса при удерживании
    public float forceTime = 4f; // Время, за которое сила импульса увеличивается от минимальной до максимальной

    private Vector3 impulseDirection = new Vector3(1, 1, 0); // Направление импульса
    private bool isPressing = false; // Флаг для отслеживания удержания клавиши пробела
    private float pressStartTime; // Время начала удерживания клавиши пробела
    public Player player;

    void Update()
    {
        // Проверка нажатия клавиши пробела
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPressing = true;
            pressStartTime = Time.time;
        }

        // Проверка отпускания клавиши пробела
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isPressing = false;
            float heldTime = Time.time - pressStartTime;
            float t = Mathf.Clamp01(heldTime / forceTime);
            float currentImpulseForce = Mathf.Lerp(impulseForceMin, impulseForceMax, t);
            player.Push(impulseDirection * currentImpulseForce);
        }

       
    }

    void Push(Vector3 impulse)
    {
        // Ваш код для применения импульса к объекту
        // Например, если это Rigidbody, то:
        // GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse);
        Debug.Log("Impulse applied: " + impulse);
    }
}
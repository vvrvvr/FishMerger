using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    private Camera cam;
    private bool isDragging;
    [SerializeField] private Trajectory trajectory;
    [SerializeField] private Player player;
    [SerializeField] private float pushForce = 4f;
    [SerializeField] private LayerMask colliderPlaneLayer;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;
    public Transform startpoint;
    public Transform dragPoint;
    private bool isPressedDuringOnGround; // to prevent bug (adding some force during mouse button up when it pressed in air)

    //vectors
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;
    private Vector3 force;
    float distance;

    void Start()
    {
        cam = Camera.main;

        pauseButton.SetActive(true);
        winScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    void Update()
    {
        if (player.isGrounded && player.HasControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isPressedDuringOnGround = true;
                isDragging = true;
                OnDragStart();

            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                OnDragEnd();
            }
            if (isDragging)
            {
                OnDrag();
            }
        }
        else
        {
            isPressedDuringOnGround = false;
            trajectory.Hide();
        }
    }

    private void OnDragStart()
    {
        //player.DeactivateRb();
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10;
        startPoint = cam.ScreenToWorldPoint(mousepos);
        startpoint.position = startPoint;
        trajectory.Show();
    }
    private void OnDrag()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10;
        endPoint = cam.ScreenToWorldPoint(mousepos);
        dragPoint.position = endPoint;
        distance = Vector3.Distance(startpoint.localPosition, dragPoint.localPosition);
        direction = (startpoint.localPosition - dragPoint.localPosition).normalized;
        force = direction * (distance / 6) * pushForce;
        trajectory.UpdateDots(player.pos, force);
    }

    private void OnDragEnd()
    {
        //player.ActivateRb();
        if(isPressedDuringOnGround)
            player.Push(force);
        dragPoint.position = Vector3.zero;
        startpoint.position = Vector3.zero;
        trajectory.Hide();
    }

    public void PauseGame()
    {
        pauseButton.SetActive(false);
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseButton.SetActive(true);
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinGame()
    {
        StartCoroutine(EndgameActivity());
    }

    public void ExitGamse()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
    }

    private IEnumerator EndgameActivity()
    {
        yield return new WaitForSeconds(1f);
        pauseButton.SetActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        

    }
}

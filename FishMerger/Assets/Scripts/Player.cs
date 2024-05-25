using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public SphereCollider col;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool HasControl;

    [SerializeField] private Material onGroundMaterial;
    [SerializeField] private Material onAirMaterial;
    [SerializeField] private SkinnedMeshRenderer rend;
    [SerializeField] private CheckGround[] components = new CheckGround[5];
    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        HasControl = true;
    }

    private void Update()
    {
        foreach (CheckGround comp in components)
        {
            if (comp.CheckComponentGround())
            {
                isGrounded = true;
                break;
            }
            isGrounded = false;
        }
        Changecolor(isGrounded);

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     Push(new Vector3(10, 5,0));
        // }
        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     DeactivateRb();
        // }
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     ActivateRb();
        // }
    }

    public void Push(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
    private void Changecolor(bool grounded)
    {
        if (grounded)
            rend.material = onGroundMaterial;
        else
            rend.material = onAirMaterial;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    public void FinishGame()
    {
        StartCoroutine(EndgameActivity());
    }

    private IEnumerator EndgameActivity()
    {
        yield return new WaitForSeconds(0.5f);
        ActivateRb();
        HasControl = false; 
    }
}

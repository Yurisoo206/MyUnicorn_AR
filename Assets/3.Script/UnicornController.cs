using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornController : MonoBehaviour
{
    [SerializeField] GameObject unicorn;
    [SerializeField] GameObject effect;

    [SerializeField] Transform callPoint;

    MainManager mainManager;
    Rigidbody rb;

    public bool isActive = false;
    bool isReady = false;
    bool isMoving = false;
    [SerializeField] bool isCall= false;
    public bool isMove = false;

    float moveSpeed = 0.08f;
    Vector3 targetPosition;

    public bool isWalk;
    public int States = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainManager = FindObjectOfType<MainManager>();
    }

    private void Update()
    {
        if (!mainManager.isActive && isActive && !isReady) 
        {
            isReady = true;
            isMove = false;
            StartCoroutine(UnActive_co());
        }
        else if (mainManager.isActive && !isActive) 
        {
            StartCoroutine(Start_co());
        }

        if (isMove) 
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            Vector3 lookDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);

            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f || isCall)
            {
                if (isCall)
                {
                    targetPosition = new Vector3(callPoint.transform.position.x, transform.position.y, callPoint.transform.position.z);

                    if (Vector3.Distance(transform.position, targetPosition) < 0.07f)
                    {
                        isMoving = false;
                        States = 3;
                        moveSpeed = 0.08f;
                    }
                    else if ((Vector3.Distance(transform.position, targetPosition) < 0.07f) &&
                             (Vector3.Distance(transform.position, targetPosition) > 0.1f))
                    {
                        moveSpeed = 0.15f;
                        States = 5;
                    }
                }
                else if (!isCall) 
                {
                    States = 6;
                    SetRandomTargetPosition();
                }
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Deployment"))
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-2f, 2f); 
        float randomZ = Random.Range(-2f, 2f); 
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    public void Calling()
    {
        if (!unicorn.activeSelf)
        {
            return;
        }
        if (isCall)
        {
            isCall = false;
            isMoving = false;
            States = 6;
        }

        else if (!isCall && !isMoving)
        {
            isCall = true;
            isMoving = true;
            moveSpeed = 0.15f;
            States = 5;
        }
        
    }

    public void OnReal()
    {
        if (!unicorn.activeSelf)
        {
            return;
        }
        if (!rb.useGravity)
        {
            rb.useGravity = true;
        }
        else if (rb.useGravity) 
        {
            rb.useGravity = false;
        }
    }


    IEnumerator UnActive_co()
    {
        effect.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        unicorn.SetActive(false);
        yield return new WaitForSeconds(2f);
        effect.SetActive(false);
        isActive = false;
        isReady = false;
    }

    IEnumerator Start_co()
    {
        SetRandomTargetPosition();
        effect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        unicorn.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        isActive = true;
        effect.SetActive(false);
        isMove = true;
        isWalk = true;
        States = 6;
    }
}

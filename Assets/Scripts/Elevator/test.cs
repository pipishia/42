using UnityEngine;
using System.Collections;


public class DualPlatformController : MonoBehaviour
{
    public Transform controlledPlatform;
    public Transform secondPlatform;
    public Transform oppositePlatform;
    public float moveSpeed = 20f;
    private float originalYOpposite;
    private float originalYSecond;
    private float targetYOpposite;
    private float targetYSecond;
    private Transform playerDefTransform;
    private Coroutine moveOppositeCoroutine;
    private Coroutine moveSecondCoroutine;


    void Start()
    {
        originalYOpposite = oppositePlatform.transform.position.y;
        originalYSecond = secondPlatform.transform.position.y;
        targetYOpposite = originalYOpposite;
        targetYSecond = originalYSecond;
        playerDefTransform = GameObject.FindGameObjectWithTag("hit").transform.parent;
    }

    void Update()
    {

    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Klay" )
        {
            
            if (controlledPlatform.position.x<85)
            {
                targetYOpposite = originalYOpposite + 10f;
                targetYSecond = originalYSecond - 10f;

            }
            else if(controlledPlatform.position.x>85)
            {
                targetYOpposite = originalYOpposite - 10f;
                targetYSecond = originalYSecond + 10f;

            }
            if (moveOppositeCoroutine != null)
                StopCoroutine(moveOppositeCoroutine);
            if (moveSecondCoroutine != null)
                StopCoroutine(moveSecondCoroutine);
            moveOppositeCoroutine = StartCoroutine(MoveToYPosition(oppositePlatform, targetYOpposite));
            moveSecondCoroutine = StartCoroutine(MoveToYPosition(secondPlatform, targetYSecond));
        }
        if (other.gameObject.CompareTag("hit") && other.collider.GetType() == typeof(BoxCollider2D))
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
        
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Klay")
        {
            if (moveOppositeCoroutine != null)
                StopCoroutine(moveOppositeCoroutine);
            if (moveSecondCoroutine != null)
                StopCoroutine(moveSecondCoroutine);
            moveOppositeCoroutine = StartCoroutine(MoveToYPosition(oppositePlatform, originalYOpposite));
            moveSecondCoroutine = StartCoroutine(MoveToYPosition(secondPlatform, originalYSecond));

        }
        if (other.gameObject.CompareTag("hit") && other.collider.GetType() == typeof(BoxCollider2D))
        {
            other.gameObject.transform.parent = playerDefTransform;
        }
    }
    private IEnumerator MoveToYPosition(Transform platform, float targetY)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = platform.position;
        Vector3 targetPosition = new Vector3(platform.position.x, targetY, platform.position.z);

        while (elapsedTime < moveSpeed)
        {
            platform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        platform.position = targetPosition;
    }
}


using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int _hp;
    public int _mp;
    public float maxSpeed;
    public float moveTime;
    public float bulletSpeed;

    private Rigidbody2D rigid;
    [SerializeField]
    private float attackTime = 5f;
    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject thunderPrefab;
    private float thunderHeight = 3f;
    private float[] thunderDistance = { 1f, 2f, 3f, 4f, 5f };

    void Start()
    {
        _hp = 100;
        _mp = 100;
        maxSpeed = 2f;
        moveTime = 1.5f;
        bulletSpeed = 7f;
        rigid = GetComponent<Rigidbody2D>();

        InvokeRepeating("Move", 0f, 1f);
        InvokeRepeating("PerformRandomAttack", 0f, attackTime);
    }

    void Update()
    {
        // Update 로직 필요시 추가
    }

    private void Move()
    {
        if (isMoving) return;

        int movePattern = Random.Range(0, 3);

        switch (movePattern)
        {
            case 0:
                StartCoroutine(Left());
                break;
            case 1:
                StartCoroutine(Stop());
                break;
            case 2:
                StartCoroutine(Right());
                break;
        }
    }

    private IEnumerator Left()
    {
        isMoving = true;

        float moveTimer = 0;
        while (moveTimer < moveTime)
        {
            rigid.AddForce(Vector2.left * -1, ForceMode2D.Impulse);
            if (rigid.velocity.x > maxSpeed * -1)
            {
                rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
            }
            moveTimer += Time.deltaTime;
            yield return null;
        }
        rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.75f, rigid.velocity.y);
        isMoving = false;
    }

    private IEnumerator Stop()
    {
        isMoving = true;

        yield return new WaitForSeconds(moveTime);

        isMoving = false;
    }

    private IEnumerator Right()
    {
        isMoving = true;

        float moveTimer = 0;
        while (moveTimer < moveTime)
        {
            rigid.AddForce(Vector2.right * 1, ForceMode2D.Impulse);
            if (rigid.velocity.x > maxSpeed)
            {
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            }
            moveTimer += Time.deltaTime;

            yield return null;
        }
        rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.75f, rigid.velocity.y);
        isMoving = false;
    }

    private void PerformRandomAttack()
    {
        int attackPattern = 1;

        switch (attackPattern)
        {
            case 0:
                AttackPattern1();
                break;
            case 1:
                StartCoroutine(AttackPattern2());
                break;
            case 2:
                AttackPattern3();
                break;
            case 3:
                AttackPattern4();
                break;
            case 4:
                AttackPattern5();
                break;
        }
    }

    private void AttackPattern1()
    {
        Shoot(Vector2.left);
        Shoot(new Vector2(-1, 1).normalized);
        Shoot(Vector2.up);
        Shoot(new Vector2(1,1).normalized);
        Shoot(Vector2.right);
      }

    private void Shoot(Vector2 direction)
    {
        GameObject Bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D bulletRigid = Bullet.GetComponent<Rigidbody2D>();
        if (bulletRigid != null)
        {
            bulletRigid.velocity = direction * bulletSpeed;
        }
    }
    private IEnumerator AttackPattern2()
    {
        Vector3 currentPosition = transform.position;

        for(int i = 0; i < thunderDistance.Length; i++)
        {
            Vector3 leftThunder = new Vector3(currentPosition.x + thunderDistance[i] * -1, thunderHeight, currentPosition.z);
            Vector3 rightThunder = new Vector3(currentPosition.x + thunderDistance[i], thunderHeight, currentPosition.z);

            Instantiate(thunderPrefab, leftThunder, Quaternion.identity);
            Instantiate(thunderPrefab, rightThunder, Quaternion.identity);

            yield return new WaitForSeconds(0.25f);
        }
    }
    private void AttackPattern3()
    {
        Debug.Log("Executing Attack Pattern 1");
    }
    private void AttackPattern4()
    {
        Debug.Log("Executing Attack Pattern 1");
    }
    private void AttackPattern5()
    {
        Debug.Log("Executing Attack Pattern 1");
    }
}

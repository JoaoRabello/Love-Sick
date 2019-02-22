using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float mySpeed;
    [HideInInspector] public Vector3 mousePos;
    [SerializeField] private string bulletType;
    [SerializeField] private Bullet gota;

    private Rigidbody2D myRigidbody;
    [SerializeField] private float timeToDestroy = 2f;
    private float timer = 0f;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update () {

        //transform.Translate(mousePos * mySpeed * Time.deltaTime);
        myRigidbody.velocity = mousePos * mySpeed;

        if (timer <= timeToDestroy)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if(bulletType == "bomb")
            {
                InstantiateBullet();
                InstantiateBullet();
                InstantiateBullet();
            }
            Destroy(gameObject);
        }
    }

    void InstantiateBullet()
    {
        Vector3 destiny = new Vector3(Random.Range(-5f, 6f), Random.Range(-5f, 6f), 0f);
        Bullet bala = Instantiate(gota, transform.position, Quaternion.identity);
        bala.mousePos = destiny;
        bala.mySpeed = 1f;
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float mySpeed;
    [HideInInspector] public Vector3 mousePos;

    private Rigidbody2D myRigidbody;
    private float timeToDestroy = 2f;
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
            Destroy(gameObject);
        }
    }
}

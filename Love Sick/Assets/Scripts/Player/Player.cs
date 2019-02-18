using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float mySpeed = 3f;
    [SerializeField] private Bullet bullet;
    private Bullet bulletInstance;

    private Rigidbody2D myRigidbody;
    private Vector2 northeast, northwest, southeast, southwest;
    private Vector3 mousePos;

    void Start () {

        northeast = new Vector2(0.75f,0.75f);
        northwest = new Vector2(-0.75f, 0.75f);
        southeast = new Vector2(0.75f, -0.75f);
        southwest = new Vector2(-0.75f, -0.75f);
	}
	
	void Update () {

        GetMovementInput();
        GetMouseInput();
    }

    #region Methods
    
    #region Movement
    void GetMovementInput()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            Move("WD");
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                Move("WA");
            }
            else
            {
                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                {
                    Move("SD");
                }
                else
                {
                    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                    {
                        Move("SA");
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            Move("Up");
                        }
                        if (Input.GetKey(KeyCode.S))
                        {
                            Move("Down");
                        }
                        if (Input.GetKey(KeyCode.A))
                        {
                            Move("Left");
                        }
                        if (Input.GetKey(KeyCode.D))
                        {
                            Move("Right");
                        }
                    }
                }
            }
        }
    }

    void Move(string direction)
    {
        switch (direction)
        {
            case "Up":
                transform.Translate(Vector2.up * Time.deltaTime * mySpeed);
                break;
            case "Down":
                transform.Translate(Vector2.down * Time.deltaTime * mySpeed);
                break;
            case "Left":
                transform.Translate(Vector2.left * Time.deltaTime * mySpeed);
                break;
            case "Right":
                transform.Translate(Vector2.right * Time.deltaTime * mySpeed);
                break;
            case "WD":
                transform.Translate(northeast * Time.deltaTime * mySpeed);
                break;
            case "WA":
                transform.Translate(northwest * Time.deltaTime * mySpeed);
                break;
            case "SD":
                transform.Translate(southeast * Time.deltaTime * mySpeed);
                break;
            case "SA":
                transform.Translate(southwest * Time.deltaTime * mySpeed);
                break;
        }
    }
    #endregion

    #region Attack
    void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Shoot();
        }
    }

    void Shoot()
    {
        bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletInstance.mousePos = (mousePos - transform.position).normalized;
    }
    #endregion
    
    #endregion
}

using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float mySpeed = 3f;
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject[] weapons = new GameObject[4];

    private Bullet bulletInstance;
    private string weapon;
    private float timeBtwAttack;
    private float timer = 0f;

    public bool canMove   = false;
    public bool canAttack = false;

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
        if(canMove)
            GetMovementInput();
        if(canAttack)
            GetMouseInput();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MachineGun"))
        {
            Destroy(col.gameObject);
            weapon = "MachineGun";
            SetWeapon(weapon);
            timeBtwAttack = 0.3f;
            timer = timeBtwAttack + 1f;
        }
        if (col.gameObject.CompareTag("Pistol"))
        {
            Destroy(col.gameObject);
            weapon = "Pistol";
            SetWeapon(weapon);
        }
        if (col.gameObject.CompareTag("Sword"))
        {
            Destroy(col.gameObject);
            weapon = "Sword";
            SetWeapon(weapon);
        }

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
        if(weapon == "MachineGun")
        {
            if (Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (timer >= timeBtwAttack)
                {
                    Shoot();
                    timer = 0f;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Shoot();
            }
        }
    }

    void Shoot()
    {
        bulletInstance = Instantiate(bullet, ((mousePos - transform.position).normalized)* 6f + transform.position, Quaternion.identity);
        bulletInstance.mousePos = (mousePos - transform.position).normalized;
    }

    #endregion

    #region Visual

    void SetWeapon(string w)
    {
        foreach(GameObject weaponGO in weapons)
        {
            if (weaponGO.CompareTag(w))
            {
                Debug.Log("Tem a tag");
                weaponGO.SetActive(true);
            }
            else
            {
                weaponGO.SetActive(false);
            }
        }
    }

    #endregion

    #endregion
}

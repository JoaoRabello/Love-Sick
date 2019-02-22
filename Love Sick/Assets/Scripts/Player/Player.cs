using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float mySpeed = 3f;
    [SerializeField] private Bullet[] bullet = new Bullet[4];
    [SerializeField] private GameObject[] weapons = new GameObject[4];

    private Bullet bulletInstance;
    private string weapon = null;
    private float timeBtwAttack;
    private float timer = 0f;

    public bool canMove = false;
    public bool canAttack = false;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    [SerializeField] private Transform actualWeaponTF;
    private Vector2 northeast, northwest, southeast, southwest;
    private Vector3 mousePos;

    void Start () {

        myAnimator = GetComponent<Animator>();

        northeast = new Vector2(0.75f,0.75f);
        northwest = new Vector2(-0.75f, 0.75f);
        southeast = new Vector2(0.75f, -0.75f);
        southwest = new Vector2(-0.75f, -0.75f);
	}
	
	void Update () {
        if (canMove)
        {
            GetMovementInput();
            if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                myAnimator.SetBool("isWalking", false);
            }
        }
        if (canAttack)
            GetMouseInput();
        if (weapon != null)
            Animate();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MachineGun"))
        {
            Destroy(col.gameObject);
            weapon = "MachineGun";
            SetWeapon(weapon);
            timeBtwAttack = 0.15f;
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
        if (col.gameObject.CompareTag("GrenadeLauncher"))
        {
            Destroy(col.gameObject);
            weapon = "GrenadeLauncher";
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
        myAnimator.SetBool("isWalking", true);
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
        switch (weapon)
        {
            case "MachineGun":
                if (Input.GetMouseButton(0))
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (timer >= timeBtwAttack)
                    {
                        Shoot(bullet[2]);
                        timer = 0f;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                }
                break;
            case "Pistol":
                if (Input.GetMouseButtonDown(0))
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Shoot(bullet[0]);
                }
                break;
            case "Sword":
                if (Input.GetMouseButtonDown(0))
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Shoot(bullet[1]);
                }
                break;
            case "GrenadeLauncher":
                if (Input.GetMouseButtonDown(0))
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Shoot(bullet[3]);
                }
                break;
        }
    }

    void Shoot(Bullet bullet)
    {
        bulletInstance = Instantiate(bullet, ((mousePos - transform.position).normalized)* 6f + transform.position, actualWeaponTF.localRotation);
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
                weaponGO.SetActive(true);
                actualWeaponTF = weaponGO.transform;
            }
            else
            {
                weaponGO.SetActive(false);
            }
        }
    }

    void Animate()
    {
        float rotation = actualWeaponTF.localEulerAngles.z;
        Debug.Log("Rotation: " + rotation);
        if ((rotation <= 22.5f && rotation > 0f) || (rotation < 360f && rotation > 337.5f))
        {
            myAnimator.SetFloat("x", 0f);
            myAnimator.SetFloat("y", 1f);
        }
        else
        {
            if (rotation <= 337.5f && rotation > 292.5f)
            {
                myAnimator.SetFloat("x", 1f);
                myAnimator.SetFloat("y", 1f);
            }
            else
            {
                if (rotation <= 292.5f && rotation > 247.5f)
                {
                    myAnimator.SetFloat("x", 1f);
                    myAnimator.SetFloat("y", 0);
                }
                else
                {
                    if (rotation <= 247.5f && rotation > 202.5f)
                    {
                        myAnimator.SetFloat("x", 1f);
                        myAnimator.SetFloat("y", -1f);
                    }
                    else
                    {
                        if (rotation <= 202.5f && rotation > 157.5f)
                        {
                            myAnimator.SetFloat("x", 0);
                            myAnimator.SetFloat("y", -1f);
                        }
                        else
                        {
                            if (rotation <= 157.5f && rotation > 112.5f)
                            {
                                myAnimator.SetFloat("x", -1f);
                                myAnimator.SetFloat("y", -1f);
                            }
                            else
                            {
                                if (rotation <= 112.5f && rotation > 67.5f)
                                {
                                    myAnimator.SetFloat("x", -1f);
                                    myAnimator.SetFloat("y", 0);
                                }
                                else
                                {
                                    if (rotation <= 67.5f && rotation > 22.5f)
                                    {
                                        myAnimator.SetFloat("x", -1f);
                                        myAnimator.SetFloat("y", 1f);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    #endregion

    #endregion
}

using Assets.Scripts.Persistence;
using Assets.Scripts.Persistence.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public float MovimentSpeed;
    public float TurnSpeed;
    public Animator Anim;

    public Character Character;
    public Weapon Weapon;

    public Slider Lifebar;


    // Start is called before the first frame update
    void Start()
    {
        HideCursor();
        InitializeDatabaseObjectsReference();
        InitializeLifeBarAndSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TurnAround();
        Attack();
        ShowCursor();
    }

    public void UpdateHealth(int damage)
    {
        Character.VlHealth -= damage;
        Lifebar.value = Character.VlHealth;
    }

    public void InitializeLifeBarAndSpeed()
    {
        Lifebar.value = Character.VlHealth;
        MovimentSpeed = Character.VlAgility;
    }

    public void InitializeDatabaseObjectsReference()
    {
        Character = GameCodeDataSource.Instance.CharacterDAO.GetCharacter(1);
        Weapon = GameCodeDataSource.Instance.WeaponDAO.GetWeapon(Character.FkWeapon);
    }

    public void ShowCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void HideCursor()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void Move()
    {
        //var moviment = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        var moviment = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));

        if (moviment.x != 0 || moviment.z != 0)
        {
            transform.Translate(moviment * MovimentSpeed * Time.deltaTime);
            Anim.SetBool("isRunning", true);
        }
        else
        {
            if (Anim.GetBool("isRunning"))
                Anim.SetBool("isRunning", false);
        }
    }

    public void Attack()
    {
        bool input = Input.GetMouseButtonDown(0);

        if (input)
            Anim.SetBool("isAttacking", true);
        else
            Anim.SetBool("isAttacking", false);
    }

    public void TurnAround()
    {
        var xMouse = Input.GetAxis("Mouse X");

        if (xMouse != 0)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                transform.eulerAngles.y + xMouse * TurnSpeed * Time.deltaTime,
                                                transform.eulerAngles.z);
    }

}

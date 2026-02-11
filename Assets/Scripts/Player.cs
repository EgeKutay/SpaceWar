using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 rawInput;
    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    Health health;
    Shooter shooter;
    Vector2 minBounds;
    Vector2 maxBounds;
    LevelManager levelManager;
    Camera mainCamera;
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        health = GetComponent<Health>();
        shooter = GetComponent<Shooter>();
        mainCamera = Camera.main;
    }
    void Start()
    {
        InitBounds();
        OnFire();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void InitBounds()
    {

        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    void MovePlayer()
    {

        Vector2 delta = rawInput * movementSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, paddingLeft + minBounds.x, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, paddingBottom + minBounds.y, maxBounds.y - paddingTop);
        transform.position = newPos;
    }
    void OnTouchInput(InputValue value)
    {
        Touch touch = Input.GetTouch(0);
        var touchScreenVec = mainCamera.ScreenToViewportPoint(touch.position);
        if (touchScreenVec.x >= 0.5)
        {
            touchScreenVec = new Vector3(1, 0, 0);
        }
        else if (touchScreenVec.x < 0.5)
        {
            touchScreenVec = new Vector3(-1, 0, 0);
        }
        rawInput = touchScreenVec;


        if (!value.isPressed)
        {
            rawInput = new Vector2(0, 0);
        }
    }
    void OnFire()
    {
        if (shooter != null)
        {
            shooter.isFiring = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;

    private Rigidbody2D _rigid;
    private float _jumpForce = 7f;
    [SerializeField]
    private LayerMask _groundLayer;
    private bool _resetJump = false;
    private float _speed = 3f;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private object _sprite;
    private bool _grounded;
    [SerializeField]
    private int _health = 1;
    private bool _isDeath = false;

    public int Health { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = _health;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDeath == false)
            Movement();
        else
        {
            StartCoroutine(GoToMenu());
        }

        Attack();
        
    }

    void Movement()
    {

        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();
        if(move > 0)
        {
            Flip(true);
        }
        else if(move < 0)
        {
            Flip(false);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button"))&& _grounded == true)
        {
            _playerAnim.Jump(true);
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _resetJump = true;
            StartCoroutine(ResetJumpNeededRoutine());
            
        }
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move);
    }

    void Attack()
    {
        if(( CrossPlatformInputManager.GetButtonDown("A_Button")) && IsGrounded()) //Input.GetMouseButtonDown(0) ||
        {
            _playerAnim.Attack();
        }
    }

    bool IsGrounded()
    {
        //2D raycast
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down,0.6f, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
            //adding new code
            
            
        }
        return false;
    }

    void Flip(bool FaceRight)
    {
        if(FaceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;

        }
        else if(FaceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main_Menu");
    }

    public void Damage()
    {
        if (_isDeath == false)
        {
            Health--;
            UIManager.Instance.UpdateLives();
            if (Health < 1)
            {
                _isDeath = true;
                _playerAnim.Death();
            }
        }
    }

    public void AddGems(int amount)
    {

        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}

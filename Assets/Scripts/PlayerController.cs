using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	public Text countText;
	public Text winText;
	private int count;

    private bool jump = false;

	//ジャンプ力
	[SerializeField]
	private float jumpPower = 10;

    private Animator animator;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
        animator = GetComponent<Animator>();
	}
		
	void FixedUpdate()
	{

		//Direction of Caracter's face
		var inputHorizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		var inputVertical = CrossPlatformInputManager.GetAxisRaw ("Vertical");

        if (inputHorizontal > 0f || inputVertical > 0)
        {

            animator.SetBool("Wait", false);
            animator.SetBool("Run", true);

            //カメラの方向から、x-z平面の単位のベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            //入力値とカメラの向きから、移動方向を決定
            Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

            //移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
            rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
        else {
            animator.SetBool("Wait", true);
            animator.SetBool("Run", false);
        }

        //ジャンプ
		if (!jump && CrossPlatformInputManager.GetButtonDown("Jump")) {

            animator.SetBool("Wait", false);
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);

            rb.velocity = new Vector3(inputHorizontal * speed, jumpPower, inputVertical * speed);
            jump = true;
		} 
	}

    // 衝突した瞬間に呼ばれる  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground") {
            jump = false;
            animator.SetBool("Wait", true);
            animator.SetBool("Jump", false);
        }
    }

    void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();

		if (count >= 9) {
			winText.text = "You Win!";
		}
	}


}

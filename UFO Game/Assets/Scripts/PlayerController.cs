using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public Text countText;
	public Text winText;
	public Text livesText;
	public string pickUpTag = "PickUp";

	Rigidbody2D rb2d;
	int pickUpsTotal;
	int count;
	int lives;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		winText.text = string.Empty;
		pickUpsTotal = GameObject.FindGameObjectsWithTag(pickUpTag).Length;
		count = 0;
		SetCountText();
		lives = 3;
		SetLivesText();
	}

	void FixedUpdate()
	{	

        if (Input.GetKey("escape")){
            Application.Quit();}
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		var movement = new Vector2(moveHorizontal, moveVertical).normalized * speed * Time.deltaTime;
		rb2d.AddForce(movement);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(pickUpTag)) 
		{
			other.gameObject.SetActive(false);
			count += 1;
			SetCountText();
		}
		if (other.gameObject.CompareTag("Enemy"))
     	{
          	other.gameObject.SetActive(false);
          	lives = lives - 1;  
          	SetLivesText();
     	}
		 if (count == 12) 
		{
    		transform.position = new Vector2(0.0f, -31.6f); 
		}
	}

	void SetCountText()
	{
		countText.text = $"Count: {count}";
		if (count >= pickUpsTotal)
		{	
			Destroy(gameObject);
			winText.text = "You win! Game created by Michael Martinez!";
		}
	}

	void SetLivesText()
	{
		livesText.text = $"Lives: {lives}";
		if(lives == 0)
		{
			Destroy(gameObject);
			winText.text = "You Lost! Game created by Michael Martinez!";
		}
	}

}
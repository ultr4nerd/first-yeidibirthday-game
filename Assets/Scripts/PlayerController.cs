using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public TextMeshProUGUI title;
    public TextMeshProUGUI message;
    public GameObject memePrefab;

    private float horizontal;
    private bool grounded;
    private bool playable = true;
    private int coins = 0;
    private Rigidbody2D rb2D;
    private Animator animator;

    public bool CoinsAreCollected()
    {
        return coins == 5;
    }

    public void finishPosition()
    {
        playable = false;
        StartCoroutine(FinishPositionIterator());
    }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ShowTitle("Jadey, la ranita cumpleañera <3");
        ShowText("Consigue cinco peso para que te dé tus regalos");
    }

    private void Update()
    {
        if (!playable) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.6f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }


        animator.SetBool("running", horizontal != 0 && grounded);

        if (horizontal < 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);


        bool pressingUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);

        if (pressingUp && grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            coins++;
            Destroy(collision.gameObject);
        }
    }

    private void Jump()
    {
        rb2D.AddForce(Vector2.up * jumpForce);
    }

    private IEnumerator ShowTitleEnumerator(string text)
    {
        yield return new WaitForSeconds(2);
        title.SetText(text);
        yield return new WaitForSeconds(7);
        title.SetText("");
    }

    private void ShowTitle(string text)
    {
        StartCoroutine(ShowTitleEnumerator(text));
    }

    private IEnumerator ShowTextEnumerator(string text)
    {
        yield return new WaitForSeconds(5);
        message.SetText(text);
        yield return new WaitForSeconds(7);
        message.SetText("");
    }

    private void ShowText(string text)
    {
        StartCoroutine(ShowTextEnumerator(text));
    }

    private IEnumerator FinalDialogEnumerator()
    {
        yield return new WaitForSeconds(4);
        message.SetText("¡Lo lograste, mi vida! Recolectaste tus cinco peso...");
        yield return new WaitForSeconds(5);
        message.SetText("");
        yield return new WaitForSeconds(2);
        message.SetText("Te tengo varias sorpresas el día de hoy. Y la primera es la más especial de todas...");
        yield return new WaitForSeconds(5);
        message.SetText("");
        yield return new WaitForSeconds(2);
        message.SetText("Dedicí dedicarte este juego, el primero hecho por mí. Es algo especial para mí...");
        yield return new WaitForSeconds(5);
        message.SetText("");
        yield return new WaitForSeconds(2);
        message.SetText("No te lo regalaría si no fueras tan especial y merecedorx de mi amor y gratitud...");
        yield return new WaitForSeconds(5);
        message.SetText("");
        yield return new WaitForSeconds(2);
        message.SetText("Iluminas mi camino a diario, espero esto te devuelva un poco de la felicidad que tú me das...");
        yield return new WaitForSeconds(5);
        message.SetText("");
        yield return new WaitForSeconds(2);
        title.SetText("¡FELIZ CUMPLEAÑOS, RANITA!");
    }

    private void ShowFinalDialog()
    {
        StartCoroutine(FinalDialogEnumerator());
    }

    private IEnumerator FinishPositionIterator()
    {
        animator.SetBool("running", false);
        yield return new WaitForSeconds(3);
        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        Instantiate(memePrefab, new Vector3(4.0f, 20.0f, 0.0f), Quaternion.identity);
        ShowFinalDialog();
    }
}

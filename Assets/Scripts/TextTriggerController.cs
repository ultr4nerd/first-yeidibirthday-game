using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextTriggerController : MonoBehaviour
{
    public TextMeshProUGUI message;
    public GameObject goalLimit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("CoinValidator"))
        {
            PlayerController controller = collision.GetComponent<PlayerController>();

            if(!controller.CoinsAreCollected())
            {
                ShowText("Debes recolectar 5 peso, mi cielo");
            } else if (goalLimit)
            {
                Destroy(goalLimit);
            }
        }

        if (CompareTag("Finish"))
        {
            PlayerController controller = collision.GetComponent<PlayerController>();

            controller.finishPosition();
        }
    }

    private IEnumerator ShowTextEnumerator(string text)
    {
        message.SetText(text);
        yield return new WaitForSeconds(5);
        message.SetText("");
    }

    private void ShowText(string text)
    {
        StartCoroutine(ShowTextEnumerator(text));
    }
}

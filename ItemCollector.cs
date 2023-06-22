using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    private int strw = 0;
    [SerializeField] private Text berriesText;

    [SerializeField] AudioSource collectionSoundEffect;

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.CompareTag("Berry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            strw++;
            berriesText.text = "Strawberries: " + strw;
        }
    }

}
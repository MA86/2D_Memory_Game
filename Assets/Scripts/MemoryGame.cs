using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    // Reference to child card back
    public GameObject CardBack;

    // Reference to scene controller
    public Controller control;

    // Card ID
    private int id;

    public int ID
    {
        get { return this.id; }

        set { this.id = value; }
    }


    // Called when clicked on a card object with Collider component
    public void OnMouseDown()
    {
        // If card is not currently 'revealed'...
        if (CardBack.activeSelf)
        {
            // Reveal it
            CardBack.SetActive(false);

            // Pass it to scene controller for match comparison
            control.CompareRevealedCard(this.GetComponent<MemoryGame>());
        }
    }

    // Called to dim the card
    public void Animate()
    {
        // TODO: not working
        this.GetComponent<SpriteRenderer>().color = new Color(60f, 33f, 33f);
    }

    // Called to unreveal the card
    public void Unreveal()
    {
        this.CardBack.SetActive(true);
    }
}

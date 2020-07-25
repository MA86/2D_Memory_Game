using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    // Reference to Controller
    public GameObject control;

    private void OnMouseEnter()
    {
        // make bigger
        this.transform.localScale = new Vector3(1.3f, 1.3f, 0f);
    }

    private void OnMouseExit()
    {
        this.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
    }

    private void OnMouseDown()
    {
        // dim
        this.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    private void OnMouseUp()
    {
        // call restart method on control
        this.control.GetComponent<Controller>().Restart();
    }
}

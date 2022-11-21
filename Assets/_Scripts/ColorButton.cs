using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    private BallPool pool;
    private BallDispenser dispenser;

    [SerializeField]
    private Material associatedMaterial;

    private Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        this.pool = GameObject.Find("BallPool").GetComponent<BallPool>();
        this.dispenser = GameObject.Find("Dispenser").GetComponent<BallDispenser>();

        this.buttonImage = GetComponent<Image>();
        this.buttonImage.color = this.associatedMaterial.color;
    }

    public void ChangeColor()
    {
        this.pool.currentBallMaterial = this.associatedMaterial;
        this.dispenser.spriteRenderer.color = this.associatedMaterial.color;
    }
}

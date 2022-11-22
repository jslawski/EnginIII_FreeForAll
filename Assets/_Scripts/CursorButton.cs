using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorButton : MonoBehaviour
{
    private BallDispenser dispenser;

    [SerializeField]
    private int ballsPerFrame;
    [SerializeField]
    private float dispenserScale;

    // Start is called before the first frame update
    void Start()
    {
        this.dispenser = GameObject.Find("Dispenser").GetComponent<BallDispenser>();
    }

    public void ChangeCursorSize()
    {
        this.dispenser.dispenserTransform.localScale = new Vector3(this.dispenserScale, this.dispenserScale, this.dispenserScale);
        this.dispenser.ballsPerFrame = this.ballsPerFrame;
    }
}

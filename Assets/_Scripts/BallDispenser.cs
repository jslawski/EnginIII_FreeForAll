using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDispenser : MonoBehaviour
{
    private Transform dispenserTransform;
    private BallPool ballPool;

    public int ballsPerFrame = 10;

    private void Awake()
    {
        this.dispenserTransform = GetComponent<Transform>();
        this.ballPool = GameObject.Find("BallPool").GetComponent<BallPool>();        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.CreateMultipleBalls();
        }

        if (Input.GetKey(KeyCode.Return))
        {
            this.CreateMultipleUnpooledBalls();
        }
    }

    private void CreateMultipleBalls()
    {
        for (int i = 0; i < this.ballsPerFrame; i++)
        {
            this.ballPool.CreateBall(this.dispenserTransform.position);
        }
    }

    private void CreateMultipleUnpooledBalls()
    {
        for (int i = 0; i < this.ballsPerFrame; i++)
        {
            Instantiate(this.ballPool.ballPrefab, this.dispenserTransform.position, new Quaternion(), this.dispenserTransform);            
            //this.ballPool.CreateBall(this.dispenserTransform.position);
        }
    }
}

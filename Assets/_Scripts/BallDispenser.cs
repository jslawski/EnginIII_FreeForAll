using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDispenser : MonoBehaviour
{
    private Transform dispenserTransform;
    private BallPool ballPool;

    private int ballsPerFrame = 50;

    [SerializeField]
    private float launchVariance = 5f;

    private Bounds dispenserBounds;

    private void Awake()
    {
        this.dispenserTransform = GetComponent<Transform>();
        this.dispenserBounds = GetComponent<Collider>().bounds;
        this.ballPool = GameObject.Find("BallPool").GetComponent<BallPool>();        
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
            Vector3 instantiationPosition = this.GetRandomInstantiationPosition();

            Ball ballReference = this.ballPool.CreateBall(instantiationPosition);

            ballReference.LaunchBall(this.GetRandomDownwardDirection());
        }
    }

    private Vector3 GetRandomInstantiationPosition()
    {        
        float randomX = Random.Range(this.dispenserBounds.min.x, this.dispenserBounds.max.x);
        float randomY = Random.Range(this.dispenserBounds.min.y, this.dispenserBounds.max.y);

        return new Vector3(randomX, randomY, this.dispenserTransform.position.z);
    }

    private Vector3 GetRandomDownwardDirection()
    {
        Vector3 launchDirection = Vector3.down;
        float randomXVariance = Random.Range(-this.launchVariance, this.launchVariance);
        launchDirection.x = randomXVariance;

        return launchDirection.normalized;
    }

    private void CreateMultipleUnpooledBalls()
    {
        for (int i = 0; i < this.ballsPerFrame; i++)
        {
            GameObject ballObject = Instantiate(this.ballPool.ballPrefab, this.dispenserTransform.position, new Quaternion(), this.dispenserTransform);
            ballObject.GetComponent<Ball>().LaunchBall(this.GetRandomDownwardDirection());
        }
    }
}

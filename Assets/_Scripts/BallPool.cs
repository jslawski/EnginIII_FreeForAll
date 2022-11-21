using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    const int PoolSize = 5000;

    private Transform parentTransform;

    [SerializeField]
    public GameObject ballPrefab;

    private Ball[] pool;

    private Ball firstAvailableBall;

    // Start is called before the first frame update
    void Awake()
    {
        this.parentTransform = GetComponent<Transform>();
        this.InitializePool();    
    }

    public Ball CreateBall(Vector3 position)
    {
        if (this.firstAvailableBall == null)
        {
            return null;
        }

        Ball newBall = this.firstAvailableBall;
        newBall.LoadObject(position);

        this.firstAvailableBall = this.firstAvailableBall.nextBall;

        return newBall;
    }

    public void DestroyBall(Ball destroyedBall)
    {
        destroyedBall.UnloadObject();
        this.pool[destroyedBall.ballIndex].nextBall = this.firstAvailableBall;
        this.firstAvailableBall = destroyedBall;
    }

    public void InitializePool()
    {
        //Instantiate objects
        this.pool = new Ball[PoolSize];
        for (int i = 0; i < this.pool.Length; i++)
        {
            GameObject ballObject = Instantiate(this.ballPrefab, this.parentTransform);
            Ball ballComponent = ballObject.GetComponent<Ball>();
            ballComponent.SetupObject(this, i);
            this.pool[i] = ballComponent;            
        }

        //Set up linked list
        this.firstAvailableBall = this.pool[0];
        for (int i = 0; i < this.pool.Length - 1; i++)
        {
            this.pool[i].nextBall = this.pool[i + 1];
        }

        this.pool[this.pool.Length - 1].nextBall = null;
    }
}

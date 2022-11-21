using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer ballRenderer;
    [SerializeField]
    private Transform ballTransform;
    [SerializeField]
    private Rigidbody ballRb;

    [HideInInspector]
    public int ballIndex;
    [HideInInspector]
    public Ball nextBall;

    private BallPool ballPool;

    public void SetupObject(BallPool pool, int index)
    {
        this.ballIndex = index;
        this.ballPool = pool;
        this.UnloadObject();
    }

    public void LoadObject(Vector3 position)
    {
        this.gameObject.SetActive(true);
        this.ballTransform.position = position;
        this.ballRb.useGravity = true;
    }

    public void UnloadObject()
    {
        this.ballTransform.position = Vector3.zero;
        this.gameObject.SetActive(false);

        //this.ballRenderer.enabled = false;
        this.ballRb.velocity = Vector3.zero;
        this.ballRb.useGravity = false;
    }

    public void LaunchBall(Vector3 direction)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillCollider")
        {
            if (this.ballPool != null)
            {
                this.ballPool.DestroyBall(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}

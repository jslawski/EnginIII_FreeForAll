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
    [SerializeField]
    private Collider ballCollider;

    [HideInInspector]
    public int ballIndex;
    [HideInInspector]
    public Ball nextBall;

    private BallPool ballPool;

    private float activationDelayInFrames = 0f;

    private float launchForce = 1f;

    private float goalTime = 3.0f;

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
        this.gameObject.layer = LayerMask.NameToLayer("DeadBall");
        this.ballRenderer.material = this.ballPool.currentBallMaterial;
    }

    public void UnloadObject()
    {
        this.ballTransform.position = Vector3.zero;
        this.gameObject.SetActive(false);

        this.ballRb.velocity = Vector3.zero;
        this.ballRb.useGravity = false;
    }

    public void LaunchBall(Vector3 direction)
    {
        this.ballRb.AddForce(direction * this.launchForce, ForceMode.Impulse);

        StartCoroutine(this.ActivateBall());
    }

    private IEnumerator ActivateBall()
    {
        for (int i = 0; i < this.activationDelayInFrames; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        this.gameObject.layer = LayerMask.NameToLayer("LiveBall");
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

        if (other.tag == "Goal")
        {
            this.StopAllCoroutines();
            StartCoroutine(this.CheckGoal());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Goal")
        {
            this.StopAllCoroutines();            
        }
    }

    private IEnumerator CheckGoal()
    {
        float currentTime = 0.0f;

        while (currentTime < this.goalTime)
        {
            yield return new WaitForFixedUpdate();
            currentTime += Time.fixedDeltaTime;
        }

        Debug.LogError("Winner!");
    }
}

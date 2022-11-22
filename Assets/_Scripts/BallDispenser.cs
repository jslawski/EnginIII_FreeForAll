using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallDispenser : MonoBehaviour
{   
    private BallPool ballPool;
    
    private Bounds dispenserBounds;

    private float minViewport = 0.05f;
    private float maxViewport = 0.95f;

    private bool createBalls = false;
    private bool createUnpooledBalls = false;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Transform dispenserTransform;
    [HideInInspector]
    public int ballsPerFrame = 5;

    private void Awake()
    {
        this.dispenserTransform = GetComponent<Transform>();
        this.dispenserBounds = GetComponent<Collider>().bounds;
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        this.ballPool = GameObject.Find("BallPool").GetComponent<BallPool>();        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 adjustedMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        adjustedMousePosition.z += Camera.main.transform.position.z + Camera.main.nearClipPlane;

        Vector3 newPosition = new Vector3(adjustedMousePosition.x, adjustedMousePosition.y, this.dispenserTransform.position.z);
        Vector3 newPositionViewport = Camera.main.WorldToViewportPoint(newPosition);

        if (newPositionViewport.x < this.minViewport)
        {
            newPositionViewport.x = this.minViewport;
        }
        else if (newPositionViewport.x > this.maxViewport)
        {
            newPositionViewport.x = this.maxViewport;
        }

        if (newPositionViewport.y < this.minViewport)
        {
            newPositionViewport.y = this.minViewport;
        }
        else if (newPositionViewport.y > this.maxViewport)
        {
            newPositionViewport.y = this.maxViewport;
        }

        this.dispenserTransform.position = Camera.main.ViewportToWorldPoint(newPositionViewport);

        this.createBalls = false;
        this.createUnpooledBalls = false;

        if (Input.GetMouseButton(0) && this.IsOverlappingButton() == false)
        {
            this.createBalls = true;
        }
        else if (Input.GetMouseButton(1) && this.IsOverlappingButton() == false)
        {
            this.createUnpooledBalls = true;
        }        
    }

    private void FixedUpdate()
    {
        if (this.createBalls == true)
        {
            this.CreateMultipleBalls();
        }
        else if (this.createUnpooledBalls == true)
        {
            this.CreateMultipleUnpooledBalls();
        }
    }

    private bool IsOverlappingButton()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void CreateMultipleBalls()
    {
        for (int i = 0; i < this.ballsPerFrame; i++)
        {
            Vector3 instantiationPosition = this.GetRandomInstantiationPosition();

            Ball ballReference = this.ballPool.CreateBall(instantiationPosition);

            if (ballReference != null)
            {
                ballReference.LaunchBall(Random.insideUnitCircle.normalized);
            }
        }
    }

    private Vector3 GetRandomInstantiationPosition()
    {        
        float randomX = this.dispenserTransform.position.x + Random.Range(this.dispenserBounds.min.x, this.dispenserBounds.max.x);
        float randomY = this.dispenserTransform.position.y + Random.Range(this.dispenserBounds.min.y, this.dispenserBounds.max.y);

        return new Vector3(randomX, randomY, 0.0f);
    }

    private void CreateMultipleUnpooledBalls()
    {
        for (int i = 0; i < this.ballsPerFrame; i++)
        {
            Vector3 instantiationPosition = this.GetRandomInstantiationPosition();

            GameObject ballObject = Instantiate(this.ballPool.ballPrefab, instantiationPosition, new Quaternion());
            ballObject.GetComponent<Ball>().LaunchBall(Random.insideUnitCircle.normalized);
        }
    }
}

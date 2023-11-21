using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;

public class handleRope : MonoBehaviour
{

    public GameObject hook;
    public GameObject ropeHint;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();

    public float ropeSegLen = 0.25f;
    public float lineWidth = 0.7f;
    private int segmentLength = 21;


    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();


        Vector3 ropeStartPoint = ropeHint.transform.position;

        for(int i = 0; i < segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
    }

    private void FixedUpdate()
    {
        this.Simulate();
        this.GenerateMeshCollider();
    }

    private void Simulate()
    {

        //Simulated
        Vector3 forceGravity = new Vector3(0f, -1f, 0f);

        for(int i = 0; i < this.segmentLength; i++)
        {
            Debug.Log("simulation");
            RopeSegment firstSegment  = this.ropeSegments[i];
            Vector3 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.deltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        for (int i = 0; i < 50; i++)
        {
            this.Constraints();
        }
    }

    private void Constraints()
    { 

        RopeSegment firstSegment = this.ropeSegments[0];


        firstSegment.posNow = ropeHint.transform.position;


        this.ropeSegments[0] = firstSegment;


        for(int i = 0; i < this.segmentLength - 2;i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;

            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector3 changeDir = Vector3.zero;

            if(dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if(dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector3 changeAmount = changeDir * error;

            if(i != 0)
            {
                firstSeg.posNow -= changeAmount * error;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }

        RopeSegment lastSegment = this.ropeSegments[20];

        lastSegment.posNow = hook.transform.position;

        this.ropeSegments[20] = lastSegment;
    }



    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for(int i = 0; i < this.segmentLength;i++) 
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector3 posNow;
        public Vector3 posOld;

        public RopeSegment(Vector3 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }

    public void GenerateMeshCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        if (collider == null)
        {
            collider.gameObject.AddComponent<MeshCollider>();
        }

        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Copied from Unity Standard Assets 2d and modified
public class CameraFollowPlayer : MonoBehaviour 
{
    public Transform target;
    public Vector2 targetOffset;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector2 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = TargetPosition();
        m_OffsetZ = transform.position.z;
        transform.parent = null;
    }

    private Vector2 TargetPosition()
    {
        return (Vector2)target.position + targetOffset;
    }


    // Update is called once per frame
    private void Update()
    {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (TargetPosition() - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = (Vector3)TargetPosition() + m_LookAheadPos;
        Vector3 newPos = Vector3.SmoothDamp(TargetPosition(), aheadTargetPos, ref m_CurrentVelocity, damping)
            + Vector3.forward*m_OffsetZ;

        transform.position = newPos;

        m_LastTargetPosition = TargetPosition();
    }
}
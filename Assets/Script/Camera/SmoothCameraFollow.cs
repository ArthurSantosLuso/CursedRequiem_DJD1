using System;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
    [SerializeField] private float positionSmoothTime = 0.3f;
    [SerializeField] private bool lockZAxis = true;

    private Vector3 _velocity = Vector3.zero;

    [SerializeField] private bool usePositionRounding = true;
    [SerializeField] private float positionRoundingPrecision = 0.05f;

    private Vector3 _targetPosition;

    void FixedUpdate()
    {


        _targetPosition = player.position + offset;

        // Lock the Z position if needed (for 2D games)
        if (lockZAxis)
            _targetPosition.z = transform.position.z;

        // Use SmoothDamp for smooth movement
        Vector3 newPosition = Vector3.SmoothDamp(
            transform.position,
            _targetPosition,
            ref _velocity,
            positionSmoothTime
        );

        // Optionally round position values to reduce micro-jitters
        if (usePositionRounding)
        {
            newPosition.x = Mathf.Round(newPosition.x / positionRoundingPrecision) * positionRoundingPrecision;
            newPosition.y = Mathf.Round(newPosition.y / positionRoundingPrecision) * positionRoundingPrecision;
        }

        // Apply the new position
        transform.position = newPosition;

    }

}
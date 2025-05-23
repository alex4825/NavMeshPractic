﻿using UnityEngine;

public interface IAgentMovable
{
    Vector3 CurrentDestination { get; }

    Vector3 CurrentVelocity { get; }

    void SetDestination(Vector3 point);
}

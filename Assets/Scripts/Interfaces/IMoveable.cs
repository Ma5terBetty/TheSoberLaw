using UnityEngine;

public interface IMoveable
{
    Rigidbody2D Rigidbody { get; }
    float MovementSpeed { get; }
    float JumpForce { get; }
    void Move();
    void Jump();
}

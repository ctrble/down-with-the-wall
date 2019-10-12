using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  public Player_Input input;
  public LayerMask staticLayer;
  public float groundLevel;
  public bool grounded;
  public float runSpeed;
  public AnimationCurve accelerationCurve;
  public float accelerationPercent;
  public float currentSpeed;
  public Vector3 previousPosition;
  public Vector3 currentPosition;
  public Vector3 positionDelta;



  // jumping again
  public float jumpingTime;
  public Vector3 jumpPosition;
  public float speedJump;
  public float gravity;

  public float jumpingTime0;
  public Vector3 jumpPosition0;
  public Vector3 speed0;
  public bool isJumping;

  void Start() {
    previousPosition = transform.position;
    currentPosition = transform.position;
    gravity = Physics2D.gravity.magnitude;
  }

  void Update() {
    // how much have we moved?
    currentPosition = transform.position; // frame start
    positionDelta = (currentPosition - previousPosition) / Time.deltaTime;

    // if (currentPosition != previousPosition) {
    // Debug.Log(positionDelta);
    // }

    Move();
    Jump();

    // store the previous position
    previousPosition = currentPosition;
  }

  void Move() {
    currentSpeed = runSpeed * Acceleration(input.Axis().x != 0) * Time.deltaTime;
    Vector3 newPosition = input.Axis() * currentSpeed;
    transform.position = transform.position + newPosition;
  }

  void Jump() {
    // Vectors
    // pos0: The position of the character when they begin to jump.
    // speed0: The speed of the character when they begin to jump.
    // pos: current position
    // speed: current speed

    // Values:
    // g: gravity.
    // t0: time when the character begins to jump.
    // t: time elapsed in the current jump.
    // speedJump: The momentum added by jumping(vertical)(constant)
    // isJumping: if your character is currently jumping

    // When the player press the space key, your hold the t0 and pos0, and compute speed0:
    if (!isJumping && input.Jumping()) {
      jumpingTime0 = Time.time;
      jumpPosition0 = transform.position;
      speed0 = positionDelta;
      speed0.y += speedJump;
      isJumping = true;
    }

    // Every frame, you may compute the new position of the character with:
    if (isJumping) {
      jumpingTime = Time.time - jumpingTime0;
      jumpPosition.y = jumpPosition0.y + speed0.y * jumpingTime - gravity * jumpingTime * jumpingTime;
      jumpPosition.x = jumpPosition0.x + speed0.x * jumpingTime;

      // And test that the character is not on the ground again.
      if (transform.position.y < groundLevel) {
        jumpPosition.y = groundLevel;
        isJumping = false;
      }

      transform.position = jumpPosition;
    }
  }

  float Acceleration(bool shouldAccelerate) {
    accelerationPercent += shouldAccelerate ? Time.deltaTime : -Time.deltaTime;
    accelerationPercent = Mathf.Clamp01(accelerationPercent);
    return accelerationCurve.Evaluate(accelerationPercent);
  }




  // Vector3 NonBlockedPosition(Vector2 direction, float distance) {
  //   Vector3 returnPosition = direction;
  //   RaycastHit2D rayHit = Physics2D.BoxCast(playerCollider.bounds.center, transform.localScale, 0, direction, distance, staticLayer);
  //   if (rayHit) {
  //     ColliderDistance2D colliderDistance = Physics2D.Distance(playerCollider, rayHit.collider);
  //     float roundedDistance = Mathf.Round(colliderDistance.distance * 100f) / 100f;
  //     return returnPosition * roundedDistance;
  //   }
  //   else {
  //     return returnPosition * distance;
  //   }
  // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour {
  public KeyCode upKey;
  public KeyCode upKeyAlt;
  public KeyCode downKey;
  public KeyCode downKeyAlt;
  public KeyCode leftKey;
  public KeyCode leftKeyAlt;
  public KeyCode rightKey;
  public KeyCode rightKeyAlt;
  public KeyCode jump;

  public Vector2 Axis() {
    return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
  }

  public bool Up() {
    return Input.GetKey(upKey) || Input.GetKey(upKeyAlt);
  }
  public bool Down() {
    return Input.GetKey(downKey) || Input.GetKey(downKeyAlt);
  }
  public bool Left() {
    return Input.GetKey(leftKey) || Input.GetKey(leftKeyAlt);
  }
  public bool Right() {
    return Input.GetKey(rightKey) || Input.GetKey(rightKeyAlt);
  }

  public bool JumpDown() {
    return Input.GetKeyDown(jump);
  }
  public bool Jumping() {
    return Input.GetKey(jump);
  }

  public bool JumpUp() {
    return Input.GetKeyUp(jump);
  }
}

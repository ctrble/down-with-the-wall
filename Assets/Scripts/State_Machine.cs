using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// https://forum.unity.com/threads/very-simple-statemachine-pattern-c.38956/
public class State_Machine : MonoBehaviour {
  public enum State {
    A,
    B,
    C,
  }

  public enum Transition {
    ENTER,
    ARRIVE,
    EXIT
  }


  public State state;
  public bool debugStates;
  protected delegate IEnumerator Routine();
  public void SetState(int state) { this.state = (State)state; }// for easy connection to UI dropdown
  Dictionary<int, Dictionary<Transition, Routine>> states = new Dictionary<int, Dictionary<Transition, Routine>>();

  void Start() {
    InitStateMachine();

    //// Test
    AssignCoroutine(State.A, Transition.ENTER, TestRoutine);
    AssignCoroutine(State.B, Transition.ENTER, TestRoutine);
    AssignCoroutine(State.C, Transition.ENTER, TestRoutine);

    StartStateMachine();
  }

  protected void InitStateMachine() {
    foreach (int key in Enum.GetValues(typeof(State))) {
      var transitions = new Dictionary<Transition, Routine>();
      foreach (Transition t in Enum.GetValues(typeof(Transition))) {
        Routine r = null;
        transitions.Add(t, r);
      }
      states.Add(key, transitions);
    }
  }

  protected void StartStateMachine() {
    StartCoroutine(Evaluate());
  }

  protected void AssignCoroutine(State state, Transition transition, Routine routine) {
    states[(int)state][transition] = routine;
  }

  protected void AddCoroutine(State state, Transition transition, Routine routine, bool clearExisting = false) {
    if (clearExisting) { states[(int)state][transition] = null; }
    states[(int)state][transition] += routine;
  }

  protected IEnumerator Evaluate() {
    while (true) {
      for (int i = 0; i < states.Count; i++) {
        if ((int)state == i) {
          if (debugStates) { Debug.Log("ENTER: " + (State)i + "\n"); }
          if (states[i][Transition.ENTER] != null) { yield return states[i][Transition.ENTER].Invoke(); }

          if (debugStates) { Debug.Log("ARRIVE: " + (State)i + "\n"); }
          if (states[i][Transition.ARRIVE] != null) { yield return states[i][Transition.ARRIVE].Invoke(); }

          while ((int)state == i) { yield return null; } // yield until state change

          if (debugStates) { Debug.Log("EXIT: " + (State)i + "\n"); }
          if (states[i][Transition.EXIT] != null) { yield return states[i][Transition.EXIT].Invoke(); }
        }
      }
    }
  }

  IEnumerator TestRoutine() {
    Debug.Log("Test Routine\n");
    yield return new WaitForSeconds(1); // Simulate processing
    yield break;
  }
}

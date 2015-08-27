using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// Displays the player's orbit as a line in front of the player
public class PlayerPathProjection : BaseBehaviour
{
  public GameObject nodePrefab;
  
  private const int startingNodes = 64;
  private const float fadeOutTime = 2;
  private const float fadeInTime = 2;
  
  private Color pathColor = Color.white;
  
  private float alpha;
  
  private PathNode[] nodes; // Used to display the line
  
  // Use this for initialization
  void Start () {
    InitNodes ();
    StartPathProjection ();
  }
  
  // Populates the node array with the Dot prefab
  private void InitNodes ()
  {
    nodes = new PathNode[startingNodes];
    for (int i = 0; i < startingNodes; i++) {
      nodes[i] = new PathNode(nodePrefab);
    }
  }
  
  // Update is called once per frame
  void FixedUpdate ()
  { 
    float dt = Time.fixedDeltaTime;
    
    bool didCollide = false;
    
    Vector3 previousPos = this.transform.position;
    Vector3 previousVel = this.GetComponent<Rigidbody>().velocity + GetForce (previousPos, ref didCollide) * dt;
    
    Vector3 currentPos = previousPos;
    Vector3 currentVel = previousVel;
    
    foreach(PathNode node in nodes) {
      currentPos = previousPos + previousVel * dt;
      
      Vector3 force = GetForce (currentPos, ref didCollide);
      currentVel = previousVel + force * dt;
      
      node.Position = currentPos;
      node.Display.GetComponent<SpriteRenderer> ().color = GetNodeColor (force.magnitude, didCollide);
      
      previousPos = currentPos;
      previousVel = currentVel;
    }
  }
  
  void Update() {
    float start = Time.fixedTime;
    float end = Time.fixedTime + Time.fixedDeltaTime;
    float current = Time.time;
    float t = Mathf.InverseLerp (start, end, current);
    foreach(PathNode node in nodes) {
      if (!float.IsNaN(node.Position.x) && !float.IsNaN(node.Position.y) && !float.IsNaN(node.Position.z)) {
        node.Display.transform.position = Vector3.Lerp(node.Display.transform.position, node.Position, t);
      }
    }
  }
  
  private Vector3 GetForce (Vector3 pos, ref bool collided)
  {
    Vector3 dest = Vector3.zero;
    
    foreach (AGravitational gravityField in SceneObjectManager.FindCachedObjectsOfType<AGravitational>()) {
      dest += gravityField.GetForceOnPoint (pos);
      if (Vector3.Distance (gravityField.transform.position, pos) < 1.0f) {
        collided = true;
      }
    }
    return dest;
  }
  
  private Color GetNodeColor (float gravMag, bool didCollide)
  {
    Color pathColor = this.pathColor;
    pathColor.a = didCollide ? 0f : alpha;
    return pathColor;
  }

  public void StartPathProjection ()
  {
    StopAllCoroutines ();
    StartCoroutine (DoPathProjection ());
  }
  
  private IEnumerator DoPathProjection ()
  {
    float initalAlpha = alpha;
    yield return Interpolate (fadeInTime - initalAlpha, (t) => alpha = initalAlpha + t);
    yield return Interpolate (fadeOutTime, (t) => alpha = 1 - t);
  }
  
  public void StopPathProjection() {
    StopAllCoroutines ();
    HideNodes ();
  }

  private void HideNodes() {
    alpha = 0;
    foreach (PathNode node in nodes) {
      node.Display.GetComponent<SpriteRenderer> ().color = Color.clear;
    }
  }

  private class PathNode {
    public Vector3 Position { set; get; }
    public GameObject Display { set; get; }
    
    public PathNode(GameObject nodePrefab) {
      Position = new Vector3();
      Display = GameObject.Instantiate(nodePrefab);
    }
  }
}

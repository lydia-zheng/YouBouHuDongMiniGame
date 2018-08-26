using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {
public enum ObstacleType {Circle, Rectangle, Pentagon};
public abstract ObstacleType type();
	

}

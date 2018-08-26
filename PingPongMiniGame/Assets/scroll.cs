
using UnityEngine;

public class scroll : MonoBehaviour
{

  private bool _isHolding = false;
    private Vector3 _holdingPosition = Vector3.zero;
    private Vector3 _holdingDelta = Vector3.zero;
    private bool _isStartedHolding = false;
    private bool _isReleased = false;
	float setY;

	private Camera _camera;
	

 public void Start(){
	 GameObject cam = GameObject.Find("Main Camera");
	_camera = cam.GetComponent<Camera>(); //tentative placement
	setY = transform.position.y;
 }   

public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_isHolding)
            {
               
                _isStartedHolding = false;
            } 
            else
            {
               
                _isStartedHolding = true;
            }

            var old = _holdingPosition;
            _holdingPosition = _camera.ScreenPointToRay(new Vector3 (Input.mousePosition.x, setY, Input.mousePosition.z)).origin;
            if (old != Vector3.zero)
                _holdingDelta = _holdingPosition - old;

				transform.position += _holdingDelta;
            //if (Contexts.sharedInstance.gameState.isGameOver)
             //   _holdingDelta = Vector3.zero;

            _isHolding = true;
            _isReleased = false;
        }
        else
        {
            if (_isHolding)
            {
                _isHolding = false;
                _isReleased = true;
            }
            else
            {
                _isReleased = false;
            }
            _holdingPosition = Vector3.zero;
            _holdingDelta = Vector3.zero;
        }
    }


}

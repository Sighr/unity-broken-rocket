using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Rigidbody2D rocket;
    public Camera cam;
    public AnimationCurve speedSizeRelation;
    
    public float MIN_SPEED_THRESHOLD;
    public float MIN_CAM_DISTANCE;
    public float MAX_SPEED_THRESHOLD;
    public float MAX_CAM_DISTANCE;
    
    private float _desiredSize;

    // Update is called once per frame
    void Update()
    {
        SetOrthographicSize();
        SetRotation();
    }

    private void SetRotation()
    {
        transform.rotation = Quaternion.identity;
    }

    private void SetOrthographicSize()
    {
        float speed = rocket.velocity.magnitude;
        if (speed < MIN_SPEED_THRESHOLD)
        {
            _desiredSize = MIN_CAM_DISTANCE;
        }
        else if (speed > MAX_SPEED_THRESHOLD)
        {
            _desiredSize = MAX_CAM_DISTANCE;
        }
        else
        {
            float normalizedSpeed = (speed - MIN_SPEED_THRESHOLD) / (MAX_SPEED_THRESHOLD - MIN_SPEED_THRESHOLD);
            _desiredSize = speedSizeRelation.Evaluate(normalizedSpeed) * (MAX_CAM_DISTANCE - MIN_CAM_DISTANCE) +
                           MIN_CAM_DISTANCE;
        }

        float orthographicSize = cam.orthographicSize;
        orthographicSize = orthographicSize + Time.deltaTime * (_desiredSize - orthographicSize);
        cam.orthographicSize = orthographicSize;
    }
}

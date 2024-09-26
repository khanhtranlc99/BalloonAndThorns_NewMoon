using UnityEngine;

public class DrawPredictedTrajectory : MonoBehaviour
{
    public Transform post;
    public Vector3 firstPost;
    public Vector3 secondPost;
    public LineRenderer lineRenderer;
    public BallTest prefapBall;
    public int numberOfPoints = 50; // Number of points to draw the trajectory
    public float timeBetweenPoints = 0.1f; // Time interval between points
    public LayerMask collisionMask;

    private void Start()
    {
        firstPost = post.position;
        // Set the number of points for LineRenderer
        lineRenderer.positionCount = numberOfPoints;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            secondPost = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            post.transform.position = new Vector3(secondPost.x, secondPost.y, 0);

            // Calculate direction and initial force (initialDirection)
            Vector2 initialDirection = (firstPost - secondPost).normalized * 10f;

            // Draw predicted trajectory
            DrawTrajectory(prefapBall.rigidbody2D, post.position, initialDirection);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 initialDirection = (firstPost - secondPost).normalized * 10f;
            var temp = SimplePool2.Spawn(prefapBall);
            temp.transform.position = post.transform.position;
            temp.AddForceBall(initialDirection);
            post.transform.position = firstPost;
            lineRenderer.positionCount = 0;
        }
    }

    void DrawTrajectory(Rigidbody2D rb, Vector2 start, Vector2 initialForce)
    {
        Vector2 currentPosition = start;
        Vector2 initialVelocity = initialForce / rb.mass;

        float gravity = Physics2D.gravity.y * rb.gravityScale;
        float timeStep = timeBetweenPoints;

        lineRenderer.positionCount = numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i * timeStep;
            Vector2 displacement = initialVelocity * t + 0.5f * new Vector2(0, gravity) * t * t;
            Vector2 newPoint = currentPosition + displacement;

            if (newPoint.y < -10)
            {
                lineRenderer.positionCount = i;
                break;
            }

            // Set the position of each point in the LineRenderer
            lineRenderer.SetPosition(i, newPoint);

            if (i > 0)
            {
                Vector2 direction = (lineRenderer.GetPosition(i) - lineRenderer.GetPosition(i - 1)).normalized;
                float distance = Vector2.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i - 1));

                // Perform a raycast from the previous point to the current point
                RaycastHit2D hit = Physics2D.CircleCast(lineRenderer.GetPosition(i - 1), 0.5f, direction, distance, collisionMask);

                if (hit.collider != null)
                {
                    // Calculate the reflection direction
                    Vector2 reflectDirection = Vector2.Reflect(direction, hit.normal);

                    // Update the position to the collision point
                    Vector2 collisionPoint = hit.point + hit.normal * 0.5f;
                    lineRenderer.SetPosition(i, collisionPoint);
                    lineRenderer.positionCount = i + 1;

                    // Start drawing the reflected trajectory
                    DrawReflectedTrajectory(rb, collisionPoint, reflectDirection);
                    break; // Exit the loop after handling the first collision
                }

                Debug.DrawRay(lineRenderer.GetPosition(i - 1), direction * distance, Color.green, 0.1f);
            }
        }
    }

    void DrawReflectedTrajectory(Rigidbody2D rb, Vector2 start, Vector2 reflectDirection)
    {
        Vector2 currentPosition = start;

        // Calculate the initial velocity after reflection considering bounciness
        Vector2 initialVelocity = reflectDirection * (10f * 0.98f); // Adjust speed based on bounciness
        initialVelocity /= rb.mass; // Adjust for mass

        float gravity = Physics2D.gravity.y * rb.gravityScale;
        float timeStep = timeBetweenPoints;

        int reflectedPoints = numberOfPoints / 2; // Limit the number of points for reflected trajectory
        lineRenderer.positionCount += reflectedPoints; // Increase the lineRenderer point count for the reflected trajectory

        for (int i = 0; i < reflectedPoints; i++)
        {
            float t = i * timeStep;
            Vector2 displacement = initialVelocity * t + 0.5f * new Vector2(0, gravity) * t * t;
            Vector2 newPoint = currentPosition + displacement;

            if (newPoint.y < -10)
            {
                lineRenderer.positionCount -= (reflectedPoints - i); // Adjust the position count to stop at the ground
                break;
            }

            // Set the position of each reflected point in the LineRenderer
            lineRenderer.SetPosition(lineRenderer.positionCount - reflectedPoints + i, newPoint);

            // Update the current position for the next iteration
            currentPosition = newPoint;

            // Apply friction effect
            initialVelocity *= (1 - 0); // Decrease the velocity by the friction factor
        }
    }
}

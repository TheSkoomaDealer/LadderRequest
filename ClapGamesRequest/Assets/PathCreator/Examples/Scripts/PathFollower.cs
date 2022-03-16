using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        //************************************************
        Vector3 newPos;//I change this code from here
        float climbVar;
        Rigidbody rigid;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {

            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                //newPos = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                //transform.position = new Vector3(newPos.x, climbVar , newPos.z);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        private void OnColliderEnter(Collider other)
        {
            if(other.gameObject.tag == "Stairs")
            {
                //rigid.velocity += new Vector3(0, -9.81f, 0);
                transform.position += new Vector3(newPos.x, 0.3f, newPos.z);
            }
            else
            {
                transform.position += new Vector3(newPos.x, -0.3f, newPos.z);
            }
           
        }
        public void Climb()
        {
            if (Input.GetButton("Fire1"))
            {
                climbVar += 0.05f;
            }
        }
        public void ClimbDown()
        {
            if (Input.GetButton("Fire1"))
            {
                climbVar += 0.05f;
            }
        }
    }
}

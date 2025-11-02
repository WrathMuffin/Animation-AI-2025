using UnityEngine;

namespace DitzelGames.FastIK
{

    public class SampleRotator : MonoBehaviour
    {
        public bool rotateOnX, rotateOnY = true, rotateOnZ;

        public float speed = 180.0f;

        void Update()
        {
            //just rotate the object, check the axis it should rotate from

            if (rotateOnX)
            {
                transform.Rotate(Time.deltaTime * speed, 0, 0);
            }

            if (rotateOnY)
            {
                transform.Rotate(0, Time.deltaTime * speed, 0);
            }

            if (rotateOnZ)
            {
                transform.Rotate(0, 0, Time.deltaTime * speed);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

//If we have a stiff rope, such as a metal wire, then we need a simplified solution
//this is also an accurate solution because a metal wire is not swinging as much as a rope made of a lighter material
namespace RopeLogic
{
    /// <summary>
    /// Клас-контроллер для поведінки мотузки
    /// </summary>
    public class RopeControllerSimple : MonoBehaviour
    {
        //Objects that will interact with the rope
        /// <summary>
        /// До чого приєднаний кінець мотузки
        /// </summary>
        public Transform whatTheRopeIsConnectedTo;
        /// <summary>
        /// Що звичає із кінця мотузки
        /// </summary>
        public Transform whatIsHangingFromTheRope;

        //Line renderer used to display the rope
        private LineRenderer lineRenderer;

        //A list with all rope sections
        public List<Vector2> allRopeSections = new List<Vector2>();

        /// <summary>
        /// Збільшується
        /// </summary>
        public bool IsIncreasing { get; set; }

        /// <summary>
        /// Зменшується
        /// </summary>
        public bool IsDecreasing { get; set; }

        //Rope data
        private float ropeLength = 1f;
        private float minRopeLength = 0.1f;

        private float maxRopeLength = 200f;

        //Mass of what the rope is carrying
        private float loadMass = 1f;

        //How fast we can add more/less rope
        float winchSpeed = 4.5f;

        //The joint we use to approximate the rope
        SpringJoint2D springJoint;

        /// <summary>
        /// Видалити мотузку
        /// </summary>
        /// <param name="length"></param>
        public void RemoveRope(float length = 1f)
        {
            lineRenderer.positionCount = 0;
            ropeLength = length;
        }

        /// <summary>
        /// Обнулити довжину
        /// </summary>
        public void ResetLength()
        {
            springJoint = whatTheRopeIsConnectedTo.GetComponent<SpringJoint2D>();

            //Init the line renderer we use to display the rope
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.positionCount = 2;

            lineRenderer.SetPosition(0, transform.position);

            lineRenderer.SetPosition(1,
                GameObject.FindWithTag("MainCamera").gameObject.transform.GetChild(0).transform.position);

            //Init the spring we use to approximate the rope from point a to b
            UpdateSpring();

            //Add the weight to what the rope is carrying
            whatIsHangingFromTheRope.GetComponent<Rigidbody2D>().mass = loadMass;
        }

        void Update()
        {
            if (whatIsHangingFromTheRope != null && whatTheRopeIsConnectedTo != null)
            {
                //Add more/less rope
                UpdateWinch();

                //Display the rope with a line renderer
                DisplayRope();
            }
        }

        //Update the spring constant and the length of the spring
        private void UpdateSpring()
        {
            //Someone said you could set this to infinity to avoid bounce, but it doesnt work
            //kRope = float.inf

            //
            //The mass of the rope
            //
            //Density of the wire (stainless steel) kg/m3
            float density = 1f;
            //The radius of the wire
            float radius = 0.02f;

            float volume = Mathf.PI * radius * radius * ropeLength;

            float ropeMass = volume * density;

            //Add what the rope is carrying
            ropeMass += loadMass;


            //
            //The spring constant (has to recalculate if the rope length is changing)
            //
            //The force from the rope F = rope_mass * g, which is how much the top rope segment will carry
            float ropeForce = ropeMass * 9.81f;

            //Use the spring equation to calculate F = k * x should balance this force, 
            //where x is how much the top rope segment should stretch, such as 0.01m

            //Is about 146000
            float kRope = ropeForce / 0.1f;

            //print(ropeMass);

            //Add the value to the spring
            //springJoint.frequency = kRope;
            //springJoint.dampingRatio = 0;

            //Update length of the rope
            springJoint.distance = ropeLength;
        }

        //Display the rope with a line renderer
        private void DisplayRope()
        {
            //This is not the actual width, but the width use so we can see the rope
            float ropeWidth = 0.1f;

            lineRenderer.startWidth = ropeWidth;
            lineRenderer.endWidth = ropeWidth;


            //Update the list with rope sections by approximating the rope with a bezier curve
            //A Bezier curve needs 4 control points
            Vector3 A = GameObject.FindWithTag("MainCamera").gameObject.transform.GetChild(0).transform.position;
            Vector3 D = whatIsHangingFromTheRope.position;

            //Upper control point
            //To get a little curve at the top than at the bottom
            Vector3 B = A + whatTheRopeIsConnectedTo.up * (-(A - D).magnitude * 0.2f);
            //B = A;

            //Lower control point
            Vector2 C = D + whatIsHangingFromTheRope.up * ((A - D).magnitude * 0.1f);

            //Get the positions
            BezierCurve.GetBezierCurve(A, B, C, D, allRopeSections);


            //An array with all rope section positions
            List<Vector3> positions = new List<Vector3>(allRopeSections.Count);

            lineRenderer.positionCount = allRopeSections.Count;


            //Add the positions to the line renderer

            for (int i = 1; i < allRopeSections.Count; i++)
            {
                lineRenderer.SetPosition(i, allRopeSections[i]);
            }
        }

        //Add more/less rope
        private void UpdateWinch()
        {
            //More rope
            if (IsIncreasing && ropeLength < maxRopeLength)
            {
                ropeLength += winchSpeed * Time.deltaTime;

                IsIncreasing = true;
            }
            else if (IsDecreasing && ropeLength > minRopeLength)
            {
                ropeLength -= winchSpeed * Time.deltaTime;

                IsDecreasing = true;
            }

            if (IsDecreasing || IsIncreasing)
            {
                ropeLength = Mathf.Clamp(ropeLength, minRopeLength, maxRopeLength);

                //Need to recalculate the k-value because it depends on the length of the rope
                UpdateSpring();
            }
        }
    }
}
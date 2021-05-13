 using System.Collections.Generic;
using UnityEngine;

namespace RopeLogic
{
//Approximate the rope with a bezier curve
/// <summary>
/// Клас, що описує логіку кривих ліній для мотузки
/// </summary>
    public static class BezierCurve
    {
        //Update the positions of the rope section        
        /// <summary>
        /// Оновлює позиції секції мотузки.
        /// </summary>
        /// <param name="A">Точка А.</param>
        /// <param name="B">Точка B.</param>
        /// <param name="C">Точка С.</param>
        /// <param name="D">Точка D.</param>
        /// <param name="allRopeSections">Усі секції мотузки.</param>
        public static void GetBezierCurve(Vector2 A, Vector2 B, Vector2 C, Vector2 D, List<Vector2> allRopeSections)
        {
            //The resolution of the line
            //Make sure the resolution is adding up to 1, so 0.3 will give a gap at the end, but 0.2 will work
            float resolution = 0.05f;

            //Clear the list
            allRopeSections.Clear();


            float t = 0;

            while (t <= 1f)
            {
                //Find the coordinates between the control points with a Bezier curve
                Vector3 newPos = DeCasteljausAlgorithm(A, B, C, D, t);

                allRopeSections.Add(newPos);

                //Which t position are we at?
                t += resolution;
            }


            allRopeSections.Add(D);
        }

        /// <summary>
        /// Алгоритм Де Кастельє.
        /// </summary>
        /// <param name="A">Точка a.</param>
        /// <param name="B">Точка b.</param>
        /// <param name="C">Точка c.</param>
        /// <param name="D">Точка d.</param>
        /// <param name="t">Змінна t.</param>
        /// <returns>Повертає остаточну інтерпольовану позицію</returns>
        static Vector2 DeCasteljausAlgorithm(Vector2 A, Vector2 B, Vector2 C, Vector2 D, float t)
        {
            //Linear interpolation = lerp = (1 - t) * A + t * B
            //Could use Vector3.Lerp(A, B, t)

            //To make it faster
            float oneMinusT = 1f - t;

            // //Layer 1
            Vector3 Q = oneMinusT * A + t * B;
            Vector3 R = oneMinusT * B + t * C;
            Vector3 S = oneMinusT * C + t * D;

            // //Layer 2
            Vector3 P = oneMinusT * Q + t * R;
            Vector3 T = oneMinusT * R + t * S;

            Vector3 U = oneMinusT * P + t * T;
            //Final interpolated position
            return U;
        }
    }
}
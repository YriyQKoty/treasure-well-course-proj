using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
	/// <summary>
	/// Відповідає за адаптивну верстку
	/// </summary>
	public class ScaleController : MonoBehaviour
	{
        /// <summary>
        /// The main camera
        /// </summary>
        public Camera MainCamera;
        /// <summary>
        /// Gets the scale of x dimension.
        /// </summary>
        /// <value>
        /// The scale of x dimension.
        /// </value>
        public float ScaleX { get; private set; }

		// Start is called before the first frame update
		void Start()
		{
			if ( MainCamera == null ) {
				MainCamera = GameObject.Find( "Main Camera" ).gameObject.GetComponent<Camera>();
			}

			float height = MainCamera.pixelHeight;
    
			Vector2 bottomLeft = MainCamera.ScreenToWorldPoint( new Vector2( 0, 0 ) );
			Vector2 topLeft = MainCamera.ScreenToWorldPoint( new Vector2( 0, height ) );

			Vector2 height_ = new Vector2( topLeft.x - bottomLeft.x, topLeft.y - bottomLeft.y );//Вектор висоти

			ScaleX = height_.magnitude * Screen.width / Screen.height;

			float value =( float ) System.Math.Round(  ScaleX / 10 + 0.08f, 2 );

			transform.localScale = new Vector3(value, value, 1f );
			if ( gameObject.tag == "Level" ) {
				if ( value > 1 ) {
					transform.localPosition = new Vector2( 0, ( value ) * - 1);
				}
			}
		}
	}
}
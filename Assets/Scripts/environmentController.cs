using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    public class environmentController : MonoBehaviour
    {
	public float oppTimeout = 0.50f;
	public bool opp = false;
	// timeout deltatime
	private float _oppTimeoutDelta;

	private GameObject[] _oppObs;
	private StarterAssetsInputs _input;
	
	private void Start()
	{
	    _input = GetComponent<StarterAssetsInputs>();
	    _oppObs = GameObject.FindGameObjectsWithTag("opp-able");
	    
	    if (opp)
	    {
		_oppObs = GameObject.FindGameObjectsWithTag("opp-able");
		foreach (GameObject ob in _oppObs)
		{
		    ob.SetActive(false);
		}
	    }
	    else
	    {
		_oppObs = GameObject.FindGameObjectsWithTag("opp-able");
		foreach (GameObject ob in _oppObs)
		{
		    ob.SetActive(true);
		}
	    }

	    // reset our timeouts on start
	    _oppTimeoutDelta = oppTimeout;
	}
	
	private void Update()
	{
	    Opposite();
	}

	private void Opposite()
	{
	    if (_oppTimeoutDelta <= 0.0f)
	    {
		if (_input.oppositeOn)
		{
		    opp = true;

		    foreach (GameObject ob in _oppObs)
		    {
			ob.SetActive(false);
		    }
		}
		else if (_input.oppositeOff)
		{
		    opp = false;
		
		    Debug.Log(_oppObs.Length);
		    foreach (GameObject ob in _oppObs)
		    {
			ob.SetActive(true);
		    }
		}
	    }
	    else if (_oppTimeoutDelta >= 0.0f)
	    {
		_oppTimeoutDelta -= Time.deltaTime;
	    }
	}
    }
}

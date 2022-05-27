using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

	private float _oppTimeoutDelta;
	private GameObject[] _oppObs;
	private StarterAssetsInputs _input;
	private VolumeProfile _volProf;
	private UnityEngine.Rendering.Universal.ColorAdjustments _colAdj;
	
	private void Start()
	{
	    _oppObs = GameObject.FindGameObjectsWithTag("opp-able");
	    _input = GetComponent<StarterAssetsInputs>();
	    _volProf = GameObject.FindGameObjectWithTag("global-vol").GetComponent<Volume>()?.profile;
	    if(!_volProf) throw new System.NullReferenceException(nameof(VolumeProfile));
     
	    if(!_volProf.TryGet(out _colAdj)) throw new System.NullReferenceException(nameof(_colAdj));
     
	    SetOpp();

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
		    if(!opp)
		    {
			opp = true;
			SetOpp();
		    }
		}
		else if (_input.oppositeOff)
		{
		    if (opp)
		    {
			opp = false;
			SetOpp();
		    }
		}
	    }
	    
	    else if (_oppTimeoutDelta >= 0.0f)
	    {
		_oppTimeoutDelta -= Time.deltaTime;
	    }
	}

	private void SetOpp()
	{
	    if (opp)
	    {
		// deactivate normal objects
		foreach (GameObject ob in _oppObs)
		{
		    ob.SetActive(false);
		}
		// hue shift
		_colAdj.hueShift.Override(Random.Range(-180.0f, 180.0f));
	    }
	    else
	    {
		// reactivate normal objects
		foreach (GameObject ob in _oppObs)
		{
		    ob.SetActive(true);
		}
		// reset hue shift
		_colAdj.hueShift.Override(0.0f);
	    }
	}
    }
}

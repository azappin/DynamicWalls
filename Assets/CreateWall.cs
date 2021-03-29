using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{

	bool creating;
	public GameObject start;
	public GameObject end;

	public GameObject wallPrefab;
	GameObject wall; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
    }

    void getInput() {
    	if(Input.GetMouseButtonDown(0)){
    		setStart();
    	} else if (Input.GetMouseButtonUp(0)) {
    		setEnd();
    	} else {
    		if(creating){
    			adjust();
    		}
    	}

    }

    void setStart() {
    	creating = true;
    	start.transform.position = getWorldPoint();

    	wall = (GameObject) Instantiate(wallPrefab, start.transform.position, Quaternion.identity);
    }

    void setEnd() {
    	creating = false;
    	end.transform.position = getWorldPoint();
    }

    void adjust() {
    	end.transform.position = getWorldPoint();
    	adjustWall();
    }

    void adjustWall() {
    	start.transform.LookAt(end.transform.position);
    	end.transform.LookAt(start.transform.position);

    	float distance = Vector3.Distance(start.transform.position, end.transform.position);
    	wall.transform.position = start.transform.position + distance/2 * start.transform.forward;
    	wall.transform.rotation = start.transform.rotation;
    	float additional = .5f;
    	wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance + additional);

    }

    Vector3 getWorldPoint() {
    	Camera cam = gameObject.GetComponent<Camera>();
    	Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    	RaycastHit hit;

    	if(Physics.Raycast(ray, out hit)) {
    		return hit.point;
    	}

    	return Vector3.zero;
    }

}

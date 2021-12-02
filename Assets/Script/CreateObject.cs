using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // Start is called before the first frame update
    // CONST
    Color FLOOR_COLOR = Color.black;
    float STUDENT_ID = 18127130f;
    Dictionary<string,List<KeyCode>> KEYS_DICTIONARY = new Dictionary<string,List<KeyCode>>();
    string STR_CREATE = "create";
    string STR_DESTROY = "destroy";

    List<KeyCode> KEYS_DESTROY = new List<KeyCode>{KeyCode.Q};

    List<KeyCode> KEYS_CREATE = new List<KeyCode>{KeyCode.Space};
    float DESTROY_DELAY = 2f;
    
    
    // Global var

    GameObject obj_a;

    public GameObject GO_B_PREFAB;


    int countB = 0;

    void game_init() {
        KEYS_DICTIONARY.Add(STR_CREATE, KEYS_CREATE);
        KEYS_DICTIONARY.Add(STR_DESTROY, KEYS_DESTROY);
    }

    Vector3 create_position(float student_id) {
        float x = Random.Range(obj_a.transform.position.x - 20, (20 + student_id % 10));
        float y = Random.Range(obj_a.transform.position.y, (10 + student_id % 10));
        float z = Random.Range(obj_a.transform.position.z, (20 + student_id % 10));
        return new Vector3(x, y, z);
    }


    GameObject create_obj_b(int index) {
        GameObject go_b =  Instantiate(GO_B_PREFAB, create_position(STUDENT_ID), Quaternion.identity);;
        return go_b;
    }



    void Start() {
        game_init();
        obj_a = GameObject.FindWithTag("obj_a");
        gameObject.GetComponent<Renderer>().material.color = FLOOR_COLOR;
    }

    // Update is called once per frame
    void Update() {
        // Checking create action and handle
        foreach(KeyCode key in KEYS_DICTIONARY[STR_CREATE]) {
            if(Input.GetKeyDown(key)) {
                create_obj_b(countB++);
            }
        }
                 // Checking destroy action and handle
        foreach(KeyCode key in KEYS_DICTIONARY[STR_DESTROY]) {
            if(Input.GetKeyDown(key)) {
                Debug.Log("Destroy after 2s...");
                GameObject[] list_gob = GameObject.FindGameObjectsWithTag("obj_b");
                GameObject target = list_gob[Random.Range(0, list_gob.Length)];
                Destroy(target, DESTROY_DELAY);
            }
        }
    }
}

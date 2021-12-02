using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Props;

public class ObjB : MonoBehaviour
{
    // Start is called before the first frame update
    Color OBJECT_B_COLOR = Color.white;
    Color OBJECT_B_OVER_COLOR = Color.red;
    Dictionary<string,List<KeyCode>> KEYS_DICTIONARY = new Dictionary<string,List<KeyCode>>();

    string STR_ROTATE = "rotate";
    string STR_CHANGE_COLOR = "color";
    List<KeyCode> KEYS_ROTATE = new List<KeyCode>{KeyCode.T};
    List<KeyCode> KEYS_COLOR = new List<KeyCode>{KeyCode.C};
    Vector3 VECTOR_OBJ_ROTATE = new Vector3(1, 0, 1);

    Vector3 INIT_LOCAL_SCALE = new Vector3(3, 3, 3); 
    Vector3 INIT_GRAVITY = new Vector3(0, 3f, 0);
    bool USE_GRAVITY = true;
    float DEFAULT_FORCE = 500f;
    Color HUB_TEXT_COLOR = Color.green;

    bool enable_disco = false;
    float obj_rotate_speed = 6f;
    bool enable_b_rotate = false;
    void Start()
    {
        KEYS_DICTIONARY.Add(STR_ROTATE, KEYS_ROTATE);
        KEYS_DICTIONARY.Add(STR_CHANGE_COLOR, KEYS_COLOR);

        transform.localScale = INIT_LOCAL_SCALE;
        Rigidbody b_body = gameObject.AddComponent<Rigidbody>();
        b_body.useGravity = USE_GRAVITY;
        b_body.AddForce(transform.up * DEFAULT_FORCE);
    }

    // Update is called once per frame
    void Update()
    {

        foreach(KeyCode key in KEYS_DICTIONARY[STR_ROTATE]) {
            if(Input.GetKeyDown(key)) {
                trigger_enable_rotate();
            }
        }

        foreach(KeyCode key in KEYS_DICTIONARY[STR_CHANGE_COLOR]) {
            if(Input.GetKeyDown(key)) {
                enable_disco = ! enable_disco;
            }
        }

        if(enable_disco) {
            var renderers = GetComponentsInChildren<Renderer>();
            if (renderers != null && renderers.Length > 0)
                renderers[0].material.color = Random.ColorHSV();
        }

        // Render rotate
        if(enable_b_rotate) {
            transform.Rotate(VECTOR_OBJ_ROTATE * obj_rotate_speed);
        }
    }
    private void OnMouseEnter() {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers != null && renderers.Length > 0)
            renderers[0].material.color = OBJECT_B_OVER_COLOR;
    }

    private void OnMouseExit() {
        var renderers = GetComponentsInChildren<Renderer>();
        if (renderers != null && renderers.Length > 0)
            renderers[0].material.color = OBJECT_B_COLOR;
    }
    void OnMouseDown()
    {         
        // this object was clicked - do something     
        Destroy (this.gameObject);
        Score.Instance.Value += 1;
    }   

    void trigger_enable_rotate() {
        enable_b_rotate = !enable_b_rotate;
    }
}

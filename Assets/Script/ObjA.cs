using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Props;

public class ObjA : MonoBehaviour
{
    // Start is called before the first frame update
    // Const var
    float STUDENT_ID = 18127130f;
    float INIT_POS_RAND_START = 10f;
    float INIT_POS_RAND_END = 20f;
    
    Color OBJECT_A_COLOR = Color.white;
    Color OBJECT_A_OVER_COLOR = Color.blue;
    string STR_UP = "up";
    string STR_DOWN = "down";
    string STR_LEFT = "left";
    string STR_RIGHT = "right";
    string STR_FLOAT_Y = "float y";
    string STR_FLOAT_X = "float x";
    string STR_ROTATE = "rotate";
    float FLOOR = 0;
    float CELL = 20;
    Vector3 VECTOR_UP = new Vector3(0, 0, 1);
    Vector3 VECTOR_DOWN = new Vector3(0, 0, -1);
    Vector3 VECTOR_LEFT = new Vector3(-1, 0, 0);
    Vector3 VECTOR_RIGHT = new Vector3(1, 0, 0);
    Vector3 VECTOR_FLOAT_UP = new Vector3(0, 1, 0);
    Vector3 VECTOR_FLOAT_DOWN = new Vector3(0, -1, 0);
    Vector3 VECTOR_OBJ_ROTATE = new Vector3(1, 0, 1);
    float FLOAT_HORIZONTAL_TIMEOUT = 3;
    List<KeyCode> KEYS_UP = new List<KeyCode>{KeyCode.UpArrow, KeyCode.W};
    List<KeyCode> KEYS_DOWN = new List<KeyCode>{KeyCode.DownArrow, KeyCode.S};
    List<KeyCode> KEYS_LEFT = new List<KeyCode>{KeyCode.LeftArrow, KeyCode.A};
    List<KeyCode> KEYS_RIGHT = new List<KeyCode>{KeyCode.RightArrow, KeyCode.D};
    List<KeyCode> KEYS_FLOAT_Y = new List<KeyCode>{KeyCode.T};
    List<KeyCode> KEYS_FLOAT_X = new List<KeyCode>{KeyCode.M};
    List<KeyCode> KEYS_ROTATE = new List<KeyCode>{KeyCode.R};
    Dictionary<string, Vector3> DIRECTIONS_DICTIONARY = new Dictionary<string, Vector3>();
    Dictionary<string,List<KeyCode>> KEYS_DICTIONARY = new Dictionary<string,List<KeyCode>>();

    Vector3 INIT_POSITION = new Vector3();
    Vector3 INIT_LOCAL_SCALE = new Vector3(3, 3, 3);

    // rect(x, y, xmax, ymax)
    Rect RECT_POSITION_LABEL = new Rect(25, 75, 100, 30);
    Rect RECT_POSITION_VALUE = new Rect(130, 75, 100, 30);
    Rect RECT_SPEED_LABEL = new Rect(25, 50, 100, 30);
    Rect RECT_SPEED_VALUE = new Rect (190, 50, 100, 30);
    Rect RECT_SPEED_SCROLLBAR = new Rect(75, 50, 100, 30);
    Dictionary<string, float> SCROLLBAR_PARAMS = new Dictionary<string, float>() {
        ["size"] = 1f,
        ["left value"] = 10f,
        ["right value"] = 41f,
    };
    Color HUB_TEXT_COLOR = Color.green; 
    Rect RECT_POINT_LABEL = new Rect(25, 100, 100, 30);

    // Global var
    float obj_speed = 10f;
    float obj_speed_factor = 0.1f;
    float obj_float_speed = 6f;
    bool enable_rotate = false;
    bool enable_float_y = false;
    bool enable_float_x = false;
    bool float_horizontal_dir_change = false;
    float time_last_change_dir = 0;

    GUIStyle style = new GUIStyle();
    float hScrollbarValue = 0;
    Vector3 float_dir_x;
    Vector3 float_dir_y;
    float float_x_speed;
    void game_init() {
        KEYS_DICTIONARY.Add(STR_UP, KEYS_UP);
        KEYS_DICTIONARY.Add(STR_DOWN, KEYS_DOWN);
        KEYS_DICTIONARY.Add(STR_LEFT, KEYS_LEFT);
        KEYS_DICTIONARY.Add(STR_RIGHT, KEYS_RIGHT);
        KEYS_DICTIONARY.Add(STR_FLOAT_Y, KEYS_FLOAT_Y);
        KEYS_DICTIONARY.Add(STR_FLOAT_X, KEYS_FLOAT_X);
        KEYS_DICTIONARY.Add(STR_ROTATE, KEYS_ROTATE);

        DIRECTIONS_DICTIONARY.Add(STR_UP, VECTOR_UP);
        DIRECTIONS_DICTIONARY.Add(STR_DOWN, VECTOR_DOWN);
        DIRECTIONS_DICTIONARY.Add(STR_LEFT, VECTOR_LEFT);
        DIRECTIONS_DICTIONARY.Add(STR_RIGHT, VECTOR_RIGHT);
    }

    void trigger_enable_float_y() {
        enable_float_y = !enable_float_y;
    }

    void trigger_enable_float_x() {
        enable_float_x = !enable_float_x;
    }

    void trigger_enable_rotate() {
        enable_rotate = !enable_rotate;
    }

    void OnGUI() {
        style.normal.textColor = HUB_TEXT_COLOR;  
        
        // Speed
        GUI.Label (RECT_SPEED_LABEL, "Speed: ", style);
        hScrollbarValue = GUI.HorizontalScrollbar(
            RECT_SPEED_SCROLLBAR, 
            hScrollbarValue, 
            SCROLLBAR_PARAMS["size"],
            SCROLLBAR_PARAMS["left value"],
            SCROLLBAR_PARAMS["right value"] 
        );
        GUI.Label (RECT_SPEED_VALUE, hScrollbarValue.ToString(), style);
        
        // Position
        GUI.Label (RECT_POSITION_LABEL, "Position(x, y, z): ", style);
        GUI.Label (RECT_POSITION_VALUE, $"({transform.position.x}, {transform.position.y}, {transform.position.z})", style);
        GUI.Label (RECT_POINT_LABEL, $"Point: {Score.Instance.Value}", style);
    }

    private void OnMouseEnter() {
        GetComponent<Renderer>().material.color = OBJECT_A_OVER_COLOR;
    }

    private void OnMouseExit() {
        GetComponent<Renderer>().material.color = OBJECT_A_COLOR;
    }

    void Start() {
        game_init();
        transform.position = INIT_POSITION;
        transform.position += new Vector3(0, Random.Range(INIT_POS_RAND_START, INIT_POS_RAND_END), 0);
        GetComponent<Renderer>().material.color = OBJECT_A_COLOR;
        transform.localScale = INIT_LOCAL_SCALE;
        float_dir_y = VECTOR_FLOAT_UP;
        float_dir_x = VECTOR_LEFT;
        float_x_speed = 2 + STUDENT_ID % 10;
    }

    // Update is called once per frame
    void Update() {
        obj_speed = hScrollbarValue;
        // Checking move actions and handle
        foreach(string dir in DIRECTIONS_DICTIONARY.Keys) {
            foreach(KeyCode key in KEYS_DICTIONARY[dir]) {
                if(Input.GetKey(key)) {
                    transform.position += DIRECTIONS_DICTIONARY[dir] * obj_speed * obj_speed_factor;
                    break;
                }
            }
        }
        // Checking float action and handle
        foreach(KeyCode key in KEYS_DICTIONARY[STR_FLOAT_Y]) {
            if(Input.GetKeyDown(key)) {
                trigger_enable_float_y();
            }
        }

        foreach(KeyCode key in KEYS_DICTIONARY[STR_FLOAT_X]) {
            if(Input.GetKeyDown(key)) {
                trigger_enable_float_x();
            }
        }

        // Checking rotate and handle
        foreach(KeyCode key in KEYS_DICTIONARY[STR_ROTATE]) {
            if(Input.GetKeyDown(key)) {
                trigger_enable_rotate();
            }
        }

        // Render float
        if(enable_float_y) {
            float float_factor = obj_speed * obj_speed_factor;
            if(transform.position.y >= CELL)
                float_dir_y = VECTOR_FLOAT_DOWN * float_factor;
            else if(transform.position.y <= FLOOR) 
                float_dir_y = VECTOR_FLOAT_UP * float_factor;
            transform.position += float_dir_y;

        }
        
        if(enable_float_x) {
            float float_factor = float_x_speed * obj_speed_factor;
            time_last_change_dir += Time.deltaTime;
            if(time_last_change_dir > FLOAT_HORIZONTAL_TIMEOUT) {
                time_last_change_dir = 0;
                float_horizontal_dir_change = !float_horizontal_dir_change;
            }
            if(float_horizontal_dir_change)
                float_dir_x = VECTOR_LEFT * float_factor;
            else 
                float_dir_x = VECTOR_RIGHT * float_factor;
            
            transform.position += float_dir_x;
        }
        else time_last_change_dir = 0;
        
        // Render rotate
        if(enable_rotate) {
            transform.Rotate(VECTOR_OBJ_ROTATE * obj_float_speed);
        }
    }
}

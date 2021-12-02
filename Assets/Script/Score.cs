using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props {
    public sealed class Score : Component {
        private static readonly Score instance = new Score();
        private int value;
        public Score() {
            value = 0;
        }
        public static Score Instance {
            get { return instance; }
        }
        public static Score GetInstance() {
            return instance;
        }
        
        public int Value {
            get { return value; }
            set { this.value = value; }
        }
    }    
}

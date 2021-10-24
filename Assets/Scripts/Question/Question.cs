using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Question
{
    [System.Serializable]
    public class Question
    {
        public int id;

        public string question;

        public string expectedAnswer;

        public string wrongAnswer1;

        public string wrongAnswer2;

        public string wrongAnswer3;
    }
}

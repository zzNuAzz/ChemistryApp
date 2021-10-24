using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettings
{
    [System.Serializable]
    public class SubjectData
    {
        public int subjectIndex;

        public SubjectData(int index) {
            subjectIndex = index;
        }

        public SubjectData() {
            subjectIndex = 0;
        }

    }

}
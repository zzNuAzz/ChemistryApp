using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Question
{
    public class QuestionManager : Utils.Singleton<QuestionManager>
    {
        private List<Question> questionDB;
        private List<Question> m_questions;
        private Question m_currentQuestion;
        private int m_subject;

        public int subject {
            get {
                return m_subject;
            }
            set {
                if(m_subject == value) {
                    return;
                }
                var subjectData = new GameSettings.SubjectData(value);
                Utils.SaveSystem.Save<GameSettings.SubjectData>(subjectData, "subject");
                m_subject = value;
            }
        }

        public Question getRandomQuestion()
        {
            if(m_questions != null && m_questions.Count == 0) {
                m_questions = new List<Question>(questionDB);
            }

            if (m_questions != null && m_questions.Count > 0)
            {
                int randIdx = Random.Range(0, m_questions.Count);
                m_currentQuestion = m_questions[randIdx];
                m_questions.RemoveAt (randIdx);
            }
            else
            {
                Debug.Log("Could not get question");
                // something wrong
            }
            return m_currentQuestion;
        }

        private void LoadQuestionFromCSV(string csvName)
        {
            //CSV reader
            string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
            char[] TRIM_CHARS = { '\"' };

            TextAsset data = Resources.Load(csvName) as TextAsset;
            var lines = Regex.Split(data.text, LINE_SPLIT_RE);

            //no record
            if (lines.Length <= 1) return;

            var header = Regex.Split(lines[0], SPLIT_RE);
            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "") continue;

                var entry = new Dictionary<string, object>();
                Question question = new Question();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value =
                        value
                            .TrimStart(TRIM_CHARS)
                            .TrimEnd(TRIM_CHARS)
                            .Replace("\\", "");
                    switch (header[j])
                    {
                        //case "id": qd.id = int.Parse(value);
                        //    break;
                        case "question":
                            question.question = value;
                            break;
                        case "expectedAnswer":
                            question.expectedAnswer = value;
                            break;
                        case "wrongAnswer1":
                            question.wrongAnswer1 = value;
                            break;
                        case "wrongAnswer2":
                            question.wrongAnswer2 = value;
                            break;
                        case "wrongAnswer3":
                            question.wrongAnswer3 = value;
                            break;
                    }
                }
                question.id = questionDB.Count;
                questionDB.Add (question);
            }
            Debug.Log("Load " + questionDB.Count + " question.");
        }

       
        private void Awake()
        {
            var subjectData = Utils.SaveSystem.Load<GameSettings.SubjectData>("subject");
            if(subjectData == null) {
                subjectData = new GameSettings.SubjectData();
                subject = subjectData.subjectIndex; // save
            }
            m_subject = subjectData.subjectIndex;

            LoadSetIndex(m_subject);
        }

        

        public void LoadSetIndex(int index) {
            subject = index;
            questionDB = new List<Question>();

            if(index < questionSet.GetLength(0)) {
                LoadQuestionFromCSV("QuestionDB/" + questionSet[index].Item2);
            }
            m_questions = new List<Question>();
        }

        public static (string, string)[] questionSet = {
            ("Hoá học lóp 8", "Grade8"),
            ("Hoá học lóp 9", "Grade9"),
            ("Test", "test_question"),
        };
    }
}

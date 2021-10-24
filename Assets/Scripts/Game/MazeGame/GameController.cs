using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MazeGame {
    public class GameController : Game.GameController
    {
        [Header("Maze generation values:")]
        public float holep;
        public int w, h;

        public Transform Container, Player, Goal, Staring;
        public GameObject Floor, Wall, Question;
        public CinemachineVirtualCamera cam;

        public bool[,] hwalls, vwalls; // bool[x,y]
        public bool[,] qs; // bool[x,y]
        // player init coordinate
        public (int, int) startCoordinate; //startX, startY;
        // current player coordinate
        public int x, y;
        public (int,int) goalCoordinate;

        private GameObject m_question;

        new void Start()
        {
            base.Start();
            GenerateLevel();
        }

        new void Update()
        {
            base.Update();
            if(this.pauseGame) return;

            HandleMovement();
            
            // go to goal
            if (Vector3.Distance(Player.position, Goal.position) < 0.2f)
            {
                AddScore(100);
                GenerateNextLevel();
            }
        }

        private void GenerateNextLevel() {
            // increase maze size
            if (w < h - 2 || Random.value < 0.5f)  {
                w++;
            } else {
                h++;
            } 
            GenerateLevel();
        }

        private void SetPlayerPosition(int x, int y) {
            this.x = x;
            this.y = y;
            Player.position = new Vector3(x, y);
        }

        private void HandleMovement() {
            var dirs = new[]
            {
                (x - 1, y, hwalls, x, y, Vector3.right, 90, KeyCode.A),
                (x + 1, y, hwalls, x + 1, y, Vector3.right, 90, KeyCode.D),
                (x, y - 1, vwalls, x, y, Vector3.up, 0, KeyCode.S),
                (x, y + 1, vwalls, x, y + 1, Vector3.up, 0, KeyCode.W),
            };
            foreach (var (nx, ny, wall, wx, wy, sh, ang, k) in dirs.OrderBy(d => Random.value))
                if (Input.GetKeyDown(k))
                    if (wall[wx, wy])
                        Player.position = Vector3.Lerp(Player.position, new Vector3(nx, ny), 0.1f);
                    else (x, y) = (nx, ny);

            Player.position = Vector3.Lerp(Player.position, new Vector3(x, y), Time.deltaTime * 12);
        }

        private void GenerateLevel() {
            foreach (Transform child in Container)
                Destroy(child.gameObject);
            hwalls = new bool[w + 1, h];
            vwalls = new bool[w, h + 1];
            var st = new int[w, h];

            void dfs(int x, int y)
            {
                st[x, y] = 1;
                GameObject m_floor = Instantiate(Floor, new Vector3(x, y), Quaternion.identity, Container);
                m_floor.name = "Floor - X:" + x + " Y:" + y;
                var dirs = new[]
                {
                    (Wall, x - 1, y, hwalls, x, y, Vector3.right, 90, KeyCode.A),
                    (Wall, x + 1, y, hwalls, x + 1, y, Vector3.right, 90, KeyCode.D),
                    (Wall, x, y - 1, vwalls, x, y, Vector3.up, 0, KeyCode.S),
                    (Wall, x, y + 1, vwalls, x, y + 1, Vector3.up, 0, KeyCode.W),
                };
                foreach (var (_Wall, nx, ny, wall, wx, wy, sh, ang, k) in dirs.OrderBy(d => Random.value))
                    if (!(0 <= nx && nx < w && 0 <= ny && ny < h) || (st[nx, ny] == 2 && Random.value > holep))
                    {
                        wall[wx, wy] = true;
                        GameObject m_wall = Instantiate(_Wall, new Vector3(wx, wy) - sh / 2, Quaternion.Euler(0, 0, ang), Container);
                        if(ang == 0) {
                            m_wall.name = "hWall - X:" + wx + " Y:" + wy;
                        } else {
                            m_wall.name = "vWall - X:" + wx + " Y:" + wy;
                        }
                    }
                    else if (st[nx, ny] == 0) dfs(nx, ny);
                st[x, y] = 2;
            }
            dfs(0, 0);
            
            GenQuestion();
            SetPlayer();
            SetGoal();

            // update cam orthographic size
            // cam.m_Lens.OrthographicSize = Mathf.Pow(w / 3 + h / 2, 0.7f) + 1;
            cam.m_Lens.OrthographicSize = Mathf.Pow(w / 6 + h / 5, 0.7f) + 1;
        }

        private void SetPlayer() {
            (int,int) _random;
            do {
                _random = (Random.Range(0, Mathf.CeilToInt(w/2)), Random.Range(Mathf.CeilToInt(h/2), h));
            } while(qs[_random.Item1, _random.Item2]);
            startCoordinate = (_random.Item1, _random.Item2);
            Staring.position = new Vector3(startCoordinate.Item1, startCoordinate.Item2);
            SetPlayer(_random.Item1, _random.Item2);
        }
        private void SetPlayer(int x, int y) {
            this.x = x;
            this.y = y;
            Player.position = new Vector3(x, y);
        }

        private void SetGoal() {
            // set goal position out range of player
            Vector3 _goalPosition;
            (int,int) _goalCoordinate;
            do {
                _goalCoordinate = (Random.Range(Mathf.CeilToInt(w/2), w), Random.Range(0, Mathf.CeilToInt(h/2)));
                _goalPosition = new Vector3(_goalCoordinate.Item1, _goalCoordinate.Item2);
            }
            while (Vector3.Distance(Player.position, _goalPosition) < (w + h) / 4 && qs[_goalCoordinate.Item1, _goalCoordinate.Item2]);
            Goal.position = _goalPosition;
            goalCoordinate = _goalCoordinate;
        }

        private void GenQuestion() {
            qs = new bool[w, h];
            int n_question = Mathf.CeilToInt(w * h / 5);
            for(int i = 0; i < n_question; i++) {
                int pos_x, pos_y;
                do {
                    pos_x = Random.Range(0,w);
                    pos_y = Random.Range(0,h);
                }
                while (
                    qs[pos_x, pos_y] || 
                    (pos_x - 1 >= 0 && qs[pos_x - 1, pos_y]) || 
                    (pos_x + 1 < w && qs[pos_x + 1, pos_y]) || 
                    (pos_y - 1 >= 0 && qs[pos_x, pos_y - 1]) || 
                    (pos_y + 1 < h && qs[pos_x, pos_y + 1])
                );
                qs[pos_x, pos_y] = true;
                GameObject QuestionIns = Instantiate(Question, new Vector3(pos_x, pos_y), Quaternion.identity, Container);
                QuestionIns.name = "Question_" + i;
               
            }
        }

        public void SetActiveQuestion(GameObject g) {
            m_question = g;
        } 

        protected override void SubmitAnswerCallback(bool isCorrect) {
            if(isCorrect) {
                if(m_question) {
                    Destroy(m_question);
                }
            } else {
                SetPlayer(startCoordinate.Item1, startCoordinate.Item2);
            }
        }

        private void OnValidate() {
            cam.m_Lens.OrthographicSize = Mathf.Pow(w / 6 + h / 5, 0.7f) + 1;
        }
    }
}
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallGameLogic : MonoBehaviour
    {
        [Header("Referências")]
        public BallMoviment ballMoviment;

        [Header("UI")]
        public TextMeshProUGUI leftScoreText;
        public TextMeshProUGUI rightScoreText;
        public TextMeshProUGUI pointMessageText;

        public MustacheAnim mustacheAnim;

        private const string leftGoalTag = "leftGoal";
        private const string rightGoalTag = "rightGoal";

        private int leftScore = 0;
        private int rightScore = 0;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            UpdateScoreUI();
            pointMessageText.enabled = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject goal = collision.gameObject;

            if (goal.CompareTag(leftGoalTag))
            {
                rightScore++;
                if (rightScore == 5)
                {
                    UpdateScoreUI();
                    leftScore = 0;
                    rightScore = 0;
                    pointMessageText.text = "Jogador Laranja venceu";
                    pointMessageText.enabled = true;
                    ballMoviment.ResetGame();
                }
                else
                {
                    pointMessageText.text = "Jogador Laranja fez ponto!";
                    HandlePoint();
                }
                mustacheAnim.startAnim();
            }
            else if (goal.CompareTag(rightGoalTag))
            {
                leftScore++;
                if(leftScore == 5)
                {
                    UpdateScoreUI();
                    leftScore = 0;
                    rightScore = 0;
                    pointMessageText.text = "Jogador Azul venceu";
                    pointMessageText.enabled = true;
                    ballMoviment.ResetGame();
                }
                else
                {
                    pointMessageText.text = "Jogador Azul fez ponto!";
                    HandlePoint();
                }
                mustacheAnim.startAnim();
            }



        }

        private void HandlePoint()
        {
            UpdateScoreUI();
            pointMessageText.enabled = true;

            ballMoviment.StopBall();
            ballMoviment.SetPosition(Vector3.zero);

            StartCoroutine(ShowPointAndRestart());
        }

        private IEnumerator ShowPointAndRestart()
        {
            yield return new WaitForSeconds(2f);
            pointMessageText.enabled = false;
            yield return new WaitForSeconds(0.5f);

            ballMoviment.ResetSpeed();
            ballMoviment.LaunchBall(); // Internamente usa linearVelocity
        }

        private void UpdateScoreUI()
        {
            leftScoreText.text = leftScore.ToString();
            rightScoreText.text = rightScore.ToString();
        }
    }
}
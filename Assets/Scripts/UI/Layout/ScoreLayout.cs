using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLayout : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreOne, _scoreTwo, _scoreThree, _scoreFour;
    [SerializeField] Image _squareOne, _squareTwo, _squareThree, _squareFour;

    public void Init()
    {
        ScoreManager.Instance.OnScoreUpdate += UpdateScore;
    }


    public void UpdateScore()
    {
        List<TeamScore> teamScore = ScoreManager.Instance.GetLeaderBoard();

        _scoreOne.text = teamScore[0].Score.ToString();
        _squareOne.color = ColorManager.Instance.GetColorByTeam(teamScore[0].Team);

        if (teamScore[0].Team == PlayerManager.Instance.PlayerTeamColor) _scoreOne.fontStyle = FontStyles.Bold;
        else _scoreOne.fontStyle = FontStyles.Normal;

        _scoreTwo.text = teamScore[1].Score.ToString();
        _squareTwo.color = ColorManager.Instance.GetColorByTeam(teamScore[1].Team);

        if (teamScore[1].Team == PlayerManager.Instance.PlayerTeamColor) _scoreTwo.fontStyle = FontStyles.Bold;
        else _scoreTwo.fontStyle = FontStyles.Normal;

        _scoreThree.text = teamScore[2].Score.ToString();
        _squareThree.color = ColorManager.Instance.GetColorByTeam(teamScore[2].Team);

        if (teamScore[2].Team == PlayerManager.Instance.PlayerTeamColor) _scoreThree.fontStyle = FontStyles.Bold;
        else _scoreThree.fontStyle = FontStyles.Normal;

        _scoreFour.text = teamScore[3].Score.ToString();
        _squareFour.color = ColorManager.Instance.GetColorByTeam(teamScore[3].Team);

        if (teamScore[3].Team == PlayerManager.Instance.PlayerTeamColor) _scoreFour.fontStyle = FontStyles.Bold;
        else _scoreFour.fontStyle = FontStyles.Normal;

    }
}

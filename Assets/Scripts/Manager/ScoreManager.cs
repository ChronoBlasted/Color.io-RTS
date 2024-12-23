using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public UnityAction OnScoreUpdate;

    public int ScoreBlue, ScoreRed, ScoreYellow, ScoreGreen;
    public void AddScore(Team teamToAddScore, int amountToAdd = 1)
    {
        switch (teamToAddScore)
        {
            case Team.Blue:
                ScoreBlue += amountToAdd;
                break;
            case Team.Red:
                ScoreRed += amountToAdd;
                break;
            case Team.Yellow:
                ScoreYellow += amountToAdd;
                break;
            case Team.Green:
                ScoreGreen += amountToAdd;
                break;
            default:
                break;
        }

        OnScoreUpdate?.Invoke();
    }

    public int GetScoreByTeam(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                return ScoreBlue;
            case Team.Red:
                return ScoreRed;
            case Team.Yellow:
                return ScoreYellow;
            case Team.Green:
                return ScoreGreen;
        }

        return 0;
    }

    public List<TeamScore> GetLeaderBoard()
    {
        List<TeamScore> teamScores = new List<TeamScore>
        {
            new TeamScore(Team.Blue, ScoreBlue),
            new TeamScore(Team.Red, ScoreRed),
            new TeamScore(Team.Yellow, ScoreYellow),
            new TeamScore(Team.Green, ScoreGreen)
        };

        teamScores.Sort((a, b) => b.Score.CompareTo(a.Score));

        return teamScores;
    }
}

public class TeamScore
{
    public Team Team;
    public int Score;

    public TeamScore(Team teamName, int score)
    {
        Team = teamName;
        Score = score;
    }
}

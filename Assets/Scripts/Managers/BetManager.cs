using System.Collections.Generic;
using Enums;
using Scripts.Bet;
using Scripts.Helpers;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    private Dictionary<int, PlayerBetHolder> _playerBets;

    public void Initialize()
    {
        _playerBets = new Dictionary<int, PlayerBetHolder>();
    }

    public void PlaceBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue)
    {
        if (!_playerBets.ContainsKey(playerId))
        {
            _playerBets[playerId] = new PlayerBetHolder();
        }

        _playerBets[playerId].AddBet(betType, numbers, chipValue);
        
        string numbersString = string.Join(", ", numbers);
        
        Debug.Log("Bet placed: " + betType + " " + numbersString + " " + chipValue);
    }
    
    public void RemoveBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue)
    {
        if (_playerBets.TryGetValue(playerId, out var bet))
        {
            bet.RemoveBet(betType, numbers, chipValue);
        }
    }

    public List<PlayerBet> GetWinningBets(int playerId, int winningNumber)
    {
        List<PlayerBet> winningBets = new List<PlayerBet>();

        if (_playerBets.TryGetValue(playerId, out var playerBet))
        {
            foreach (var bet in playerBet.GetAllBets())
            {
                if (IsWinningBet(bet, winningNumber))
                {
                    winningBets.Add(bet);
                }
            }
        }

        return winningBets;
    }

    private bool IsWinningBet(PlayerBet bet, int winningNumber)
    {
        switch (bet.BetType)
        {
            case BetType.Single:
                return bet.Numbers[0] == winningNumber;
            case BetType.Split:
            case BetType.Street:
            case BetType.Corner:
            case BetType.Line:
                return bet.Numbers.Contains(winningNumber);
            case BetType.Red:
                return IsRedNumber(winningNumber);
            case BetType.Black:
                return !IsRedNumber(winningNumber);
            case BetType.Even:
                return winningNumber % 2 == 0 && winningNumber != 0;
            case BetType.Odd:
                return winningNumber % 2 == 1;
            case BetType.Low:
                return winningNumber >= 1 && winningNumber <= 18;
            case BetType.High:
                return winningNumber >= 19 && winningNumber <= 36;
            case BetType.Column when bet.Numbers[0] == 1:
                return winningNumber % 3 == 1;
            case BetType.Column when bet.Numbers[0] == 2:
                return winningNumber % 3 == 2;
            case BetType.Column when bet.Numbers[0] == 3:
                return winningNumber % 3 == 0;
            case BetType.Dozen when bet.Numbers[0] == 1:
                return winningNumber >= 1 && winningNumber <= 12;
            case BetType.Dozen when bet.Numbers[0] == 13:
                return winningNumber >= 13 && winningNumber <= 24;
            case BetType.Dozen when bet.Numbers[0] == 25:
                return winningNumber >= 25 && winningNumber <= 36;
            default:
                return false;
        }
    }

    private bool IsRedNumber(int number)
    {
        List<int> redNumbers = new List<int>
        {
            1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36
        };
        
        return redNumbers.Contains(number);
    }
}
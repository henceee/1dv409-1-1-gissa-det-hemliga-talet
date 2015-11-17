using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.Models
{
    public class SecretNumber
    {
        private List<GuessedNumber> _guessedNumbers;
        private GuessedNumber _lastGuessedNumber;
        private int? _number;
        public static readonly int MaxNumberOfGuesses = 7;
        private const int MinVal = 1;
        private const int MaxVal = 100;

        public bool CanMakeGuess
        {
            get
            {
                return Count < MaxNumberOfGuesses && _lastGuessedNumber.Outcome != Outcome.Right;                
            }
        }

        public int Count
        {   
            get
            {
                return _guessedNumbers.Count;
            }
        }

        public IList<GuessedNumber> GuessedNumbers
        {
            get
            {
                return _guessedNumbers.AsReadOnly();
            }
        }

        public GuessedNumber LastGuessedNumber { get { return _lastGuessedNumber; } }
        
        public int? Number 
        {
            get
            {
                return CanMakeGuess ? null : _number; 
            } 
            set
            {
                _number = value;
            }
        }
        
        public SecretNumber()
        {
            _guessedNumbers = new List<GuessedNumber>(MaxNumberOfGuesses);
            this.Initialize();
        }
            

        public void Initialize()
        {
            _guessedNumbers.Clear();
            _number = new Random().Next(MinVal, MaxVal);
            _lastGuessedNumber.Number = null;
            _lastGuessedNumber.Outcome = Outcome.Indefinite;
        }

        public Outcome MakeGuess(int guess)
        {
            if (!CanMakeGuess)
            {
                _lastGuessedNumber.Outcome = Outcome.NoMoreGuesses;
            }   
            if (guess < MinVal || guess > MaxVal)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_guessedNumbers.Exists(g =>g.Number == guess))
            {
                _lastGuessedNumber.Outcome = Outcome.OldGuess;
            }
            else
            {
                if (guess > _number)
                {
                    _lastGuessedNumber.Outcome = Outcome.High;
                }
                if (guess < _number)
                {
                    _lastGuessedNumber.Outcome = Outcome.Low;
                }
                if (guess == _number)
                {
                    _lastGuessedNumber.Outcome = Outcome.Right;
                }
                _lastGuessedNumber.Number = guess;
                _guessedNumbers.Add(_lastGuessedNumber);
            }            

            return _lastGuessedNumber.Outcome;
        }
    }
}


   
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

        public bool CanMakeGuess
        {
            get
            {
                if(Count >= MaxNumberOfGuesses)
                {
                    return true;
                }
                return false;

            }
        }

        public int Count { get { return _guessedNumbers.Count; } }

        public IList<GuessedNumber> GuessedNumbers { get { return _guessedNumbers.AsReadOnly(); } }

        public GuessedNumber LastGuessedNumber { get { return _lastGuessedNumber; } }

        [Range(1,100,ErrorMessage ="Talet måste vara mellan 1-100")]
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

        //TODO: ADD cstror with 1 param
        public SecretNumber()
        {
            this.Initialize();
        }
            

        public void Initialize()
        {
            _guessedNumbers = new List<GuessedNumber>();
            _guessedNumbers.Capacity = 7;
            Random rand = new Random();
            _number = rand.Next(1, 100);
            _lastGuessedNumber.Number = null;
            _lastGuessedNumber.Outcome = Outcome.Indefinite;
        }

        public Outcome MakeGuess(int guess)
        {
            if(_guessedNumbers.Exists(g =>g.Number == guess))
            {
                _lastGuessedNumber.Outcome = Outcome.OldGuess;
            }
            if (!CanMakeGuess)
            {
                _lastGuessedNumber.Outcome = Outcome.NoMoreGuesses;
            }
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

            return _lastGuessedNumber.Outcome;
        }
    }
}


   
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.Models
{
    public class SecretNumber
    {
        #region fields
        private List<GuessedNumber> _guessedNumbers;
        private GuessedNumber _lastGuessedNumber;
        private int? _number;
        public static readonly int MaxNumberOfGuesses = 7;
        private const int MinVal = 1;
        private const int MaxVal = 100;
        #endregion

        #region properties
        /// <summary>
        /// 
        /// </summary>
        public bool CanMakeGuess
        {
            get
            {
                return Count < MaxNumberOfGuesses && _lastGuessedNumber.Outcome != Outcome.Right;            
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {   
            get
            {
                return _guessedNumbers.Count;
            }
        }
    
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<GuessedNumber> GuessedNumbers
        {
            get
            {
                return _guessedNumbers.AsReadOnly();
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>        
        public SecretNumber()
        {
            _guessedNumbers = new List<GuessedNumber>(MaxNumberOfGuesses);
            this.Initialize();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        #region Initialize
        public void Initialize()
        {
            _guessedNumbers.Clear();
            _number = new Random().Next(MinVal, MaxVal + 1);
            _lastGuessedNumber = new GuessedNumber { Number = null, Outcome = Outcome.Indefinite};
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        #region MakeGuess
        public Outcome MakeGuess(int guess)
        {
            
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
                
                if(!CanMakeGuess)
                {
                    _lastGuessedNumber.Outcome = Outcome.NoMoreGuesses;
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
                    if (CanMakeGuess)
                    {
                        _guessedNumbers.Add(_lastGuessedNumber);
                    }
                }
                
            }            

            return _lastGuessedNumber.Outcome;
        }
        #endregion
    }
}


   
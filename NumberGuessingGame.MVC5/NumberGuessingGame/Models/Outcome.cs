using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGuessingGame.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum Outcome
    {
        Indefinite,
        Low,
        High,
        Right,
        NoMoreGuesses,
        OldGuess
    }
}

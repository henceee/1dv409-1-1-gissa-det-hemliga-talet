using System.ComponentModel.DataAnnotations;

namespace NumberGuessingGame.Models.ViewModels
{
    public class ViewModel
    {
        #region properties

        [Required(ErrorMessage = "A number must be provided.")]
        [Range(1, 100, ErrorMessage = "The number must be within the interval 1-100")]
        public int? Guess { get; set; }

        public Outcome Outcome
        {
            get { return SecretNumber.LastGuessedNumber.Outcome; }
        }

        public SecretNumber SecretNumber { get; set; }

        #endregion
        /// <summary>
        /// Display the output, to high, to low and so on.
        /// </summary>
        /// <returns></returns>
        #region DisplayOutCome
        public string OutComeText
        {
            get
            {
                string output = string.Empty;

                switch (SecretNumber.LastGuessedNumber.Outcome)
                {
                    case Outcome.Low:
                        output = string.Format("{0} is too low.", Guess);
                        break;
                    case Outcome.High:
                        output = string.Format("{0} is too high.", Guess);
                        break;
                    case Outcome.Right:
                        output = string.Format("Congrats! You guessed the right number, after {0} guesses.",
                                                SecretNumber.Count);
                        break;
                    case Outcome.NoMoreGuesses:
                        output = string.Format("No more guesses. The correct number was {0}",
                                                SecretNumber.Number);
                        break;
                    case Outcome.OldGuess:
                        output = string.Format("You have already guessed the number{0}.",
                                               Guess);
                        break;
                    default:
                        break;
                }

                return output;
            }
        }
        #endregion

        /// <summary>
        /// Display the header, for example First Guess, Second Guess and so on.
        /// </summary>
        /// <returns></returns>
        #region DisplayHeaderText
        public string HeaderText
        {
            get
            {
                if (!SecretNumber.CanMakeGuess)
                {
                    return "No more guesses!";
                }
                else if (SecretNumber.LastGuessedNumber.Outcome == Outcome.Right)
                {
                    return "Correct!";
                }
                else
                {
                    switch (SecretNumber.Count)
                    {
                        case 1:
                            return "First Guess";
                        case 2:
                            return "Second Guess";
                        case 3:
                            return "Third Guess";
                        case 4:
                            return "Fourth Guess";
                        case 5:
                            return "Fith Guess";
                        case 6:
                            return "Sixth Guess";
                        case 7:
                            return "Seventh Guess";

                        default:
                            return string.Empty;
                    }
                }

            }
        }

        public bool AnyGuesses
        {
            get
            {
                return SecretNumber != null && SecretNumber.Count > 0;
            }
        }

        #endregion
    }



}
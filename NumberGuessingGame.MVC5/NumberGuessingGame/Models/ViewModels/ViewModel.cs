using System.ComponentModel.DataAnnotations;

namespace NumberGuessingGame.Models.ViewModels
{
    public class ViewModel
    {
        private SecretNumber _secretNumber;

        [Required(ErrorMessage ="A number must be provided.")]
        [Range(1, 100, ErrorMessage = "The number must be within the interval 1-100")]
        public int Guess { get; set; }

        public Outcome Outcome { get; set; }

        public SecretNumber SecretNumber
        {
            get
            { 
                return _secretNumber;
            }
            set
            {
                _secretNumber = value;
            }
        }

     
        public string DisplayOutCome()
        {
            string output = "";
            switch (SecretNumber.LastGuessedNumber.Outcome)
            {
                case Outcome.Low:
                    output =  string.Format("{0} is too low.", Guess);
                    break;
                case Outcome.High:
                    output = string.Format("{0} is too high.", Guess);
                    break;
                case Outcome.Right:
                    output = string.Format("Congrats! You guessed the right number, after {0} guesses.",
                                            SecretNumber.Count);
                    break;
                case Outcome.NoMoreGuesses:
                    output = string.Format("{0} No more guesses. The correct number was {1}",
                                            output, SecretNumber.Number);
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
}
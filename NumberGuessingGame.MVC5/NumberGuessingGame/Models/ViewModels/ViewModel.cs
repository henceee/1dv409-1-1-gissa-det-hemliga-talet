using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.Models.ViewModels
{
    public class ViewModel
    {
        private SecretNumber _secretNumber;
        
        [Required]
        [Range(1, 100, ErrorMessage = "Talet måste vara mellan 1-100")]
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
                _secretNumber = value
            }
        }
    }
}
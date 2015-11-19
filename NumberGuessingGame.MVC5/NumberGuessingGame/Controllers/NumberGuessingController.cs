using NumberGuessingGame.Models;
using NumberGuessingGame.Models.ViewModels;
using System.Web.Mvc;

namespace NumberGuessingGame.Controllers
{
    public class NumberGuessingController : Controller
    {
        private SecretNumber _secretNumber;
     
        /// <summary>
        /// Property for the field _secretnumber
        /// 
        /// </summary>
        public  SecretNumber SecretNumber
        {
            //If the field _secretnumber != null ret. it, otherwise
            // instantiate & ret. new secrecNumber obj.
            get
            {
                return _secretNumber ?? (_secretNumber = new SecretNumber());
            }
            set
            {
                _secretNumber = value;
            }
        }
        /// <summary>
        /// Property for Session to hold ref. to hold SecretNumber obj.
        /// </summary>
        public SecretNumber SecretNumberSession
        {
            // If the sesssion != null, cast & return it,
            // otherwise set to SecretNumber prop
            get
            {
                return (SecretNumber)Session["SecretNumber"] ?? (SecretNumberSession = SecretNumber);
            }

            set
            {
                Session["SecretNumber"] = value;
            }
        }
        
        // GET: NumberGuessing
        public ActionResult Index()
        {
            NewSession();

            var viewModel = new ViewModel { Outcome = Outcome.Indefinite, SecretNumber = SecretNumber };

            return View("Index",viewModel);
        }

        //POST: NumberGuessing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Guess")]ViewModel viewModel)
        {
            SecretNumber = SecretNumberSession;

            if (ModelState.IsValid)
            {
                viewModel.Outcome = SecretNumber.MakeGuess(viewModel.Guess);
                viewModel.SecretNumber = SecretNumber;

                return View(viewModel);
            }
            return View("Index");
        }

        //Get Index
        public ActionResult newNumber()
        {
            NewSession();

            return View("Index");
        }
        /// <summary>
        /// Function to clear and set 
        /// SecretNumber prop.
        /// </summary>
        public void NewSession()
        {
            Session.Clear();

            SecretNumber = SecretNumberSession;
        }


    }
}
using NumberGuessingGame.Models;
using NumberGuessingGame.Models.ViewModels;
using System.Web.Mvc;

namespace NumberGuessingGame.Controllers
{
    public class NumberGuessingController : Controller
    {
        private SecretNumber _secretNumber;
        public  SecretNumber SecretNumber
        {
            get
            {
                return _secretNumber ?? (_secretNumber = new SecretNumber());
            }
            set
            {
                _secretNumber = value;
            }
        }

        public SecretNumber SecretNumberSession
        {
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
            Session.Clear();

            SecretNumber = SecretNumberSession;

            var viewModel = new ViewModel { Outcome = Outcome.Indefinite, SecretNumber = SecretNumber };

            return View(viewModel);
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
            return View();
        }


    }
}
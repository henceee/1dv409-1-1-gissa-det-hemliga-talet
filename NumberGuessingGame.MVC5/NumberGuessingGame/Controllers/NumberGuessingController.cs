using NumberGuessingGame.Models;
using NumberGuessingGame.Models.ViewModels;
using System.Web.Mvc;

namespace NumberGuessingGame.Controllers
{
    public class NumberGuessingController : Controller
    {
        /// <summary>
        /// Property for the field _secretnumber
        /// 
        /// </summary>
        public SecretNumber SecretNumber
        {
            //If the field _secretnumber != null ret. it, otherwise
            // instantiate & ret. new secrecNumber obj.
            get
            {
                return Session["SecretNumber"] as SecretNumber ?? (SecretNumber)(Session["SecretNumber"] = new SecretNumber());
            }
        }

        // GET: NumberGuessing
        public ActionResult Index()
        {
            var viewModel = new ViewModel { SecretNumber = SecretNumber };

            return View("Index", viewModel);
        }

        //POST: NumberGuessing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Guess")]ViewModel viewModel)
        {
            //If the session hasn't timed out....
            if (Session.IsNewSession)
            {
                //it must've timed out (set to 30 sek for testing,see web config for session timeout)
                return View("Timeout");
            }

            viewModel.SecretNumber = SecretNumber;
            //and the modelstate is valid
            if (ModelState.IsValid)
            {
                SecretNumber.MakeGuess(viewModel.Guess.Value);
            }

            return View("Index", viewModel);
        }

        //Get Index
        public ActionResult newNumber()
        {
            SecretNumber.Initialize();

            return RedirectToAction("Index");
        }
    }
}
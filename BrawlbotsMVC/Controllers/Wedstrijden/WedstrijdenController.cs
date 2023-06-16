using BrawlbotsMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrawlbotsMVC.Controllers.Wedstrijden
{
    public class WedstrijdenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FetchMatches()
        {
            List<WedstrijdenData> returnList = new List<WedstrijdenData>();
            WedstrijdenData wedstrijd = new WedstrijdenData();

            returnList = wedstrijd.FetchAllMatchData();
            return View(returnList);
        }

        public IActionResult CreateWedstrijdForm()
        {

            return View("CreateWedstrijdForm");
        }

        public IActionResult ProcessCreate(WedstrijdenData wedstrijden)
        {
            WedstrijdenData Wedstrijd = new WedstrijdenData();

            Wedstrijd.CreateMatch(wedstrijden);

            return RedirectToAction("FetchMatches", wedstrijden);

        }

        public ActionResult Delete(int id)
        {
            WedstrijdenData wedstrijd = new WedstrijdenData();
            wedstrijd.deleteMatch(id);
            return RedirectToAction("FetchMatches");
        }

        public IActionResult FetchLeaderbord()
        {
            List<WedstrijdenData> returnList = new List<WedstrijdenData>();
            WedstrijdenData leaderbord = new WedstrijdenData();

            returnList = leaderbord.FetchLeaderbord();
            return View(returnList);
        }
    }
}

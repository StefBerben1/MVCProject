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

        public IActionResult CreateEditForm(int id)
        {

            WedstrijdenData wedstrijdForm = new WedstrijdenData();
            WedstrijdenData opgehaaldeMatch = wedstrijdForm.FetchSingleMatch(id);
            return View("editMatch", opgehaaldeMatch);
        }

        [HttpPost]
        public IActionResult RetrieveMatchFormData()
        {
            WedstrijdenData formdata = new WedstrijdenData();
            formdata.id = Convert.ToInt32(HttpContext.Request.Form["id"]);
            formdata.arena_name = HttpContext.Request.Form["arena_name"].ToString();
            formdata.match_date = Convert.ToDateTime(HttpContext.Request.Form["match_date"]);
           
            int result = formdata.UpdateMatchData();

            return RedirectToAction("FetchMatches");
        }
    }
}

using BrawlbotsMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrawlbotsMVC.Controllers.Robot
{
    public class RobotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Robot()
        {
            List<RobotData> returnList = new List<RobotData>();
            RobotData robot = new RobotData();

            returnList = robot.FetchAll();
            return View(returnList);
        }


        public IActionResult CreateRobotForm()
        {

            return View("addRobot");
        }

        public IActionResult CreateEditForm(int id )
        {
 
            RobotData robot = new RobotData();
            RobotData opgehaaldeRobot = robot.FetchSingleRobot(id);
            return View("editRobot", opgehaaldeRobot);
        }

        public IActionResult ProcessCreate(RobotData CreateRobot)
        {
            RobotData robot = new RobotData();

            robot.CreateRobot(CreateRobot);

            return RedirectToAction("Robot", CreateRobot);

        }



        public ActionResult Delete(int id)
        {
            RobotData robot = new RobotData();
            robot.deleteRobot(id);
            return RedirectToAction("Robot");
        }


        [HttpPost]
        public IActionResult RetrieveFormData()
        {
            RobotData formdata = new RobotData();
            formdata.id  = Convert.ToInt32(HttpContext.Request.Form["id"]);
            formdata.name = HttpContext.Request.Form["name"].ToString();
            formdata.Weapon = HttpContext.Request.Form["Weapon"].ToString();
            formdata.type_of_movement = HttpContext.Request.Form["type_of_movement"].ToString();
            formdata.weight_class = HttpContext.Request.Form["weight_class"].ToString();
            formdata.team_name = HttpContext.Request.Form["team_name"].ToString();

            int result = formdata.UpdateRobotData();

            return RedirectToAction("Robot");
        }
    }
}

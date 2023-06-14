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
    }
}

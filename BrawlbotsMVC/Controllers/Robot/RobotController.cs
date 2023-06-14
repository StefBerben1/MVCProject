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

        public IActionResult CreateEditForm(int id)
        {

            RobotData robot = new RobotData();

            robot.FetchSingleRobot(id);
            return View("editRobot", robot);
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            RobotData robot = new RobotData();
            robot.FetchSingleRobot(id);
            return View(robot);
        }

        //[HttpPost]
        //public IActionResult Edit(RobotData robot)
        //{
        //    RobotData robot = new RobotData();
        //    robot.FetchSingleRobot(id);
        //    return View(robot);
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amazon.DynamoDBv2.DocumentModel;
using DemoChess.GameScripts;

namespace DemoChess.Controllers
{

    public class GameController : Controller
    {
        // GET: GameManager
        [HttpPost]
        public ActionResult Index(String playerName , String team)
        {
            Debug.WriteLine(playerName + " " + team);

            ViewBag.playerName = playerName;
            ViewBag.playerTeam = team;

            return View();
        }

        public ActionResult GameBoard()
        {
            return PartialView();
        }


        public ActionResult PlayedGames()
        {
            GameDatabase database = new GameDatabase();

            List<Document> documents =  database.GetTableInfo();

            Debug.WriteLine(documents.Count);

            return View(documents);
        }


        public ActionResult GetGame(string id)
        {
            GameDatabase database = new GameDatabase();
            List<Document> documents = new List<Document>();
   
    
            return Json(database.GetGameEntry(id).ToJson());
        }



    }
}
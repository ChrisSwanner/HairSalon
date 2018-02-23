using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {

    [HttpGet("/stylists/{stylistID}/clients/new")]
    public ActionResult CreateClientForm(int stylistId)
    {
      Stylist foundstylist = Stylist.Find(stylistId);
      return View(foundstylist);
    }

//not currently used
    [HttpGet("/clients/{id}")]
    public ActionResult Detail(int id)
    {
      Client item = Client.Find(id);
      return View(item);
    }

    [HttpGet("/clients/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Client thisItem = Client.Find(id);
      return View(thisItem);
    }

    [HttpPost("/clients/{id}/update")]
    public ActionResult UpdateItem(int id)
    {
      Client thisItem = Client.Find(id);
      thisItem.Edit(Request.Form["newname"], Request.Form["newdate"]);
      return RedirectToAction("Detail", "stylists", new {Id = thisItem.GetstylistId()});
    }

    [HttpGet("/clients/{id}/delete")]
    public ActionResult DeleteItem(int id)
    {
      Client thisItem = Client.Find(id);
      //int catId = thisItem.GetstylistId();
      thisItem.Delete();
      return RedirectToAction("Detail", "stylists", new {Id = thisItem.GetstylistId()});
    }
  }
}

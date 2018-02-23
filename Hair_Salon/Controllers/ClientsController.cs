using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {

    [HttpGet("/stylists/{stylistID}/clients/new")]
    public ActionResult CreateStylistForm(int stylistId)
    {
      //Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      return View(foundStylist);

    }
    [Route("/clients")]
    public ActionResult ClientIndex()
    {
      return View("/Clients/Index", Client.GetAll());
    }

    [HttpGet("/clients/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/clients")]
    public ActionResult Create()
    {
      Client newClient = new Client(Request.Form["new-item"], Request.Form["raw-date"]);
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }

    [HttpGet("/clients/{id}")]
    public ActionResult Detail(int id)
    {
      Client client = Client.Find(id);
      return View(client);
    }

    [HttpGet("/clients/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Client thisClient = Client.Find(id);
      return View(thisClient);
    }

    [HttpPost("/clients/{id}/update")]
    public ActionResult Update(int id)
    {
      Client thisClient = Client.Find(id);
      thisItem.Edit(Request.Form["newname"]);
      return RedirectToAction("ClientIndex");
    }

    [HttpGet("/clients/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Client thisClient = Client.Find(id);
      thisClient.Delete();
      return RedirectToAction("ClientIndex");
    }
  }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [Route("/")]
    public ActionResult Index()
    {
      List<Stylist> allCats = Stylist.GetAll();
      return View("Index", allCats);
    }

    [HttpGet("/stylists/new")]
    public ActionResult CreateStylistForm()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Createstylist()
    {
      Stylist newstylist = new Stylist(Request.Form["name"]);
      newstylist.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Detail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedstylist = Stylist.Find(id);
      List<Client> allItems = selectedstylist.GetItems();
      model.Add("stylist", selectedstylist);
      model.Add("clients", allItems);
      return View(model);
    }

    [HttpPost("/stylists/{id}/clients")]
    public ActionResult CreateItem(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist foundstylist = Stylist.Find(id);
        List<Client> stylistItems = foundstylist.GetItems();
        Client newItem = new Client(Request.Form["new-item"], Request.Form["raw-date"]);
        newItem.SetCatId(foundstylist.GetId());
        stylistItems.Add(newItem);
        newItem.Save();
        model.Add("clients", stylistItems);
        model.Add("stylist", foundstylist);
        return View("Detail", model);
    }

    [HttpGet("/stylists/{id}/delete")]
    public ActionResult Deletestylist(int id)
    {
      Stylist thisstylist = Stylist.Find(id);
      thisstylist.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}/clients/sort-asc")]
    public ActionResult SortAsc(int id)
    {
      Stylist sortCat = Stylist.Find(id);
      List<Client> sortedItems = new List<Client>{};
      sortedItems = sortCat.SortAsc();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", sortCat);
      model.Add("clients", sortedItems);
      return View("Detail", model);
    }

    [HttpGet("/stylists/{id}/clients/sort-desc")]
    public ActionResult SortDesc(int id)
    {
      Stylist sortCat = Stylist.Find(id);
      List<Client> sortedItems = new List<Client>{};
      sortedItems = sortCat.SortDesc();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", sortCat);
      model.Add("clients", sortedItems);
      return View("Detail", model);
    }
  }
}

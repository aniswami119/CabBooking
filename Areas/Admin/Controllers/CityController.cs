using Book_Cab_App.DataAccess.Repository;
using Book_Cab_App.DataAccess.Repository.IRepository;
using Book_Cab_App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Cab_App.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CityController : Controller
    {

    
     private readonly IUnitOfWork _unitOfWork;


    public CityController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {
        City city = new City();
        if (id == null)
            //this is for create a new city
            return View(city);
        city = _unitOfWork.City.Get(id.GetValueOrDefault());
        if (city == null)
            return NotFound();
        return View(city);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(City city)
    {
        if (ModelState.IsValid)
        {
            if (city.Id == 0)
                _unitOfWork.City.Add(city);
            else
                _unitOfWork.City.Update(city);
            _unitOfWork.Save();
        }

        return RedirectToAction("Index");
    }

    #region API Calls

    [HttpGet]
    public IActionResult GetAll()
    {
        var listOfCities = _unitOfWork.City.GetAll();
        return Json(new { data = listOfCities });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var cityFromDb = _unitOfWork.City.Get(id);
        if (cityFromDb == null)
            return Json(new { success = false, message = "Error while deleting the record" });
        _unitOfWork.City.Remove(cityFromDb);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Record Deleted Successfully" });
    }

    #endregion

}
}

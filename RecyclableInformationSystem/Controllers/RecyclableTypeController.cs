using RecyclableInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecyclableInformationSystem.Controllers
{
    public class RecyclableTypeController : Controller
    {
        // GET: RecyclableType
        private RecyclableDBEntities myEntity = new RecyclableDBEntities();
        [HttpGet]
        public ActionResult List()
        {
            var myRecyclableList = myEntity.RecyclableTypes.ToList();         
            return View(myRecyclableList);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddData(RecyclableType item)
        {
            try
            {
                var itemTemp = myEntity.RecyclableTypes.FirstOrDefault(c => c.Type == item.Type);
                if(itemTemp != null) return ShowModal("Add", "RecyclableType", "Type is already exist!");
                //
                myEntity.RecyclableTypes.Add(item);
                myEntity.SaveChanges();
                return ShowModal("List", "RecyclableType", "Recyclable Type Added Successfully!");
            }
            catch (Exception ex)
            {
                return ShowModal("List", "RecyclableType", $"Error: {ex.Message}");
            }
        }
        [HttpPost]
        public ActionResult Edit(int id)
        {
            var itemData = myEntity.RecyclableTypes.FirstOrDefault(c => c.Id == id);
            return View(itemData);
        }
        [HttpPost]
        public ActionResult EditData(RecyclableType item)
        {
            try
            {
                var itemToUpdate = myEntity.RecyclableTypes.FirstOrDefault(c => c.Id == item.Id);
                itemToUpdate.MaxKg = item.MaxKg;
                itemToUpdate.MinKg = item.MinKg;
                itemToUpdate.Rate = item.Rate;
                itemToUpdate.Type = item.Type;
                //
                myEntity.SaveChanges();
                //
                return ShowModal("List", "RecyclableType", "Data Updated Successfully!");
            }
            catch (Exception ex)
            {
                return ShowModal("List", "RecyclableType", $"Error:{ex.Message}");
            }
        }
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteData(int id)
        {
            try
            {
                var itemToRemove = myEntity.RecyclableTypes.FirstOrDefault(c => c.Id == id);
                //
                myEntity.RecyclableTypes.Remove(itemToRemove);
                myEntity.SaveChanges();
                return ShowModal("List", "RecyclableType", "Deleted Successfully!");
            }
            catch (Exception ex)
            {
                return ShowModal("List", "RecyclableType", $"Error:{ex.Message}");
            }
        }
        private ActionResult ShowModal(string actionName, string controllerName, string msg)
        {
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            ViewBag.Messages = msg;
            return View("~/Views/Includes/AlertModal.cshtml");
        }
    }
}
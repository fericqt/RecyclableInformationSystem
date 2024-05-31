using RecyclableInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecyclableInformationSystem.Controllers
{
    public class RecyclableItemController : Controller
    {
        // GET: RecyclableItem
        private RecyclableDBEntities myEntity = new RecyclableDBEntities();
        [HttpGet]
        public ActionResult List()
        {
            var myModel = myEntity.RecyclableItems.ToList();
            return View(myModel);
        }
        public ActionResult Add()
        {
            return View(GetRecyclableTypes());
        }
        [HttpPost]
        public ActionResult AddData(RecyclableItem item)
        {
            try
            {
                var itemTemp = myEntity.RecyclableItems.FirstOrDefault(c => c.Id == item.Id);
                if (itemTemp != null) return ShowModal("List", "RecyclableItem", "Id is already exist!");
                //
                myEntity.RecyclableItems.Add(item);
                myEntity.SaveChanges();
                return ShowModal("List", "RecyclableItem", "Data Added Successfully!");
            }
            catch (Exception ex)
            {
                return ShowModal("List", "RecyclableItem", $"Error:{ex.Message}");
            }         
        }
        [HttpPost]
        public ActionResult Edit(int id)
        {

            var itemData = myEntity.RecyclableItems.FirstOrDefault(c => c.Id == id);
            var itemList = GetRecyclableTypes();

            var myModel = new RecyclableItemModelNew
            {
                ListItem = itemList,
                RecyclableItem = itemData
            };

            return View(myModel);
        }
        [HttpPost]
        public ActionResult EditData(RecyclableItem item)
        {
            try
            {
                var itemToUpdate = myEntity.RecyclableItems.FirstOrDefault(c => c.Id == item.Id);
                itemToUpdate.ItemDescription = item.ItemDescription;
                itemToUpdate.RecyclableTypeId = item.RecyclableTypeId;
                itemToUpdate.ComputedRate = item.ComputedRate;
                itemToUpdate.Weight = item.Weight;
                //
                myEntity.SaveChanges();
                return ShowModal("List", "RecyclableItem", "Data Updated Successfully!");
            }
            catch (Exception ex)
            {
                return ShowModal("List", "RecyclableItem", $"Error:{ex.Message}");
            }
        }
        [HttpPost]
        public ActionResult DeleteData(int id)
        {
            try
            {
                var itemToRemove = myEntity.RecyclableItems.FirstOrDefault(c => c.Id == id);
                //
                myEntity.RecyclableItems.Remove(itemToRemove);
                myEntity.SaveChanges();
                return ShowModal("List", "RecyclableItem", "Deleted Successfully!");
            }
            catch (Exception ex)
            {
                return ShowModal("List", "RecyclableItem", $"Error:{ex.Message}");
            }
        }
        [HttpPost]
        public ActionResult CalculateRate(int recyclableTypeId)
        {
            var rate = myEntity.RecyclableTypes.FirstOrDefault(c => c.Id == recyclableTypeId).Rate;
            return Json(new { Rate = rate });
        }
        [HttpPost]
        public ActionResult GetMaxMinKg(int recyclableTypeId)
        {
            var minKg = myEntity.RecyclableTypes.FirstOrDefault(c => c.Id == recyclableTypeId).MinKg;
            var maxKg = myEntity.RecyclableTypes.FirstOrDefault(c => c.Id == recyclableTypeId).MaxKg;
            return Json(new { MaxKg = maxKg, MinKg = minKg });
        }
        private IEnumerable<SelectListItem> GetRecyclableTypes()
        {
            var item = myEntity.RecyclableTypes.ToList();
            return item.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Type
            });
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
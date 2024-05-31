using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecyclableInformationSystem.Models
{
    public class RecyclableItemModelNew
    {
        public RecyclableItem RecyclableItem { get; set; }
        public IEnumerable<SelectListItem> ListItem { get; set; }
    }
}
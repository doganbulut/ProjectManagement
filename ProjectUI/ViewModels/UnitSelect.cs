using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectUI.ViewModels
{
    public class UnitSelect
    {
        public int SelectedId { get; set; }
        public IEnumerable<SelectListItem> UnitList { get; set; }
    }
}
using inventoryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inventoryProject.ViewModels
{
    public class VmProduct:product
    {
       public Nullable<int> quantity { get; set; }
    }
}
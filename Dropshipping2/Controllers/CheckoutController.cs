using Microsoft.AspNetCore.Mvc;
using Database_Methods;
using Microsoft.VisualBasic;
//using Dropshipping2.ViewModel;

namespace Dropshipping2.Controllers
{
   
    public class CheckoutController : Controller
    {
        Add_To_Database database_ = new Add_To_Database();
        public IActionResult  AddressDetails()
        {
            return View();
        }   
        
        //cart data is saved to database          
        public IActionResult SaveCart()
        {                
               DateTime orderDate = DateTime.Now;      
               database_.orderDBSave(orderDate);            
               return View();
        }

    }
}

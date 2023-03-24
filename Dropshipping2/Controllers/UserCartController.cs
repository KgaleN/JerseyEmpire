using Database_Methods;
using Database_Methods.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Dropshipping2.Controllers
{
    public class UserCartController : Controller
    {
        //DropshippingDBContext database= new DropshippingDBContext();
        Add_To_Database DbMeth = new Add_To_Database();
        Add_To_Cart CartObj= new Add_To_Cart();
        bool isUserLoggedIn = false;
        static decimal TotalCost;

        public IActionResult LoginUser_saveToCart()
        {
            DisplayClothingController.ErrorEmail = null;
            DisplayClothingController.WrongPassword = null;
            ViewBag.TotalCost = Decimal.Round(CartObj.TotalCartCost(),2);
            var cartItems = CartObj.GetCart();
            return View(cartItems);           
        }

        public ActionResult Delete(string i)
        {
            CartObj.RemoveItem(i);
            return RedirectToAction("LoginUser_saveToCart", "UserCart");
        }

        public IActionResult ReturnToShopping()
        {            
            return RedirectToAction("ClothesDisplayed", "DisplayClothing");
        }

        public IActionResult InuputAddress()
        {
            return RedirectToAction("AddressDetails", "Checkout");
        }
    }
}

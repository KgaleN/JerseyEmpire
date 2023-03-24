
//using Dropshipping2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Cryptography;
using System.Security.Policy;
using Database_Methods;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
//using Dropshipping2.ViewModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace Dropshipping2.Controllers
{

    public class DisplayClothingController : Controller
    {    
        Add_To_Database DbMeth = new Add_To_Database();
       public static string SearchWord;
       public static bool isUserLoggedIn = false;
       public static int ClothingId;
       public static string WrongPassword;
       public static string ErrorEmail;
       public static int ErrorSizes;
       public static int ErrorQuantity;
       Add_To_Cart CartObj = new Add_To_Cart();

        public IActionResult ClothesDisplayed()
        {
            ViewBag.Currency ="R";
            //if search filter not used display all clothing items
            if (SearchWord == null)
            {
                var fullDisplay = DbMeth.fullClothingList();
                return View(fullDisplay);
            }

           //if search filter used display filtered clothing items
            else
            {
                var filteredDisplay = DbMeth.filterSearchBar(SearchWord);
                SearchWord = null;
                return View(filteredDisplay);
            }
           
        }

        public IActionResult ClothesSelected(int clothingId)
        {
             ClothingId = clothingId;              
            return RedirectToAction("ClothSelected", "DisplayClothing");
        }
       

        public IActionResult ClothSelected()
        {
            ViewBag.WrongPassword = WrongPassword;
            ViewBag.ErrorEmail = ErrorEmail;
            ViewBag.ErrorSizes = ErrorSizes;
            ViewBag.ErrorQuantity = ErrorQuantity;      
            ViewBag.Currency = "R";

            DbMeth.clothingSize(ClothingId);
            return View(DbMeth.SelectedItem(ClothingId));       
        }

        public IActionResult SearchFilter(string searchWord)
        {         
                SearchWord = searchWord;
                return RedirectToAction("ClothesDisplayed", "DisplayClothing");             
        }


       

        public IActionResult LogUserInfo(int clothingId, int clothingSize, int clothingQuantity, string firstName, string surname, string email, string phone, string password, string newEmail, string newPassword,string SignupTell,string LoginTell,string PastUrl)
        {
            
            if (DbMeth.userFound(password, email)> 0 &&string.IsNullOrEmpty(firstName)&&string.IsNullOrEmpty(surname) || isUserLoggedIn)
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionVariables.SessionID)))
                {
                    HttpContext.Session.SetString(SessionVariables.SessionID, Guid.NewGuid().ToString());
                    isUserLoggedIn = true;
                }
                //sesion not null
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionVariables.SessionID)))
                {
                    CartObj.SetCart(clothingId, clothingSize, clothingQuantity, email, password, clothingSize);
                }
                return RedirectToAction("LoginUser_saveToCart", "UserCart");
            }
            else
            {    
                if(LoginTell!= null) 
                { 
                    ErrorSizes = clothingSize;
                    ErrorQuantity=clothingQuantity;
                    ErrorEmail = null;
                    WrongPassword = "The password or email which you entered is incorrect."; 
                }
            }

            if (!(DbMeth.userEmailFound(newEmail) > 0) && !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(surname))
            {
                DbMeth.CustomerDBSave(firstName, surname, newEmail, phone, newPassword);
                if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionVariables.SessionID)))
                {
                    HttpContext.Session.SetString(SessionVariables.SessionID, Guid.NewGuid().ToString());
                    isUserLoggedIn = true;
                }

                if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionVariables.SessionID)))
                {                                    
                    CartObj.SetCart(clothingId, clothingSize, clothingQuantity, newEmail, newPassword, clothingSize);               
                }

                return RedirectToAction("LoginUser_saveToCart", "UserCart");
            }

            else 
            {
                if (SignupTell != null) 
                {
                    ErrorSizes = clothingSize;
                    ErrorQuantity = clothingQuantity;
                    ErrorEmail = "The email which you entered already exists.";
                    WrongPassword=null;
                }                   
            }
            return RedirectToAction("ClothSelected", "DisplayClothing");
        }
       

    }
}   
      
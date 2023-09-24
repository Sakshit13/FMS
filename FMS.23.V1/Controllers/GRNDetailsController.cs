using COMMON;
using FMS._23.V1.BAL;
using FMS._23.V1.Models;
using FMS._23.V1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using System.Xml.Linq;

namespace FMS._23.V1.Controllers
{
    public class GRNDetailsController : Controller
    {
        private readonly GRNBAL _grnbal;

        public GRNDetailsController(GRNBAL grnbal)
        {                         
            this._grnbal = grnbal;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.GRNNO = _grnbal.GetGRNNO();


            ViewBag.SupplierList = _grnbal.GetVendorList();

            ViewBag.PurchaseOrderList = new List<SelectListItem>
                        {
                           
                        };
      
            return View();
        }

        public IActionResult GetPurchaseOrders(string supplierId, string Type)
        {
            List<object> purchaseOrderList = new List<object>();

            if (Type == "PO")
            {
                foreach (var item in _grnbal.GetAllPendingPOList(supplierId))
                {
                    purchaseOrderList.Add(new
                    {
                        Value = item.Value, 
                        Text = item.Text   
                    });
                }
            }
            else if (Type == "PENDING")
            {
                foreach (var item in _grnbal.GetAllPendingPOList(supplierId))
                {
                    purchaseOrderList.Add(new
                    {
                        Value = item.Value,
                        Text = item.Text  
                    });
                }
            }
            return Json(purchaseOrderList);
        }

        public JsonResult GetPurchaseOrdersDetails(string purchaseOrderNo)
        {
            List<GRNFieldModel> potransactionDetails = new List<GRNFieldModel>();
            try
            {
                potransactionDetails = _grnbal.GetPurchaseOrdersDetails(purchaseOrderNo);
            }
            catch (Exception)
            {

                throw;
            }
            return Json(potransactionDetails);
        }


        [HttpPost]
        public ActionResult SaveData(string GRNData)
        {
            string grnSave = string.Empty;
            try
            {
                // Parse the JSON string
                JObject jsonData = JObject.Parse(GRNData);

                // Extract values into variables
                string modifidata = jsonData["ModifiedData"].ToString();
                string GRNNO = (string)jsonData["GRNNO"];
                string ChallanNo = (string)jsonData["ChallanNo"];
                string BillNO = (string)jsonData["BillNO"];
                DateTime DateOfRecipt = (DateTime)jsonData["DateOfRecipt"];
                DateTime DateOfBill = (DateTime)jsonData["DateOfBill"];
                string Supplier = (string)jsonData["Supplier"];
                string PurchaseOrderNo = (string)jsonData["PurchaseOrderNo"];
                grnSave = _grnbal.SaveData(GRNData,GRNNO,ChallanNo,BillNO,DateOfRecipt,DateOfBill,Supplier,PurchaseOrderNo);

            }
            catch (Exception ex)
            {
                
            }

            return Json(grnSave);
        }

    }

}

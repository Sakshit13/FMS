using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS._23.V1.ViewModels
{
	public class MasterViewModel
	{
		public List<SelectListItem> ItemList { get; set; }	
		public List<SelectListItem> VendorList { get; set; }	
		public string VendorSelected { get; set; }	
	}
}

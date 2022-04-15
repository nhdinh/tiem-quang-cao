using System;

namespace TiemQuangCao.Entities
{
	public class PaymentVoucher
	{
		public int PV_Id { get; set; }
		public string PV_Code { get; set; }

		public DateTime PV_Date { get; set; }
		public DateTime PV_DateSave { get; set; }
		public bool PV_isCustomer { get; set; }
		public int PV_CustomerId { get; set; }
		public string PV_Receiver { get; set; }
		public string PV_Address { get; set; }
		public float PV_Money { get; set; }
		public string PV_PaymentMethod { get; set; }
		public string PV_Reason { get; set; }
		public int? PV_CodeProduct { get; set; }

		public int? PV_ConstructionId { get; set; }
	}
}

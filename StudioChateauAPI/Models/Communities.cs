using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public class Communities
    {

        public Communities()
        {
            Location_Id = -1;
            Community_Image = "default.jpg";
            Community_Desc = "";
            Community_Code = "";
            Community_Tax = 0;
            Community_Notifications = "";
            Community_Email = "";
            Community_Change_Order_Notify = "";
            Community_Cutoff_Admin_Email = "";
            Community_Buyers_See_Placements = "Y";
            Floor_Sales_Tax = 0.0;
            Floor_Labor_Tax = 0.0;
            Vendor_Sales_Tax = 0.0;
            Design_Center_Phone = "";
            Phase = "";
            Vendor = "";
            Lot = "";
            Designers_Assign_Custom_Category = true;
            Designers_Access_Custom_Requests = true;
            Phase_Hold_Orders = "Y";
            Active_Flag = true;
            Disable_Dcs_Flag = true;
            Builder_Title = "";
            Oo_Title = "";
            Customer_Service_Email = "";
            Dev_Admin_Email = "";
            Msrepl_Tran_Version = new Guid();
        }

        [Key]
        public int Community_id
        {
            get;
            set;
        }

        [Required]
        public int Builder_Id
        {
            get;
            set;
        }


        [StringLength(50, ErrorMessage = "Community Name cannot be more than 50 characters")]
        [Required]
        public string Community_Name
        {
            get;
            set;
        }

        [StringLength(255, ErrorMessage = "Community image URL length cannot be more than 255 characters")]
        public string Community_Image
        {
            get;
            set;
        }

        public string Community_Desc
        {
            get;
            set;
        }

        public string Community_Disclaimer1
        {
            get;
            set;
        }

        public string Community_Disclaimer2
        {
            get;
            set;
        }

        [StringLength(16, ErrorMessage = "Community code cannot be more than 16 characters")]
        public string Community_Code
        {
            get;
            set;
        }

        public Nullable<double> Community_Tax
        {
            get;
            set;
        }

        [StringLength(255, ErrorMessage = "Community notifications cannot be more than 255 characters")]
        public string Community_Notifications
        {
            get;
            set;
        }

        public string Community_Price_Request_Disclaimer
        {
            get;
            set;
        }

        public string Community_Vendor_Notice_Disclaimer
        {
            get;
            set;
        }

        [StringLength(64, ErrorMessage = "Community email address cannot be more than 64 characters")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Community Email")]
        public string Community_Email
        {
            get;
            set;
        }

        [StringLength(255, ErrorMessage = "Community change order message cannot be more than 255 characters")]
        public string Community_Change_Order_Notify
        {
            get;
            set;
        }

        public string Community_Disclaimer3
        {
            get;
            set;
        }

        [StringLength(255, ErrorMessage = "Community cutoff admin email cannot be more than 255 characters")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Community Cutoff Email")]
        public string Community_Cutoff_Admin_Email
        {
            get;
            set;
        }

        [StringLength(1, ErrorMessage = "Community buyeers see placements cannot be more than 1 characters")]
        public string Community_Buyers_See_Placements
        {
            get;
            set;
        }

        public Nullable<double> Floor_Sales_Tax
        {
            get;
            set;
        }

        public Nullable<double> Floor_Labor_Tax
        {
            get;
            set;
        }

        public Nullable<double> Vendor_Sales_Tax
        {
            get;
            set;
        }

        public string Community_Email_Disclaimer
        {
            get;
            set;
        }
        [StringLength(256, ErrorMessage = "Design center phone cannot be more than 256 characters")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Design Center Phone Number")]
        public string Design_Center_Phone
        {
            get;
            set;
        }
        [StringLength(50, ErrorMessage = "Phase cannot be more than 50 characters")]
        public string Phase
        {
            get;
            set;
        }
        [StringLength(50, ErrorMessage = "Vendor cannot be more than 50 characters")]
        public string Vendor
        {
            get;
            set;
        }
        [StringLength(50, ErrorMessage = "Lot cannot be more than 50 characters")]
        public string Lot
        {
            get;
            set;
        }
        [StringLength(50, ErrorMessage = "Community external GUID cannot be more than 50 characters")]
        public string Community_External_GUID
        {
            get;
            set;
        }

       
        public Guid Msrepl_Tran_Version
        {
            get;
            set;
        }

        public Nullable<bool> Designers_Assign_Custom_Category
        {
            get;
            set;
        }
        [StringLength(500, ErrorMessage = "Builder info cannot be more than 500 characters")]
        public string Builder_Info
        {
            get;
            set;
        }

        public Nullable<bool> Designers_Access_Custom_Requests
        {
            get;
            set;
        }
        [StringLength(500, ErrorMessage = "Vendor infor cannot be more than 500 characters")]
        public string Vendor_Info
        {
            get;
            set;
        }
        [StringLength(1, ErrorMessage = "Phase hold orders cannot be more than 1 character")]
        public string Phase_Hold_Orders
        {
            get;
            set;
        }
        [StringLength(5000, ErrorMessage = "Finalization form info cannot be more than 5000 characters")]
        public string Finalization_Form_Info
        {
            get;
            set;
        }
        [StringLength(5000, ErrorMessage = "Finalization form footer cannot be more than 5000 characters")]
        public string Finalization_Form_Footer
        {
            get;
            set;
        }

        public string Option_Order_Disclaimer
        {
            get;
            set;
        }

        public Nullable<bool> Active_Flag
        {
            get;
            set;
        }

        public Nullable<int> Location_Id 
        {
            get;
            set;
        }

        public Nullable<bool> Disable_Dcs_Flag
        {
            get;
            set;
        }
        [StringLength(150, ErrorMessage = "Builder title cannot be more than 150 characters")]
        public string Builder_Title
        {
            get;
            set;
        }

        [StringLength(150, ErrorMessage = "OO title cannot be more than 150 characters")]
        public string Oo_Title
        {
            get;
            set;
        }
        [StringLength(64, ErrorMessage = "Customer service email cannot be more than 64 characters")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Customer Service Email")]
        public string Customer_Service_Email
        {
            get;
            set;
        }

        public string Vendor_Review_Disclaimer
        {
            get;
            set;
        }
        [StringLength(64, ErrorMessage = "Dev admin email cannot be more than 64 characters")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Dev Admin Email")]
        public string Dev_Admin_Email
        {
            get;
            set;
        }

        [JsonIgnore]
        public Communities[] communities { get; set; }

    }
}
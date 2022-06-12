using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StudioChateauProcess.Models
{
    [DataContract(Name="Lot", Namespace="")]
    class Lot
    {

        [DataMember(Name = "Community_Name", Order = 1)]
        public string Community_Name { get; set; }

        [DataMember(Name = "Community_External_GUID", Order = 2)]
        public string Community_External_GUID { get; set; }

        [DataMember(Name = "Plan_Name", Order = 3)]
        public string Plan_Name { get; set; }

        [DataMember(Name = "Plan_Seq", Order = 4)]
        public int Plan_Seq { get; set; }

        [DataMember(Name = "Plan_Sqft", Order = 5)]
        public int Plan_Sqft { get; set; }

        [DataMember(Name = "Phase_No", Order = 6)]
        public string Phase_No { get; set;}

        [DataMember(Name = "Phase_Hold_Orders", Order = 7)]
        public string Phase_Hold_Orders { get; set; }

        [DataMember(Name = "Phase_Retail_Price_Override", Order = 8)]
        public string Phase_Retail_Price_Override { get; set; }

        [DataMember(Name = "Lot_No", Order = 9)]
        public string Lot_No { get; set; }

        [DataMember(Name = "Lot_Tract", Order = 10)]
        public string Lot_Tract { get; set; }

        [DataMember(Name = "Lot_Address1", Order = 11)]
        public string Lot_Address1 { get; set; }

        [DataMember(Name = "Lot_Address2", Order = 12)]
        public string Lot_Address2 { get; set; }

        [DataMember(Name = "Lot_City", Order = 13)]
        public string Lot_City { get; set; }

        [DataMember(Name = "Lot_State", Order = 14)]
        public string Lot_State { get; set; }

        [DataMember(Name = "Lot_Zip_Code", Order = 15)]
        public string Lot_Zip_Code { get; set; }

        [DataMember(Name = "Lot_Home_Buyer", Order = 16)]
        public string Lot_Home_Buyer { get; set; }

        [DataMember(Name = "Lot_Current_Phone", Order = 17)]
        public string Lot_Current_Phone { get; set; }

        [DataMember(Name = "Lot_Email", Order = 18)]
        public string Lot_Email { get; set; }

        [DataMember(Name = "Lot_Work_Phone", Order = 19)]
        public string Lot_Work_Phone { get; set; }

        [DataMember(Name = "Lot_Cell_Phone", Order = 20)]
        public string Lot_Cell_Phone { get; set; }

        [DataMember(Name = "Lot_Elevation", Order = 21)]
        public string Lot_Elevation { get; set; }

        [DataMember(Name = "Lot_Sequence", Order = 22)]
        public string Lot_Sequence { get; set; }
        /* 
         
                <Builder_Id></Builder_Id>
                <Community_Name></Community_Name>
                <Community_External_GUID></Community_External_GUID>
                <Plan_Name></Plan_Name>
                <Plan_Seq></Plan_Seq>
                <Plan_Sqft></Plan_Sqft>
                <Phase_No></Phase_No>
                <Phase_Hold_Orders></Phase_Hold_Orders>
                <Lot_No></Lot_No>
                <Lot_Tract></Lot_Tract>
                <Lot_Address1></Lot_Address1>
                <Lot_Address2></Lot_Address2>
                <Lot_City></Lot_City>
                <Lot_State></Lot_State>
                <Lot_Zip_Code></Lot_Zip_Code>
                <Lot_Home_Buyer></Lot_Home_Buyer>
                <Lot_Current_Phone></Lot_Current_Phone>
                <Lot_Email></Lot_Email>
                <Lot_Work_Phone></Lot_Work_Phone>
                <Lot_Cell_Phone></Lot_Cell_Phone>
                <Lot_Elevation></Lot_Elevation>
                <Lot_Sequence></Lot_Sequence>
         
         
         */
    }
}

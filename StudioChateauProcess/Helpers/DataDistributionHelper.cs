using StudioChateauAPI.Models;
using StudioChateauProcess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudioChateauProcess.Helpers
{
    class DataDistributionHelper
    {
        private StudioChateauProcess.Models.Lot lotInputData;
        private CommunityProxy communityProxy;
        private PlanProxy planProxy;
        private PhaseProxy phaseProxy;
        private LotProxy lotProxy;
        private string jsonCommunity;
        private string jsonPlan;
        private string jsonPhase;
        private string jsonLot;

        public DataDistributionHelper(StudioChateauProcess.Models.Lot lotInputData, int builderId)
        {
            this.lotInputData = lotInputData;

            communityProxy = new CommunityProxy();
            communityProxy.Builder_Id = builderId;
            communityProxy.Community_External_GUID = lotInputData.Community_External_GUID;
            communityProxy.Community_Name = lotInputData.Community_Name;

            planProxy = new PlanProxy();
            planProxy.plan_name = lotInputData.Plan_Name;
            planProxy.plan_seq = lotInputData.Plan_Seq;
            planProxy.plan_sqft = lotInputData.Plan_Sqft;
            

            phaseProxy = new PhaseProxy();
            phaseProxy.phase_no = lotInputData.Phase_No;
            phaseProxy.phase_hold_orders = lotInputData.Phase_Hold_Orders;
            phaseProxy.phase_retail_price_override = lotInputData.Phase_Retail_Price_Override;

            lotProxy = new LotProxy();
            lotProxy.lot_no = lotInputData.Lot_No;
            lotProxy.lot_tract = lotInputData.Lot_Tract;
            lotProxy.lot_address1 = lotInputData.Lot_Address1;
            lotProxy.lot_address2 = lotInputData.Lot_Address2;
            lotProxy.lot_city = lotInputData.Lot_City;
            lotProxy.lot_state = lotInputData.Lot_State;
            lotProxy.lot_zip_code = lotInputData.Lot_Zip_Code;
            lotProxy.lot_home_buyer = lotInputData.Lot_Home_Buyer;
            lotProxy.lot_current_phone = lotInputData.Lot_Current_Phone;
            lotProxy.lot_email = lotInputData.Lot_Email;
            lotProxy.lot_work_phone = lotInputData.Lot_Work_Phone;
            lotProxy.lot_cell_phone = lotInputData.Lot_Cell_Phone;
            lotProxy.lot_elevation = lotInputData.Lot_Elevation;
            lotProxy.lot_sequence = Int32.Parse(String.IsNullOrEmpty(
                                                    lotInputData.Lot_Sequence)
                                                 ?"0":lotInputData.Lot_Sequence);

            jsonCommunity = GenericJSON<CommunityProxy>.GetJSON(communityProxy);

        }

        public List<StudioChateauAPIErrorLog> MakeAPICalls(int externalServiceID, int xmlDumpId, int xmlSequence)
        {

            List<StudioChateauAPIErrorLog> errors = new List<StudioChateauAPIErrorLog>();

            string response = "";
            string communitiesPath = System.Configuration.ConfigurationManager.AppSettings["communitiesPath"];
            string plansPath = System.Configuration.ConfigurationManager.AppSettings["plansPath"];
            string phasesPath = System.Configuration.ConfigurationManager.AppSettings["phasesPath"];
            string lotsPath = System.Configuration.ConfigurationManager.AppSettings["lotsPath"];

            int communityID = 0;
            int phaseID = 0;
            int planID = 0;

            try
            {
                
                response = GenericRESTCall<Communities>.GenericPostCall(communitiesPath, jsonCommunity);
                communityID = GenericRESTCall<Communities>.GetObjectID(response,"Community_id");
            }
            catch (WebException we)
            {
                errors.Add(CheckAndLogError(externalServiceID, xmlDumpId, xmlSequence, we.Message + we.InnerException));
            }

            if (communityID != 0)
            {
                try
                {
                    phaseProxy.community_id = communityID;
                    jsonPhase = GenericJSON<PhaseProxy>.GetJSON(phaseProxy);

                    response = GenericRESTCall<Phase>.GenericPostCall(phasesPath, jsonPhase);
                    phaseID = GenericRESTCall<Phase>.GetObjectID(response, "phase_id");
                }
                catch (WebException we)
                {
                    errors.Add(CheckAndLogError(externalServiceID, xmlDumpId, xmlSequence, we.Message + we.InnerException));
                }

                try
                {
                    planProxy.community_id = communityID;
                    jsonPlan = GenericJSON<PlanProxy>.GetJSON(planProxy);

                    response = GenericRESTCall<Plan>.GenericPostCall(plansPath, jsonPlan);
                    planID = GenericRESTCall<Plan>.GetObjectID(response, "plan_id");
                }
                catch (WebException we)
                {
                    errors.Add(CheckAndLogError(externalServiceID, xmlDumpId, xmlSequence, we.Message + we.InnerException));
                }

                try
                {
                    lotProxy.phase_id = phaseID;
                    lotProxy.plan_id = planID;
                    jsonLot = GenericJSON<LotProxy>.GetJSON(lotProxy);
                    response = GenericRESTCall<StudioChateauProcess.Models.Lot>.GenericPostCall(lotsPath, jsonLot);
                }
                catch (WebException we)
                {
                    errors.Add(CheckAndLogError(externalServiceID, xmlDumpId, xmlSequence, we.Message + we.InnerException));
                }
            }

            return errors;
        }

        private static StudioChateauAPIErrorLog CheckAndLogError(int externalServiceID, 
                int xmlDumpId, int xmlSequence, string response)
        {
            StudioChateauAPIErrorLog logResponse = null;

            if (response.Contains(HttpStatusCode.BadRequest.ToString()) ||
                response.Contains(HttpStatusCode.NotFound.ToString()) ||
                response.Contains(HttpStatusCode.Unauthorized.ToString()))
            {
                StudioChateauAPIErrorLog logEntry = new StudioChateauAPIErrorLog();
                logEntry.Error_Message = response.ToString();
                logEntry.External_Service_Id = externalServiceID;
                logEntry.XML_Dump_Id = xmlDumpId;
                logEntry.XML_Element_Sequence = xmlSequence;
                logEntry.CreatedBy = "Process";

                string logJson = GenericJSON<StudioChateauAPIErrorLog>.GetJSON(logEntry);

                string postResponse = GenericRESTCall<StudioChateauAPIErrorLog>.GenericPostCall("http://localhost:9090/ProjectConfig/StudioChateauAPIErrorLogs", logJson);

                logResponse = GenericRESTCall<StudioChateauAPIErrorLog>.GetObject(postResponse);
            }
    
            return logResponse;
        }

    }

    class GenericJSON<T>
    {
        public static string GetJSON(T studioChateauModelType)
        {
            string jsonString = null;

            MemoryStream jsonMemory = new MemoryStream();
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T), settings);
  
            ser.WriteObject(jsonMemory, studioChateauModelType);
          
            jsonMemory.Position = 0;
            StreamReader sr = new StreamReader(jsonMemory);

            jsonString = sr.ReadToEnd();


            return jsonString;
        }
    }
}

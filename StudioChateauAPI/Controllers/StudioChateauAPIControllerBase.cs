using StudioChateauAPI.Exceptions;
using StudioChateauAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.OData;

namespace StudioChateauAPI.Controllers
{
    public class StudioChateauAPIControllerBase : ODataController
    {
        private ProjectConfigDbContext db = new ProjectConfigDbContext();

        protected void GetBuilderAccess(Communities communities, out BuilderAccessList builderAccess, out IEnumerable<AuthenticationHeaderValue> challenges)
        {
            
            builderAccess =
                            db.builderAccess.Where(b => b.UserName == User.Identity.Name
                            && b.BuilderId == communities.Builder_Id)
                            .FirstOrDefault();

            challenges = new AuthenticationHeaderValue[]{
                new AuthenticationHeaderValue("Bearer")
            };
        }

        protected bool IsLotPayloadValid(Lot lot, Lot inboundLot)
        {
            bool result = false;
            Lot lotToValidate = lot;

            if (inboundLot != null)
            {
                 lotToValidate = inboundLot;
            }

            int community_id = ValidatePlanAndPhase(lotToValidate);

            if (community_id < 0)
            {
                return false;
            }

            Communities communities = db.Communities.Find(community_id);

            BuilderAccessList builderAccess;
            IEnumerable<AuthenticationHeaderValue> challenges;
            GetBuilderAccess(communities, out builderAccess, out challenges);

            if (builderAccess == null)
                result = false; 

            return result;
        }

        private int ValidatePlanAndPhase(Lot lot)
        {
            int result = -1;

            Phase phaseExisting = db.phases.Find(lot.phase_id);

            if (phaseExisting == null)
            {
                throw new PhaseNotFoundException(lot.phase_id);
            }

            Plan planExisting = db.plans.Find(lot.plan_id);

            if (planExisting == null)
            {
                throw new PlanNotFoundException(lot.plan_id);
            }

            Lot lotExisting = new Lot();

            lotExisting = db.lots.Where(c => c.lot_id == lot.lot_id & c.phase_id == lot.phase_id).FirstOrDefault();

            if (phaseExisting.community_id == planExisting.community_id)
            {
                result = phaseExisting.community_id;
            }

            return result;
        }

        protected Communities PopulateDefaults(Communities community)
        {
            Builders builder = db.builders.Where(b => b.builder_id == community.Builder_Id).FirstOrDefault<Builders>();

            if (string.IsNullOrEmpty(community.Builder_Info))
                community.Builder_Info = builder.builder_info;
            if (string.IsNullOrEmpty(community.Vendor_Info))
            {
                community.Vendor_Info = builder.builder_info;
            }
            if (string.IsNullOrEmpty(community.Community_Disclaimer1))
            {
                community.Community_Disclaimer1 = builder.community_disclaimer1;
            }
            if (string.IsNullOrEmpty(community.Community_Disclaimer2))
            {
                community.Community_Disclaimer2 = builder.community_disclaimer2;
            }
            if (string.IsNullOrEmpty(community.Community_Disclaimer3))
            {
                community.Community_Disclaimer3 = builder.community_disclaimer3;
            }
            if (string.IsNullOrEmpty(community.Community_Price_Request_Disclaimer))
            {
                community.Community_Price_Request_Disclaimer = builder.community_price_request_disclaimer;
            }
            if (string.IsNullOrEmpty(community.Community_Email_Disclaimer))
            {
                community.Community_Email_Disclaimer = builder.community_email_disclaimer;
            }
            if (string.IsNullOrEmpty(community.Vendor_Info))
            {
                community.Vendor_Info = builder.builder_info;
            }
            if (string.IsNullOrEmpty(community.Option_Order_Disclaimer))
            {
                community.Option_Order_Disclaimer = builder.option_order_disclaimer;
            }
            if (string.IsNullOrEmpty(community.Finalization_Form_Footer))
            {
                community.Finalization_Form_Footer = builder.finalization_form_footer;
            }
            if (string.IsNullOrEmpty(community.Finalization_Form_Info))
            {
                community.Finalization_Form_Info = builder.finalization_form_info;
            }
            if (string.IsNullOrEmpty(community.Vendor_Review_Disclaimer))
            {
                community.Vendor_Review_Disclaimer = builder.vendor_review_disclaimer;
            }

            return community;
        }

    }
}

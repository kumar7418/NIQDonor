using DonorTracking.Data;
using System.Net.Mail;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
namespace DonorTracking.Data
{
    public class DonorUpdateModel
    {

}
    public class Application
    {
       // [Required]
        public bool Approved {  get; set; }
       // [Required]
        public string FirstName{  get; set; }

        public string LastName {  get; set; }
       // [Required]
        public string DateOfBirth{  get; set; }
       // [Required]
        public string EmailAddress{  get; set; }
       // [Required]
        public string PhoneNumber{  get; set; } 
        public string ShippingAddress1{  get; set; } 
        public string ShippingAddress2{  get; set; } 
        public string ShippingCity{  get; set; } 
        public string ShippingState{  get; set; } 
        public string ShippingCountry{  get; set; } 
        public string ShippingZipCode{  get; set; } 
        public string MailingAddress1{  get; set; } 
        public string MailingAddress2{  get; set; } 
        public string MailingCity{  get; set; } 
        public string MailingState{  get; set; } 
        public string MailingCountry{  get; set; } 
        public string MailingZipCode{  get; set; } 
    }

    public class QueryResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string DonorID { get; set; }

    }

    public class BloodWork
    {
        [Required]
        public string DonorID { get; set; }
        [Required]
        public int BloodWorkStatus { get; set; }
        [Required]
        public string PostedDate { get; set; }

    }

    public class MilkKits
    {

        //[Required(ErrorMessage = "DonorID is required.")]
        public string DonorID { get; set; }
        //[Required]
        //[EnumDataType(typeof(MilkKitStatus))]
        public int MilkKitStatus { get; set; }
       // [Required]
        public int NumberOfKits { get; set; }

    }

    public class MilkKitsupdate
    {
        public string DonorID { get; set; }
        public string MilkKitID { get; set; }
        public int MilkKitStatus { get; set; }
        public string ReceivedDate { get; set; }
        public string FinalizedDate { get; set; }
        public string PaidDate { get; set; }
        public int VolumeMl { get; set; }


    }

    public class Reponse
    {

        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
    public enum BloodWorkStatus
    {
        InProgress = 0,
        Passed = 1,
        Failed = 2,
        Expired = 3   
    }

    public enum MilkKitStatus 
    {
         Requested=0,
         ShippedToDonor=1,
         ReceivedFromDonor=2,
         MilkTestingStarted=3,
         Passed=4,
         Failed=5,
         Paid=6
    }
}





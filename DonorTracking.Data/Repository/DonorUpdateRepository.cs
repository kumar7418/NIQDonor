using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using Dapper;
using Dapper.Contrib.Extensions;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;

namespace DonorTracking.Data
{
    public class DonorUpdateRepository : IDonorUpdateRepository
    {
        private readonly string _dbconn;

        public DonorUpdateRepository(IRepositoryConfigurationProvider config)
        {
            _dbconn = config.ConnectionString;
        }

        // string _dbconn = "Data Source=RAJAN\\MSSQLSERVER01; database=NiQ_DonorTracking;Integrated Security=True";// ConfigurationManager.ConnectionStrings["NiQ_DonorTracking"].ToString(); ;


        public string CreateApplication(Application donor)
        {
            string DonorID = string.Empty;
            try
            {
                // Create a connection to the database
                using (SqlConnection connection = new SqlConnection(_dbconn))
                {
                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("usp_SaveDonor", connection))
                    {
                        // Specify that the command is a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameters to the command
                        command.Parameters.Add(new SqlParameter("@Approved", SqlDbType.Bit) { Value = donor.Approved });
                        command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = donor.FirstName });
                        command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar) { Value = donor.LastName });
                        command.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = donor.DateOfBirth });
                        command.Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.VarChar) { Value = donor.EmailAddress });
                        command.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar) { Value = donor.PhoneNumber });
                        command.Parameters.Add(new SqlParameter("@ShippingAddress1", SqlDbType.VarChar) { Value = donor.ShippingAddress1 });
                        command.Parameters.Add(new SqlParameter("@ShippingAddress2", SqlDbType.VarChar) { Value = donor.ShippingAddress2 });
                        command.Parameters.Add(new SqlParameter("@ShippingCity", SqlDbType.VarChar) { Value = donor.ShippingCity });
                        command.Parameters.Add(new SqlParameter("@ShippingState", SqlDbType.VarChar) { Value = donor.ShippingState });
                        command.Parameters.Add(new SqlParameter("@ShippingCountry", SqlDbType.VarChar) { Value = donor.ShippingCountry });
                        command.Parameters.Add(new SqlParameter("@ShippingZipCode", SqlDbType.VarChar) { Value = donor.ShippingZipCode });
                        command.Parameters.Add(new SqlParameter("@MailingAddress1", SqlDbType.VarChar) { Value = donor.MailingAddress1 });
                        command.Parameters.Add(new SqlParameter("@MailingAddress2", SqlDbType.VarChar) { Value = donor.MailingAddress2 });
                        command.Parameters.Add(new SqlParameter("@MailingCity", SqlDbType.VarChar) { Value = donor.MailingCity });
                        command.Parameters.Add(new SqlParameter("@MailingState", SqlDbType.VarChar) { Value = donor.MailingState });
                        command.Parameters.Add(new SqlParameter("@MailingCountry", SqlDbType.VarChar) { Value = donor.MailingCountry });
                        command.Parameters.Add(new SqlParameter("@MailingZipCode", SqlDbType.VarChar) { Value = donor.MailingZipCode });
                        // Open the connection
                        connection.Open();
                        QueryResponse QueryResponse = new QueryResponse();
                        // Execute the command
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                DonorID = reader.GetString(reader.GetOrdinal("DonorID"));

                               // QueryResponse = new QueryResponse
                               // {
                               //     Status = reader.GetInt32(reader.GetOrdinal("Status")),
                               //     Message = reader.GetString(reader.GetOrdinal("Message")),
                               //     DonorID = reader.GetString(reader.GetOrdinal("DonorID"))
                               // };
                               //// employees.Add(employee);
                            }
                        }


                    }
                }

            }
            catch (System.Exception )
            {

                throw;
            }

            return DonorID;

        }


        public List<string> CreateMilkKits(MilkKits _MilkKits)
        {
            List<string> _nokits = new  List<string>();
            string NoofMilkKits = "";
            try
            {
                // Create a connection to the database
                using (SqlConnection connection = new SqlConnection(_dbconn))
                {
                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("usp_SaveMilkkits", connection))
                    {
                        // Specify that the command is a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameters to the command
                        command.Parameters.Add(new SqlParameter("@DonorID", SqlDbType.VarChar) { Value = _MilkKits.DonorID });
                        command.Parameters.Add(new SqlParameter("@MilkKitStatus", SqlDbType.VarChar) { Value = _MilkKits.MilkKitStatus });
                        command.Parameters.Add(new SqlParameter("@NumberOfKits", SqlDbType.Int) { Value = _MilkKits.NumberOfKits });
                        // Open the connection
                        connection.Open();
                        QueryResponse QueryResponse = new QueryResponse();
                        // Execute the command
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                NoofMilkKits=reader.GetString(reader.GetOrdinal("NoofMilkKits"));

                                string[] ary_milkkits = NoofMilkKits.Split(',');
                                if(ary_milkkits.Length>0)
                                _nokits = ary_milkkits.ToList();

                                // QueryResponse = new QueryResponse
                                // {
                                //     Status = reader.GetInt32(reader.GetOrdinal("Status")),
                                //     Message = reader.GetString(reader.GetOrdinal("Message")),
                                //     DonorID = reader.GetString(reader.GetOrdinal("DonorID"))
                                // };
                                //// employees.Add(employee);
                            }
                        }


                    }
                }

            }
            catch (System.Exception)
            {

                throw;
            }

            return _nokits;

        }



    }
}
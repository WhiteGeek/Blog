using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPal;
using PayPal.Api.Payments;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PayPalRestConsoleApplication
{
    class Program
    {
        //Merchant credentials
        private static string clientID = "AWtntRAKxlY4vM-7JBoisYOQpMPQedeSy1gpJdevvXO6QIFBwWsevpsHyAAM";
        private static string clientSecret = "EHQoXhBrNaecy_YU1EnBGdwKHjZ1_aRx4TSHkIrVGGNNj-litQW6lve9_p3c";
        private static string token;

        static void Main(string[] args)
        {
            Console.WriteLine(".::. PAYPAL REST API PAYMENT EXAMPLE .::.");
            Console.WriteLine();
            Console.WriteLine("Please wait, we are retriving the token from PayPal.....");
            Console.WriteLine();

            //Printing the access token
            PrintToken();
            //Establishing the PayPal API call
            Console.WriteLine("*************************************************************************");
            Console.WriteLine();
            Console.Write("Do you want to test the Payment? YES or NO : ");
            string x = Console.ReadLine();

            if (x == "YES" || x == "yes" || x == "y" || x == "Y")
            {
                Console.WriteLine();
                Console.WriteLine("*************************************************************************");
                Console.WriteLine();
                PaymentRequest();
                Console.WriteLine();
                Console.WriteLine("*************************************************************************");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("*************************************************************************");
                Console.WriteLine();
                Console.WriteLine("Press any button to close the application");
            }

            //Keeping the console open
            Console.ReadLine();
        }

        //Retriving the Token
        public static string GetPayPalToken()
        {
            OAuthTokenCredential tokenCredential = new OAuthTokenCredential(clientID, clientSecret);
            string accessToken = tokenCredential.GetAccessToken();

            return accessToken;
        }

        //Printing the token
        public static void PrintToken()
        {
            //Getting the access token
            
            token = GetPayPalToken();

            if (token.Contains("Token Error"))
            {
                Console.WriteLine("A runtime exception has been generated, please check your internet connectivity and try again");
            }
            else
            {
                Console.WriteLine("*************************************************************************");
                Console.WriteLine("PayPal Token = " + token);
            }
        }

        //Establishing the Payment
        private static void PaymentRequest()
        {
            try
            {
                Payment p = new Payment()
                {
                    intent = "sale",
                    payer = new Payer
                    {
                        payment_method = "credit_card",

                        funding_instruments = new List<FundingInstrument>
                    {
                        new FundingInstrument
                        {
                            credit_card = new CreditCard
                            {
                                type = "visa",
                                number = "4983742029722659",
                                expire_month = 12,
                                expire_year = 2019,
                                cvv2 = "222",
                                first_name = "Valerio",
                                last_name = "DAlessio",

                                billing_address = new Address
                                {
                                    line1 = "52 N Main ST",
                                    city = "Johnstown",
                                    country_code = "US",
                                    postal_code = "43210",
                                    state = "OH",
                                },
                            },
                        },
                    },
                    },

                    transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            total = "10",
                            currency = "GBP"
                        },
                        description = "This is the payment transaction description"
                    }
 
                },

                }.Create(token);

                Console.WriteLine("Payment Completed: \n\r\n\r");
                Console.WriteLine(p.ConvertToJson());
            }
            catch (Exception e)
            {
                Console.WriteLine("RUNTIME EXCEPTION" + e);
            }

        }
    }
}

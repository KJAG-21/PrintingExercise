using System;
using System.Collections.Generic;

namespace PrintingDevelopmentExercise.Model
{
    public class CustomerRecord
    {
        public int Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string AccountNumber { get; set; }
        public List<DetailsRecord> DetailsRecordList { get; set; }
        public Decimal TotalAmount { get; set; }

        public CustomerRecord(string rowData) {

            ParseData(rowData);
            
            this.TotalAmount = CalculateTotalAmount();
        }

        /// <summary>
        /// Parse the customer data
        /// </summary>
        /// <param name="rowData">raw customer data</param>
        private void ParseData(string rowData) {
            string[] data = rowData.Split('|');
            //Parse data into properties
            this.Id = Convert.ToInt32(data[0]);
            this.FirstName = data[1];
            this.LastName = data[2];
            this.PhoneNumber = data[3];
            this.Address1 = data[4];
            this.Address2 = data[5];
            this.City = data[6];
            this.State = data[7];
            this.ZipCode = data[8];
            this.AccountNumber = data[9];

            this.DetailsRecordList = new List<DetailsRecord>();
            DetailsRecordList.Add(new DetailsRecord { Description = data[10], Amount = Convert.ToDecimal(data[11]) });
            DetailsRecordList.Add(new DetailsRecord { Description = data[12], Amount = Convert.ToDecimal(data[13]) });
            DetailsRecordList.Add(new DetailsRecord { Description = data[14], Amount = Convert.ToDecimal(data[15]) });

            foreach (var item in DetailsRecordList)
            {
                item.Code = item.AmountCodeConverter();
            }
        }

        /// <summary>
        /// Sum the price of each detail record
        /// </summary>
        /// <returns></returns>
        public decimal CalculateTotalAmount() { 
        
            decimal total = 0;
            foreach (var item in DetailsRecordList)
            {
                total += item.Amount;
            }
            return total;
        }

        public override string ToString()
        {
            string str = $"\"CUSTOMER_RECORD\", {Id}, \"{FirstName}\", \"{LastName}\", \"{Address1}\", \"{Address2}\", \"{City}\", \"{State}\", {ZipCode}, {AccountNumber}\n";

            foreach (var item in DetailsRecordList)
            {
                str += item.ToString() + "\n";
            }
            str += $"\"DETAILS_RECORD\", \"TOTAL\", \"{TotalAmount:C2}\"\n";
            return str;
        }

    }
}

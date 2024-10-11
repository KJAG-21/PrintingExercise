namespace PrintingDevelopmentExercise.Model
{
    public class DetailsRecord
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }

        public string AmountCodeConverter()
        {
            var amount = this.Amount;
            string code = "";
            if (amount < 500)
                code = "N";
            else if (amount > 500 && amount < 1000)
                code = "A";
            else if (amount > 1000 && amount < 1500)
                code = "C";
            else if (amount > 1500 && amount < 2000)
                code = "L";
            else if (amount > 2000 && amount < 2500)
                code = "P";
            else if (amount > 2500 && amount < 3000)
                code = "X";
            else if (amount > 3000 && amount < 5000)
                code = "T";
            else if (amount > 5000 && amount < 10000)
                code = "S";
            else if (amount > 10000 && amount < 20000)
                code = "U";
            else if (amount > 20000 && amount < 30000)
                code = "R";
            else if (amount > 30000)
                code = "V";
            return code;
        }

        public override string ToString()
        {
            string str = $"\"DETAILS_RECORD\", \"{Description}\", \"{Code}\", \"{Amount:C2}\"";
            return str;
        }
    }
}

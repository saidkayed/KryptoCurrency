namespace KryptoValutaer
{
    internal class Program
    {
        static void Main(string[] args)
        {


        }







        public class KryptoValuta
        {

            public Dictionary<string, double> kryptoValutas = new Dictionary<string, double>();


            /// <summary>
            /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
            /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
            /// bliver den gamle værdi overskrevet af den nye værdi
            /// </summary>
            /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
            /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
            public void SetPricePerUnit(String currencyName, double price)
            {
                if (price <= 0.00)
                {
                    throw new ArgumentException("Price cannot be negative or zero. Please enter a positive over 0.00.");
                }

                if (kryptoValutas.ContainsKey(currencyName))
                {
                    kryptoValutas[currencyName] = price;
                }
                else
                {
                    kryptoValutas.Add(currencyName, price);
                }

            }

            /// <summary>
            /// Konverterer fra en kryptovaluta til en anden. 
            /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
            /// 
            /// </summary>
            /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
            /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
            /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
            /// <returns>Værdien af beløbet i toCurrencyName</returns>
            public double Convert(String fromCurrencyName, String toCurrencyName, double amount)
            {

                if (amount <= 0.00)
                {
                    throw new ArgumentException("Amount cannot be negative or zero. Please enter a positive value.");
                }

                if (!kryptoValutas.ContainsKey(fromCurrencyName) || !kryptoValutas.ContainsKey(toCurrencyName))
                {
                    throw new ArgumentException("One or more of the currencies does not exist. Please enter a valid currency.");
                }

                double fromCurrencyPrice = kryptoValutas[fromCurrencyName];
                double toCurrencyPrice = kryptoValutas[toCurrencyName];



                double convertedAmount = amount * (fromCurrencyPrice / toCurrencyPrice);

                return convertedAmount;
            }



        }

    }
}
using Xunit;
using FluentAssertions;
using static KryptoValutaer.Program;

namespace KryptoValutaer
{
    public class KryptoValutaTest
    {


        [Fact]
        public void The_System_Cant_Have_Multiple_of_the_Same_crypto_valutas()
        {
            //arrange
            KryptoValuta sut = new KryptoValuta();
            sut.SetPricePerUnit("Bitcoin", 1000);
            sut.SetPricePerUnit("Bitcoin", 2000);


            //act
            int result = sut.kryptoValutas.Count;



            //assert
            result.Should().Be(1);
        }



        [Fact]
        public void The_System_Can_Add_two_diffrent_crypto_valutas_in_list()
        {
            //arrange
            KryptoValuta sut = new KryptoValuta();
            sut.SetPricePerUnit("Bitcoin", 1000);
            sut.SetPricePerUnit("Ethereum", 2000);

            //act
            int result = sut.kryptoValutas.Count;



            //assert
            result.Should().Be(2);
        }


        [Fact]
        public void When_there_already_a_crypto_with_same_name_it_should_override_the_price()
        {
            //arrange
            KryptoValuta sut = new KryptoValuta();
            sut.SetPricePerUnit("Bitcoin", 1000);
            sut.SetPricePerUnit("Bitcoin", 2000);

            //act
            double result = sut.kryptoValutas["Bitcoin"];



            //assert
            result.Should().Be(2000);
        }


        [Fact]
        public void Price_Set_To_Crypto_Cant_be_Negative()
        {
            Action action = () => new KryptoValuta().SetPricePerUnit("Bitcoin", -1);

            action.Should().Throw<ArgumentException>().WithMessage("Price cannot be negative or zero. Please enter a positive over 0.00.");

        }

        [Fact]
        public void Price_Set_To_Crypto_Cant_be_Zero()
        {
            Action action = () => new KryptoValuta().SetPricePerUnit("Bitcoin", 0);

            action.Should().Throw<ArgumentException>().WithMessage("Price cannot be negative or zero. Please enter a positive over 0.00.");

        }



        [Theory]
        [MemberData(nameof(Data))]
        public void The_Sytem_Should_Convert_a_crypyo_Currency_to_a_correct_ammount(
             String fromCurrencyName,
             double fromPrice,
             String toCurrencyName,
             double toPrice,
             double amount,
             double expectedResult)
        {
            //arrange
            KryptoValuta sut = new KryptoValuta();
            sut.SetPricePerUnit(fromCurrencyName, fromPrice);
            sut.SetPricePerUnit(toCurrencyName, toPrice);


            //act
            double result = sut.Convert(fromCurrencyName, toCurrencyName, amount);

            //assert
            result.Should().Be(expectedResult);

        }

        [Fact]
        public void The_System_Cant_Convert_Crypto_Currency_From_To_With_A_Negative_Amount_()
        {
            Action action = () => new KryptoValuta().Convert("Bitcoin", "Ethereum", -1);

            action.Should().Throw<ArgumentException>().WithMessage("Amount cannot be negative or zero. Please enter a positive value.");
        }

        [Fact]
        public void The_System_Cant_Convert_Crypto_Currency_From_To_With_A_Zero_Amount_()
        {
            Action action = () => new KryptoValuta().Convert("Bitcoin", "Ethereum", 0);

            action.Should().Throw<ArgumentException>().WithMessage("Amount cannot be negative or zero. Please enter a positive value.");

        }


        [Fact]
        public void The_System_Cant_Convert_From_or_To_A_Crypto_Currency_That_Doesnt_Exist()
        {
            Action action = () => new KryptoValuta().Convert("Bitcoin", "Ethereum", 1000);

            action.Should().Throw<ArgumentException>().WithMessage("One or more of the currencies does not exist. Please enter a valid currency.");
        }

        public static TheoryData<string, double, string, double, double, double> Data()
        {
            return new TheoryData<string, double, string, double, double, double>
            {
              {"Bitcoin", 1000.0, "Ethereum", 2000.0, 1.0, 0.5},
              {"Ethereum", 500.0, "Bitcoin", 1000.0, 2.5, 1.25},
              {"Litecoin", 250.0, "Ripple", 100.0, 3.0, 7.5},
              {"Stellar", 120.0, "EOS", 10.0, 4.0, 48.0},
            };


        }
    }
}

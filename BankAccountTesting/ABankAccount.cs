using BankAccountLib;
using NUnit.Framework.Internal;
using System.Security.Principal;

namespace BankAccountTesting
{
    public class ABankAccount
    {


        [TestCase("0abc123def6789", 100, TestName = "ShouldSetTheAccountNumberAndTheBalanceWhenConstructed")]
        public void ShouldSetTheAccountNumberAndTheBalanceWhenConstructed(string accountNumber, decimal initialBalance)
        {
            var sut = new BankAccount(accountNumber, initialBalance);
            Assert.That(sut.AccountNumber, Is.EqualTo(accountNumber));
            Assert.That(sut.Balance, Is.EqualTo(initialBalance));

        }




        [TestCase("0abc123def6789", -200, TestName = "ShouldThrowArgumentExceptionWhenConstructedForConstructedWithLessThanZero")]
        [TestCase("", 500, TestName = "ShouldThrowArgumentExceptionWhenConstructedForConstructedWithWhiteSpace")]
        [TestCase("", -500, TestName = "ShouldThrowArgumentExceptionWhenConstructedForWhiteSpaceAndBalanceLessthanZero")]
        public void ShouldThrowArgumentExceptionWhenAccountNumberAndBalanceIsConstructed(string accountNumber, decimal initialBalance) {

            Assert.Throws<ArgumentException>(() => new BankAccount(accountNumber, initialBalance));
        }


        [TestCase(10, TestName = "ShouldIncreaseBalaneAfterDeposti")]
        public void ShouldIncreaseBalanceAfterDeposit(decimal amount)
        {

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 10;
            var sut = new BankAccount( accountNumber, initialBalance);

            sut.Deposit(amount);

            Assert.That(sut.Balance, Is.EqualTo(20));

        }

        [TestCase(-10, TestName = "ShouldThrowArgumentExceptionWhenDepositAmountIsLessThanZero")]
        [TestCase(0, TestName = "ShouldThrowArgumentExceptionWhenDepositAmountIsEqualToZero")]
        public void ShouldThrowArgumentExceptionWhenDepositAmountLessThanEqualToZero(decimal amount)
        {
            string accountNumber = "0abc123def6789";
            decimal initialBalance = 10;
            var sut = new BankAccount(accountNumber, initialBalance);


            Assert.Throws<ArgumentException>(() => sut.Deposit(amount));

        }


        [TestCase(10, TestName ="ShouldDecreaseBalanceAfterWithdraw")]
        public void ShouldDecreaseBalanceAfterWithdraw(decimal amount)
        {
            string accountNumber = "0abc123def6789";
            decimal initialBalance = 100;
            var sut = new BankAccount(accountNumber, initialBalance);

            sut.Withdraw(amount);

            Assert.That(sut.Balance, Is.EqualTo(90));

        }

     
        [TestCase(100,-20, TestName = "ShouldThrowArgumentExceptionWhenWithdrawAmountIsNegative")]
        [TestCase(100,0, TestName = "ShouldThrowArgumentExceptionWhenWithdrawAmountIsZero")]
       
        public void ShouldThrowArgumentExceptionWhenWithdrawAmountLessThanEqualToZero( decimal initialBalance, decimal amount)
        {

            string accountNumber = "0abc123def6789";
     
            var sut = new BankAccount(accountNumber, initialBalance);


            Assert.Throws<ArgumentException>(() => sut.Withdraw(amount));
        }


        [TestCase(100, 200, TestName = "ShouldThrowInvalidOperationExceptionWhenWithdrawAmountGreaterThanBalance")]
        public void ShouldThrowInvalidOperationExceptionWhenWithdrawAmountGreaterThanBalance(decimal initialBalance, decimal amount)
        {

            string accountNumber = "0abc123def6789";

            var sut = new BankAccount(accountNumber, initialBalance);
            Assert.Throws<InvalidOperationException>(() => sut.Withdraw(amount));
        }


        [TestCase(50, "Low",  TestName = "ShouldReturnAccountStatusAsLow")]
        [TestCase(500, "Normal", TestName = "ShouldReturnAccountStatusAsNormal")]
        [TestCase(1500, "High", TestName = "ShouldReturnAccountStatusAsHigh")]


        public void ShouldReturnAccountStatusLowNormalorHighBasedOnBalance(decimal initialBalance, string accountStatus)
        {
            string accountNumber = "0abc123def6789";
          
            var sut = new BankAccount(accountNumber, initialBalance);

            string accountStatusIs = sut.GetAccountStatus();

            Assert.That(accountStatusIs, Is.EqualTo(accountStatus));


        }

    }

}
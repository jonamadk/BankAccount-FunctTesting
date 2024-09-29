using BankAccountLib;
using NUnit.Framework.Internal;
using System.Runtime.ConstrainedExecution;
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


        [TestCase(100, TestName = "ShouldDecreaseBalanceAndIncreaseTheRecipientBalanceAfterTransfer")]
        public void ShouldDecreaseBalanceAndIncreaseTheRecipientBalanceAfterTransfer(decimal amount)
        {
            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 500;


            var recipient = new BankAccount(recpAccountNumber, recpInitialBalance);

            var sut = new BankAccount(accountNumber, initialBalance);

            sut.TransferTo(recipient, amount);

            
            Assert.That(recipient.Balance, Is.EqualTo(200));
            Assert.That(sut.Balance, Is.EqualTo(400));
        }


        [TestCase(300, TestName = "ShouldThrowInvalidOperationExceptionWhenTransferAmountGreaterThanBalanceForPositiveAmount")]
        public void ShouldThrowInvalidOperationExceptionWhenTransferAmountGreaterThanBalanceForPositiveAmount(decimal amount)
        {
            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 200;


            var recipient = new BankAccount(recpAccountNumber, recpInitialBalance);

            var sut = new BankAccount(accountNumber, initialBalance);

           

            Assert.Throws<InvalidOperationException>(() => sut.TransferTo(recipient, amount));


        }

        [TestCase(300, TestName = "ShouldThrowInvalidOperationExceptionWhenTransferAmountGreaterThanBalanceForPositiveAmount")]
        
        public void ShouldThrowInvalidOperationExceptionForInsufficientFund(decimal amount)
        {
            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 200;


            var recipient = new BankAccount(recpAccountNumber, recpInitialBalance);

            var sut = new BankAccount(accountNumber, initialBalance);



            Assert.Throws<InvalidOperationException>(() => sut.TransferTo(recipient, amount));


        }


        [TestCase(-100, TestName = "ShouldThrowArgumentExceptionWhenTransferAmountLessThanZero")]
        [TestCase(0, TestName = "ShouldThrowArgumentExceptionWhenTransferAmountIsZero")]
        public void ShouldThrowArgumentExceptionForTransferWithInvalidAmounts(decimal amount)
        {
            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 200;


            var recipient = new BankAccount(recpAccountNumber, recpInitialBalance);

            var sut = new BankAccount(accountNumber, initialBalance);



            Assert.Throws<ArgumentException>(() => sut.TransferTo(recipient, amount));


        }





    }

}
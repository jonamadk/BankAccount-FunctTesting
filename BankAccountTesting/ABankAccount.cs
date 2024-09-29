using BankAccountLib;
using NUnit.Framework.Internal;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Text;

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
        public void ShouldThrowArgumentExceptionWhenAccountNumberAndBalanceIsConstructed(string accountNumber, decimal initialBalance)
        {

            Assert.Throws<ArgumentException>(() => new BankAccount(accountNumber, initialBalance));
        }


        [TestCase(10, TestName = "ShouldIncreaseBalaneAfterDeposti")]
        public void ShouldIncreaseBalanceAfterDeposit(decimal amount)
        {

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 10;
            var sut = new BankAccount(accountNumber, initialBalance);

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


        [TestCase(10, TestName = "ShouldDecreaseBalanceAfterWithdraw")]
        public void ShouldDecreaseBalanceAfterWithdraw(decimal amount)
        {
            string accountNumber = "0abc123def6789";
            decimal initialBalance = 100;
            var sut = new BankAccount(accountNumber, initialBalance);

            sut.Withdraw(amount);

            Assert.That(sut.Balance, Is.EqualTo(90));

        }


        [TestCase(100, -20, TestName = "ShouldThrowArgumentExceptionWhenWithdrawAmountIsNegative")]
        [TestCase(100, 0, TestName = "ShouldThrowArgumentExceptionWhenWithdrawAmountIsZero")]

        public void ShouldThrowArgumentExceptionWhenWithdrawAmountLessThanEqualToZero(decimal initialBalance, decimal amount)
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


        [TestCase(50, "Low", TestName = "ShouldReturnAccountStatusAsLow")]
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



        // 2 BOUNDARY VALUE ANALYSIS //
        [TestCase("", 500, TestName = "Constructor_EmptyAccountNumber")]
        [TestCase("123456789", -1, TestName = "Constructor_NegativeBalance")]
        [TestCase("123456789", 0, TestName = "Constructor_ZeroBalance")]
        [TestCase("123456789", 1, TestName = "Constructor_PositiveBalance")]
        public void EvalauteConstructor_BoundaryValueAnalysis(string accountNumber, decimal initialBalance)
        {

            if (accountNumber == "" || initialBalance < 0)
            {

                Assert.Throws<ArgumentException>(() => new BankAccount(accountNumber, initialBalance));
            }

            else
            {
                var sut = new BankAccount(accountNumber, initialBalance);
                Assert.That(sut.AccountNumber, Is.EqualTo(accountNumber));
                Assert.That(sut.Balance, Is.EqualTo(initialBalance));
            }

        }


        [TestCase("123456789", 1e-28, TestName = "VerySmallPositivevalueBoundary")]
        [TestCase("123456789", 1e-27, TestName = "JustBelowVerySmallPositivevalueBoundary")]
      

        public void EvaluateConstructor_VerySmallPositiveValue(string accountNumber, decimal initialBalance)
        {

           
                var sut = new BankAccount(accountNumber, initialBalance);
                Assert.That(sut.AccountNumber, Is.EqualTo(accountNumber));
                Assert.That(sut.Balance, Is.EqualTo(initialBalance));

        }

        [TestCase("123456789", -1e-28, TestName = "VerySmallPositivevalueBoundary")]
        [TestCase("123456789", -1e-27, TestName = "JustBelowVerySmallPositivevalueBoundary")]
        public void EvaluateConstructor_VerySmallNegativeValue(string accountNumber, decimal initialBalance)
        {


            Assert.Throws<ArgumentException>(
                    () => new BankAccount(accountNumber, initialBalance));
        }


        [TestCase()]
        public void EvaluateConstructor_VerylargePositiveValue()
        {

            string accountNumber = "123456789";
            decimal initialBalance = decimal.MaxValue;  

            var sut = new BankAccount(accountNumber, initialBalance);
            Assert.That(sut.AccountNumber, Is.EqualTo(accountNumber));
            Assert.That(sut.Balance, Is.EqualTo(initialBalance));




        }
        [Test]
        public void EvaluateConstructor_VeryLargeNegativePositiveValue()
        {

            string accountNumber = "123456789";
            decimal initialBalance = decimal.MinValue;


            Assert.Throws<ArgumentException>(
               () => new BankAccount(accountNumber, initialBalance));


        }

        // Deposit Method
       
        [TestCase(0, TestName = "DepositZero")]
        [TestCase(-0.01, TestName = "DepositJustBelowZero")]
        [TestCase(0.01, TestName = "DepositJustAboveZero")]
        public void EvalaueDeposit_BoundaryValueAnalysis(decimal amount)
        {
            string accountNumber = "0abc456789";
            decimal initialBalance = 10;

            var sut = new BankAccount(accountNumber, initialBalance);
            if (amount <= 0)
            {
                Assert.Throws<ArgumentException>(
                        () => sut.Deposit(amount));
            }
            else
            {
        
                sut.Deposit(amount);
                Assert.That(sut.Balance, Is.EqualTo(initialBalance + amount));
            }
        }

        [Test]
        public void Deposit_VeryLargePositiveValue()

        {
            string accountNumber = "0abc456789";
            decimal initialBalance = 100;
            decimal amount = decimal.MaxValue;

            var sut = new BankAccount(accountNumber, initialBalance);
            sut.Deposit(amount);
            Assert.That(sut.Balance, Is.EqualTo(initialBalance + amount));
        }

        [Test]
        public void Deposit_VeryLargeNegativeValue()
        {
            string accountNumber = "0abc456789";
            decimal initialBalance = 100;
            decimal amount = decimal.MinValue;

            var sut = new BankAccount(accountNumber, initialBalance);
            Assert.Throws<ArgumentException>(
                        () => sut.Deposit(amount));
        }



    }
}
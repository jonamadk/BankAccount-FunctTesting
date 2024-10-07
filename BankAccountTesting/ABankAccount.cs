using BankAccountLib;
using NUnit.Framework.Internal;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Text;

namespace BankAccountTesting
{
    public class ABankAccount
    {

     

        // 1. Constructor: EQUIVALENCE PARTIONING //
        [Category("Equivalence Partioning")]
        [TestCase("0abc123def6789", 100, TestName = "ShouldSetTheAccountNumberAndTheBalanceWhenConstructed")]
        public void ShouldSetTheAccountNumberAndTheBalanceWhenConstructed(string accountNumber, decimal initialBalance)
        {
            var sut = new BankAccount(accountNumber, initialBalance);
            Assert.That(sut.AccountNumber, Is.EqualTo(accountNumber));
            Assert.That(sut.Balance, Is.EqualTo(initialBalance));

        }


        [Category("Equivalence Partioning")]
        [TestCase("0abc123def6789", -200, TestName = "ShouldThrowArgumentExceptionWhenConstructedForConstructedWithLessThanZero")]
        [TestCase("", 500, TestName = "ShouldThrowArgumentExceptionWhenConstructedForConstructedWithWhiteSpace")]
        [TestCase("", -500, TestName = "ShouldThrowArgumentExceptionWhenConstructedForWhiteSpaceAndBalanceLessthanZero")]
        [TestCase(null, 100, TestName = "ShouldThrowArgumentExceptionWhenConstructedForNullAccountNumber")]
        public void ShouldThrowArgumentExceptionWhenAccountNumberAndBalanceIsConstructed(string? accountNumber, decimal initialBalance)
        {

            Assert.Throws<ArgumentException>(() => new BankAccount(accountNumber, initialBalance));
        }

        // 2. Deposit: EQUIVALENCE PARTIONING //

        [Category("Equivalence Partioning")]
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

        // 4. Withdraw: EQUIVALENCE PARTIONING //

        [Category("Equivalence Partioning")]
        [TestCase(10, TestName = "ShouldDecreaseBalanceAfterWithdraw")]
        public void ShouldDecreaseBalanceAfterWithdraw(decimal amount)
        {
            string accountNumber = "0abc123def6789";
            decimal initialBalance = 100;
            var sut = new BankAccount(accountNumber, initialBalance);

            sut.Withdraw(amount);

            Assert.That(sut.Balance, Is.EqualTo(90));

        }

        [Category("Equivalence Partioning")]
        [TestCase(100, -20, TestName = "ShouldThrowArgumentExceptionWhenWithdrawAmountIsNegative")]
        [TestCase(100, 0, TestName = "ShouldThrowArgumentExceptionWhenWithdrawAmountIsZero")]

        public void ShouldThrowArgumentExceptionWhenWithdrawAmountLessThanEqualToZero(decimal initialBalance, decimal amount)
        {

            string accountNumber = "0abc123def6789";

            var sut = new BankAccount(accountNumber, initialBalance);


            Assert.Throws<ArgumentException>(() => sut.Withdraw(amount));
        }

        [Category("Equivalence Partioning")]

        [TestCase(100, 200, TestName = "ShouldThrowInvalidOperationExceptionWhenWithdrawAmountGreaterThanBalance")]
        public void ShouldThrowInvalidOperationExceptionWhenWithdrawAmountGreaterThanBalance(decimal initialBalance, decimal amount)
        {

            string accountNumber = "0abc123def6789";

            var sut = new BankAccount(accountNumber, initialBalance);
            Assert.Throws<InvalidOperationException>(() => sut.Withdraw(amount));
        }


        // 4. GetAccountStatus: EQUIVALENCE PARTIONING //
        [Category("Equivalence Partioning")]
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

        // 5. TransferTo : EQUIVALENCE PARTIONING //
        // 5.1 TransferTo : Transfer between valid accounts//
        [Category("Equivalence Partioning")]
        [TestCase(100, TestName = "ShouldDecreaseBalanceAndIncreaseTheRecipientBalanceAfterTransfer")]
        public void ShouldDecreaseBalanceAndIncreaseTheRecipientBalanceAfterTransfer(decimal amount)
        {
            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 500;


            BankAccount recipient = new BankAccount(recpAccountNumber, recpInitialBalance);
            var sut = new BankAccount(accountNumber, initialBalance);

            sut.TransferTo(recipient, amount);


            Assert.That(recipient.Balance, Is.EqualTo(200));
            Assert.That(sut.Balance, Is.EqualTo(400));
        }

        // 5.3 TransferTo : Transfers with invalid amounts: Amount > Balance //
        [Category("Equivalence Partioning")]
        [TestCase(300, TestName = "ShouldThrowInvalidOperationExceptionWhenTransferAmountGreaterThanBalanceForPositiveAmount")]
        public void ShouldThrowInvalidOperationExceptionWhenTransferAmountGreaterThanBalanceForPositiveAmount(decimal amount)
        {
            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 200;


            BankAccount recipient = new BankAccount(recpAccountNumber, recpInitialBalance);
            var sut = new BankAccount(accountNumber, initialBalance);



            Assert.Throws<InvalidOperationException>(() => sut.TransferTo(recipient, amount));


        }


        // 5.2 TransferTo : Transfers with insufficient funds  //

        [Category("Equivalence Partioning")]
        [TestCase(300, TestName = "ShouldThrowInvalidOperationExceptionWhenTransferAmountGreaterThanBalanceForPositiveAmount")]

        public void ShouldThrowInvalidOperationExceptionForInsufficientFund(decimal amount)
        {
            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 200;


            BankAccount recipient = new BankAccount(recpAccountNumber, recpInitialBalance);
            var sut = new BankAccount(accountNumber, initialBalance);



            Assert.Throws<InvalidOperationException>(() => sut.TransferTo(recipient, amount));


        }

        // 5.3 TransferTo : Transfers with invalid amounts //
        [Category("Equivalence Partioning")]
        [TestCase(-100, TestName = "ShouldThrowArgumentExceptionWhenTransferAmountLessThanZero")]
        [TestCase(0, TestName = "ShouldThrowArgumentExceptionWhenTransferAmountIsZero")]
        public void ShouldThrowArgumentExceptionForTransferWithInvalidAmounts(decimal amount)
        {
            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 200;


            BankAccount recipient = new BankAccount(recpAccountNumber, recpInitialBalance);
            var sut = new BankAccount(accountNumber, initialBalance);


            Assert.Throws<ArgumentException>(() => sut.TransferTo(recipient, amount));


        }

        // 5.4 TransferTo: Transfers to null recipient accounts //
        [Category("Equivalence Partioning")]
        [TestCase(100, TestName = "ShouldThrowArgumentNullExceptionWhenRecipientIsNullForAmountLessThanBalance")]
        [TestCase(-10, TestName = "ShouldThrowArgumentNullExceptionWhenRecipientIsNullForNegativeAmount")]
        [TestCase(300, TestName = "ShouldThrowArgumentNullExceptionWhenRecipientIsNullForAmountGreaterThanBalance")]


        public void ShouldThrowNullArgumentExceptionForTransferWhenRecipientIsNull(decimal amount)
        {
           

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 200;

            BankAccount recipient = null;
            var sut = new BankAccount(accountNumber, initialBalance);
            

            Assert.Throws<ArgumentNullException>(() => sut.TransferTo(recipient, amount));


        }


        [Category("Boundary Value Analysis")]
        // 2 CONSTRUCTOR : BOUNDARY VALUE ANALYSIS //
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
        [Category("Boundary Value Analysis")]

        [TestCase("123456789", 1e-28, TestName = "VerySmallPositivevalueBoundary")]
        [TestCase("123456789", 1e-27, TestName = "JustBelowVerySmallPositivevalueBoundary")]
      

        public void EvaluateConstructor_VerySmallPositiveValue(string accountNumber, decimal initialBalance)
        {

           
                var sut = new BankAccount(accountNumber, initialBalance);
                Assert.That(sut.AccountNumber, Is.EqualTo(accountNumber));
                Assert.That(sut.Balance, Is.EqualTo(initialBalance));

        }
        [Category("Boundary Value Analysis")]
        [TestCase("123456789", -1e-28, TestName = "VerySmallPositivevalueBoundary")]
        [TestCase("123456789", -1e-27, TestName = "JustBelowVerySmallPositivevalueBoundary")]
        public void EvaluateConstructor_VerySmallNegativeValue(string accountNumber, decimal initialBalance)
        {


            Assert.Throws<ArgumentException>(
                    () => new BankAccount(accountNumber, initialBalance));
        }

        [Category("Boundary Value Analysis")]
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


        // 2. DEPOSIT METHOD : BOUNDARY VALUE ANALYSIS
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
            decimal amount = decimal.MaxValue - initialBalance;

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


        // 2. WITHDRAW METHOD : BOUNDARY VALUE ANALYSIS

        [TestCase(0, TestName = "WithdrawZero")]
        [TestCase(-0.0001, TestName = "WithdrawJustBelowZero")]
        [TestCase(0.01, TestName = "WtihdrawJustAboveZero(0.01)")]
        [TestCase(0.001, TestName = "WtihdrawJustAboveZero(0.001)")]
        [TestCase(9.99, TestName = "WithdrawJustBelowBalance")]
        [TestCase(10, TestName = "WithdrawEqualsBalance")]
        [TestCase(10.01, TestName = "WithdrawJustAboveBalance")]
        public void EvalaueWithdraw_BoundaryValueAnalysis(decimal amount)
        {
            string accountNumber = "0abc456789";
            decimal initialBalance = 10;

            var sut = new BankAccount(accountNumber, initialBalance);
            if (amount <= 0)
            {
                Assert.Throws<ArgumentException>(
                        () => sut.Withdraw(amount));
            }
            else if (amount > sut.Balance) {
                Assert.Throws<InvalidOperationException>(
                       () => sut.Withdraw(amount));

            }
            else {
            
                sut.Withdraw(amount);
                Assert.That(sut.Balance, Is.EqualTo(initialBalance - amount));
            }
        }


        [Test]
        public void Withdraw_VeryLargePositiveValue()

        {
            string accountNumber = "0abc456789";
            decimal initialBalance = 100;
            decimal amount = decimal.MaxValue;

            var sut = new BankAccount(accountNumber, initialBalance);
            Assert.Throws<InvalidOperationException>(
                   () => sut.Withdraw(amount));
        }

        [Test]
        public void Withdraw_VeryLargeNegativeValue()
        {
            string accountNumber = "0abc456789";
            decimal initialBalance = 100;
            decimal amount = decimal.MinValue;

            var sut = new BankAccount(accountNumber, initialBalance);
            Assert.Throws<ArgumentException>(
                        () => sut.Withdraw(amount));
        }


        // 3.  GETACCOUNTSTATUS : BOUNDARY VALUE ANALYSIS //

        [TestCase(99.999,"Low", TestName = "GetAccountStatusJustBelowLowThreshold(99.999)")]
        [TestCase(99.99, "Low",TestName = "GetAccountStatusJustBelowLowThreshold(99.99)")]
        [TestCase(100, "Normal", TestName = "GetAccountStatusMinNormalThreshold")]
        [TestCase(100.01, "Normal", TestName = "GetAccountStatus_JustAboveMinNormalThreshold")]
        [TestCase(999.99, "Normal", TestName = "GetAccountStatus_JustBelowHighThreshold")]
        [TestCase(1000,"High", TestName = "GetAccountStatus_MinHighThreshold")]
        [TestCase(1000.01, "High",TestName = "GetAccountStatus_JustAboveMinHighThreshold")]
        public void EvalauteGetAccountStatus_BoundaryValueAnalysis(decimal Balance, string expectedSatus)
        {
            string accountNumber = "0abc456789";
            decimal initialBalance = Balance;

            var sut = new BankAccount(accountNumber, initialBalance); 
            string actualStatus = sut.GetAccountStatus();

            Assert.That(actualStatus, Is.EqualTo(expectedSatus));
        }

      

        // 4. TRANSFERTO : BOUNDARY VALUE ANALYSIS //
            
        

        [TestCase(100, TestName = "TransferToEntireBalance")]     //  4.1 : Trasferring the entire Balance
        [TestCase(0.01, TestName = "TransferVerySmallBalance")]   // 4.2 : Transferring a very small amount(e.g 0.01)//
        [TestCase(0, TestName = "TransferJustZeroBalance")]            //  Extra Case: Transferring Zero Balance
        [TestCase(-0.1, TestName = "TransferJustBelowZeroBalance")]
        [TestCase(101, TestName = "TransferJustAboveBalance")]
        [TestCase(99, TestName = "TransferJustBelowBalance")]


        public void TransferTo_VerySmallBalance( decimal amount)
        {


            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = 100;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 100;


            BankAccount recipient = new BankAccount(recpAccountNumber, recpInitialBalance);
            var sut = new BankAccount(accountNumber, initialBalance);

            if (amount > 0 && amount > sut.Balance)
            {
                Assert.Throws<InvalidOperationException>(
                       () => sut.TransferTo(recipient, amount));

            } 

           else if (amount > 0)
            {
                sut.TransferTo(recipient, amount);

                Assert.That(sut.Balance, Is.EqualTo(initialBalance - amount));
                Assert.That(recipient.Balance, Is.EqualTo(recpInitialBalance + amount));

            }
            else
            {
                Assert.Throws<ArgumentException>(() => sut.TransferTo(recipient, amount));
            }
        }




        // 4.3 Transferring to an account with a very high balance

        [Test]

        public void TransferTo_RecipientWithVeryhighBalance()
        {


            string recpAccountNumber = "recp123def6789";
            decimal recpInitialBalance = decimal.MaxValue;

            string accountNumber = "0abc123def6789";
            decimal initialBalance = 100;

            decimal amount = 10;

            BankAccount recipient = new BankAccount(recpAccountNumber, recpInitialBalance - amount);
            var sut = new BankAccount(accountNumber, initialBalance);

            sut.TransferTo(recipient, amount);

            Assert.That(sut.Balance, Is.EqualTo(initialBalance - amount));
            Assert.That(recipient.Balance, Is.EqualTo(recpInitialBalance));

        }


        // 3.2 COMBINATORIAL TESTING

        [Test, Combinatorial]
        public void CombinatorialTesting([Values(1000, 1)] decimal initialBalance,
                                         [Values(10, 100, 200)] decimal depositAmount,
                                         [Values(50, 500, 10)] decimal withdrawAmount)
        {

            

            string expectedAccountStatus;


            var sut = new BankAccount("0abc456789", initialBalance);
            sut.Deposit(depositAmount);
            sut.Withdraw(withdrawAmount);

            string actualAccountStatusIs = sut.GetAccountStatus();
            
            decimal finnalBalanceIs = initialBalance + depositAmount - withdrawAmount;

            if (finnalBalanceIs < 100)
                expectedAccountStatus = "Low";

            else if (finnalBalanceIs < 1000)

                expectedAccountStatus = "Normal";
            else
                expectedAccountStatus = "High";

            Assert.That(sut.Balance, Is.EqualTo(finnalBalanceIs));

            Assert.That(actualAccountStatusIs, Is.EqualTo(expectedAccountStatus));
        }


        // 3.2 PAIRWISE TESTING

        [Test, Pairwise]
        public void PairwiseTesting([Values(1000, 1)] decimal initialBalance,
                                       [Values(10, 100, 200)] decimal depositAmount,
                                       [Values(50, 500, 10)] decimal withdrawAmount)
        {



            string expectedAccountStatus;


            var sut = new BankAccount("0abc456789", initialBalance);
            sut.Deposit(depositAmount);
            sut.Withdraw(withdrawAmount);

            string actualAccountStatusIs = sut.GetAccountStatus();

            decimal finnalBalanceIs = initialBalance + depositAmount - withdrawAmount;

            if (finnalBalanceIs < 100)
                expectedAccountStatus = "Low";

            else if (finnalBalanceIs < 1000)

                expectedAccountStatus = "Normal";
            else
                expectedAccountStatus = "High";

            Assert.That(sut.Balance, Is.EqualTo(finnalBalanceIs));

            Assert.That(actualAccountStatusIs, Is.EqualTo(expectedAccountStatus));
        }

    }
}
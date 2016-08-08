using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MSTestExtensions;
using System.Collections;
using System.Collections.Generic;

namespace TestBank
{
    [TestClass]
    public class BankTest
    {
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        // Use TestInitialize to run code before running each test
        [TestInitialize]
        public void TestInitialize()
        {
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void TestBankAddAccount()
        {
            Bank bank = new Bank();
            Account acc = new Account();
            bank.AddAccount(acc);
            Assert.AreEqual(bank.AccountsCount, 1);
            Assert.AreSame(bank[0], acc);
        }

        //MSTestExtensions;
        //StringAssert
        [TestMethod]
        // [ExpectedException(typeof(ArgumentException))]
        public void TestBankAddNullAccount()
        {
            Bank bank = new Bank();
            //bank.AddAccount(null);
            var ex = ThrowsAssert.Throws<ArgumentException>(() => bank.AddAccount(null));
            StringAssert.Contains(ex.Message, "accounts are");
        }

        [TestMethod]
        public void TestBankAddRemoveAccount()
        {
            Bank bank = new Bank();
            Account acc = new Account();
            bank.AddAccount(acc);
            Assert.AreEqual(bank.AccountsCount, 1);
            bank.RemoveAccount(acc);
            Assert.AreEqual(bank.AccountsCount, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBankRemoveInvalidAccount()
        {
            Bank bank = new Bank();
            Account acc = new Account();
            bank.AddAccount(acc);
            Account anotherAcc = new Account();
            bank.RemoveAccount(anotherAcc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBankRemoveNullAccount()
        {
            Bank bank = new Bank();
            bank.RemoveAccount(null);
        }

        [TestMethod]
        public void TestBankAccountIndexer()
        {
            Bank bank = new Bank();
            Account acc = new Account();
            bank.AddAccount(acc);
            Account sameAcc = bank[0];
            Assert.AreSame(acc, sameAcc);

            Account secondAcc = new Account();
            bank.AddAccount(secondAcc);
            Account sameSecondAcc = bank[1];
            Assert.AreSame(secondAcc, sameSecondAcc);

            Assert.AreNotSame(sameAcc, sameSecondAcc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBankAccountIndexerInvalidRange()
        {
            Bank bank = new Bank();
            Account acc = new Account();
            bank.AddAccount(acc);
            Account accFromBank = bank[1];
        }

        [TestMethod]
        [Ignore]
        public void TestBankIgnoreTest()
        {
            // This test is not executed
        }

        //Test members with internal modifier
        [TestMethod]
        public void TestInternal()
        {
            Bank bank = new Bank();
            var res = bank.BankInternal();
            Assert.AreEqual(1, res);
        }

        //Test private members
        [TestMethod]
        public void TestPrivate()
        {
            PrivateObject privateObj = new PrivateObject(typeof(Bank));
            var res = privateObj.Invoke("BankPrivate");
            Assert.AreEqual(2, res);
        }

        //Parametrized test
        private TestContext testContext;
        public BankTest()
        {
            this.TestContext = null;
        }

        public TestContext TestContext
        {
            get { return this.testContext; }
            set { this.testContext = value; }
        }

        [TestMethod]
        [DeploymentItem("TestBank\\ParamTest.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   "|DataDirectory|\\ParamTest.xml",
                   "Row",
                   DataAccessMethod.Sequential)]
        public void ParametrizedTest_Sum()
        {
            int a = int.Parse((string)TestContext.DataRow["A"]);
            int b = int.Parse((string)TestContext.DataRow["B"]);
            int result = int.Parse((string)TestContext.DataRow["Result"]);
            Bank bank = new Bank();
            Assert.AreEqual(result, bank.Sum(a, b));
        }

        //CollectionAssert
        [TestMethod]
        public void CollectionTests()
        {
            Bank bank = new Bank();
            ICollection accounts = (ICollection)bank.Accounts;
            CollectionAssert.AllItemsAreInstancesOfType(accounts, typeof(Account));
            CollectionAssert.AllItemsAreNotNull(accounts);
            CollectionAssert.AllItemsAreUnique(accounts);
            CollectionAssert.IsSubsetOf(new List<int>() { 1, 2, 3 }, new List<int>() { 5, 1, 2, 3, 6 });
            CollectionAssert.Contains(new List<int>() { 1, 2, 3 }, 2);
        }
    }
}

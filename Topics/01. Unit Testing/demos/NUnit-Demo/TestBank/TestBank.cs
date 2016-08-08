using System;
using NUnit.Framework;

[TestFixture]
public class TestBank
{
    [SetUp]
    public void InitBeforeEachTest()
    {
        // TODO: to be implemented, remove ignore attribute
    }

    [TearDown]
    public void DisposeAfterEachTest()
    {
        // TODO: to be implemented, remove ignore attribute
    }


    [OneTimeSetUp]
    public void Init()
    {
        // TODO: to be implemented
    }

    [OneTimeTearDown]
    public void Dispose()
    {
        // TODO: to be implemented
    }

    [Test]
    public void TestBankAddAccount()
    {
        Bank bank = new Bank();
        Account acc = new Account();
        bank.AddAccount(acc);
        Assert.AreEqual(bank.AccountsCount, 1);
        Assert.AreSame(bank[0], acc);
    }

    [Test]
    //old[ExpectedException(typeof(ArgumentException))]
    public void TestBankAddNullAccount()
    {
        Bank bank = new Bank();
        //bank.AddAccount(null);
        var ex = Assert.Throws<ArgumentException>(() => bank.AddAccount(null));
        StringAssert.Contains("accounts are not", ex.Message);
    }

    [Test]
    public void TestBankAddRemoveAccount()
    {
        Bank bank = new Bank();
        Account acc = new Account();
        bank.AddAccount(acc);
        Assert.AreEqual(bank.AccountsCount, 1);
        bank.RemoveAccount(acc);
        Assert.AreEqual(bank.AccountsCount, 0);
    }

    [Test]
    //old[ExpectedException(typeof(ArgumentException))]
    public void TestBankRemoveInvalidAccount()
    {
        Bank bank = new Bank();
        Account acc = new Account();
        bank.AddAccount(acc);
        Account anotherAcc = new Account();
        Assert.Throws<ArgumentException>(() => bank.RemoveAccount(anotherAcc));
    }

    [Test]
    //old[ExpectedException(typeof(ArgumentException))]
    public void TestBankRemoveNullAccount()
    {
        Bank bank = new Bank();
        Assert.Throws<ArgumentException>(() => bank.RemoveAccount(null));
    }

    [Test]
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

    [Test]
    //old[ExpectedException(typeof(ArgumentException))]
    public void TestBankAccountIndexerInvalidRange()
    {
        Bank bank = new Bank();
        Account acc = new Account();
        bank.AddAccount(acc);
        //Account accFromBank = bank[1];
        Assert.Throws<ArgumentException>(() => { var accFomBank = bank[1]; });
    }

    [Test, Ignore("For test")]
    public void TestBankIgnoreTest()
    {
        // TODO: to be implemented
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using System.Numerics;

public class GameManagerTests
{

    [Test]
    public void TestBigIntToExponentStringHundreds()
    {
        Assert.AreEqual("112", GameManager.BigIntToExponentString(new BigInteger(112)));
    }

    [Test]
    public void TestBigIntToExponentStringThousands()
    {
        Assert.AreEqual("3,112", GameManager.BigIntToExponentString(new BigInteger(3112)));
    }

    [Test]
    public void TestBigIntToExponentStringMillions()
    {
        Assert.AreEqual("1.12aa", GameManager.BigIntToExponentString(new BigInteger(1122112)));
    }

    [Test]
    public void TestBigIntToExponentStringTenMillions()
    {
        Assert.AreEqual("11.2aa", GameManager.BigIntToExponentString(new BigInteger(11212112)));
    }

    [Test]
    public void TestBigIntToExponentStringHundredMillions()
    {
        Assert.AreEqual("311aa", GameManager.BigIntToExponentString(new BigInteger(311212112)));
    }

    [Test]
    public void TestBigIntToExponentStringBillions()
    {
        Assert.AreEqual("4.31ab", GameManager.BigIntToExponentString(new BigInteger(4311212112)));
    }
}
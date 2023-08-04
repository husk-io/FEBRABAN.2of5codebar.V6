using FEBRABAN.TwoOfFiveCodebar.Interface;
using FEBRABAN.TwoOfFiveCodebar.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FEBRABAN.TwoOfFiveCodebar.Tests;

[TestClass]
public class BoletoFebrabanTests
{
  [TestMethod]
  [DataTestMethod]
  [DataRow("856000000047 747100091806 523000035072 486800312523")]
  [DataRow("83640000001-1 33120138000-2 81288462711-6 08013618155-1")]
  public void ShouldReturnError_WhenMod10DACIsInvalid(string linhaDigitavel)
  {
    Boleto boleto = new Boleto(linhaDigitavel);
    IModulo mod = boleto.Modulo;
    bool result = mod.ValidDAC(boleto);
    Assert.IsFalse(!result, "invalid codebar");
  }

  [TestMethod]
  [DataTestMethod]
  [DataRow("856000000046 747100091806 523000035072 486800312523")] //Resto 0
  public void ShouldReturnError_WhenMod10DVGIsInvalid(string linhaDigitavel) 
  {
    Boleto boleto = new Boleto(linhaDigitavel);
    IModulo mod = boleto.Modulo;
    bool result = mod.ValidDVG(boleto);
    Assert.IsFalse(!result, "invalid codebar");
  }

  [TestMethod]
  [DataTestMethod]
  [DataRow("858900000204 000003281833 240720183105 618666712531")]
  public void ShouldReturnError_WhenMod11DACIsInvalid(string linhaDigitavel)
  {
    Boleto boleto = new Boleto(linhaDigitavel);
    IModulo mod = boleto.Modulo;
    bool result = mod.ValidDAC(boleto);
    Assert.IsFalse(!result, "invalid codebar");
  }

  [TestMethod]
  [DataTestMethod]
  [DataRow("858900000204 000003281833 240720183105 618666712531")]
  public void ShouldReturnError_WhenMod11DVGIsInvalid(string linhaDigitavel)
  {
    Boleto boleto = new Boleto(linhaDigitavel);
    IModulo mod = boleto.Modulo;
    bool result = mod.ValidDVG(boleto);
    Assert.IsFalse(!result, "invalid codebar");
  }
}


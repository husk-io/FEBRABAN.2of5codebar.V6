using System.Text.RegularExpressions;
using FEBRABAN.TwoOfFiveCodebar.Interface;
using FEBRABAN.TwoOfFiveCodebar.Lib;

namespace FEBRABAN.TwoOfFiveCodebar.Module;

public class Modulo10 : IModulo
{
  public bool ValidDAC(Boleto boleto)
  {
    foreach(string bloco in boleto.LinhaDigitavelBlocks)
    {
      int somatorio = 0;

      for(int x = 0; x < bloco.Count()-1; x++)
      {
        int digit = bloco[x] - '0';
        int produto = digit * (x % 2 == 0 ? 2 : 1);
        somatorio += produto > 9 ? produto.ToString().Sum(c => c - '0') : produto;
      }

      int resto = somatorio % 10;
      int DAC = 10 - (resto == 0 ? 10 : resto);

      if (DAC != (bloco.Last() - '0'))
        return false;
    }
    return true;
  }

  public bool ValidDVG(Boleto boleto) 
  {
    int somatorio = 0;
    int DVG = boleto.LinhaDigitavelWithoutDACandDVG.DVG;

    string blocos = boleto.LinhaDigitavelWithoutDACandDVG.LinhaDigitavelSemDACeDVG;

    for (int x = 0; x < blocos.Count(); x++)
    {
      int digit = blocos[x] - '0';
      int produto = digit * (x % 2 == 0 ? 2 : 1);
      somatorio += produto > 9 ? produto.ToString().Sum(c => c - '0') : produto;
    }

    int resto = somatorio % 10;
    int DAC = 10 - (resto == 0 ? 10 : resto);

    if (DAC != DVG)
      return false;
    return true;
  }
}
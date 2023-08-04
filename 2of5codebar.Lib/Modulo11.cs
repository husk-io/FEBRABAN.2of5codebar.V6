using FEBRABAN.TwoOfFiveCodebar.Interface;
using FEBRABAN.TwoOfFiveCodebar.Lib;

namespace FEBRABAN.TwoOfFiveCodebar.Module;

public class Modulo11 : IModulo
{
  public bool ValidDAC(Boleto boleto)
  {
    foreach (string bloco in boleto.LinhaDigitavelBlocks)
    {
      int somatorio = 0;

      for (int x = 0; x < bloco.Count()-1; x++)
      {
        int digit = bloco[x] - '0';
        int multiplicador = (x < 3 ? 4 - x : 12 - x);
        int produto = digit * multiplicador;
        somatorio += produto;
      }

      int resto = somatorio % 11;
      int DAC = 11 - (resto == 0 || resto == 1 ? 11 : resto == 10 ? 10 : resto);

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

    List<char> reverseList = blocos.Reverse().ToList();

    for (int x = 0; x < reverseList.Count(); x++)
    {
      int digit = reverseList[x] - '0';
      int aux = x % 8;
      int multiplicador = aux + 2;
      int produto = digit * multiplicador;
      somatorio += produto;
    }

    int resto = somatorio % 11;
    int DAC = 11 - (resto == 0 || resto == 1 ? 11 : resto == 10 ? 10 : resto);

    if (DAC != DVG)
      return false;
    return true;
  }
}
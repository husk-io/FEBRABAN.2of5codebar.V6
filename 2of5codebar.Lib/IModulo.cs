using System.ComponentModel;
using FEBRABAN.TwoOfFiveCodebar.Lib;

namespace FEBRABAN.TwoOfFiveCodebar.Interface; 

public interface IModulo
{
  [Description("Digito de Auto-Conferência")]
  bool ValidDAC(Boleto boleto);
  
  [Description("Digito Verificador Geral")]
  bool ValidDVG(Boleto boleto);
}
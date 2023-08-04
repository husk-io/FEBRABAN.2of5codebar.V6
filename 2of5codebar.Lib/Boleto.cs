using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FEBRABAN.TwoOfFiveCodebar.Interface;
using FEBRABAN.TwoOfFiveCodebar.Module;

namespace FEBRABAN.TwoOfFiveCodebar.Lib; 

public class Boleto 
{
  private string _linhaDigitavel;
  private IModulo _modulo;

  public string LinhaDigitavel => _linhaDigitavel;
  public IModulo Modulo => _modulo;
  // [Description("Identificação do Produto")]
  // [RegularExpression(@"^[8]$", ErrorMessage = "Não é um código de arrecadação")]
  // private char ProductId { get; }

  // [Description("Identificação do Segmento")]
  // [RegularExpression(@"^[1,2,3,4,5,6,7,9]$", ErrorMessage = "Não é um código de arrecadação")]
  // private char SegmentId { get; }

  // [Description("Identificador de Valor Efetivo ou Referência")]
  // [RegularExpression(@"^[6,7,8,9]$", ErrorMessage = "")]
  // private char RealValueIdId_Ref { get; }

  // [Description("Dígito Verificador")]
  // [RegularExpression(@"^[\d]$", ErrorMessage = "")]
  // private char GeneralVerifDigit { get; }

  // [Description("Valor Efetivo ou Valor Referência")]
  // [RegularExpression(@"^[\d]{11}$", ErrorMessage = "")]
  // private string Value { get; }

  // [Description("Identificação da Empresa/Órgão")]
  // [RegularExpression(@"^[\d]{4}$", ErrorMessage = "")]
  // private string CompanyId { get; }

  // [Description("Campo livre de utilização da Empresa/Órgão")]
  // [RegularExpression(@"^[\d]{25}$", ErrorMessage = "")]
  // private string FreeFieldCompanyId { get; }

  // [Description("CNPJ / MF")]
  // [RegularExpression(@"^[\d]{8}$", ErrorMessage = "")]
  // private string CNPJ_MF { get; }

  // [Description("Campo livre de utilização da Empresa/Órgão")]
  // [RegularExpression(@"^[\d]{21}$", ErrorMessage = "")]
  // private string CompanyFreeFieldII { get; }


    // if(codebar.Length == 44) 
    // {
    //   ProductId = codebar[0];
    //   SegmentId = codebar[1];
    //   RealValueIdId_Ref = codebar[2];
    //   GeneralVerifDigit = codebar[3];
    //   Value = codebar[4..14];
    //   CompanyId = codebar[15..18];
    //   FreeFieldCompanyId = codebar[19..43];
    //   CNPJ_MF = codebar[15..22];
    //   CompanyFreeFieldII = codebar[19..43];
    // } else 
    //   throw new Exception("Not a valid barcode");
  

  public Boleto(string linhaDigitavel)
  {
    linhaDigitavel = linhaDigitavel.Replace(" ","").Replace("-","").Replace(".","");

    Regex rg = new Regex(@"^[\d]{48}$");
    Boolean validLength = rg.IsMatch(linhaDigitavel);

    if(!validLength)
      throw new Exception("Invalid format");

    _linhaDigitavel = linhaDigitavel;

    int[] mod10 = {6, 7};
    int[] mod11 = {8, 9};
    
    bool isMod10 = mod10.Contains(LinhaDigitavel[2] - '0');
    bool isMod11 = mod11.Contains(LinhaDigitavel[2] - '0');

    if(!isMod10 && !isMod11)
      throw new Exception("boleto invalido");

    _modulo = isMod10 ? new Modulo10() : new Modulo11();
  }

  public ICollection<string> LinhaDigitavelBlocks => new HashSet<string>() 
    { _linhaDigitavel[0..12], _linhaDigitavel[12..24], _linhaDigitavel[24..36], _linhaDigitavel[36..48] };

  public (string LinhaDigitavelSemDACeDVG, int DVG) LinhaDigitavelWithoutDACandDVG => 
    ((_linhaDigitavel[0..3] + _linhaDigitavel[4..11]) + _linhaDigitavel[12..23] + _linhaDigitavel[24..35] + _linhaDigitavel[36..47], _linhaDigitavel[3] - '0');

  public bool IsValid()
  {
    Boleto boleto = new Boleto(LinhaDigitavel);

    IModulo mod = _modulo;

    bool isDACValid = mod.ValidDAC(boleto);

    if(!isDACValid)
      return false;

    bool isDVGValid = mod.ValidDVG(boleto);
    if (!isDVGValid)
      return false;

    return true;
  }
}



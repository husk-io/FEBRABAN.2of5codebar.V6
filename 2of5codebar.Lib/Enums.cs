using System.ComponentModel;

namespace FEBRABAN.TwoOfFiveCodebar.Lib.Enum;

public enum Segments 
{
  [Description("Prefeitura")]
  Prefeitura=1,
  [Description("Saneamento")]
  Saneamento=2,
  [Description("Energia Elétrica e Gás")]
  EnergiaEletrica=3,
  [Description("Telecomunicações")]
  Telecom=4,
  [Description("Órgãos Governamentais")]
  Gov=5,
  [Description(" Carnes e Assemelhados ou demais Empresas / Órgãos que serão identificadas através do CNPJ")]
  CarnesAfim=6,
  [Description("Multas de trânsito")]
  Transito=7,
  [Description("Uso exclusivo do banco")]
  Banco=9
}

public enum EffetiveValues 
{
  [Description("Valor a ser cobrado efetivamente em reais")]
  ReaisMod10=6,
  [Description("Quantidade de moeda")]
  MoedaMod10=7,
  [Description("Valor a ser cobrado efetivamente em reais")]
  ReaisMod11=8,
  [Description("Quantidade de moeda")]
  MoedaMod11=9,
}
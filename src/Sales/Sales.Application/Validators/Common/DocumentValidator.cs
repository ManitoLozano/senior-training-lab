namespace Sales.Application.Validators.Common;

public static class DocumentValidator
{
    public static bool IsValidCpfOrCnpj(string document)
    {
        var onlyNumbers = OnlyNumbers(document);

        return IsValidCpf(onlyNumbers) || IsValidCnpj(onlyNumbers);
    }

    private static string OnlyNumbers(string value)
    {
        return new string(value.Where(char.IsDigit).ToArray());
    }

    private static bool IsValidCpf(string cpf)
    {
        if (cpf.Length != 11)
            return false;

        if (cpf.Distinct().Count() == 1)
            return false;

        var firstDigit = CalculateCpfDigit(cpf, 9);
        var secondDigit = CalculateCpfDigit(cpf, 10);

        return cpf[9].ToString() == firstDigit.ToString()
               && cpf[10].ToString() == secondDigit.ToString();
    }

    private static int CalculateCpfDigit(string cpf, int length)
    {
        var sum = 0;

        for (var i = 0; i < length; i++)
        {
            sum += int.Parse(cpf[i].ToString()) * (length + 1 - i);
        }

        var remainder = sum % 11;

        return remainder < 2 ? 0 : 11 - remainder;
    }

    private static bool IsValidCnpj(string cnpj)
    {
        if (cnpj.Length != 14)
            return false;

        if (cnpj.Distinct().Count() == 1)
            return false;

        var firstDigit = CalculateCnpjDigit(cnpj, new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });
        var secondDigit = CalculateCnpjDigit(cnpj, new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });

        return cnpj[12].ToString() == firstDigit.ToString()
               && cnpj[13].ToString() == secondDigit.ToString();
    }

    private static int CalculateCnpjDigit(string cnpj, int[] multipliers)
    {
        var sum = 0;

        for (var i = 0; i < multipliers.Length; i++)
        {
            sum += int.Parse(cnpj[i].ToString()) * multipliers[i];
        }

        var remainder = sum % 11;

        return remainder < 2 ? 0 : 11 - remainder;
    }
}
using System.ComponentModel.DataAnnotations;

namespace CargasBrasilDB.Common.Validation
{
   
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CpfAttribute : ValidationAttribute
    {
        public CpfAttribute() : base("CPF inválido.") { }

        public override bool IsValid(object? value)
        {
            if (value is null) return true; 
            if (value is not string s) return false;

            var digits = new string(s.Where(char.IsDigit).ToArray());
            if (digits.Length != 11 || digits.Distinct().Count() == 1) return false;

            int CheckDigit(int length)
            {
                var sum = 0;
                for (var i = 0; i < length; i++)
                    sum += (digits[i] - '0') * (length + 1 - i);
                var rest = sum % 11;
                return rest < 2 ? 0 : 11 - rest;
            }

            return CheckDigit(9) == digits[9] - '0' && CheckDigit(10) == digits[10] - '0';
        }
    }
}

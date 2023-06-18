using System;
using Scripts2D.Interfaces;

namespace Scripts2D.Enums
{
    using System.ComponentModel;

    public enum OperatorE
    {
        [Description("+")]
        Add,
        [Description("-")]
        Subtract,
        [Description("*")]
        Multiply,
        [Description("/")]
        Divide,
        [Description("(")]
        LParenthesis,
        [Description(")")]
        RParenthesis,
        [Description("^")]
        Exponentiation,
        [Description("%")]
        Modulus,
        [Description("=")]
        Assignment,
        
    }

    public static class OperatorExtensions
    {
        public static string GetSymbol(this OperatorE op)
        {
            var memberInfo = op.GetType().GetMember(op.ToString());
            var descriptionAttribute = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false)
                as DescriptionAttribute[];
        
            return descriptionAttribute.Length > 0 ? descriptionAttribute[0].Description : op.ToString();
        }
    }

}
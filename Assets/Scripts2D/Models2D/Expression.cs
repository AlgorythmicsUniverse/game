using Scripts2D.Enums;
using Scripts2D.Interfaces;
using UnityEngine;

namespace Scripts2D.Models2D
{
    public class Expression:MonoBehaviour,IBlock
    {
        private ExpressionsE? _expression;

        public void SetExpression(ExpressionsE newEx)
        {
            _expression = newEx;
        }

        public ExpressionsE? GetExpression()
        {
            return _expression;
        }
    }
}
using Scripts2D.Enums;
using Scripts2D.Interfaces;
using UnityEngine;

namespace Scripts2D.Models2D
{
    public class Operator:MonoBehaviour,IBlock
    {
        private OperatorE operatorType;

        public void SetOperator(OperatorE newOp)
        {
            operatorType = newOp;
        }

        public string GetOpSymbol()
        {
            return operatorType.GetSymbol();
        }
        
    }
}
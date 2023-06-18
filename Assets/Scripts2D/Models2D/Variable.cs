using Scripts2D.Enums;
using Scripts2D.Interfaces;
using Scripts2D.Scene2Scripts;
using UnityEngine;

namespace Scripts2D.Models2D
{
    public class Variable:MonoBehaviour,IBlock
    {
        private TYPE? Type = null;

        public void SetVarType(TYPE newType)
        {
            this.Type = newType;
        }

        public TYPE? GetVarType()
        {
            return this.Type;
        }
    }
}
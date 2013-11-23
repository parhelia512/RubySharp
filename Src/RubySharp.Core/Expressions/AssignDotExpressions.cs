﻿namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    public class AssignDotExpressions : IExpression
    {
        private static int hashtag = typeof(AssignDotExpressions).GetHashCode();

        private DotExpression leftvalue;
        private IExpression expression;

        public AssignDotExpressions(DotExpression leftvalue, IExpression expression)
        {
            this.leftvalue = leftvalue;
            this.expression = expression;
        }

        public DotExpression LeftValue { get { return this.leftvalue; } }

        public IExpression Expression { get { return this.expression; } }

        public object Evaluate(Context context)
        {
            var obj = (DynamicObject)this.leftvalue.Expression.Evaluate(context);
            var method = obj.GetMethod(this.leftvalue.Name + "=");
            return method.Apply(obj, new object[] { this.expression.Evaluate(context) });
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is AssignDotExpressions)
            {
                var cmd = (AssignDotExpressions)obj;

                return this.leftvalue.Equals(cmd.leftvalue) && this.Expression.Equals(cmd.Expression);
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            return this.LeftValue.GetHashCode() + this.Expression.GetHashCode() + hashtag;
        }
    }
}

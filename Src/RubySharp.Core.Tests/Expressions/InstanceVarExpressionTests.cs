﻿namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class InstanceVarExpressionTests
    {
        [TestMethod]
        public void EvaluateUndefinedInstanceVar()
        {
            InstanceVarExpression expr = new InstanceVarExpression("foo");
            DynamicObject obj = new DynamicObject(null);
            Context context = new Context(obj, null);

            Assert.IsNull(expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedInstanceVar()
        {
            InstanceVarExpression expr = new InstanceVarExpression("one");
            DynamicObject obj = new DynamicObject(null);
            obj.SetValue("one", 1);
            Context context = new Context(obj, null);

            Assert.AreEqual(1, expr.Evaluate(context));
        }

        [TestMethod]
        public void Equals()
        {
            InstanceVarExpression expr1 = new InstanceVarExpression("one");
            InstanceVarExpression expr2 = new InstanceVarExpression("two");
            InstanceVarExpression expr3 = new InstanceVarExpression("one");

            Assert.IsTrue(expr1.Equals(expr3));
            Assert.IsTrue(expr3.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr3.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
        }
    }
}

﻿namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;

    [TestClass]
    public class NameExpressionTests
    {
        [TestMethod]
        public void EvaluateUndefinedName()
        {
            NameExpression expr = new NameExpression("foo");
            Context context = new Context();

            try
            {
                expr.Evaluate(context);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NameError));
                Assert.AreEqual("undefined local variable or method 'foo'", ex.Message);
            }
        }

        [TestMethod]
        public void EvaluateUndefinedConstant()
        {
            NameExpression expr = new NameExpression("Foo");
            Context context = new Context();

            try
            {
                expr.Evaluate(context);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NameError));
                Assert.AreEqual("unitialized constant Foo", ex.Message);
            }
        }

        [TestMethod]
        public void EvaluateDefinedName()
        {
            NameExpression expr = new NameExpression("one");
            Context context = new Context();
            context.SetLocalValue("one", 1);

            Assert.AreEqual(1, expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedFunction()
        {
            Machine machine = new Machine();
            NameExpression expr = new NameExpression("foo");
            Context context = machine.RootContext;
            context.Self.Class.SetInstanceMethod("foo", new DefinedFunction(new ConstantExpression(1), new string[0], context));

            Assert.AreEqual(1, expr.Evaluate(context));
        }

        [TestMethod]
        public void NamedExpression()
        {
            INamedExpression expr = new NameExpression("foo");

            Assert.AreSame("foo", expr.Name);
            Assert.IsNull(expr.TargetExpression);
        }

        [TestMethod]
        public void Equals()
        {
            NameExpression expr1 = new NameExpression("one");
            NameExpression expr2 = new NameExpression("two");
            NameExpression expr3 = new NameExpression("one");

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

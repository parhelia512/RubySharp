﻿namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class IfCommandTests
    {
        [TestMethod]
        public void ExecuteSimpleIfWhenConditionIsTrue()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(true), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.AreEqual(1, cmd.Execute(context));
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void ExecuteSimpleIfWhenConditionIsFalse()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(false), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.IsNull(cmd.Execute(context));
            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void ExecuteSimpleIfWhenConditionIsNull()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(null), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.IsNull(cmd.Execute(context));
            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void ExecuteSimpleIfWhenConditionIsZero()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(0), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.AreEqual(1, cmd.Execute(context));
            Assert.AreEqual(1, context.GetValue("one"));
        }
    }
}

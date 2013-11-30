﻿namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class DynamicClass : DynamicObject
    {
        private static IFunction newfunction = new NewFunction();

        private string name;
        private DynamicClass superclass;
        private DynamicClass parent;
        private IDictionary<string, IFunction> methods = new Dictionary<string, IFunction>();
        private Context constants = new Context();

        public DynamicClass(string name, DynamicClass superclass = null)
            : this(null, name, superclass)
        {
        }

        public DynamicClass(DynamicClass @class, string name, DynamicClass superclass = null, DynamicClass parent = null)
            : base(@class)
        {
            this.name = name;
            this.superclass = superclass;
            this.parent = parent;
        }

        public string Name { get { return this.name; } }

        public DynamicClass SuperClass { get { return this.superclass; } }

        public Context Constants { get { return this.constants; } }

        public string FullName
        {
            get
            {
                if (this.parent == null)
                    return this.Name;

                return this.parent.FullName + "::" + this.Name;
            }
        }

        public void SetInstanceMethod(string name, IFunction method)
        {
            this.methods[name] = method;
        }

        public IFunction GetInstanceMethod(string name)
        {
            if (this.methods.ContainsKey(name))
                return this.methods[name];

            if (this.superclass != null)
                return this.superclass.GetInstanceMethod(name);

            return null;
        }

        public DynamicObject CreateInstance()
        {
            return new DynamicObject(this);
        }

        public override IFunction GetMethod(string name)
        {
            return base.GetMethod(name);
        }

        public IList<string> GetOwnInstanceMethodNames()
        {
            return this.methods.Keys.ToList();
        }

        public override string ToString()
        {
            return this.FullName;
        }

        internal void SetSuperClass(DynamicClass superclass)
        {
            this.superclass = superclass;
        }
    }
}

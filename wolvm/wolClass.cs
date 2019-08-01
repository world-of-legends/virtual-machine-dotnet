﻿using System;
using System.Collections.Generic;
using System.Text;

namespace wolvm
{
    public class wolClass
    {
        public Dictionary<string, wolFunction> methods, constructors;
        public List<wolFunction> destructors;
        public Dictionary<string, Value> fields, constants;
        public Dictionary<string, wolClass> parents;
        public SecurityModifer security;
        public wolClassType classType;
        public string strtype;
        
        //for overriding
        public wolClass()
        {
            //pass
        }

        public wolClass(string name, SecurityModifer securityModifer = SecurityModifer.PUBLIC, wolClassType type = wolClassType.DEFAULT, string ConstructorName = "init")
        {
            strtype = name;
            security = securityModifer;
            classType = type;
            switch (classType)
            {
                case wolClassType.DEFAULT:
                    methods = new Dictionary<string, wolFunction>();
                    constructors = new Dictionary<string, wolFunction>
                    {
                        { ConstructorName, new wolFunction() }
                    };
                    destructors = new List<wolFunction>
                    {
                        new wolFunction()
                    };
                    fields = new Dictionary<string, Value>();
                    parents = new Dictionary<string, wolClass>
                    {
                        { "void", VirtualMachine.Void }
                    };
                    break;
                case wolClassType.ENUM:
                    constants = new Dictionary<string, Value>();
                    parents = new Dictionary<string, wolClass>
                    {
                        { "int", VirtualMachine.wolInt }
                    };
                    break;
                case wolClassType.STATIC:
                    parents = new Dictionary<string, wolClass>
                    {
                        { "void", VirtualMachine.Void }
                    };
                    fields = new Dictionary<string, Value>();
                    methods = new Dictionary<string, wolFunction>();
                    break;
                case wolClassType.STRUCT:
                    constants = new Dictionary<string, Value>();
                    parents = new Dictionary<string, wolClass>
                    {
                        { "void", VirtualMachine.Void }
                    };
                    fields = new Dictionary<string, Value>();
                    methods = new Dictionary<string, wolFunction>();
                    constructors = new Dictionary<string, wolFunction>
                    {
                        { ConstructorName, new wolFunction() }
                    };
                    destructors = new List<wolFunction>
                    {
                        new wolFunction()
                    };
                    break;
                case wolClassType.ABSTRACT:
                    parents = new Dictionary<string, wolClass>
                    {
                        { "void", VirtualMachine.Void }
                    };
                    fields = new Dictionary<string, Value>();
                    methods = new Dictionary<string, wolFunction>();
                    break;
            }
        }

        public override string ToString() => strtype;

        /// <summary>
        /// Implement all parents
        /// </summary>
        public void Implements()
        {
            foreach (wolClass parent in parents.Values)
            {
                foreach (KeyValuePair<string, wolFunction> method in parent.methods)
                {
                    if (!(method.Value.security == SecurityModifer.CLOSE))
                        methods.Add(method.Key, method.Value);
                }
                foreach (KeyValuePair<string, Value> field in parent.fields)
                {
                    fields.Add(field.Key, field.Value);
                }
                foreach (KeyValuePair<string, wolFunction> constructor in parent.constructors)
                {
                    if (!(constructor.Value.security == SecurityModifer.CLOSE))
                        constructors.Add(constructor.Key, constructor.Value);
                }
                foreach (wolFunction destructor in parent.destructors)
                {
                    if (!(destructor.security == SecurityModifer.CLOSE))
                        destructors.Add(destructor);
                }
            }
        }
    }

    public enum wolClassType
    {
        DEFAULT,
        STATIC,
        STRUCT,
        ENUM,
        ABSTRACT
    }
}

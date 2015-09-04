﻿using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is less than the other property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LessThanAttribute : CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public LessThanAttribute(string otherProperty)
            : base(Ops.LessThan, otherProperty)
        {
        }
    }
}
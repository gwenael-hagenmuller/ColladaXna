﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColladaXna.Base.Materials
{
    /// <summary>
    /// Defines complex material properties as a shader program
    /// and its parameter bindings. Available vertex data like
    /// position, normal, texture coordinates etc. are automatically
    /// passed to the shader via their respective semantics.
    /// </summary>
    public abstract class ShaderProperty : MaterialProperty
    {
        /// <summary>
        /// Filename of a FX file that contains the shader code in HLSL
        /// </summary>
        public String Filename { get; set; }

        public override Object GetValue()
        {
            return Filename;
        }
    }
}

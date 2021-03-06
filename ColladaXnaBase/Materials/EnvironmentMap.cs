﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColladaXna.Base.Materials
{
    public class EnvironmentMap : TextureProperty
    {
        public override string Name
        {
            get { return "EnvironmentMap"; }
        }

        public override string Code
        {
            // [R]eflective [m]ap
            get { return "Rm"; }
        }

        public override ShaderInstructions ShaderInstructions
        {
            get { return _shaderInstructions; }
        }

        static readonly ShaderInstructions _shaderInstructions = new ShaderInstructions
        {
            ParameterType = "Texture"
        };

        public EnvironmentMap()
        {
            
        }

        public EnvironmentMap(TextureReference texture)
        {
            Texture = texture;
        }
    }
}

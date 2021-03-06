﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ColladaXna.Base.Geometry
{
    /// <summary>
    /// An instance of a mesh having a specific transformation
    /// and position in the scene graph hierarchy.
    /// </summary>
    public class MeshInstance
    {
        /// <summary>
        /// String reference to the mesh used
        /// </summary>
        public String MeshName;

        /// <summary>
        /// Absolute transformation of this instance
        /// </summary>
        public Matrix AbsoluteTransform;

        /// <summary>
        /// Parent joint of this mesh instance.
        /// This is not connected to a possible skin definition
        /// </summary>
        [ContentSerializer(SharedResource = true)]
        public Joint ParentJoint;
    }
}

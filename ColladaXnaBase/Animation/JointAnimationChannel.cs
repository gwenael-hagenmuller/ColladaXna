﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColladaXna.Base.Geometry;

namespace ColladaXna.Base.Animation
{
    /// <summary>
    /// An animation channel corresponding to the channel XML element.
    /// Animations are "compacted" by the importer so that there is only
    /// one animation per joint, i.e. rather than animating all 16 components
    /// of the transform matrix individually only the resulting matrix is
    /// animated. 
    /// </summary>
    public class JointAnimationChannel
    {
        private JointAnimationSampler _sampler;
        private Joint _target;

        /// <summary>
        /// Sampler
        /// </summary>
        public JointAnimationSampler Sampler { get { return _sampler; } set { _sampler = value; } }

        /// <summary>
        /// Joint targetted by this channel
        /// </summary>
        public Joint Target { get { return _target; } set { _target = value; } }

        /// <summary>
        /// Creates a new joint animation channel
        /// </summary>
        /// <param name="sampler">Sampler</param>
        /// <param name="target">Target Joint</param>
        public JointAnimationChannel(JointAnimationSampler sampler, Joint target)
        {
            _sampler = sampler;
            _target = target;
        }
    }
}

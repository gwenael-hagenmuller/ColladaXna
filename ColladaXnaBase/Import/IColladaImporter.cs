﻿using System.Xml;

namespace ColladaXna.Base.Import
{
    /// <summary>
    /// Common interface of Collada importer classes
    /// </summary>
    public interface IColladaImporter
    {
        /// <summary>
        /// Import data from a COLLADA file represented by its root XML node
        /// and copy the imported data to given model instance.
        /// </summary>
        /// <param name="xmlRoot">root node of COLLADA xml document</param>
        /// <param name="model">model instance</param>
        void Import(XmlNode xmlRoot, ColladaModel model);
    }
}

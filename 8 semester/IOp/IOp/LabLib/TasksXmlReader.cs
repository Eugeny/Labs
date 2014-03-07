using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Data.Text;
using System.Xml;
using System.IO;
using System.Linq;

namespace LabLib
{
	public class TasksXmlReader
	{
		private readonly string fileName;

		public TasksXmlReader (string fileName)
		{
			this.fileName = fileName;
		}

		public Dictionary<string, ILPTask> ReadTasks ()
		{
			var doc = new XmlDocument ();
			doc.Load (fileName);

			var tasks = new Dictionary<string, ILPTask> ();

			foreach (XmlNode node in doc.GetElementsByTagName("Task").Cast<XmlNode>()) {
				string name = readAttribute (node, "Name");

				var A = (DenseMatrix)(readMatrixChild (node, "A"));
				var b = (DenseVector)readMatrixChild (node, "b").Row (0);
				var c = (DenseVector)readMatrixChild (node, "c").Row (0);

				DenseMatrix dLeftMatrix = readMatrixChild (node, "dLeft");
				DenseVector dLeft = dLeftMatrix == null ? null : (DenseVector)dLeftMatrix.Row (0);

				DenseMatrix dRightMatrix = readMatrixChild (node, "dRight");
				DenseVector dRight = dRightMatrix == null ? null : (DenseVector)dRightMatrix.Row (0);

				var task = new ILPTask { A = A, B = b, C = c, DL = dLeft, DR = dRight };

				tasks [name] = task;
			}

			return tasks;
		}

		private DenseMatrix readMatrixChild (XmlNode node, string childName)
		{
			XmlElement element = node [childName];
			if (element == null)
				return null;

			return DenseMatrix.OfMatrix (DelimitedReader.Read<double> (new StringReader (element.InnerText)));
		}

		private string readAttribute (XmlNode node, string attributeName)
		{
			XmlAttribute attribute = node.Attributes [attributeName];
			if (attribute == null)
				return null;

			return attribute.InnerText;
		}
	}
}



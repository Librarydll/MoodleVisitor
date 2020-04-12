using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace MoodleVisitor.Models.Core
{
	public class XmlSerializerHelper<T> where T:class
	{
		private XmlSerializer serializer = new XmlSerializer(typeof(T));
		public XmlSerializerHelper()
		{

		}

		public void Serialize(T obj,string path)
		{
			try
			{
				using (Stream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
				{
					serializer.Serialize(stream, obj);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public T DeSerialize(string path)
		{
			T obj = null;
			using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				try
				{
					obj = (T)serializer.Deserialize(stream);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			return obj;
		}
	}
}

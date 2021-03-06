﻿using MoodleVisitor.Models.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace MoodleVisitor.Models.Infrastructure
{
	public class ShceduleProvider : IShceduleProvider
	{
		XmlSerializerHelper<Shcedule> serializer = new XmlSerializerHelper<Shcedule>();
		public Shcedule Shcedule { get; private set; }

		public ShceduleProvider()
		{

		}

		public  void CreateInitialSchedule(string path)
		{
			if (File.Exists(path)) { return; }

			Shcedule shcedule = new Shcedule();
			var s = new List<Subject>
			{
				new Subject{ DayOfWeek=DayOfWeek.Monday,StartingTime=DateTime.Now,SubjectName="Axborot Sovat"},
				new Subject{ DayOfWeek=DayOfWeek.Monday,StartingTime=DateTime.Now,SubjectName="Milliy G'oya(m)"},
				new Subject{ DayOfWeek=DayOfWeek.Monday,StartingTime=DateTime.Now,SubjectName="Milliy G'oya(s)"},
				new Subject{ DayOfWeek=DayOfWeek.Tuesday,StartingTime=DateTime.Now,SubjectName="Web ilovalarni(m)"},
				new Subject{ DayOfWeek=DayOfWeek.Tuesday,StartingTime=DateTime.Now,SubjectName="Web ilovalarni(a)"},
				new Subject{ DayOfWeek=DayOfWeek.Tuesday,StartingTime=DateTime.Now,SubjectName="C#(a)"},
				new Subject{ DayOfWeek=DayOfWeek.Wednesday,StartingTime=DateTime.Now,SubjectName="Elektron hukumat(m)"},
				new Subject{ DayOfWeek=DayOfWeek.Wednesday,StartingTime=DateTime.Now,SubjectName="Xorijiy tili"},
				new Subject{ DayOfWeek=DayOfWeek.Wednesday,StartingTime=DateTime.Now,SubjectName="C#(a)"},
				new Subject{ DayOfWeek=DayOfWeek.Thursday,StartingTime=DateTime.Now,SubjectName="Pedagogika(a)"},
				new Subject{ DayOfWeek=DayOfWeek.Thursday,StartingTime=DateTime.Now,SubjectName="Pedagogika(m)"},
				new Subject{ DayOfWeek=DayOfWeek.Thursday,StartingTime=DateTime.Now,SubjectName="Dasturiy taminotlarni"},
				new Subject{ DayOfWeek=DayOfWeek.Thursday,StartingTime=DateTime.Now,SubjectName="Web ilovalarni(a)"},
				new Subject{ DayOfWeek=DayOfWeek.Friday,StartingTime=DateTime.Now,SubjectName="Elektron hukumat(a)"},
				new Subject{ DayOfWeek=DayOfWeek.Friday,StartingTime=DateTime.Now,SubjectName="Operatsion tizimlari(m)"},
				new Subject{ DayOfWeek=DayOfWeek.Friday,StartingTime=DateTime.Now,SubjectName="Operatsion tizimlari(a)"},
				new Subject{ DayOfWeek=DayOfWeek.Saturday,StartingTime=DateTime.Now,SubjectName="Dasturiy taminotlarni(m)"},
				new Subject{ DayOfWeek=DayOfWeek.Saturday,StartingTime=DateTime.Now,SubjectName="C#"},
				new Subject{ DayOfWeek=DayOfWeek.Saturday,StartingTime=DateTime.Now,SubjectName="Dasturiy taminotlarni"},
			};
			shcedule.Subjects = s.ToArray();
			serializer.Serialize(shcedule, path);			
		}

		public Shcedule GetScheduleFromFile(string path)
		{
			Shcedule = serializer.DeSerialize(path);
			return Shcedule;
		}
	}
}

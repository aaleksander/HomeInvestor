using System;
using HomeInvestor.CondParser;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class Test_Conds
	{
		[Test]
		public void Test_01_GetConds()
		{
			var str = @"
			бла-бла-бла
			""first cond""
			вавава
			""second""
			";

			var res = Parser.GetConds(str);
			Assert.AreEqual(2, res.Count, "1");
			Assert.AreEqual("first cond", res[0], "2");
			Assert.AreEqual("second", res[1], "3");
		}

		[Test]
		public void Test_02_Parse_What_do()
		{
			var str = @"""продать если цена больше 100""";
			var res = Parser.GetConds(str);
			var cond = Parser.Parse(res[0]);
			Assert.AreEqual(WhatDos.sell, cond.WhatDo, "1");
			Assert.AreEqual(Whats.price, cond.What, "3");
			Assert.AreEqual(Conds.great, cond.Cond, "4");
			Assert.AreEqual(100, (decimal)cond.Value, "5");

			str = @"""buy если % меньше 10.3""";
			res = Parser.GetConds(str);
			cond = Parser.Parse(res[0]);
			Assert.AreEqual(WhatDos.buy, cond.WhatDo, "2");
			Assert.AreEqual(Whats.percent, cond.What, "6");
			Assert.AreEqual(Conds.less, cond.Cond, "7");
			Assert.AreEqual(10.3, (decimal)cond.Value, "8");
		}

		[Test]
		public void Test_03_ParseDate()
		{
			var str = @"""продавать если дата больше 01.01.2020""";
			var res = Parser.GetConds(str);
			var cond = Parser.Parse(res[0]);
			Assert.AreEqual(Whats.date, cond.What, "1");
			Assert.AreEqual(WhatDos.sell, cond.WhatDo, "0");
			Assert.AreEqual("01.01.2020", ((DateTime)cond.Value).ToString("dd.MM.yyyy"), "2");
		}

		[Test]
		public void Test_03_age()
		{
			var str = @"""продавать если возраст больше 100""";
			var res = Parser.GetConds(str);
			var cond = Parser.Parse(res[0]);
			Assert.AreEqual(Whats.age, cond.What, "1");
			Assert.AreEqual(WhatDos.sell, cond.WhatDo, "0");
			Assert.AreEqual(100, (decimal)cond.Value, "2");
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.DataAccess
{
	public static class BloodTypesMap
	{
		public static Dictionary<string, int> mp = new Dictionary<string, int>
			{
				{"A+", 1},
				{"A-", 2},
				{"B+", 3},
				{"B-", 4},
				{"AB+", 5},
				{"AB-", 6},
				{"O+", 7},
				{"O-", 8}
			};
	}
}

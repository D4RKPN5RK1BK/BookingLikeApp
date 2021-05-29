using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class Responce
	{
		public string Redirect { get; set; }
		public string Message { get; set; }
		public string Target { get; set; }

		public object Data { get; set; }

		public bool Success { get; set; }

		public Dictionary<string, string> Messages { get; set; }

		public Exception Exception { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Conversion;

namespace BookingLikeApp.Models
{
	public class Pack
	{
		public int Id { get; set; }
		public int NumberId { get; set; }

		[Required]
		[DisplayName("Название сборки")]
		public string? Name { get; set; }

		[DisplayName("Доступен")]
		public bool Enable { get; set; }

		public Number Number { get; set; }
		public List<PackTenant> PackTenants { get; set; }
		public List<PackService> PackServices { get; set; }

	}
}

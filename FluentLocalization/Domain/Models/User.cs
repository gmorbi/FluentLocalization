using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace FluentLocalization.Domain.Models
{
	public class User
	{
        [Display(Name = "Id", ResourceType = typeof(Resources.Resources))]
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "Age", ResourceType = typeof(Resources.Resources))]
        public int Age { get; set; }
        [Display(Name = "Gender", ResourceType = typeof(Resources.Resources))]
        public string Gender { get; set; }
    }
}
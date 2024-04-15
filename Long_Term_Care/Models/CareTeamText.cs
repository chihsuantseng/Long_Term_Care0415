using System.ComponentModel.DataAnnotations;

namespace bigproject.Models
{
	
		public class CareTeamText
		{
			
			public int cID { get; set; }

		    public string cName { get; set; } = null!;
           public string  cLink { get; set; } = null!;

            public string cText { get; set; } = null!;


            public string cTime { get; set; } = null!;

    }
	
}
